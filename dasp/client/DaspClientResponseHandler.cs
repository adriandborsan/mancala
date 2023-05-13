
using static dasp.DaspClient;
namespace dasp
{
    internal class DaspClientResponseHandler
    {
        private readonly DaspClient _client;
        private readonly Func<string, Task> _sendLog;
        public event RoomsUpdatedHandler RoomsUpdated;
        public event PlayersUpdatedHandler PlayersUpdated;
        public event GameUpdatedHandler GameUpdated;
        public event AddMessageHandler AddMessage;
        private readonly Dictionary<string, Func<DaspRequest, DaspConnection, Task>> commandHandlers;
        public DaspClientResponseHandler(DaspClient client, Func<string, Task> sendLog)
        {
            _client = client;
            _sendLog = sendLog;
            commandHandlers = new Dictionary<string, Func<DaspRequest, DaspConnection, Task>>
            {
                { DaspConstants.REGISTER, HandleRegister },
                { DaspConstants.LOGIN, HandleLogin },
                { DaspConstants.ROOMS_UPDATED, HandleRoomUpdated },
                { DaspConstants.JOIN_ROOM, HandleJoinRoom },
                { DaspConstants.CREATE_ROOM, HandleCreateRoom },
                { DaspConstants.LEAVE_ROOM, HandleLeaveRoom },
                { DaspConstants.CHAT_UPDATED, HandleChatUpdated },
                { DaspConstants.PLAYERS_UPDATED, HandlePlayersUpdated },
                { DaspConstants.GAME_STATE_UPDATED, HandleGameUpdated }
            };
        }

     

        public async Task HandleIncomingResponse(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            try
            {
                string command = daspRequest.DaspHeader.Command;
                if (commandHandlers.TryGetValue(command, out Func<DaspRequest, DaspConnection, Task> handler))
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
                _sendLog($"something went wrong {ex}");
            }

        }
        private async Task HandlePlayersUpdated(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            PlayersUpdated(daspRequest.DaspBody.Players);
        }
        private async Task HandleLeaveRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            bool leaveSuccess = DaspConstants.SUCCESS.Equals(daspRequest.DaspBody.Status);
            if (!leaveSuccess)
            {
                _sendLog($"leave failed because {daspRequest.DaspBody.Reason}");
            }
            _client.LeaveRoomTcs.TrySetResult(leaveSuccess);
        }
        private async Task HandleChatUpdated(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            AddMessage(daspRequest.DaspBody.Message);
        }
        private async Task HandleRoomUpdated(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            RoomsUpdated(daspRequest.DaspBody.Rooms);
        }
        private async Task HandleLogin(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            bool loginSuccess = DaspConstants.SUCCESS.Equals(daspRequest.DaspBody.Status);
            if (!loginSuccess)
            {
                _sendLog($"login failed because {daspRequest.DaspBody.Reason}");
            }
            _client.LoginTcs.TrySetResult(loginSuccess);
        }
        private async Task HandleRegister(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            bool registerSuccess = DaspConstants.SUCCESS.Equals(daspRequest.DaspBody.Status);
            if (!registerSuccess)
            {
                _sendLog($"registration failed because {daspRequest.DaspBody.Reason}");
            }
            else
            {
                _sendLog("registered successfully");
            }
        }
        private async Task HandleJoinRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            bool joinSuccess = DaspConstants.SUCCESS.Equals(daspRequest.DaspBody.Status);
            if (!joinSuccess)
            {
                _sendLog($"join room failed because {daspRequest.DaspBody.Reason}");
            }
            _client.JoinRoomTcs.TrySetResult(joinSuccess);
        }
        private async Task HandleCreateRoom(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            bool createSuccess = DaspConstants.SUCCESS.Equals(daspRequest.DaspBody.Status);
            if (!createSuccess)
            {
                _sendLog($"create room failed because {daspRequest.DaspBody.Reason}");
            }
            _client.CreateRoomTcs.TrySetResult(createSuccess);
        }


        private async Task HandleGameUpdated(DaspRequest daspRequest, DaspConnection daspConnection)
        {
            DaspBody daspBody = daspRequest.DaspBody;
            GameUpdated(daspBody.GameStateMatrix,daspBody.PlayerTurnInformation,daspBody.GameEndStatus);
        }
    }
}
