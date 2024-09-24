namespace SyncFolders
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            label_SourcePath = new Label();
            label_ReplicaPath = new Label();
            textbox_SourcePath = new TextBox();
            textbox_ReplicaPath = new TextBox();
            notifyIcon = new NotifyIcon(components);
            notifyIconMenu = new ContextMenuStrip(components);
            treToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            button_StartSync = new Button();
            button_StopSync = new Button();
            button_SelectSource = new Button();
            button_SelectReplica = new Button();
            notifyIconMenu.SuspendLayout();
            SuspendLayout();
            // 
            // label_SourcePath
            // 
            label_SourcePath.AutoSize = true;
            label_SourcePath.Location = new Point(7, 5);
            label_SourcePath.Name = "label_SourcePath";
            label_SourcePath.Size = new Size(43, 15);
            label_SourcePath.TabIndex = 0;
            label_SourcePath.Text = "Source";
            // 
            // label_ReplicaPath
            // 
            label_ReplicaPath.AutoSize = true;
            label_ReplicaPath.Location = new Point(12, 55);
            label_ReplicaPath.Name = "label_ReplicaPath";
            label_ReplicaPath.Size = new Size(45, 15);
            label_ReplicaPath.TabIndex = 1;
            label_ReplicaPath.Text = "Replica";
            // 
            // textbox_SourcePath
            // 
            textbox_SourcePath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textbox_SourcePath.Location = new Point(12, 23);
            textbox_SourcePath.Name = "textbox_SourcePath";
            textbox_SourcePath.ReadOnly = true;
            textbox_SourcePath.Size = new Size(389, 23);
            textbox_SourcePath.TabIndex = 2;
            // 
            // textbox_ReplicaPath
            // 
            textbox_ReplicaPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_ReplicaPath.Location = new Point(12, 73);
            textbox_ReplicaPath.Name = "textbox_ReplicaPath";
            textbox_ReplicaPath.ReadOnly = true;
            textbox_ReplicaPath.Size = new Size(389, 23);
            textbox_ReplicaPath.TabIndex = 3;
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipText = "Synchronization started";
            notifyIcon.ContextMenuStrip = notifyIconMenu;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "notifyIcon1";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // notifyIconMenu
            // 
            notifyIconMenu.Items.AddRange(new ToolStripItem[] { treToolStripMenuItem, exitToolStripMenuItem });
            notifyIconMenu.Name = "contextMenuStrip1";
            notifyIconMenu.Size = new Size(104, 48);
            // 
            // treToolStripMenuItem
            // 
            treToolStripMenuItem.Name = "treToolStripMenuItem";
            treToolStripMenuItem.Size = new Size(103, 22);
            treToolStripMenuItem.Text = "Open";
            treToolStripMenuItem.Click += treToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // button_StartSync
            // 
            button_StartSync.Anchor = AnchorStyles.Bottom;
            button_StartSync.Location = new Point(29, 111);
            button_StartSync.Name = "button_StartSync";
            button_StartSync.Size = new Size(164, 23);
            button_StartSync.TabIndex = 4;
            button_StartSync.Text = "Start Synchronization";
            button_StartSync.UseVisualStyleBackColor = true;
            button_StartSync.Click += button_StartSync_Click;
            // 
            // button_StopSync
            // 
            button_StopSync.Anchor = AnchorStyles.Bottom;
            button_StopSync.Location = new Point(221, 111);
            button_StopSync.Name = "button_StopSync";
            button_StopSync.Size = new Size(164, 23);
            button_StopSync.TabIndex = 5;
            button_StopSync.Text = "Stop Synchronization";
            button_StopSync.UseVisualStyleBackColor = true;
            button_StopSync.Click += button_StopSync_Click;
            // 
            // button_SelectSource
            // 
            button_SelectSource.Anchor = AnchorStyles.Right;
            button_SelectSource.Location = new Point(411, 23);
            button_SelectSource.Name = "button_SelectSource";
            button_SelectSource.Size = new Size(35, 23);
            button_SelectSource.TabIndex = 6;
            button_SelectSource.Text = "...";
            button_SelectSource.UseVisualStyleBackColor = true;
            button_SelectSource.Click += button_SelectSource_Click;
            // 
            // button_SelectReplica
            // 
            button_SelectReplica.Anchor = AnchorStyles.Right;
            button_SelectReplica.Location = new Point(411, 73);
            button_SelectReplica.Name = "button_SelectReplica";
            button_SelectReplica.Size = new Size(35, 23);
            button_SelectReplica.TabIndex = 7;
            button_SelectReplica.Text = "...";
            button_SelectReplica.UseVisualStyleBackColor = true;
            button_SelectReplica.Click += button_SelectReplica_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 155);
            Controls.Add(button_SelectReplica);
            Controls.Add(button_SelectSource);
            Controls.Add(button_StopSync);
            Controls.Add(button_StartSync);
            Controls.Add(textbox_ReplicaPath);
            Controls.Add(textbox_SourcePath);
            Controls.Add(label_ReplicaPath);
            Controls.Add(label_SourcePath);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(700, 194);
            MinimizeBox = false;
            MinimumSize = new Size(474, 194);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sync Folders";
            FormClosing += Form1_FormClosing;
            notifyIconMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_SourcePath;
        private Label label_ReplicaPath;
        private TextBox textbox_SourcePath;
        private TextBox textbox_ReplicaPath;
        private NotifyIcon notifyIcon;
        private Button button_StartSync;
        private Button button_StopSync;
        private ContextMenuStrip notifyIconMenu;
        private ToolStripMenuItem treToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button button_SelectSource;
        private Button button_SelectReplica;
    }
}
