namespace client
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            loginPanel = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            loginButton = new Button();
            registerButton = new Button();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            connectPanel = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            addressTextBox = new TextBox();
            portTextBox = new TextBox();
            connectButton = new Button();
            homePanel = new Panel();
            chatRoomDataGridView = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numberOfPlayersDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            chatRoomBindingSource = new BindingSource(components);
            createRoomButton = new Button();
            joinRoomButton = new Button();
            roomPanel = new Panel();
            clearSelectedButton = new Button();
            usersListBox = new ListBox();
            gamePanel = new Panel();
            sendMessageButton = new Button();
            messageRichTextBox = new RichTextBox();
            chatRichTextBox = new RichTextBox();
            startGameButton = new Button();
            leaveButton = new Button();
            logRichTextBox = new RichTextBox();
            loginPanel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            connectPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            homePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chatRoomDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chatRoomBindingSource).BeginInit();
            roomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.White;
            loginPanel.Controls.Add(tableLayoutPanel2);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Location = new Point(251, 0);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(1045, 704);
            loginPanel.TabIndex = 0;
            loginPanel.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(loginButton, 0, 2);
            tableLayoutPanel2.Controls.Add(registerButton, 1, 2);
            tableLayoutPanel2.Controls.Add(usernameTextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(passwordTextBox, 1, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Location = new Point(12, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel2.Size = new Size(333, 246);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.None;
            loginButton.Location = new Point(45, 201);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 0;
            loginButton.Text = "login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += LoginButton_Click;
            // 
            // registerButton
            // 
            registerButton.Anchor = AnchorStyles.None;
            registerButton.Location = new Point(212, 201);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(75, 23);
            registerButton.TabIndex = 0;
            registerButton.Text = "register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += RegisterButton_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Anchor = AnchorStyles.None;
            usernameTextBox.Location = new Point(199, 33);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(100, 23);
            usernameTextBox.TabIndex = 0;
            usernameTextBox.Text = "geo";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Anchor = AnchorStyles.None;
            passwordTextBox.Location = new Point(199, 123);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(100, 23);
            passwordTextBox.TabIndex = 0;
            passwordTextBox.Text = "geo";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(53, 37);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 0;
            label3.Text = "username";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(54, 127);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 0;
            label4.Text = "password";
            // 
            // connectPanel
            // 
            connectPanel.BackColor = Color.White;
            connectPanel.Controls.Add(tableLayoutPanel1);
            connectPanel.Dock = DockStyle.Fill;
            connectPanel.Location = new Point(251, 0);
            connectPanel.Name = "connectPanel";
            connectPanel.Size = new Size(1045, 704);
            connectPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.8382359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.1617661F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 69F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Controls.Add(addressTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(portTextBox, 3, 0);
            tableLayoutPanel1.Controls.Add(connectButton, 4, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(441, 116);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(6, 50);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "address";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(239, 50);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 0;
            label2.Text = "port";
            // 
            // addressTextBox
            // 
            addressTextBox.Anchor = AnchorStyles.None;
            addressTextBox.Location = new Point(90, 46);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(100, 23);
            addressTextBox.TabIndex = 0;
            addressTextBox.Text = "127.0.0.1";
            // 
            // portTextBox
            // 
            portTextBox.Anchor = AnchorStyles.None;
            portTextBox.Location = new Point(290, 46);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(63, 23);
            portTextBox.TabIndex = 0;
            portTextBox.Text = "42069";
            // 
            // connectButton
            // 
            connectButton.Anchor = AnchorStyles.None;
            connectButton.Location = new Point(361, 46);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 0;
            connectButton.Text = "connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += ConnectButton_Click;
            // 
            // homePanel
            // 
            homePanel.BackColor = Color.White;
            homePanel.Controls.Add(chatRoomDataGridView);
            homePanel.Controls.Add(createRoomButton);
            homePanel.Controls.Add(joinRoomButton);
            homePanel.Dock = DockStyle.Fill;
            homePanel.Location = new Point(251, 0);
            homePanel.Name = "homePanel";
            homePanel.Size = new Size(1045, 704);
            homePanel.TabIndex = 0;
            homePanel.Visible = false;
            // 
            // chatRoomDataGridView
            // 
            chatRoomDataGridView.Anchor = AnchorStyles.None;
            chatRoomDataGridView.AutoGenerateColumns = false;
            chatRoomDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            chatRoomDataGridView.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, numberOfPlayersDataGridViewTextBoxColumn });
            chatRoomDataGridView.DataSource = chatRoomBindingSource;
            chatRoomDataGridView.Location = new Point(6, 64);
            chatRoomDataGridView.Name = "chatRoomDataGridView";
            chatRoomDataGridView.RowTemplate.Height = 25;
            chatRoomDataGridView.Size = new Size(623, 628);
            chatRoomDataGridView.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // numberOfPlayersDataGridViewTextBoxColumn
            // 
            numberOfPlayersDataGridViewTextBoxColumn.DataPropertyName = "NumberOfPlayers";
            numberOfPlayersDataGridViewTextBoxColumn.HeaderText = "NumberOfPlayers";
            numberOfPlayersDataGridViewTextBoxColumn.Name = "numberOfPlayersDataGridViewTextBoxColumn";
            // 
            // chatRoomBindingSource
            // 
            chatRoomBindingSource.DataSource = typeof(dasp.ChatRoom);
            // 
            // createRoomButton
            // 
            createRoomButton.Anchor = AnchorStyles.None;
            createRoomButton.Location = new Point(162, 15);
            createRoomButton.Name = "createRoomButton";
            createRoomButton.Size = new Size(104, 23);
            createRoomButton.TabIndex = 0;
            createRoomButton.Text = "create new room";
            createRoomButton.UseVisualStyleBackColor = true;
            createRoomButton.Click += CreateRoomButton_Click;
            // 
            // joinRoomButton
            // 
            joinRoomButton.Anchor = AnchorStyles.None;
            joinRoomButton.Location = new Point(393, 15);
            joinRoomButton.Name = "joinRoomButton";
            joinRoomButton.Size = new Size(120, 23);
            joinRoomButton.TabIndex = 0;
            joinRoomButton.Text = "join selected room";
            joinRoomButton.UseVisualStyleBackColor = true;
            joinRoomButton.Click += JoinRoomButton_Click;
            // 
            // roomPanel
            // 
            roomPanel.BackColor = Color.White;
            roomPanel.Controls.Add(clearSelectedButton);
            roomPanel.Controls.Add(usersListBox);
            roomPanel.Controls.Add(gamePanel);
            roomPanel.Controls.Add(sendMessageButton);
            roomPanel.Controls.Add(messageRichTextBox);
            roomPanel.Controls.Add(chatRichTextBox);
            roomPanel.Controls.Add(startGameButton);
            roomPanel.Controls.Add(leaveButton);
            roomPanel.Dock = DockStyle.Fill;
            roomPanel.Location = new Point(251, 0);
            roomPanel.Name = "roomPanel";
            roomPanel.Size = new Size(1400, 704);
            roomPanel.TabIndex = 0;
            roomPanel.Visible = false;
            // 
            // clearSelectedButton
            // 
            clearSelectedButton.Location = new Point(208, 12);
            clearSelectedButton.Name = "clearSelectedButton";
            clearSelectedButton.Size = new Size(157, 23);
            clearSelectedButton.TabIndex = 0;
            clearSelectedButton.Text = "clear selected player";
            clearSelectedButton.UseVisualStyleBackColor = true;
            clearSelectedButton.Click += ClearSelectedButton_Click;
            // 
            // usersListBox
            // 
            usersListBox.FormattingEnabled = true;
            usersListBox.ItemHeight = 15;
            usersListBox.Location = new Point(12, 45);
            usersListBox.Name = "usersListBox";
            usersListBox.Size = new Size(190, 619);
            usersListBox.TabIndex = 0;
            // 
            // gamePanel
            // 
            gamePanel.BackColor = Color.FromArgb(255, 255, 128);
            gamePanel.Location = new Point(628, 12);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(700, 700);
            gamePanel.TabIndex = 0;
            gameForm = new GameForm(this) { TopLevel = false };
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gamePanel.Controls.Add(gameForm);
            gameForm.Show();
            // 
            // sendMessageButton
            // 
            sendMessageButton.Location = new Point(547, 573);
            sendMessageButton.Name = "sendMessageButton";
            sendMessageButton.Size = new Size(75, 23);
            sendMessageButton.TabIndex = 0;
            sendMessageButton.Text = "send message";
            sendMessageButton.UseVisualStyleBackColor = true;
            sendMessageButton.Click += SendMessageButton_Click;
            // 
            // messageRichTextBox
            // 
            messageRichTextBox.Location = new Point(208, 573);
            messageRichTextBox.Name = "messageRichTextBox";
            messageRichTextBox.Size = new Size(333, 96);
            messageRichTextBox.TabIndex = 0;
            messageRichTextBox.Text = "";
            // 
            // chatRichTextBox
            // 
            chatRichTextBox.Location = new Point(208, 44);
            chatRichTextBox.Name = "chatRichTextBox";
            chatRichTextBox.Size = new Size(414, 523);
            chatRichTextBox.TabIndex = 0;
            chatRichTextBox.Text = "";
            // 
            // startGameButton
            // 
            startGameButton.Location = new Point(547, 12);
            startGameButton.Name = "startGameButton";
            startGameButton.Size = new Size(75, 23);
            startGameButton.TabIndex = 0;
            startGameButton.Text = "start game";
            startGameButton.UseVisualStyleBackColor = true;
            startGameButton.Click += StartGameButton_Click;
            // 
            // leaveButton
            // 
            leaveButton.Location = new Point(12, 12);
            leaveButton.Name = "leaveButton";
            leaveButton.Size = new Size(75, 23);
            leaveButton.TabIndex = 0;
            leaveButton.Text = "leave";
            leaveButton.UseVisualStyleBackColor = true;
            leaveButton.Click += LeaveButton_Click;
            // 
            // logRichTextBox
            // 
            logRichTextBox.Dock = DockStyle.Left;
            logRichTextBox.Location = new Point(0, 0);
            logRichTextBox.Name = "logRichTextBox";
            logRichTextBox.ReadOnly = true;
            logRichTextBox.Size = new Size(251, 704);
            logRichTextBox.TabIndex = 0;
            logRichTextBox.Text = "";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 704);
            Controls.Add(connectPanel);
            Controls.Add(loginPanel);
            Controls.Add(homePanel);
            Controls.Add(roomPanel);
            Controls.Add(logRichTextBox);
            Name = "ClientForm";
            Text = "Client";
            FormClosing += ClientForm_FormClosing;
            loginPanel.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            connectPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            homePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chatRoomDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)chatRoomBindingSource).EndInit();
            roomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel loginPanel;
        private TableLayoutPanel tableLayoutPanel2;
        private Button loginButton;
        private Button registerButton;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Label label3;
        private Label label4;
        private Panel connectPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private TextBox addressTextBox;
        private TextBox portTextBox;
        private Button connectButton;
        private Panel homePanel;
        private DataGridView chatRoomDataGridView;
        private Button joinRoomButton;
        private Button createRoomButton;
        private Panel roomPanel;
        private Panel gamePanel;
        private Button sendMessageButton;
        private RichTextBox messageRichTextBox;
        private RichTextBox chatRichTextBox;
        private Button startGameButton;
        private Button leaveButton;
        private BindingSource chatRoomBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numberOfPlayersDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn maxNumberOfPlayersDataGridViewTextBoxColumn;
        private ListBox usersListBox;
        private Button clearSelectedButton;
        private RichTextBox logRichTextBox;
        private GameForm gameForm;
    }
}