using dasp.server;
using System.Net.Sockets;
namespace dasp
{
    public class DaspServer
    {
        private readonly DaspServerConnection _daspServerConnection;
        private readonly List<ChatRoom> _chatRooms;
        private readonly List<DaspConnection> _connections;
        private readonly Func<string, Task> _sendLog;
        private readonly DaspDatabase daspDatabase;
        private readonly Dictionary<string, Func<DaspRequest, DaspConnection, Task>> commandHandlers;
        public DaspServer(int port, string address, string database, string username, string password, Func<string, Task> sendLog)
        {
            _connections = new List<DaspConnection>();
            _chatRooms = new List<ChatRoom>();
            daspDatabase = new DaspDatabase(address, database, username, password);
            _daspServerConnection = new DaspServerConnection(port, HandleClientAsync, sendLog);
            _sendLog = sendLog;
            commandHandlers = new()
            {
                { DaspConstants.REGISTER, (request, connection) => HandleRegister(request, connection) },
                { DaspConstants.LOGIN, (request, connection) => HandleLogin(request, connection) },
                { DaspConstants.JOIN_ROOM, (request, connection) => HandleJoinRoom(request, connection) },
                { DaspConstants.CREATE_ROOM, (request, connection) => HandleCreateRoom(request, connection) },
                { DaspConstants.LEAVE_ROOM, (request, connection) => HandleLeaveRoom(request, connection) },
                { DaspConstants.SEND_PRIVATE_MESSAGE, (request, connection) => HandleSendPrivateMessage(request, connection) },
                { DaspConstants.SEND_PUBLIC_MESSAGE, (request, connection) => HandleSendPublicMessage(request, connection) },
                { DaspConstants.GAME_STARTED, (request, connection) => HandleGameStarted(request, connection) },
                { DaspConstants.GAME_STATE_UPDATED, (request, connection) => HandleGameStateUpdated(request, connection) }
            };
        }

        private async Task HandleGameStateUpdated(DaspRequest request, DaspConnection connection)
        {
            int pocket = request.DaspBody.Pocket;
            connection.ChatRoom.PocketMoved(pocket);
        }

        public async Task GameStateUpdated(ChatRoom chatRoom)
        {
            foreach (DaspConnection daspConnection in chatRoom.daspConnections)
            {
                DaspBodyBuilder daspBodyBuilder = new DaspBodyBuilder();
                
                switch (daspConnection.PlayerState)
                {
                    case PlayerState.SPECTATING:
                        daspBodyBuilder
                            .WithGameStateMatrix(chatRoom.GameStateMatrix)
                            .WithGameEndStatus(-2)
                            .WithPlayerTurnInformation(false);
                        break;
                    case PlayerState.CURRENTLY_MOVING:
                        daspBodyBuilder
                            .WithGameStateMatrix(chatRoom.GameStateMatrix)
                            .WithGameEndStatus(-1)
                            .WithPlayerTurnInformation(chatRoom.MoveInProgress&&true);
                        break;
                    case PlayerState.WAITING_TO_MOVE:
                        daspBodyBuilder
                            .WithGameStateMatrix(chatRoom.GameStateMatrix)
                            .WithGameEndStatus(-1)
                            .WithPlayerTurnInformation(false);
                        break;
                }
                daspConnection.Send(new DaspRequest(new DaspHeader(DaspConstants.GAME_STATE_UPDATED), daspBodyBuilder.Build()));
            }
        }


        private async Task HandleGameStarted(DaspRequest request, DaspConnection connection)
        {
            List<DaspConnection> daspConnections = connection.ChatRoom.daspConnections;
            DaspConnection targetConnection = daspConnections.FirstOrDefault(conn => conn.Username != connection.Username);
            DaspBody daspBody;

            daspBody = new DaspBodyBuilder().Build();
            targetConnection.Send(new DaspRequest(new DaspHeader(DaspConstants.GAME_STARTED), new DaspBodyBuilder().Build()));
            connection.ChatRoom.GameStarted(connection, targetConnection);
            connection.Send(new DaspRequest(new DaspHeader(DaspConstants.GAME_STARTED), daspBody));
            foreach (DaspConnection spectatorconnection in connection.ChatRoom.daspConnections)
            {
                if (spectatorconnection != connection && spectatorconnection != targetConnection)
                {
                    spectatorconnection.Send(new DaspRequest(new DaspHeader(DaspConstants.GAME_STARTED), new DaspBody()));
                }
            }
        }

