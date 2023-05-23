using dasp;
using System.Windows.Forms;

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
        public void RefreshRoomList(List<ChatRoomInfo> updatedChatRooms)
        {
            chatRoomBindingSource.DataSource = updatedChatRooms;
        }
        private void JoinRoomButton_Click(object sender, EventArgs e)
        {
            if (chatRoomDataGridView.SelectedRows.Count > 0)
            {
                clientController.JoinRoom(chatRoomDataGridView.SelectedRows[0].DataBoundItem as ChatRoomInfo);
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
            if (labelCurrentPlayer.Text == updatedUsers[0])
            {
                startGameButton.Visible = true;
            }
            else
            {
                startGameButton.Visible = false;
            }
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
            //trebuie facuta logica ca daca se temrina si win lose game inclusiv sa vada si spectatorii
            gameForm.ReceiveGameState(sender, sender2, sender3);
            if (sender3 != -1)
            {
                startGameButton.Enabled = true;
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            clientController.StartGame();
        }

        public void GameStarted()
        {
            //name e al doile ajucator, si primul din userlistbox e primul jcuator sa stiie toti spectatorii
            //spectatorii sa aiba mesaj custom in gamestarted cand apasa aiurea pe joc
            gameForm.GameStarted();
            startGameButton.Enabled = false;
        }
        public void SetPlayerName(string playerName)
        {
            labelCurrentPlayer.Text = playerName;
        }

        private void logRichTextBox_TextChanged(object sender, EventArgs e)
        {
            // Scroll to the end of the text
            logRichTextBox.SelectionStart = logRichTextBox.Text.Length;
            logRichTextBox.ScrollToCaret();
        }
    }
}
