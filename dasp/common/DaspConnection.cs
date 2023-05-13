using System.Net.Sockets;
using System.Text;
namespace dasp
{
    public enum PlayerState
    {
        SPECTATING,
        CURRENTLY_MOVING,
        WAITING_TO_MOVE
    }
    public class DaspConnection
    {
        private readonly Socket _socket;
        private readonly Func<DaspRequest, DaspConnection, Task> _onRequestReceived;
        private readonly Func<string, Task> _logAsync;
        public PlayerState PlayerState { get; set; }= PlayerState.SPECTATING;
        public int GameEndStatus { get; set; }
        public ChatRoom ChatRoom{ get; set; }
        public string Username { get; set; }

        public DaspConnection(string address, int port, Func<DaspRequest, DaspConnection, Task> onRequestReceived, Func<string, Task> logAsync)
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(address, port);
            _onRequestReceived = onRequestReceived;
            _logAsync = logAsync;
            StartReceivingData();
        }
        public DaspConnection(Socket socket, Func<DaspRequest, DaspConnection, Task> onRequestReceived, Func<string, Task> logAsync)
        {
            _socket = socket;
            _onRequestReceived = onRequestReceived;
            _logAsync = logAsync;
            StartReceivingData();
        }
        private void StartReceivingData()
        {
            Task.Run(async () => await ReceiveData());
        }
        private async Task ReceiveData()
        {
            byte[] buffer = new byte[1024];
            StringBuilder stringBuilder = new();
            _logAsync("started receiving");
            while (true)
            {
                try
                {
                    int bytesRead = await _socket.ReceiveAsync(buffer, SocketFlags.None);
                    _logAsync("received new");
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    while (true)
                    {
                        if (DaspProcessor.TryStringToDaspRequest(stringBuilder.ToString(), out DaspRequest request))
                        {
                            _logAsync("found new request");
                            stringBuilder.Remove(0, request.Size);
                            try
                            {
                                _onRequestReceived(request, this);
                            }
                            catch (Exception ex)
                            {
                                _logAsync($"something went wrong {ex}");
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logAsync($"something went wrong {ex}");
                    break;
                }
            }
        }
        public void Send(DaspRequest request)
        {
            string daspMessage = DaspProcessor.DaspRequestToString(request);
            byte[] bytes = Encoding.UTF8.GetBytes(daspMessage);
            _socket.Send(bytes);
            _logAsync("request sent");
        }
    }
}
