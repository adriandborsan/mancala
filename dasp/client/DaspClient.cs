
namespace dasp
{
    public class DaspClient
    {
        private readonly DaspConnection _daspConnection;
        public delegate void RoomsUpdatedHandler(List<ChatRoomInfo> updatedChatRooms);
        public delegate void PlayersUpdatedHandler(List<string> updatedPlayers);
        public delegate void AddMessageHandler(string message);
        public delegate void GameStartedHandler();
        public delegate void GameUpdatedHandler(int[,] sender, bool sender2, int sender3);
        public TaskCompletionSource<bool> LoginTcs { get; set; }
        public TaskCompletionSource<bool> LeaveRoomTcs { get; set; }
        public TaskCompletionSource<bool> CreateRoomTcs { get; set; }
        public TaskCompletionSource<bool> JoinRoomTcs { get; set; }
        private readonly DaspClientResponseHandler _responseHandler;
        private readonly DaspClientRequestSender _messageSender;

        public event GameUpdatedHandler GameUpdated
        {
            add => _responseHandler.GameUpdated += value;
            remove => _responseHandler.GameUpdated -= value;
        }
        public event GameStartedHandler GameStarted
        {
            add => _responseHandler.GameStarted += value;
            remove => _responseHandler.GameStarted -= value;
        }
        public event RoomsUpdatedHandler RoomsUpdated
        {
            add => _responseHandler.RoomsUpdated += value;
            remove => _responseHandler.RoomsUpdated -= value;
        }
        public event PlayersUpdatedHandler PlayersUpdated
        {
            add => _responseHandler.PlayersUpdated += value;
            remove => _responseHandler.PlayersUpdated -= value;
        }
        public event AddMessageHandler AddMessage
        {
            add => _responseHandler.AddMessage += value;
            remove => _responseHandler.AddMessage -= value;
        }
        public DaspClient(string address, int port, Func<string, Task> sendLog)
        {
            _responseHandler = new DaspClientResponseHandler(this, sendLog);
            _daspConnection = new DaspConnection(address, port, _responseHandler.HandleIncomingResponse, sendLog);
            _messageSender = new DaspClientRequestSender(this, sendLog, _daspConnection);
        }
        public Task<bool> CreateRoom() => _messageSender.CreateRoom();
        public Task<bool> JoinRoom(int id) => _messageSender.JoinRoom(id);
        public Task<bool> LeaveRoom() => _messageSender.LeaveRoom();
        public Task<bool> Login(string username, string password) => _messageSender.Login(username, password);
        public Task Register(string username, string password) => _messageSender.Register(username, password);
        public Task SendPrivateMessage(string recipient, string message) => _messageSender.SendPrivateMessage(recipient, message);
        public Task SendPublicMessage(string message) => _messageSender.SendPublicMessage(message);

        public void StartGame() => _messageSender.StartGame();

      

        public void SendPressedPocket(int v) => _messageSender.SendPressedPocket(v);
    }
}
