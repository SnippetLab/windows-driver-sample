namespace SecureAgent
{
    partial class ShareFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareFileForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_RevokeControl = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_ExpireDate = new System.Windows.Forms.DateTimePicker();
            this.textBox_UserPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_GetComputerId = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_IncludeComputerIds = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_OutputFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ExcludeUserNames = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_IncludeUserNames = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label_AccessFlags = new System.Windows.Forms.Label();
            this.dateTimePicker_ExpireTime = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_ExcludeProcessNames = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_IncludeProcessNames = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_Apply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Share file expire time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File name";
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.Location = new System.Drawing.Point(213, 20);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.Size = new System.Drawing.Size(242, 20);
            this.textBox_FileName.TabIndex = 2;
            this.textBox_FileName.Text = "c:\\test\\test.txt";
            this.textBox_FileName.TextChanged += new System.EventHandler(this.textBox_FileName_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_RevokeControl);
            this.groupBox1.Controls.Add(this.dateTimePicker_ExpireDate);
            this.groupBox1.Controls.Add(this.textBox_UserPassword);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button_GetComputerId);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox_IncludeComputerIds);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox_OutputFilePath);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button_OpenFile);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_ExcludeUserNames);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_IncludeUserNames);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label_AccessFlags);
            this.groupBox1.Controls.Add(this.dateTimePicker_ExpireTime);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_ExcludeProcessNames);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_IncludeProcessNames);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_FileName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 401);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // checkBox_RevokeControl
            // 
            this.checkBox_RevokeControl.AutoSize = true;
            this.checkBox_RevokeControl.Location = new System.Drawing.Point(212, 378);
            this.checkBox_RevokeControl.Name = "checkBox_RevokeControl";
            this.checkBox_RevokeControl.Size = new System.Drawing.Size(167, 17);
            this.checkBox_RevokeControl.TabIndex = 72;
            this.checkBox_RevokeControl.Text = "Enable revoke access control";
            this.checkBox_RevokeControl.UseVisualStyleBackColor = true;
            this.checkBox_RevokeControl.Click += new System.EventHandler(this.checkBox_RevokeControk_Click);
            // 
            // dateTimePicker_ExpireDate
            // 
            this.dateTimePicker_ExpireDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker_ExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_ExpireDate.Location = new System.Drawing.Point(212, 100);
            this.dateTimePicker_ExpireDate.Name = "dateTimePicker_ExpireDate";
            this.dateTimePicker_ExpireDate.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker_ExpireDate.TabIndex = 71;
            // 
            // textBox_UserPassword
            // 
            this.textBox_UserPassword.Location = new System.Drawing.Point(212, 296);
            this.textBox_UserPassword.Name = "textBox_UserPassword";
            this.textBox_UserPassword.Size = new System.Drawing.Size(242, 20);
            this.textBox_UserPassword.TabIndex = 10;
            this.textBox_UserPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 296);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Require user password";
            // 
            // button_GetComputerId
            // 
            this.button_GetComputerId.Location = new System.Drawing.Point(461, 330);
            this.button_GetComputerId.Name = "button_GetComputerId";
            this.button_GetComputerId.Size = new System.Drawing.Size(51, 23);
            this.button_GetComputerId.TabIndex = 12;
            this.button_GetComputerId.Text = "...";
            this.button_GetComputerId.UseVisualStyleBackColor = true;
            this.button_GetComputerId.Click += new System.EventHandler(this.button_GetComputerId_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(211, 353);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 12);
            this.label13.TabIndex = 68;
            this.label13.Text = "(Seperate multiple items with \';\' )";
            // 
            // textBox_IncludeComputerIds
            // 
            this.textBox_IncludeComputerIds.Location = new System.Drawing.Point(212, 332);
            this.textBox_IncludeComputerIds.Name = "textBox_IncludeComputerIds";
            this.textBox_IncludeComputerIds.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeComputerIds.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "Authorized  computer Ids";
            // 
            // textBox_OutputFilePath
            // 
            this.textBox_OutputFilePath.Location = new System.Drawing.Point(213, 60);
            this.textBox_OutputFilePath.Name = "textBox_OutputFilePath";
            this.textBox_OutputFilePath.ReadOnly = true;
            this.textBox_OutputFilePath.Size = new System.Drawing.Size(242, 20);
            this.textBox_OutputFilePath.TabIndex = 4;
            this.textBox_OutputFilePath.Text = "c:\\test\\test.txt.psf";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Share encrypted file name";
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Location = new System.Drawing.Point(461, 18);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(51, 23);
            this.button_OpenFile.TabIndex = 3;
            this.button_OpenFile.Text = "browse";
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(211, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "(split with \';\' ,user name format \"domain\\user\" )";
            // 
            // textBox_ExcludeUserNames
            // 
            this.textBox_ExcludeUserNames.Location = new System.Drawing.Point(213, 260);
            this.textBox_ExcludeUserNames.Name = "textBox_ExcludeUserNames";
            this.textBox_ExcludeUserNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludeUserNames.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Unauthorized user names";
            // 
            // textBox_IncludeUserNames
            // 
            this.textBox_IncludeUserNames.Location = new System.Drawing.Point(213, 217);
            this.textBox_IncludeUserNames.Name = "textBox_IncludeUserNames";
            this.textBox_IncludeUserNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeUserNames.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Authorized user names";
            // 
            // label_AccessFlags
            // 
            this.label_AccessFlags.AutoSize = true;
            this.label_AccessFlags.Location = new System.Drawing.Point(16, 372);
            this.label_AccessFlags.Name = "label_AccessFlags";
            this.label_AccessFlags.Size = new System.Drawing.Size(0, 13);
            this.label_AccessFlags.TabIndex = 12;
            // 
            // dateTimePicker_ExpireTime
            // 
            this.dateTimePicker_ExpireTime.CustomFormat = " HH:mm:ss";
            this.dateTimePicker_ExpireTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker_ExpireTime.Location = new System.Drawing.Point(335, 100);
            this.dateTimePicker_ExpireTime.Name = "dateTimePicker_ExpireTime";
            this.dateTimePicker_ExpireTime.ShowUpDown = true;
            this.dateTimePicker_ExpireTime.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker_ExpireTime.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(211, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 12);
            this.label12.TabIndex = 57;
            this.label12.Text = "( split with \';\' , process format \"notepad.exe\" )";
            // 
            // textBox_ExcludeProcessNames
            // 
            this.textBox_ExcludeProcessNames.Location = new System.Drawing.Point(213, 180);
            this.textBox_ExcludeProcessNames.Name = "textBox_ExcludeProcessNames";
            this.textBox_ExcludeProcessNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludeProcessNames.TabIndex = 7;
            this.textBox_ExcludeProcessNames.Text = "explorer.exe;";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 13);
            this.label11.TabIndex = 55;
            this.label11.Text = "Unauthorized  process names";
            // 
            // textBox_IncludeProcessNames
            // 
            this.textBox_IncludeProcessNames.Location = new System.Drawing.Point(213, 137);
            this.textBox_IncludeProcessNames.Name = "textBox_IncludeProcessNames";
            this.textBox_IncludeProcessNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeProcessNames.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Authorized process names";
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(436, 421);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(114, 23);
            this.button_Apply.TabIndex = 15;
            this.button_Apply.Text = "Apply Change";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // ShareFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 470);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShareFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AssureFiles Secure File Sharing With Access Control";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_FileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_AccessFlags;
        private System.Windows.Forms.TextBox textBox_ExcludeProcessNames;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_IncludeProcessNames;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ExcludeUserNames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_IncludeUserNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker_ExpireTime;
        private System.Windows.Forms.Button button_OpenFile;
        private System.Windows.Forms.TextBox textBox_OutputFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_GetComputerId;
        private System.Windows.Forms.TextBox textBox_IncludeComputerIds;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.TextBox textBox_UserPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker_ExpireDate;
        private System.Windows.Forms.CheckBox checkBox_RevokeControl;
    }
}