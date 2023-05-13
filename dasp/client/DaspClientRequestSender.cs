
namespace dasp
{
    internal class DaspClientRequestSender
    {
        private readonly DaspClient _client;
        private readonly Func<string, Task> _sendLog;
        private readonly DaspConnection _daspConnection;
        public DaspClientRequestSender(DaspClient client, Func<string, Task> sendLog, DaspConnection daspConnection)
        {
            _client = client;
            _sendLog = sendLog;
            _daspConnection = daspConnection;
        }
        public async Task Register(string username, string password)
        {
            DaspHeader daspHeader = new DaspHeader(DaspConstants.REGISTER);
            DaspBody daspBody = new DaspBodyBuilder().WithUsername(username).WithPassword(password).Build(); 
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
        }
        public async Task<bool> Login(string username, string password)
        {
            _client.LoginTcs = new TaskCompletionSource<bool>();
            try
            {
                DaspHeader daspHeader = new DaspHeader(DaspConstants.LOGIN);
                DaspBody daspBody = new DaspBodyBuilder().WithUsername(username).WithPassword(password).Build();
                DaspRequest daspRequest = new(daspHeader, daspBody);
                _daspConnection.Send(daspRequest);
            }
            catch (Exception ex)
            {
                _sendLog($"something went wrong {ex}");
            }
            return await _client.LoginTcs.Task;
        }
        public async Task<bool> CreateRoom()
        {
            _client.CreateRoomTcs = new TaskCompletionSource<bool>();
            DaspHeader daspHeader = new DaspHeader(DaspConstants.CREATE_ROOM);
            DaspRequest daspRequest = new(daspHeader, null);
            _daspConnection.Send(daspRequest);
            return await _client.CreateRoomTcs.Task;
        }
        public async Task<bool> JoinRoom(int id)
        {
            _client.JoinRoomTcs = new TaskCompletionSource<bool>();
            DaspHeader daspHeader = new DaspHeader(DaspConstants.JOIN_ROOM);
            DaspBody daspBody = new DaspBodyBuilder().WithRoomId(id).Build();
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
            try
            {
                bool v = await _client.JoinRoomTcs.Task;
                return v;
            }
            catch (Exception ex)
            {
                _sendLog($"something went wrong {ex}");
                return false;
            }
        }
        public async Task<bool> LeaveRoom()
        {
            _client.LeaveRoomTcs = new TaskCompletionSource<bool>();
            DaspHeader daspHeader = new DaspHeader(DaspConstants.LEAVE_ROOM);
            DaspRequest daspRequest = new(daspHeader, null);
            _daspConnection.Send(daspRequest);
            return await _client.LeaveRoomTcs.Task;
        }
        public async Task SendPrivateMessage(string recipient, string message)
        {
            DaspHeader daspHeader = new(DaspConstants.SEND_PRIVATE_MESSAGE);
            DaspBody daspBody = new DaspBodyBuilder().WithRecipient(recipient).WithMessage(message).Build();
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
        }
        public async Task SendPublicMessage(string message)
        {
            DaspHeader daspHeader = new DaspHeader(DaspConstants.SEND_PUBLIC_MESSAGE);
            DaspBody daspBody = new DaspBodyBuilder().WithMessage(message).Build();
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
        }

        internal void StartGame()
        {
            DaspHeader daspHeader = new DaspHeader(DaspConstants.GAME_STARTED);
            DaspBody daspBody = new DaspBodyBuilder().Build();
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
        }

        internal void SendPressedPocket(int v)
        {
            DaspHeader daspHeader = new DaspHeader(DaspConstants.GAME_STATE_UPDATED);
            DaspBody daspBody = new DaspBodyBuilder().WithPocket(v).Build();
            DaspRequest daspRequest = new(daspHeader, daspBody);
            _daspConnection.Send(daspRequest);
        }
    }
}
