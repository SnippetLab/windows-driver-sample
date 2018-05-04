namespace AutoFileCryptTool
{
    partial class Form_FileCrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_FileCrypt));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "You can drag&drop the empty folders to here"}, -1, System.Drawing.Color.Turquoise, System.Drawing.Color.Empty, new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "The encryption driver will encrypt/decrypt the files automatically"}, -1, System.Drawing.Color.Turquoise, System.Drawing.Color.Empty, new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "The files will be encrypted with 256 bit AES algorithm"}, -1, System.Drawing.Color.Turquoise, System.Drawing.Color.Empty, new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            this.button_ChangePassword = new System.Windows.Forms.Button();
            this.button_ApplySetting = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox_ExcludedUsers = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_IncludedUsers = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label_user = new System.Windows.Forms.Label();
            this.button_SelectExcludePID = new System.Windows.Forms.Button();
            this.textBox_ExcludePID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_SelectIncludePID = new System.Windows.Forms.Button();
            this.textBox_IncludePID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage_Help = new System.Windows.Forms.TabPage();
            this.linkLabel_SDK = new System.Windows.Forms.LinkLabel();
            this.linkLabel_Report = new System.Windows.Forms.LinkLabel();
            this.label_VersionInfo = new System.Windows.Forms.Label();
            this.tabPage_DecryptFile = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_BrowseFileToDecrypt = new System.Windows.Forms.Button();
            this.checkBox_ShowPasswordToDecrypt = new System.Windows.Forms.CheckBox();
            this.textBox_FileNameToDecrypt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PasswordToDecrypt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_StartToDecrypt = new System.Windows.Forms.Button();
            this.tabPage_EncryptFile = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_BrowseEncryptFile = new System.Windows.Forms.Button();
            this.checkBox_ShowPasswordToEncrypt = new System.Windows.Forms.CheckBox();
            this.textBox_FileNameToEncrypt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_PasswordToEncrypt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_StartToEncrypt = new System.Windows.Forms.Button();
            this.tabPage_Folder = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_RemoveFolder = new System.Windows.Forms.Button();
            this.button_AddFolder = new System.Windows.Forms.Button();
            this.listView_Folders = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Settings.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage_Help.SuspendLayout();
            this.tabPage_DecryptFile.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage_EncryptFile.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage_Folder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder_ok.png");
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.Controls.Add(this.button_ChangePassword);
            this.tabPage_Settings.Controls.Add(this.button_ApplySetting);
            this.tabPage_Settings.Controls.Add(this.groupBox5);
            this.tabPage_Settings.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Settings.Name = "tabPage_Settings";
            this.tabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Settings.Size = new System.Drawing.Size(548, 255);
            this.tabPage_Settings.TabIndex = 5;
            this.tabPage_Settings.Text = "Settings";
            this.tabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // button_ChangePassword
            // 
            this.button_ChangePassword.Location = new System.Drawing.Point(25, 215);
            this.button_ChangePassword.Name = "button_ChangePassword";
            this.button_ChangePassword.Size = new System.Drawing.Size(122, 20);
            this.button_ChangePassword.TabIndex = 42;
            this.button_ChangePassword.Text = "Change password";
            this.button_ChangePassword.UseVisualStyleBackColor = true;
            this.button_ChangePassword.Click += new System.EventHandler(this.button_ChangePassword_Click);
            // 
            // button_ApplySetting
            // 
            this.button_ApplySetting.Location = new System.Drawing.Point(431, 215);
            this.button_ApplySetting.Name = "button_ApplySetting";
            this.button_ApplySetting.Size = new System.Drawing.Size(97, 20);
            this.button_ApplySetting.TabIndex = 46;
            this.button_ApplySetting.Text = "Apply settings";
            this.button_ApplySetting.UseVisualStyleBackColor = true;
            this.button_ApplySetting.Click += new System.EventHandler(this.button_ApplySetting_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox_ExcludedUsers);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.textBox_IncludedUsers);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label_user);
            this.groupBox5.Controls.Add(this.button_SelectExcludePID);
            this.groupBox5.Controls.Add(this.textBox_ExcludePID);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.button_SelectIncludePID);
            this.groupBox5.Controls.Add(this.textBox_IncludePID);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Location = new System.Drawing.Point(25, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(503, 192);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Access control";
            // 
            // textBox_ExcludedUsers
            // 
            this.textBox_ExcludedUsers.Location = new System.Drawing.Point(151, 153);
            this.textBox_ExcludedUsers.Name = "textBox_ExcludedUsers";
            this.textBox_ExcludedUsers.Size = new System.Drawing.Size(287, 20);
            this.textBox_ExcludedUsers.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Excluded users";
            // 
            // textBox_IncludedUsers
            // 
            this.textBox_IncludedUsers.Location = new System.Drawing.Point(151, 105);
            this.textBox_IncludedUsers.Name = "textBox_IncludedUsers";
            this.textBox_IncludedUsers.Size = new System.Drawing.Size(287, 20);
            this.textBox_IncludedUsers.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Included users";
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Location = new System.Drawing.Point(148, 128);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(290, 13);
            this.label_user.TabIndex = 47;
            this.label_user.Text = "(format:domain\\username, split with \';\' for multiple users)";
            // 
            // button_SelectExcludePID
            // 
            this.button_SelectExcludePID.Location = new System.Drawing.Point(458, 65);
            this.button_SelectExcludePID.Name = "button_SelectExcludePID";
            this.button_SelectExcludePID.Size = new System.Drawing.Size(30, 20);
            this.button_SelectExcludePID.TabIndex = 44;
            this.button_SelectExcludePID.Text = "...";
            this.button_SelectExcludePID.UseVisualStyleBackColor = true;
            this.button_SelectExcludePID.Click += new System.EventHandler(this.button_SelectExcludePID_Click);
            // 
            // textBox_ExcludePID
            // 
            this.textBox_ExcludePID.Location = new System.Drawing.Point(151, 65);
            this.textBox_ExcludePID.Name = "textBox_ExcludePID";
            this.textBox_ExcludePID.ReadOnly = true;
            this.textBox_ExcludePID.Size = new System.Drawing.Size(287, 20);
            this.textBox_ExcludePID.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Excluded process IDs";
            // 
            // button_SelectIncludePID
            // 
            this.button_SelectIncludePID.Location = new System.Drawing.Point(458, 23);
            this.button_SelectIncludePID.Name = "button_SelectIncludePID";
            this.button_SelectIncludePID.Size = new System.Drawing.Size(30, 20);
            this.button_SelectIncludePID.TabIndex = 41;
            this.button_SelectIncludePID.Text = "...";
            this.button_SelectIncludePID.UseVisualStyleBackColor = true;
            this.button_SelectIncludePID.Click += new System.EventHandler(this.button_SelectIncludePID_Click);
            // 
            // textBox_IncludePID
            // 
            this.textBox_IncludePID.Location = new System.Drawing.Point(151, 27);
            this.textBox_IncludePID.Name = "textBox_IncludePID";
            this.textBox_IncludePID.ReadOnly = true;
            this.textBox_IncludePID.Size = new System.Drawing.Size(287, 20);
            this.textBox_IncludePID.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Included process IDs";
            // 
            // tabPage_Help
            // 
            this.tabPage_Help.Controls.Add(this.linkLabel_SDK);
            this.tabPage_Help.Controls.Add(this.linkLabel_Report);
            this.tabPage_Help.Controls.Add(this.label_VersionInfo);
            this.tabPage_Help.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Help.Name = "tabPage_Help";
            this.tabPage_Help.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Help.Size = new System.Drawing.Size(548, 255);
            this.tabPage_Help.TabIndex = 4;
            this.tabPage_Help.Text = "Help&Support";
            this.tabPage_Help.UseVisualStyleBackColor = true;
            // 
            // linkLabel_SDK
            // 
            this.linkLabel_SDK.AutoSize = true;
            this.linkLabel_SDK.Location = new System.Drawing.Point(24, 82);
            this.linkLabel_SDK.Name = "linkLabel_SDK";
            this.linkLabel_SDK.Size = new System.Drawing.Size(290, 13);
            this.linkLabel_SDK.TabIndex = 10;
            this.linkLabel_SDK.TabStop = true;
            this.linkLabel_SDK.Text = "Online Transparent Encryption SDK and Example Download";
            this.linkLabel_SDK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SDK_LinkClicked);
            // 
            // linkLabel_Report
            // 
            this.linkLabel_Report.AutoSize = true;
            this.linkLabel_Report.Location = new System.Drawing.Point(24, 43);
            this.linkLabel_Report.Name = "linkLabel_Report";
            this.linkLabel_Report.Size = new System.Drawing.Size(173, 13);
            this.linkLabel_Report.TabIndex = 9;
            this.linkLabel_Report.TabStop = true;
            this.linkLabel_Report.Text = "Report a bug or make a suggestion";
            this.linkLabel_Report.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Report_LinkClicked);
            // 
            // label_VersionInfo
            // 
            this.label_VersionInfo.AutoSize = true;
            this.label_VersionInfo.Location = new System.Drawing.Point(24, 26);
            this.label_VersionInfo.Name = "label_VersionInfo";
            this.label_VersionInfo.Size = new System.Drawing.Size(0, 13);
            this.label_VersionInfo.TabIndex = 9;
            // 
            // tabPage_DecryptFile
            // 
            this.tabPage_DecryptFile.Controls.Add(this.groupBox3);
            this.tabPage_DecryptFile.Controls.Add(this.button_StartToDecrypt);
            this.tabPage_DecryptFile.Location = new System.Drawing.Point(4, 22);
            this.tabPage_DecryptFile.Name = "tabPage_DecryptFile";
            this.tabPage_DecryptFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_DecryptFile.Size = new System.Drawing.Size(548, 255);
            this.tabPage_DecryptFile.TabIndex = 2;
            this.tabPage_DecryptFile.Text = "Decrypt File";
            this.tabPage_DecryptFile.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_BrowseFileToDecrypt);
            this.groupBox3.Controls.Add(this.checkBox_ShowPasswordToDecrypt);
            this.groupBox3.Controls.Add(this.textBox_FileNameToDecrypt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox_PasswordToDecrypt);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(17, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(471, 142);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // button_BrowseFileToDecrypt
            // 
            this.button_BrowseFileToDecrypt.Location = new System.Drawing.Point(411, 83);
            this.button_BrowseFileToDecrypt.Name = "button_BrowseFileToDecrypt";
            this.button_BrowseFileToDecrypt.Size = new System.Drawing.Size(44, 23);
            this.button_BrowseFileToDecrypt.TabIndex = 6;
            this.button_BrowseFileToDecrypt.Text = "...";
            this.button_BrowseFileToDecrypt.UseVisualStyleBackColor = true;
            this.button_BrowseFileToDecrypt.Click += new System.EventHandler(this.button_BrowseFileToDecrypt_Click);
            // 
            // checkBox_ShowPasswordToDecrypt
            // 
            this.checkBox_ShowPasswordToDecrypt.AutoSize = true;
            this.checkBox_ShowPasswordToDecrypt.Location = new System.Drawing.Point(347, 26);
            this.checkBox_ShowPasswordToDecrypt.Name = "checkBox_ShowPasswordToDecrypt";
            this.checkBox_ShowPasswordToDecrypt.Size = new System.Drawing.Size(108, 17);
            this.checkBox_ShowPasswordToDecrypt.TabIndex = 5;
            this.checkBox_ShowPasswordToDecrypt.Text = "Display password";
            this.checkBox_ShowPasswordToDecrypt.UseVisualStyleBackColor = true;
            this.checkBox_ShowPasswordToDecrypt.CheckedChanged += new System.EventHandler(this.checkBox_ShowPasswordToDecrypt_CheckedChanged);
            // 
            // textBox_FileNameToDecrypt
            // 
            this.textBox_FileNameToDecrypt.Location = new System.Drawing.Point(117, 85);
            this.textBox_FileNameToDecrypt.Name = "textBox_FileNameToDecrypt";
            this.textBox_FileNameToDecrypt.Size = new System.Drawing.Size(288, 20);
            this.textBox_FileNameToDecrypt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name";
            // 
            // textBox_PasswordToDecrypt
            // 
            this.textBox_PasswordToDecrypt.Location = new System.Drawing.Point(117, 23);
            this.textBox_PasswordToDecrypt.Name = "textBox_PasswordToDecrypt";
            this.textBox_PasswordToDecrypt.Size = new System.Drawing.Size(211, 20);
            this.textBox_PasswordToDecrypt.TabIndex = 1;
            this.textBox_PasswordToDecrypt.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Master Password";
            // 
            // button_StartToDecrypt
            // 
            this.button_StartToDecrypt.Location = new System.Drawing.Point(413, 187);
            this.button_StartToDecrypt.Name = "button_StartToDecrypt";
            this.button_StartToDecrypt.Size = new System.Drawing.Size(75, 23);
            this.button_StartToDecrypt.TabIndex = 6;
            this.button_StartToDecrypt.Text = "Start";
            this.button_StartToDecrypt.UseVisualStyleBackColor = true;
            this.button_StartToDecrypt.Click += new System.EventHandler(this.button_StartToDecrypt_Click);
            // 
            // tabPage_EncryptFile
            // 
            this.tabPage_EncryptFile.Controls.Add(this.groupBox2);
            this.tabPage_EncryptFile.Controls.Add(this.button_StartToEncrypt);
            this.tabPage_EncryptFile.Location = new System.Drawing.Point(4, 22);
            this.tabPage_EncryptFile.Name = "tabPage_EncryptFile";
            this.tabPage_EncryptFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_EncryptFile.Size = new System.Drawing.Size(548, 255);
            this.tabPage_EncryptFile.TabIndex = 1;
            this.tabPage_EncryptFile.Text = "Encrypt File";
            this.tabPage_EncryptFile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_BrowseEncryptFile);
            this.groupBox2.Controls.Add(this.checkBox_ShowPasswordToEncrypt);
            this.groupBox2.Controls.Add(this.textBox_FileNameToEncrypt);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_PasswordToEncrypt);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(471, 142);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // button_BrowseEncryptFile
            // 
            this.button_BrowseEncryptFile.Location = new System.Drawing.Point(411, 83);
            this.button_BrowseEncryptFile.Name = "button_BrowseEncryptFile";
            this.button_BrowseEncryptFile.Size = new System.Drawing.Size(44, 23);
            this.button_BrowseEncryptFile.TabIndex = 6;
            this.button_BrowseEncryptFile.Text = "...";
            this.button_BrowseEncryptFile.UseVisualStyleBackColor = true;
            this.button_BrowseEncryptFile.Click += new System.EventHandler(this.button_BrowseEncryptFile_Click);
            // 
            // checkBox_ShowPasswordToEncrypt
            // 
            this.checkBox_ShowPasswordToEncrypt.AutoSize = true;
            this.checkBox_ShowPasswordToEncrypt.Location = new System.Drawing.Point(357, 26);
            this.checkBox_ShowPasswordToEncrypt.Name = "checkBox_ShowPasswordToEncrypt";
            this.checkBox_ShowPasswordToEncrypt.Size = new System.Drawing.Size(108, 17);
            this.checkBox_ShowPasswordToEncrypt.TabIndex = 5;
            this.checkBox_ShowPasswordToEncrypt.Text = "Display password";
            this.checkBox_ShowPasswordToEncrypt.UseVisualStyleBackColor = true;
            this.checkBox_ShowPasswordToEncrypt.CheckedChanged += new System.EventHandler(this.checkBox_ShowPasswordToEncrypt_CheckedChanged);
            // 
            // textBox_FileNameToEncrypt
            // 
            this.textBox_FileNameToEncrypt.Location = new System.Drawing.Point(117, 85);
            this.textBox_FileNameToEncrypt.Name = "textBox_FileNameToEncrypt";
            this.textBox_FileNameToEncrypt.Size = new System.Drawing.Size(288, 20);
            this.textBox_FileNameToEncrypt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "File name";
            // 
            // textBox_PasswordToEncrypt
            // 
            this.textBox_PasswordToEncrypt.Location = new System.Drawing.Point(117, 23);
            this.textBox_PasswordToEncrypt.Name = "textBox_PasswordToEncrypt";
            this.textBox_PasswordToEncrypt.Size = new System.Drawing.Size(227, 20);
            this.textBox_PasswordToEncrypt.TabIndex = 1;
            this.textBox_PasswordToEncrypt.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master Password";
            // 
            // button_StartToEncrypt
            // 
            this.button_StartToEncrypt.Location = new System.Drawing.Point(408, 189);
            this.button_StartToEncrypt.Name = "button_StartToEncrypt";
            this.button_StartToEncrypt.Size = new System.Drawing.Size(75, 23);
            this.button_StartToEncrypt.TabIndex = 4;
            this.button_StartToEncrypt.Text = "Start";
            this.button_StartToEncrypt.UseVisualStyleBackColor = true;
            this.button_StartToEncrypt.Click += new System.EventHandler(this.button_StartToEncrypt_Click);
            // 
            // tabPage_Folder
            // 
            this.tabPage_Folder.Controls.Add(this.splitContainer1);
            this.tabPage_Folder.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Folder.Name = "tabPage_Folder";
            this.tabPage_Folder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Folder.Size = new System.Drawing.Size(548, 255);
            this.tabPage_Folder.TabIndex = 0;
            this.tabPage_Folder.Text = "Auto Folder Encrption";
            this.tabPage_Folder.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView_Folders);
            this.splitContainer1.Size = new System.Drawing.Size(542, 249);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_RemoveFolder);
            this.groupBox1.Controls.Add(this.button_AddFolder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 249);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // button_RemoveFolder
            // 
            this.button_RemoveFolder.Location = new System.Drawing.Point(6, 127);
            this.button_RemoveFolder.Name = "button_RemoveFolder";
            this.button_RemoveFolder.Size = new System.Drawing.Size(94, 23);
            this.button_RemoveFolder.TabIndex = 1;
            this.button_RemoveFolder.Text = "Remove folder";
            this.button_RemoveFolder.UseVisualStyleBackColor = true;
            this.button_RemoveFolder.Click += new System.EventHandler(this.button_RemoveFolder_Click);
            // 
            // button_AddFolder
            // 
            this.button_AddFolder.Location = new System.Drawing.Point(6, 34);
            this.button_AddFolder.Name = "button_AddFolder";
            this.button_AddFolder.Size = new System.Drawing.Size(94, 23);
            this.button_AddFolder.TabIndex = 0;
            this.button_AddFolder.Text = "Add folder";
            this.button_AddFolder.UseVisualStyleBackColor = true;
            this.button_AddFolder.Click += new System.EventHandler(this.button_AddFolder_Click);
            // 
            // listView_Folders
            // 
            this.listView_Folders.AllowDrop = true;
            this.listView_Folders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Folders.FullRowSelect = true;
            this.listView_Folders.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView_Folders.Location = new System.Drawing.Point(0, 0);
            this.listView_Folders.Name = "listView_Folders";
            this.listView_Folders.Size = new System.Drawing.Size(424, 249);
            this.listView_Folders.SmallImageList = this.imageList1;
            this.listView_Folders.TabIndex = 0;
            this.listView_Folders.UseCompatibleStateImageBehavior = false;
            this.listView_Folders.View = System.Windows.Forms.View.List;
            this.listView_Folders.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_Folders_DragDrop);
            this.listView_Folders.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_Folders_DragEnter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Folder);
            this.tabControl1.Controls.Add(this.tabPage_EncryptFile);
            this.tabControl1.Controls.Add(this.tabPage_DecryptFile);
            this.tabControl1.Controls.Add(this.tabPage_Settings);
            this.tabControl1.Controls.Add(this.tabPage_Help);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 281);
            this.tabControl1.TabIndex = 0;
            // 
            // Form_FileCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 281);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_FileCrypt";
            this.Text = "Auto FileCrypt Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FileCrypt_FormClosed);
            this.tabPage_Settings.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage_Help.ResumeLayout(false);
            this.tabPage_Help.PerformLayout();
            this.tabPage_DecryptFile.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage_EncryptFile.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage_Folder.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.TabPage tabPage_Help;
        private System.Windows.Forms.LinkLabel linkLabel_SDK;
        private System.Windows.Forms.LinkLabel linkLabel_Report;
        private System.Windows.Forms.Label label_VersionInfo;
        private System.Windows.Forms.TabPage tabPage_DecryptFile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_BrowseFileToDecrypt;
        private System.Windows.Forms.CheckBox checkBox_ShowPasswordToDecrypt;
        private System.Windows.Forms.TextBox textBox_FileNameToDecrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PasswordToDecrypt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_StartToDecrypt;
        private System.Windows.Forms.TabPage tabPage_EncryptFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_BrowseEncryptFile;
        private System.Windows.Forms.CheckBox checkBox_ShowPasswordToEncrypt;
        private System.Windows.Forms.TextBox textBox_FileNameToEncrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_PasswordToEncrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_StartToEncrypt;
        private System.Windows.Forms.TabPage tabPage_Folder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_RemoveFolder;
        private System.Windows.Forms.Button button_AddFolder;
        private System.Windows.Forms.ListView listView_Folders;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_ApplySetting;
        private System.Windows.Forms.Button button_ChangePassword;
        private System.Windows.Forms.Button button_SelectExcludePID;
        private System.Windows.Forms.TextBox textBox_ExcludePID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_SelectIncludePID;
        private System.Windows.Forms.TextBox textBox_IncludePID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.TextBox textBox_ExcludedUsers;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_IncludedUsers;
        private System.Windows.Forms.Label label9;
    }
}

