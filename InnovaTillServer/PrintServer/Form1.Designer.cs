namespace PrintServer
{
    partial class Form1
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
            tabControl1 = new TabControl();
            tabServer = new TabPage();
            btnServerStop = new Button();
            btnServerStart = new Button();
            lstbLog = new ListBox();
            chbSocket = new CheckBox();
            chbMqtt = new CheckBox();
            tabClient = new TabPage();
            btnClientStart = new Button();
            btnSend = new Button();
            tabControl1.SuspendLayout();
            tabServer.SuspendLayout();
            tabClient.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabServer);
            tabControl1.Controls.Add(tabClient);
            tabControl1.Dock = DockStyle.Bottom;
            tabControl1.Location = new Point(0, 37);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 413);
            tabControl1.TabIndex = 0;
            // 
            // tabServer
            // 
            tabServer.Controls.Add(btnSend);
            tabServer.Controls.Add(btnServerStop);
            tabServer.Controls.Add(btnServerStart);
            tabServer.Controls.Add(lstbLog);
            tabServer.Controls.Add(chbSocket);
            tabServer.Controls.Add(chbMqtt);
            tabServer.Location = new Point(4, 24);
            tabServer.Name = "tabServer";
            tabServer.Padding = new Padding(3);
            tabServer.Size = new Size(792, 385);
            tabServer.TabIndex = 0;
            tabServer.Text = "Server";
            tabServer.UseVisualStyleBackColor = true;
            // 
            // btnServerStop
            // 
            btnServerStop.Location = new Point(320, 41);
            btnServerStop.Name = "btnServerStop";
            btnServerStop.Size = new Size(75, 23);
            btnServerStop.TabIndex = 2;
            btnServerStop.Text = "Stop";
            btnServerStop.UseVisualStyleBackColor = true;
            btnServerStop.Click += btnServerStop_Click;
            // 
            // btnServerStart
            // 
            btnServerStart.Location = new Point(230, 41);
            btnServerStart.Name = "btnServerStart";
            btnServerStart.Size = new Size(75, 23);
            btnServerStart.TabIndex = 2;
            btnServerStart.Text = "Start";
            btnServerStart.UseVisualStyleBackColor = true;
            btnServerStart.Click += btnServerStart_Click;
            // 
            // lstbLog
            // 
            lstbLog.Dock = DockStyle.Bottom;
            lstbLog.FormattingEnabled = true;
            lstbLog.ItemHeight = 15;
            lstbLog.Location = new Point(3, 78);
            lstbLog.Name = "lstbLog";
            lstbLog.Size = new Size(786, 304);
            lstbLog.TabIndex = 1;
            // 
            // chbSocket
            // 
            chbSocket.AutoSize = true;
            chbSocket.Location = new Point(98, 38);
            chbSocket.Name = "chbSocket";
            chbSocket.Size = new Size(88, 19);
            chbSocket.TabIndex = 0;
            chbSocket.Text = "Web Socket";
            chbSocket.UseVisualStyleBackColor = true;
            chbSocket.CheckedChanged += chbSocket_CheckedChanged;
            // 
            // chbMqtt
            // 
            chbMqtt.AutoSize = true;
            chbMqtt.Location = new Point(23, 38);
            chbMqtt.Name = "chbMqtt";
            chbMqtt.Size = new Size(57, 19);
            chbMqtt.TabIndex = 0;
            chbMqtt.Text = "MQTT";
            chbMqtt.UseVisualStyleBackColor = true;
            chbMqtt.CheckedChanged += chbMqtt_CheckedChanged;
            // 
            // tabClient
            // 
            tabClient.Controls.Add(btnClientStart);
            tabClient.Location = new Point(4, 24);
            tabClient.Name = "tabClient";
            tabClient.Padding = new Padding(3);
            tabClient.Size = new Size(792, 385);
            tabClient.TabIndex = 1;
            tabClient.Text = "Client";
            tabClient.UseVisualStyleBackColor = true;
            // 
            // btnClientStart
            // 
            btnClientStart.Location = new Point(375, 32);
            btnClientStart.Name = "btnClientStart";
            btnClientStart.Size = new Size(141, 23);
            btnClientStart.TabIndex = 0;
            btnClientStart.Text = "Start";
            btnClientStart.UseVisualStyleBackColor = true;
            btnClientStart.Click += btnClientStart_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(419, 41);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Test Platform";
            tabControl1.ResumeLayout(false);
            tabServer.ResumeLayout(false);
            tabServer.PerformLayout();
            tabClient.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabServer;
        private ListBox lstbLog;
        private CheckBox chbMqtt;
        private TabPage tabClient;
        private CheckBox checkBox2;
        private CheckBox chbSocket;
        private Button btnServerStart;
        private Button btnServerStop;
        private Button btnClientStart;
        private Button btnSend;
    }
}
