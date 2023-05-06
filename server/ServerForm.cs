
namespace server
{
    public partial class ServerForm : Form
    {
        private readonly ServerController serverController;
        public ServerForm()
        {
            InitializeComponent();
            serverController = new ServerController(this);
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            string port = portTextBox.Text;
            string address = addressTextBox.Text;
            string database = databaseTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            serverController.Start(port, address, database, username, password);
        }
        public void AddLog(string message)
        {
            logRichTextBox.AppendText(message);
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            serverController.Stop();
        }
        public void EnableStartButton()
        {
            startButton.Enabled = true;
        }
        public void EnableStopButton()
        {
            stopButton.Enabled = true;
        }
        public void RunOnUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
