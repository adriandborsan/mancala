
using System.Net.Sockets;
using System.Net;
namespace dasp.server
{
    public class DaspServerConnection
    {
        private Socket socket;
        private readonly Func<Socket, Task> handleClientAsync;
        private readonly Func<string, Task> _logAsync;
        private volatile bool _isRunning = true;
        public DaspServerConnection(int port, Func<Socket, Task> handleClientAsync, Func<string, Task> logAsync)
        {
            this.handleClientAsync = handleClientAsync;
            _logAsync = logAsync;
            Initialize(port);
            StartListeningForClients();
        }
        private void Initialize(int port)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen();
        }
        private void StartListeningForClients()
        {
            Task.Run(() => ListenForClientsAsync());
        }
        private async Task ListenForClientsAsync()
        {
            _isRunning = true;
            while (_isRunning)
            {
                try
                {
                    Socket clientSocket = await Task.Factory.FromAsync<Socket>(
                        socket.BeginAccept,
                        socket.EndAccept,
                        null
                    );

                    _logAsync("Client connected: " + clientSocket.RemoteEndPoint);
                    handleClientAsync(clientSocket);
                }
                catch (Exception ex)
                {
                    _logAsync($"something went wrong {ex}");
                }
            }
        }
        public void StopListening()
        {
            _isRunning = false;

            if (socket != null)
            {
                socket.Close();
                socket.Dispose();
            }
        }
    }
}
