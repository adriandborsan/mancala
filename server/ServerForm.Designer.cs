namespace server
{
    partial class ServerForm
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
            panel1 = new Panel();
            logRichTextBox = new RichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            stopButton = new Button();
            portTextBox = new TextBox();
            startButton = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            label5 = new Label();
            addressTextBox = new TextBox();
            databaseTextBox = new TextBox();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(logRichTextBox);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(680, 333);
            panel1.TabIndex = 0;
            // 
            // logRichTextBox
            // 
            logRichTextBox.Dock = DockStyle.Right;
            logRichTextBox.Location = new Point(343, 0);
            logRichTextBox.Name = "logRichTextBox";
            logRichTextBox.Size = new Size(337, 333);
            logRichTextBox.TabIndex = 2;
            logRichTextBox.Text = "";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(stopButton, 1, 5);
            tableLayoutPanel2.Controls.Add(portTextBox, 1, 4);
            tableLayoutPanel2.Controls.Add(startButton, 0, 5);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 2);
            tableLayoutPanel2.Controls.Add(label1, 0, 4);
            tableLayoutPanel2.Controls.Add(label5, 0, 3);
            tableLayoutPanel2.Controls.Add(addressTextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(databaseTextBox, 1, 1);
            tableLayoutPanel2.Controls.Add(usernameTextBox, 1, 2);
            tableLayoutPanel2.Controls.Add(passwordTextBox, 1, 3);
            tableLayoutPanel2.Location = new Point(12, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(315, 308);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // stopButton
            // 
            stopButton.Anchor = AnchorStyles.None;
            stopButton.Enabled = false;
            stopButton.Location = new Point(198, 270);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 2;
            stopButton.Text = "stop server";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += StopButton_Click;
            // 
            // portTextBox
            // 
            portTextBox.Anchor = AnchorStyles.None;
            portTextBox.Location = new Point(189, 218);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(94, 23);
            portTextBox.TabIndex = 0;
            portTextBox.Text = "42069";
            // 
            // startButton
            // 
            startButton.Anchor = AnchorStyles.None;
            startButton.Location = new Point(41, 270);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 1;
            startButton.Text = "start server";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(55, 18);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 0;
            label2.Text = "address";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(51, 69);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 1;
            label3.Text = "database";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(49, 120);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 2;
            label4.Text = "username";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(47, 222);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 3;
            label1.Text = "server port";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(50, 171);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 3;
            label5.Text = "password";
            // 
            // addressTextBox
            // 
            addressTextBox.Anchor = AnchorStyles.None;
            addressTextBox.Location = new Point(186, 14);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(100, 23);
            addressTextBox.TabIndex = 6;
            addressTextBox.Text = "localhost";
            // 
            // databaseTextBox
            // 
            databaseTextBox.Anchor = AnchorStyles.None;
            databaseTextBox.Location = new Point(186, 65);
            databaseTextBox.Name = "databaseTextBox";
            databaseTextBox.Size = new Size(100, 23);
            databaseTextBox.TabIndex = 7;
            databaseTextBox.Text = "mancala";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Anchor = AnchorStyles.None;
            usernameTextBox.Location = new Point(186, 116);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(100, 23);
            usernameTextBox.TabIndex = 8;
            usernameTextBox.Text = "root";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Anchor = AnchorStyles.None;
            passwordTextBox.Location = new Point(186, 167);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(100, 23);
            passwordTextBox.TabIndex = 9;
            passwordTextBox.Text = "hus4l1337";
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 333);
            Controls.Add(panel1);
            Name = "ServerForm";
            Text = "Server";
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private RichTextBox logRichTextBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox addressTextBox;
        private TextBox databaseTextBox;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private TextBox portTextBox;
        private Button startButton;
        private Button stopButton;
        private Label label1;
    }
}