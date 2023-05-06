using dasp;
namespace server
{
    internal class ServerController
    {
        private readonly ServerForm serverForm;
        private DaspServer daspServer;
        public ServerController(ServerForm serverForm)
        {
            this.serverForm = serverForm;
        }
        internal async Task Start(string port, string address, string database, string username, string password)
        {
            daspServer = await Task.Run(() => new DaspServer(int.Parse(port), address, database, username, password, SendLog));
            serverForm.RunOnUiThread(() => serverForm.EnableStopButton());
        }
        internal async Task Stop()
        {
            await daspServer.Stop();
            serverForm.RunOnUiThread(() => serverForm.EnableStartButton());
        }
        internal async Task SendLog(string message)
        {
            serverForm.RunOnUiThread(() => serverForm.AddLog($"{DateTime.Now}:{message}\n"));
        }
    }
}
