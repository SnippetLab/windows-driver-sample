namespace SecureAgent
{
    partial class SecureAgentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecureAgentForm));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ShareFileManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_AccessLogViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.eventLogViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.popupNotifier1 = new NotificationWindow.PopupNotifier();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Start,
            this.toolStripMenuItem_ShareFileManager,
            this.toolStripMenuItem_AccessLogViewer,
            this.toolStripMenuItem5,
            this.toolStripMenuItem_Settings,
            this.toolStripSeparator1,
            this.eventLogViewerToolStripMenuItem,
            this.toolStripMenuItem_Help,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem_Exit});
            this.contextMenuStrip.Name = "contextMenuStrip_fileAgent";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // toolStripMenuItem_Start
            // 
            this.toolStripMenuItem_Start.Name = "toolStripMenuItem_Start";
            resources.ApplyResources(this.toolStripMenuItem_Start, "toolStripMenuItem_Start");
            this.toolStripMenuItem_Start.Click += new System.EventHandler(this.toolStripMenuItem_Start_Click);
            // 
            // toolStripMenuItem_ShareFileManager
            // 
            this.toolStripMenuItem_ShareFileManager.Name = "toolStripMenuItem_ShareFileManager";
            resources.ApplyResources(this.toolStripMenuItem_ShareFileManager, "toolStripMenuItem_ShareFileManager");
            this.toolStripMenuItem_ShareFileManager.Click += new System.EventHandler(this.toolStripMenuItem_ShareFileManager_Click);
            // 
            // toolStripMenuItem_Settings
            // 
            this.toolStripMenuItem_Settings.Name = "toolStripMenuItem_Settings";
            resources.ApplyResources(this.toolStripMenuItem_Settings, "toolStripMenuItem_Settings");
            this.toolStripMenuItem_Settings.Click += new System.EventHandler(this.toolStripMenuItem_Settings_Click);
            // 
            // toolStripMenuItem_AccessLogViewer
            // 
            this.toolStripMenuItem_AccessLogViewer.Name = "toolStripMenuItem_AccessLogViewer";
            resources.ApplyResources(this.toolStripMenuItem_AccessLogViewer, "toolStripMenuItem_AccessLogViewer");
            this.toolStripMenuItem_AccessLogViewer.Click += new System.EventHandler(this.toolStripMenuItem_AccessLogViewer_Click);
            // 
            // eventLogViewerToolStripMenuItem
            // 
            this.eventLogViewerToolStripMenuItem.Name = "eventLogViewerToolStripMenuItem";
            resources.ApplyResources(this.eventLogViewerToolStripMenuItem, "eventLogViewerToolStripMenuItem");
            this.eventLogViewerToolStripMenuItem.Click += new System.EventHandler(this.eventLogViewerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_Help
            // 
            this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            resources.ApplyResources(this.toolStripMenuItem_Help, "toolStripMenuItem_Help");
            this.toolStripMenuItem_Help.Click += new System.EventHandler(this.toolStripMenuItem_Help_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // exitToolStripMenuItem_Exit
            // 
            this.exitToolStripMenuItem_Exit.Name = "exitToolStripMenuItem_Exit";
            resources.ApplyResources(this.exitToolStripMenuItem_Exit, "exitToolStripMenuItem_Exit");
            this.exitToolStripMenuItem_Exit.Click += new System.EventHandler(this.exitToolStripMenuItem_Exit_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            // 
            // popupNotifier1
            // 
            this.popupNotifier1.BodyColor = System.Drawing.Color.SteelBlue;
            this.popupNotifier1.BorderColor = System.Drawing.Color.Navy;
            this.popupNotifier1.ButtonHoverColor = System.Drawing.Color.Orange;
            this.popupNotifier1.ContentColor = System.Drawing.Color.White;
            this.popupNotifier1.ContentFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.popupNotifier1.ContentText = null;
            this.popupNotifier1.HeaderColor = System.Drawing.Color.Navy;
            this.popupNotifier1.Image = null;
            this.popupNotifier1.OptionsMenu = null;
            this.popupNotifier1.Size = new System.Drawing.Size(400, 100);
            this.popupNotifier1.TitleColor = System.Drawing.Color.White;
            this.popupNotifier1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.popupNotifier1.TitleText = null;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // SecureAgentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecureAgentForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecureForm_FormClosing);
            this.Load += new System.EventHandler(this.SecureForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Start;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ShareFileManager;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AccessLogViewer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Settings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        internal NotificationWindow.PopupNotifier popupNotifier1;
        private System.Windows.Forms.ToolStripMenuItem eventLogViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}