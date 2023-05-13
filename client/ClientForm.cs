using dasp;
namespace client
{
    public partial class ClientForm : Form
    {
        private readonly ClientController clientController;
        public ClientForm()
        {
            InitializeComponent();
            clientController = new ClientController(this);
        }
        public void AddLog(string message)
        {
            logRichTextBox.AppendText(message);
        }
        public void RunOnUiThread(Action action, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            clientController.Connect(addressTextBox.Text, portTextBox.Text);
        }
        public void SwitchToLogin()
        {
            connectPanel.Visible = false;
            loginPanel.Visible = true;
            loginPanel.BringToFront();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            clientController.Login(usernameTextBox.Text, passwordTextBox.Text);
        }
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            clientController.Register(usernameTextBox.Text, passwordTextBox.Text);
        }
        public void SwitchToHome()
        {
            loginPanel.Visible = false;
            homePanel.Visible = true;
            homePanel.BringToFront();
        }
        public void RefreshRoomList(List<ChatRoom> updatedChatRooms)
        {
            chatRoomBindingSource.DataSource = updatedChatRooms;
        }
        private void JoinRoomButton_Click(object sender, EventArgs e)
        {
            if (chatRoomDataGridView.SelectedRows.Count > 0)
            {
                clientController.JoinRoom(chatRoomDataGridView.SelectedRows[0].DataBoundItem as ChatRoom);
            }
        }
        private void CreateRoomButton_Click(object sender, EventArgs e)
        {
            clientController.CreateRoom();
        }
        public void SwitchToRoom()
        {
            homePanel.Visible = false;
            roomPanel.Visible = true;
            roomPanel.BringToFront();
        }
        private void LeaveButton_Click(object sender, EventArgs e)
        {
            clientController.LeaveRoom();
        }
        public void LeaveRoom()
        {
            roomPanel.Visible = false;
            homePanel.Visible = true;
            homePanel.BringToFront();
        }
        public void RefreshUserList(List<string> updatedUsers)
        {
            usersListBox.Items.Clear();
            usersListBox.Items.AddRange(updatedUsers.ToArray());
        }
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            clientController.Start(usersListBox.SelectedItem);
        }
        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            string message = messageRichTextBox.Text;
            messageRichTextBox.Text = string.Empty;
            clientController.SendMessage(message, usersListBox.SelectedItem);
        }
        public void AddMessage(string message)
        {
            chatRichTextBox.AppendText(message);
        }
        private void ClearSelectedButton_Click(object sender, EventArgs e)
        {
            usersListBox.SelectedItem = null;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientController.Dispose();
        }

        public void SendPressedPocket(int sender)
        {
            clientController.SendPressedPocket(sender);
        }

        public void ReceiveGameState(int[,] sender, bool sender2, int sender3)
        {
            clientController.ReceiveGameState(sender, sender2, sender3);
        }
    }
}  