        public async Task Stop()
        {
            _daspServerConnection.StopListening();
            daspDatabase.CloseConnection();
        }
        public async Task HandleClientAsync(Socket socket)
        {
            _connections.Add(new DaspConnection(socket, OnRequestReceived, _sendLog));
        }
        public async Task OnRequestReceived(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            _sendLog($"dasp header {daspRequest.DaspHeader}");
            _sendLog($"dasp body {daspRequest.DaspBody}");
            string command = daspRequest.DaspHeader.Command;
            try
            {
                if (commandHandlers.TryGetValue(command, out var handler))
                {
                    handler(daspRequest, daspConnection);
                }
                else
                {
                    _sendLog($"Unknown command or error: {daspRequest.DaspHeader.Command}");
                }
            }
            catch (Exception ex)
            {
                _sendLog($"Unknown command or error: {ex}");
            }
        }
        private async Task HandleRegister(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            string username = daspRequest.DaspBody.Username;
            string password = daspRequest.DaspBody.Password;
            string registerResult;
            try
            {
                registerResult = daspDatabase.Register(username, username);
            }
            catch (Exception ex)
            {
                _sendLog($"{ex}");
                registerResult = "database error";
            }
            _sendLog($"User registration with the name {username} has {registerResult}");
            DaspBody daspBody;
            if (registerResult == DaspConstants.SUCCESS)
            {
                daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.SUCCESS).Build();
            }
            else
            {
                daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason(registerResult).Build();
            }
            daspConnection.Send(new DaspRequest(new DaspHeader(DaspConstants.REGISTER), daspBody));
        }
        private async Task HandleLogin(DaspRequest daspRequest, DaspConnection daspConnection)
        {

            string username = daspRequest.DaspBody.Username;
            string password = daspRequest.DaspBody.Password;
            string loginResult;
            try
            {
                loginResult = daspDatabase.Login(username, username);
            }
            catch (Exception ex)
            {
                _sendLog($"{ex}");
                loginResult = "database error";
            }

            _sendLog($"User login  with the name {username} has {loginResult}");
            DaspBody daspBody;
            if (loginResult == DaspConstants.SUCCESS)
            {
                daspBody = daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.SUCCESS).Build();
                daspConnection.Username = username;
                UpdateChatRooms();
            }
            else
            {
                daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason(loginResult).Build();
            }
            daspConnection.Send(new DaspRequest(new DaspHeader(DaspConstants.LOGIN), daspBody));
        }
        private async Task HandleJoinRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            try
            {
                int id = daspRequest.DaspBody.RoomId;
                ChatRoom? chatRoom = _chatRooms.Find(chatRoom => chatRoom.ID == id);
                if (chatRoom != null)
                {
                    List<DaspConnection> members = chatRoom.daspConnections;
                    _connections.Remove(daspConnection);
                    members.Add(daspConnection);
                    DaspRequest daspResponse = new(new DaspHeader(DaspConstants.JOIN_ROOM), new DaspBodyBuilder().WithStatus(DaspConstants.SUCCESS).Build());
                    daspConnection.Send(daspResponse);
                    UpdateUsersInChat(members);
                    UpdateChatRooms();
                }
                else
                {
                    DaspRequest daspResponse = new(new DaspHeader(DaspConstants.JOIN_ROOM), new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason($"the room with specified id does not exist").Build());
                    daspConnection.Send(daspResponse);
                }
            }
            catch (Exception ex)
            {
                _sendLog($"Unknown command or error: {ex}");
            }

        }
        private async Task HandleCreateRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            List<DaspConnection> members = new()
            {
                daspConnection
            };
            ChatRoom item = new ChatRoom(members, GameStateUpdated);
            _chatRooms.Add(item);
            _sendLog($"new room created with  {item.ID}");

            _connections.Remove(daspConnection);
            DaspRequest daspResponse = new(new DaspHeader(DaspConstants.CREATE_ROOM), new DaspBodyBuilder().WithStatus(DaspConstants.SUCCESS).Build());
            daspConnection.Send(daspResponse);
            UpdateChatRooms();
        }

        private void UpdateChatRooms()
        {
            List<ChatRoomInfo> chatRooms = _chatRooms.Select(chatRoom => chatRoom.ConvertToChatRoomInfo()).ToList();
            DaspRequest daspRequest = new(new DaspHeader(DaspConstants.ROOMS_UPDATED), new DaspBodyBuilder().WithRooms(chatRooms).Build());
            foreach (DaspConnection daspConnection in _connections)
            {
                daspConnection.Send(daspRequest);
            }
        }
        private static void UpdateUsersInChat(List<DaspConnection> members)
        {
            List<string> usernames = members.Where(conn => conn.Username != null)
                                    .Select(conn => conn.Username)
                                    .ToList();
            DaspRequest daspRequest = new(new DaspHeader(DaspConstants.PLAYERS_UPDATED), new DaspBodyBuilder().WithPlayers(usernames).Build());
            foreach (DaspConnection daspConnection in members)
            {
                daspConnection.Send(daspRequest);
            }
        }
        private async Task HandleLeaveRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            ChatRoom chatRoom = daspConnection.ChatRoom;
            DaspBody daspBody;
            if (chatRoom != null)
            {
                List<DaspConnection> members = chatRoom.daspConnections;
                _connections.Add(daspConnection);
                members.Remove(daspConnection);
                UpdateUsersInChat(members);
                daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.SUCCESS).Build();
                UpdateChatRooms();
            }
            else
            {
                daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason("Plyaerul este intr-o camera care nu exista????").Build();
            }
            DaspRequest daspResponse = new(new DaspHeader(DaspConstants.LEAVE_ROOM), daspBody);
            daspConnection.Send(daspResponse);
        }
        private async Task HandleSendPrivateMessage(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            string targetUsername = daspRequest.DaspBody.Recipient;
            DaspConnection targetConnection = daspConnection.ChatRoom.daspConnections.FirstOrDefault(conn => conn.Username == targetUsername);
            if (targetConnection != null)
            {
                DaspRequest daspResponse = new(new DaspHeader(DaspConstants.CHAT_UPDATED), new DaspBodyBuilder().WithMessage($"private message from: {daspConnection.Username} {daspRequest.DaspBody.Message}").Build());
                targetConnection.Send(daspResponse);
            }
            else
            {
                DaspRequest daspResponse = new(new DaspHeader(DaspConstants.ERROR), new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason("nu s-a gasit ce s-a cautat").Build());
                daspConnection.Send(daspResponse);
            }
        }
        private async Task HandleSendPublicMessage(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            ChatRoom chatRoom = daspConnection.ChatRoom;
            if (chatRoom != null)
            {
                List<DaspConnection> members = chatRoom.daspConnections;
                DaspRequest daspResponse = new(new DaspHeader(DaspConstants.CHAT_UPDATED), new DaspBodyBuilder().WithMessage($"{daspConnection.Username}:{daspRequest.DaspBody.Message}").Build());
                foreach (var member in members)
                {
                    member.Send(daspResponse);
                }
            }
            else
            {
                DaspBody daspBody = new DaspBodyBuilder().WithStatus(DaspConstants.ERROR).WithReason("Plyaerul nu poate trimite mesaj intr-o camera care nu exista????").Build();
                DaspRequest daspResponse = new(new DaspHeader(DaspConstants.CREATE_ROOM), daspBody);
                daspConnection.Send(daspResponse);
            }
        }
    }
}

