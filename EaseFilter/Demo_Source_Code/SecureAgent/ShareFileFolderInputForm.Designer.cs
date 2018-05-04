namespace SecureAgent
{
    partial class ShareFileFolderInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareFileFolderInputForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_BrowseFolder = new System.Windows.Forms.Button();
            this.textBox_DropFolder = new System.Windows.Forms.TextBox();
            this.button_SetDropFolder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_BrowseFolder);
            this.groupBox1.Controls.Add(this.textBox_DropFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Share File Drop Folder";
            // 
            // button_BrowseFolder
            // 
            this.button_BrowseFolder.Location = new System.Drawing.Point(318, 34);
            this.button_BrowseFolder.Name = "button_BrowseFolder";
            this.button_BrowseFolder.Size = new System.Drawing.Size(31, 23);
            this.button_BrowseFolder.TabIndex = 1;
            this.button_BrowseFolder.Text = "...";
            this.button_BrowseFolder.UseVisualStyleBackColor = true;
            this.button_BrowseFolder.Click += new System.EventHandler(this.button_BrowseFolder_Click);
            // 
            // textBox_DropFolder
            // 
            this.textBox_DropFolder.Location = new System.Drawing.Point(151, 34);
            this.textBox_DropFolder.Name = "textBox_DropFolder";
            this.textBox_DropFolder.Size = new System.Drawing.Size(161, 20);
            this.textBox_DropFolder.TabIndex = 0;
            // 
            // button_SetDropFolder
            // 
            this.button_SetDropFolder.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_SetDropFolder.Location = new System.Drawing.Point(292, 124);
            this.button_SetDropFolder.Name = "button_SetDropFolder";
            this.button_SetDropFolder.Size = new System.Drawing.Size(75, 23);
            this.button_SetDropFolder.TabIndex = 2;
            this.button_SetDropFolder.Text = "Ok";
            this.button_SetDropFolder.UseVisualStyleBackColor = true;
            this.button_SetDropFolder.Click += new System.EventHandler(this.button_SetDropFolder_Click);
            // 
            // SecureFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 167);
            this.Controls.Add(this.button_SetDropFolder);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecureFolderForm";
            this.Text = "Secure Files Drop Folder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_BrowseFolder;
        private System.Windows.Forms.TextBox textBox_DropFolder;
        private System.Windows.Forms.Button button_SetDropFolder;
    }
}