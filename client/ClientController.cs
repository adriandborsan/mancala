using dasp;
namespace client
{
    internal class ClientController : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposed;
        private readonly ClientForm clientForm;
        public DaspClient daspClient;
        public ClientController(ClientForm clientForm)
        {
            this.clientForm = clientForm;
            _cancellationTokenSource = new CancellationTokenSource();
            SendLog("Application started");
        }

        internal async Task Connect(string address, string port)
        {
            try
            {
                daspClient = await Task.Run(() => new DaspClient(address, int.Parse(port), SendLog));
                daspClient.RoomsUpdated += RoomsUpdated;
                daspClient.PlayersUpdated += PlayersUpdated;
                daspClient.AddMessage += AddMessage;
                clientForm.RunOnUiThread(() => clientForm.SwitchToLogin(), _cancellationTokenSource.Token);
                SendLog("Connected");
            }
            catch (Exception ex)
            {
                SendLog($"something went wrong {ex}");
            }
        }
        private void RoomsUpdated(List<ChatRoom> updatedChatRooms)
        {
            clientForm.RunOnUiThread(() => clientForm.RefreshRoomList(updatedChatRooms), _cancellationTokenSource.Token);
            SendLog("RoomsUpdated");
        }
        private void PlayersUpdated(List<string> updatedPlayers)
        {
            clientForm.RunOnUiThread(() => clientForm.RefreshUserList(updatedPlayers), _cancellationTokenSource.Token);
            SendLog("PlayersUpdated");
        }
        private void AddMessage(string message)
        {
            clientForm.RunOnUiThread(() => clientForm.AddMessage(message+"\n"), _cancellationTokenSource.Token);
            SendLog("AddMessage");
        }
        internal async Task Login(string username, string password)
        {
            if (await daspClient.Login(username, password))
            {
                clientForm.RunOnUiThread(() => clientForm.SwitchToHome(), _cancellationTokenSource.Token);
                SendLog("Login");
            }
        }
        internal async Task Register(string username, string password)
        {
            daspClient.Register(username, password);
            SendLog("Register");
        }
        public async Task SendLog(string message)
        {
            clientForm.RunOnUiThread(() => clientForm.AddLog(message+"\n"), _cancellationTokenSource.Token);
        }
        internal async Task JoinRoom(ChatRoom? chatRoom)
        {
            if (chatRoom != null && await daspClient.JoinRoom(chatRoom.Id))
            {
                clientForm.RunOnUiThread(() => clientForm.SwitchToRoom(), _cancellationTokenSource.Token);
                SendLog("JoinRoom");
            }
        }
        internal async Task CreateRoom()
        {
            if (await daspClient.CreateRoom())
            {
                clientForm.RunOnUiThread(() => clientForm.SwitchToRoom(), _cancellationTokenSource.Token);
                SendLog("CreateRoom");
            }
        }
        internal async Task SendMessage(string message, object selectedItem)
        {
            if (selectedItem == null)
            {
                daspClient.SendPublicMessage(message);
                SendLog("SendMessage yay");
            }
            else
            {
                daspClient.SendPrivateMessage(selectedItem.ToString(), message);
                SendLog("SendMessage nay");
            }
        }
        internal async Task LeaveRoom()
        {
            if (await daspClient.LeaveRoom())
            {
                clientForm.RunOnUiThread(() => clientForm.SwitchToHome(), _cancellationTokenSource.Token);
                SendLog("LeaveRoom");
            }
        }

        internal void Start(object selectedItem)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }

                // Dispose any other IDisposable objects here if needed
            }

            _disposed = true;
        }

        internal void SendPressedPocket(int v)
        {
            throw new NotImplementedException();
        }

        internal void ReceiveGameState(int[,] sender, bool sender2)
        {
            throw new NotImplementedException();
        }
    }
}
