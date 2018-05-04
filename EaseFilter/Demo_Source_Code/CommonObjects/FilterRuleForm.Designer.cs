namespace EaseFilter.CommonObjects
{
    partial class FilterRuleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterRuleForm));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_SaveFilterRule = new System.Windows.Forms.Button();
            this.textBox_ExcludeFilterMask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_IncludeFilterMask = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button_RegisterMonitorIO = new System.Windows.Forms.Button();
            this.textBox_MonitorIO = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_ExcludeUserNames = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_IncludeUserNames = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_ExcludeProcessNames = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_IncludeProcessNames = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_SelectedEvents = new System.Windows.Forms.Button();
            this.textBox_SelectedEvents = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_SelectExcludePID = new System.Windows.Forms.Button();
            this.textBox_ExcludePID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_SelectIncludePID = new System.Windows.Forms.Button();
            this.textBox_IncludePID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox_AccessControl = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox_UserRights = new System.Windows.Forms.TextBox();
            this.button_AddUserRights = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox_ProcessRights = new System.Windows.Forms.TextBox();
            this.button_AddProcessRights = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_AllowSetSecurity = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowExecution = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableProtectionInBootTime = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowChange = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowDelete = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowRename = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowRemoteAccess = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowNewFileCreation = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.button_RegisterControlIO = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_ControlIO = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_HiddenFilterMask = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label_PassPhrase = new System.Windows.Forms.Label();
            this.checkBox_DisplayPassword = new System.Windows.Forms.CheckBox();
            this.label_AccessFlags = new System.Windows.Forms.Label();
            this.textBox_FileAccessFlags = new System.Windows.Forms.TextBox();
            this.button_FileAccessFlags = new System.Windows.Forms.Button();
            this.textBox_PassPhrase = new System.Windows.Forms.TextBox();
            this.checkBox_Encryption = new System.Windows.Forms.CheckBox();
            this.textBox_ReparseFileFilterMask = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox_AccessControl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(211, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "( you can use wild card character \'*\', \'?\')";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(211, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "( split with \';\' for multiple items)";
            // 
            // button_SaveFilterRule
            // 
            this.button_SaveFilterRule.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_SaveFilterRule.Location = new System.Drawing.Point(453, 714);
            this.button_SaveFilterRule.Name = "button_SaveFilterRule";
            this.button_SaveFilterRule.Size = new System.Drawing.Size(75, 23);
            this.button_SaveFilterRule.TabIndex = 8;
            this.button_SaveFilterRule.Text = "Save";
            this.button_SaveFilterRule.UseVisualStyleBackColor = true;
            this.button_SaveFilterRule.Click += new System.EventHandler(this.button_SaveFilter_Click);
            // 
            // textBox_ExcludeFilterMask
            // 
            this.textBox_ExcludeFilterMask.Location = new System.Drawing.Point(213, 52);
            this.textBox_ExcludeFilterMask.Name = "textBox_ExcludeFilterMask";
            this.textBox_ExcludeFilterMask.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludeFilterMask.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Exclude file filter mask";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage file filter mask";
            // 
            // textBox_IncludeFilterMask
            // 
            this.textBox_IncludeFilterMask.Location = new System.Drawing.Point(213, 13);
            this.textBox_IncludeFilterMask.Name = "textBox_IncludeFilterMask";
            this.textBox_IncludeFilterMask.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeFilterMask.TabIndex = 0;
            this.textBox_IncludeFilterMask.Text = "c:\\test\\*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.button_RegisterMonitorIO);
            this.groupBox1.Controls.Add(this.textBox_MonitorIO);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox_ExcludeUserNames);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox_IncludeUserNames);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_ExcludeProcessNames);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_IncludeProcessNames);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button_SelectedEvents);
            this.groupBox1.Controls.Add(this.textBox_SelectedEvents);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.button_SelectExcludePID);
            this.groupBox1.Controls.Add(this.textBox_ExcludePID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button_SelectIncludePID);
            this.groupBox1.Controls.Add(this.textBox_IncludePID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox_AccessControl);
            this.groupBox1.Controls.Add(this.textBox_ExcludeFilterMask);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_IncludeFilterMask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 694);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(211, 186);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(222, 12);
            this.label20.TabIndex = 74;
            this.label20.Text = "( split with \';\' for multiple items, format \"notepad.exe\")";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(210, 367);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(233, 12);
            this.label19.TabIndex = 73;
            this.label19.Text = "(Receive notification when registered IO was triggered )";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(211, 321);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(237, 12);
            this.label18.TabIndex = 72;
            this.label18.Text = "(Receive file event notification after file handle is closed )";
            // 
            // button_RegisterMonitorIO
            // 
            this.button_RegisterMonitorIO.Location = new System.Drawing.Point(474, 345);
            this.button_RegisterMonitorIO.Name = "button_RegisterMonitorIO";
            this.button_RegisterMonitorIO.Size = new System.Drawing.Size(41, 20);
            this.button_RegisterMonitorIO.TabIndex = 71;
            this.button_RegisterMonitorIO.Text = "...";
            this.button_RegisterMonitorIO.UseVisualStyleBackColor = true;
            this.button_RegisterMonitorIO.Click += new System.EventHandler(this.button_RegisterMonitorIO_Click);
            // 
            // textBox_MonitorIO
            // 
            this.textBox_MonitorIO.Location = new System.Drawing.Point(212, 345);
            this.textBox_MonitorIO.Name = "textBox_MonitorIO";
            this.textBox_MonitorIO.ReadOnly = true;
            this.textBox_MonitorIO.Size = new System.Drawing.Size(242, 20);
            this.textBox_MonitorIO.TabIndex = 70;
            this.textBox_MonitorIO.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 345);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 13);
            this.label16.TabIndex = 69;
            this.label16.Text = "Register monitor IO";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(210, 253);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(245, 12);
            this.label13.TabIndex = 68;
            this.label13.Text = "(split with \';\' for multiple items, format \"domain\\username\" )";
            // 
            // textBox_ExcludeUserNames
            // 
            this.textBox_ExcludeUserNames.Location = new System.Drawing.Point(212, 268);
            this.textBox_ExcludeUserNames.Name = "textBox_ExcludeUserNames";
            this.textBox_ExcludeUserNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludeUserNames.TabIndex = 67;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 268);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 13);
            this.label14.TabIndex = 66;
            this.label14.Text = "Exclude user names";
            // 
            // textBox_IncludeUserNames
            // 
            this.textBox_IncludeUserNames.Location = new System.Drawing.Point(212, 230);
            this.textBox_IncludeUserNames.Name = "textBox_IncludeUserNames";
            this.textBox_IncludeUserNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeUserNames.TabIndex = 65;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 237);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 64;
            this.label15.Text = "Include user names";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(211, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(222, 12);
            this.label12.TabIndex = 57;
            this.label12.Text = "( split with \';\' for multiple items, format \"notepad.exe\")";
            // 
            // textBox_ExcludeProcessNames
            // 
            this.textBox_ExcludeProcessNames.Location = new System.Drawing.Point(213, 165);
            this.textBox_ExcludeProcessNames.Name = "textBox_ExcludeProcessNames";
            this.textBox_ExcludeProcessNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludeProcessNames.TabIndex = 56;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 13);
            this.label11.TabIndex = 55;
            this.label11.Text = "Exclude process names";
            // 
            // textBox_IncludeProcessNames
            // 
            this.textBox_IncludeProcessNames.Location = new System.Drawing.Point(213, 92);
            this.textBox_IncludeProcessNames.Name = "textBox_IncludeProcessNames";
            this.textBox_IncludeProcessNames.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludeProcessNames.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Include process names";
            // 
            // button_SelectedEvents
            // 
            this.button_SelectedEvents.Location = new System.Drawing.Point(474, 298);
            this.button_SelectedEvents.Name = "button_SelectedEvents";
            this.button_SelectedEvents.Size = new System.Drawing.Size(41, 20);
            this.button_SelectedEvents.TabIndex = 52;
            this.button_SelectedEvents.Text = "...";
            this.button_SelectedEvents.UseVisualStyleBackColor = true;
            this.button_SelectedEvents.Click += new System.EventHandler(this.button_SelectedEvents_Click);
            // 
            // textBox_SelectedEvents
            // 
            this.textBox_SelectedEvents.Location = new System.Drawing.Point(213, 298);
            this.textBox_SelectedEvents.Name = "textBox_SelectedEvents";
            this.textBox_SelectedEvents.ReadOnly = true;
            this.textBox_SelectedEvents.Size = new System.Drawing.Size(242, 20);
            this.textBox_SelectedEvents.TabIndex = 51;
            this.textBox_SelectedEvents.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "Register file events";
            // 
            // button_SelectExcludePID
            // 
            this.button_SelectExcludePID.Location = new System.Drawing.Point(474, 204);
            this.button_SelectExcludePID.Name = "button_SelectExcludePID";
            this.button_SelectExcludePID.Size = new System.Drawing.Size(41, 20);
            this.button_SelectExcludePID.TabIndex = 44;
            this.button_SelectExcludePID.Text = "...";
            this.button_SelectExcludePID.UseVisualStyleBackColor = true;
            this.button_SelectExcludePID.Click += new System.EventHandler(this.button_SelectExcludePID_Click);
            // 
            // textBox_ExcludePID
            // 
            this.textBox_ExcludePID.Location = new System.Drawing.Point(213, 202);
            this.textBox_ExcludePID.Name = "textBox_ExcludePID";
            this.textBox_ExcludePID.ReadOnly = true;
            this.textBox_ExcludePID.Size = new System.Drawing.Size(242, 20);
            this.textBox_ExcludePID.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Exclude process Ids";
            // 
            // button_SelectIncludePID
            // 
            this.button_SelectIncludePID.Location = new System.Drawing.Point(475, 134);
            this.button_SelectIncludePID.Name = "button_SelectIncludePID";
            this.button_SelectIncludePID.Size = new System.Drawing.Size(41, 20);
            this.button_SelectIncludePID.TabIndex = 41;
            this.button_SelectIncludePID.Text = "...";
            this.button_SelectIncludePID.UseVisualStyleBackColor = true;
            this.button_SelectIncludePID.Click += new System.EventHandler(this.button_SelectIncludePID_Click);
            // 
            // textBox_IncludePID
            // 
            this.textBox_IncludePID.Location = new System.Drawing.Point(213, 135);
            this.textBox_IncludePID.Name = "textBox_IncludePID";
            this.textBox_IncludePID.ReadOnly = true;
            this.textBox_IncludePID.Size = new System.Drawing.Size(242, 20);
            this.textBox_IncludePID.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Include process Ids";
            // 
            // groupBox_AccessControl
            // 
            this.groupBox_AccessControl.Controls.Add(this.textBox_ReparseFileFilterMask);
            this.groupBox_AccessControl.Controls.Add(this.label24);
            this.groupBox_AccessControl.Controls.Add(this.label23);
            this.groupBox_AccessControl.Controls.Add(this.textBox_UserRights);
            this.groupBox_AccessControl.Controls.Add(this.button_AddUserRights);
            this.groupBox_AccessControl.Controls.Add(this.label22);
            this.groupBox_AccessControl.Controls.Add(this.textBox_ProcessRights);
            this.groupBox_AccessControl.Controls.Add(this.button_AddProcessRights);
            this.groupBox_AccessControl.Controls.Add(this.groupBox2);
            this.groupBox_AccessControl.Controls.Add(this.label21);
            this.groupBox_AccessControl.Controls.Add(this.button_RegisterControlIO);
            this.groupBox_AccessControl.Controls.Add(this.label7);
            this.groupBox_AccessControl.Controls.Add(this.textBox_ControlIO);
            this.groupBox_AccessControl.Controls.Add(this.label17);
            this.groupBox_AccessControl.Controls.Add(this.textBox_HiddenFilterMask);
            this.groupBox_AccessControl.Controls.Add(this.label8);
            this.groupBox_AccessControl.Controls.Add(this.label_PassPhrase);
            this.groupBox_AccessControl.Controls.Add(this.checkBox_DisplayPassword);
            this.groupBox_AccessControl.Controls.Add(this.label_AccessFlags);
            this.groupBox_AccessControl.Controls.Add(this.textBox_FileAccessFlags);
            this.groupBox_AccessControl.Controls.Add(this.button_FileAccessFlags);
            this.groupBox_AccessControl.Controls.Add(this.textBox_PassPhrase);
            this.groupBox_AccessControl.Controls.Add(this.checkBox_Encryption);
            this.groupBox_AccessControl.Location = new System.Drawing.Point(18, 384);
            this.groupBox_AccessControl.Name = "groupBox_AccessControl";
            this.groupBox_AccessControl.Size = new System.Drawing.Size(517, 304);
            this.groupBox_AccessControl.TabIndex = 24;
            this.groupBox_AccessControl.TabStop = false;
            this.groupBox_AccessControl.Text = "Access Control Settings";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(-2, 66);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(62, 13);
            this.label23.TabIndex = 81;
            this.label23.Text = "Users rights";
            // 
            // textBox_UserRights
            // 
            this.textBox_UserRights.Location = new System.Drawing.Point(195, 62);
            this.textBox_UserRights.Name = "textBox_UserRights";
            this.textBox_UserRights.Size = new System.Drawing.Size(242, 20);
            this.textBox_UserRights.TabIndex = 80;
            // 
            // button_AddUserRights
            // 
            this.button_AddUserRights.Location = new System.Drawing.Point(457, 63);
            this.button_AddUserRights.Name = "button_AddUserRights";
            this.button_AddUserRights.Size = new System.Drawing.Size(41, 20);
            this.button_AddUserRights.TabIndex = 82;
            this.button_AddUserRights.Text = "Add";
            this.button_AddUserRights.UseVisualStyleBackColor = true;
            this.button_AddUserRights.Click += new System.EventHandler(this.button_AddUserRights_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(-2, 42);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(84, 13);
            this.label22.TabIndex = 78;
            this.label22.Text = "Processes rights";
            // 
            // textBox_ProcessRights
            // 
            this.textBox_ProcessRights.Location = new System.Drawing.Point(195, 38);
            this.textBox_ProcessRights.Name = "textBox_ProcessRights";
            this.textBox_ProcessRights.Size = new System.Drawing.Size(242, 20);
            this.textBox_ProcessRights.TabIndex = 77;
            // 
            // button_AddProcessRights
            // 
            this.button_AddProcessRights.Location = new System.Drawing.Point(457, 38);
            this.button_AddProcessRights.Name = "button_AddProcessRights";
            this.button_AddProcessRights.Size = new System.Drawing.Size(41, 20);
            this.button_AddProcessRights.TabIndex = 79;
            this.button_AddProcessRights.Text = "Add";
            this.button_AddProcessRights.UseVisualStyleBackColor = true;
            this.button_AddProcessRights.Click += new System.EventHandler(this.button_AddProcessRights_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_AllowSetSecurity);
            this.groupBox2.Controls.Add(this.checkBox_AllowExecution);
            this.groupBox2.Controls.Add(this.checkBox_EnableProtectionInBootTime);
            this.groupBox2.Controls.Add(this.checkBox_AllowChange);
            this.groupBox2.Controls.Add(this.checkBox_AllowDelete);
            this.groupBox2.Controls.Add(this.checkBox_AllowRename);
            this.groupBox2.Controls.Add(this.checkBox_AllowRemoteAccess);
            this.groupBox2.Controls.Add(this.checkBox_AllowNewFileCreation);
            this.groupBox2.Location = new System.Drawing.Point(2, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 70);
            this.groupBox2.TabIndex = 76;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick Access Control Flags Settings";
            // 
            // checkBox_AllowSetSecurity
            // 
            this.checkBox_AllowSetSecurity.AutoSize = true;
            this.checkBox_AllowSetSecurity.Location = new System.Drawing.Point(191, 49);
            this.checkBox_AllowSetSecurity.Name = "checkBox_AllowSetSecurity";
            this.checkBox_AllowSetSecurity.Size = new System.Drawing.Size(129, 17);
            this.checkBox_AllowSetSecurity.TabIndex = 25;
            this.checkBox_AllowSetSecurity.Text = "Allow security change";
            this.checkBox_AllowSetSecurity.UseVisualStyleBackColor = true;
            this.checkBox_AllowSetSecurity.CheckedChanged += new System.EventHandler(this.checkBox_AllowSetSecurity_CheckedChanged);
            // 
            // checkBox_AllowExecution
            // 
            this.checkBox_AllowExecution.AutoSize = true;
            this.checkBox_AllowExecution.Location = new System.Drawing.Point(7, 32);
            this.checkBox_AllowExecution.Name = "checkBox_AllowExecution";
            this.checkBox_AllowExecution.Size = new System.Drawing.Size(116, 17);
            this.checkBox_AllowExecution.TabIndex = 24;
            this.checkBox_AllowExecution.Text = "Allow file execution";
            this.checkBox_AllowExecution.UseVisualStyleBackColor = true;
            this.checkBox_AllowExecution.CheckedChanged += new System.EventHandler(this.checkBox_AllowExecution_CheckedChanged);
            // 
            // checkBox_EnableProtectionInBootTime
            // 
            this.checkBox_EnableProtectionInBootTime.AutoSize = true;
            this.checkBox_EnableProtectionInBootTime.Location = new System.Drawing.Point(345, 32);
            this.checkBox_EnableProtectionInBootTime.Name = "checkBox_EnableProtectionInBootTime";
            this.checkBox_EnableProtectionInBootTime.Size = new System.Drawing.Size(173, 17);
            this.checkBox_EnableProtectionInBootTime.TabIndex = 23;
            this.checkBox_EnableProtectionInBootTime.Text = "Enable Protection In Boot Time";
            this.checkBox_EnableProtectionInBootTime.UseVisualStyleBackColor = true;
            // 
            // checkBox_AllowChange
            // 
            this.checkBox_AllowChange.AutoSize = true;
            this.checkBox_AllowChange.Location = new System.Drawing.Point(191, 14);
            this.checkBox_AllowChange.Name = "checkBox_AllowChange";
            this.checkBox_AllowChange.Size = new System.Drawing.Size(114, 17);
            this.checkBox_AllowChange.TabIndex = 15;
            this.checkBox_AllowChange.Text = "Allow file changing";
            this.checkBox_AllowChange.UseVisualStyleBackColor = true;
            this.checkBox_AllowChange.CheckedChanged += new System.EventHandler(this.checkBox_AllowChange_CheckedChanged);
            // 
            // checkBox_AllowDelete
            // 
            this.checkBox_AllowDelete.AutoSize = true;
            this.checkBox_AllowDelete.Location = new System.Drawing.Point(7, 14);
            this.checkBox_AllowDelete.Name = "checkBox_AllowDelete";
            this.checkBox_AllowDelete.Size = new System.Drawing.Size(107, 17);
            this.checkBox_AllowDelete.TabIndex = 17;
            this.checkBox_AllowDelete.Text = "Allow file deletion";
            this.checkBox_AllowDelete.UseVisualStyleBackColor = true;
            this.checkBox_AllowDelete.CheckedChanged += new System.EventHandler(this.checkBox_AllowDelete_CheckedChanged);
            // 
            // checkBox_AllowRename
            // 
            this.checkBox_AllowRename.AutoSize = true;
            this.checkBox_AllowRename.Location = new System.Drawing.Point(346, 14);
            this.checkBox_AllowRename.Name = "checkBox_AllowRename";
            this.checkBox_AllowRename.Size = new System.Drawing.Size(113, 17);
            this.checkBox_AllowRename.TabIndex = 16;
            this.checkBox_AllowRename.Text = "Allow file renaming";
            this.checkBox_AllowRename.UseVisualStyleBackColor = true;
            this.checkBox_AllowRename.CheckedChanged += new System.EventHandler(this.checkBox_AllowRename_CheckedChanged);
            // 
            // checkBox_AllowRemoteAccess
            // 
            this.checkBox_AllowRemoteAccess.AutoSize = true;
            this.checkBox_AllowRemoteAccess.Location = new System.Drawing.Point(7, 49);
            this.checkBox_AllowRemoteAccess.Name = "checkBox_AllowRemoteAccess";
            this.checkBox_AllowRemoteAccess.Size = new System.Drawing.Size(182, 17);
            this.checkBox_AllowRemoteAccess.TabIndex = 21;
            this.checkBox_AllowRemoteAccess.Text = "Allow file accessing from network";
            this.checkBox_AllowRemoteAccess.UseVisualStyleBackColor = true;
            this.checkBox_AllowRemoteAccess.CheckedChanged += new System.EventHandler(this.checkBox_AllowRemoteAccess_CheckedChanged);
            // 
            // checkBox_AllowNewFileCreation
            // 
            this.checkBox_AllowNewFileCreation.AutoSize = true;
            this.checkBox_AllowNewFileCreation.Location = new System.Drawing.Point(191, 32);
            this.checkBox_AllowNewFileCreation.Name = "checkBox_AllowNewFileCreation";
            this.checkBox_AllowNewFileCreation.Size = new System.Drawing.Size(131, 17);
            this.checkBox_AllowNewFileCreation.TabIndex = 22;
            this.checkBox_AllowNewFileCreation.Text = "Allow new file creation";
            this.checkBox_AllowNewFileCreation.UseVisualStyleBackColor = true;
            this.checkBox_AllowNewFileCreation.CheckedChanged += new System.EventHandler(this.checkBox_AllowNewFileCreation_CheckedChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(193, 109);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(242, 12);
            this.label21.TabIndex = 75;
            this.label21.Text = "(Receive a callback when the registered IO was triggered)";
            // 
            // button_RegisterControlIO
            // 
            this.button_RegisterControlIO.Location = new System.Drawing.Point(456, 88);
            this.button_RegisterControlIO.Name = "button_RegisterControlIO";
            this.button_RegisterControlIO.Size = new System.Drawing.Size(41, 20);
            this.button_RegisterControlIO.TabIndex = 74;
            this.button_RegisterControlIO.Text = "...";
            this.button_RegisterControlIO.UseVisualStyleBackColor = true;
            this.button_RegisterControlIO.Click += new System.EventHandler(this.button_RegisterControlIO_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(194, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "( split with \';\' for multiple items)";
            // 
            // textBox_ControlIO
            // 
            this.textBox_ControlIO.Location = new System.Drawing.Point(194, 88);
            this.textBox_ControlIO.Name = "textBox_ControlIO";
            this.textBox_ControlIO.ReadOnly = true;
            this.textBox_ControlIO.Size = new System.Drawing.Size(242, 20);
            this.textBox_ControlIO.TabIndex = 73;
            this.textBox_ControlIO.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(-3, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 13);
            this.label17.TabIndex = 72;
            this.label17.Text = "Register control IO";
            // 
            // textBox_HiddenFilterMask
            // 
            this.textBox_HiddenFilterMask.Location = new System.Drawing.Point(195, 154);
            this.textBox_HiddenFilterMask.Name = "textBox_HiddenFilterMask";
            this.textBox_HiddenFilterMask.Size = new System.Drawing.Size(242, 20);
            this.textBox_HiddenFilterMask.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-1, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Hidden file filter mask";
            // 
            // label_PassPhrase
            // 
            this.label_PassPhrase.AutoSize = true;
            this.label_PassPhrase.Location = new System.Drawing.Point(-1, 184);
            this.label_PassPhrase.Name = "label_PassPhrase";
            this.label_PassPhrase.Size = new System.Drawing.Size(88, 13);
            this.label_PassPhrase.TabIndex = 20;
            this.label_PassPhrase.Text = "Password phrase";
            // 
            // checkBox_DisplayPassword
            // 
            this.checkBox_DisplayPassword.AutoSize = true;
            this.checkBox_DisplayPassword.Location = new System.Drawing.Point(347, 197);
            this.checkBox_DisplayPassword.Name = "checkBox_DisplayPassword";
            this.checkBox_DisplayPassword.Size = new System.Drawing.Size(108, 17);
            this.checkBox_DisplayPassword.TabIndex = 23;
            this.checkBox_DisplayPassword.Text = "Display password";
            this.checkBox_DisplayPassword.UseVisualStyleBackColor = true;
            this.checkBox_DisplayPassword.CheckedChanged += new System.EventHandler(this.checkBox_DisplayPassword_CheckedChanged);
            // 
            // label_AccessFlags
            // 
            this.label_AccessFlags.AutoSize = true;
            this.label_AccessFlags.Location = new System.Drawing.Point(-2, 19);
            this.label_AccessFlags.Name = "label_AccessFlags";
            this.label_AccessFlags.Size = new System.Drawing.Size(146, 13);
            this.label_AccessFlags.TabIndex = 12;
            this.label_AccessFlags.Text = "Filter rule access control flags";
            // 
            // textBox_FileAccessFlags
            // 
            this.textBox_FileAccessFlags.Location = new System.Drawing.Point(195, 12);
            this.textBox_FileAccessFlags.Name = "textBox_FileAccessFlags";
            this.textBox_FileAccessFlags.Size = new System.Drawing.Size(242, 20);
            this.textBox_FileAccessFlags.TabIndex = 11;
            this.textBox_FileAccessFlags.Text = "0";
            // 
            // button_FileAccessFlags
            // 
            this.button_FileAccessFlags.Location = new System.Drawing.Point(457, 12);
            this.button_FileAccessFlags.Name = "button_FileAccessFlags";
            this.button_FileAccessFlags.Size = new System.Drawing.Size(41, 20);
            this.button_FileAccessFlags.TabIndex = 14;
            this.button_FileAccessFlags.Text = "...";
            this.button_FileAccessFlags.UseVisualStyleBackColor = true;
            this.button_FileAccessFlags.Click += new System.EventHandler(this.button_FileAccessFlags_Click);
            // 
            // textBox_PassPhrase
            // 
            this.textBox_PassPhrase.Location = new System.Drawing.Point(195, 193);
            this.textBox_PassPhrase.Name = "textBox_PassPhrase";
            this.textBox_PassPhrase.ReadOnly = true;
            this.textBox_PassPhrase.Size = new System.Drawing.Size(128, 20);
            this.textBox_PassPhrase.TabIndex = 19;
            this.textBox_PassPhrase.UseSystemPasswordChar = true;
            // 
            // checkBox_Encryption
            // 
            this.checkBox_Encryption.AutoSize = true;
            this.checkBox_Encryption.Location = new System.Drawing.Point(348, 181);
            this.checkBox_Encryption.Name = "checkBox_Encryption";
            this.checkBox_Encryption.Size = new System.Drawing.Size(111, 17);
            this.checkBox_Encryption.TabIndex = 18;
            this.checkBox_Encryption.Text = "Enable encryption";
            this.checkBox_Encryption.UseVisualStyleBackColor = true;
            this.checkBox_Encryption.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_CheckedChanged);
            // 
            // textBox_ReparseFileFilterMask
            // 
            this.textBox_ReparseFileFilterMask.Location = new System.Drawing.Point(193, 128);
            this.textBox_ReparseFileFilterMask.Name = "textBox_ReparseFileFilterMask";
            this.textBox_ReparseFileFilterMask.Size = new System.Drawing.Size(242, 20);
            this.textBox_ReparseFileFilterMask.TabIndex = 83;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(-3, 128);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(113, 13);
            this.label24.TabIndex = 84;
            this.label24.Text = "Reparse file filter mask";
            // 
            // FilterRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(596, 749);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_SaveFilterRule);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterRuleForm";
            this.Text = "Filter rule settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_AccessControl.ResumeLayout(false);
            this.groupBox_AccessControl.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ExcludeFilterMask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_IncludeFilterMask;
        private System.Windows.Forms.Button button_SaveFilterRule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_FileAccessFlags;
        private System.Windows.Forms.Label label_AccessFlags;
        private System.Windows.Forms.Button button_FileAccessFlags;
        private System.Windows.Forms.TextBox textBox_PassPhrase;
        private System.Windows.Forms.Label label_PassPhrase;
        private System.Windows.Forms.CheckBox checkBox_Encryption;
        private System.Windows.Forms.CheckBox checkBox_AllowDelete;
        private System.Windows.Forms.CheckBox checkBox_AllowRename;
        private System.Windows.Forms.CheckBox checkBox_AllowChange;
        private System.Windows.Forms.CheckBox checkBox_AllowRemoteAccess;
        private System.Windows.Forms.CheckBox checkBox_AllowNewFileCreation;
        private System.Windows.Forms.CheckBox checkBox_DisplayPassword;
        private System.Windows.Forms.GroupBox groupBox_AccessControl;
        private System.Windows.Forms.Button button_SelectExcludePID;
        private System.Windows.Forms.TextBox textBox_ExcludePID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_SelectIncludePID;
        private System.Windows.Forms.TextBox textBox_IncludePID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_HiddenFilterMask;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_ExcludeProcessNames;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_IncludeProcessNames;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_ExcludeUserNames;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_IncludeUserNames;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_SelectedEvents;
        private System.Windows.Forms.TextBox textBox_SelectedEvents;
        private System.Windows.Forms.Button button_RegisterMonitorIO;
        private System.Windows.Forms.TextBox textBox_MonitorIO;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_RegisterControlIO;
        private System.Windows.Forms.TextBox textBox_ControlIO;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_EnableProtectionInBootTime;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox_UserRights;
        private System.Windows.Forms.Button button_AddUserRights;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox_ProcessRights;
        private System.Windows.Forms.Button button_AddProcessRights;
        private System.Windows.Forms.CheckBox checkBox_AllowExecution;
        private System.Windows.Forms.CheckBox checkBox_AllowSetSecurity;
        private System.Windows.Forms.TextBox textBox_ReparseFileFilterMask;
        private System.Windows.Forms.Label label24;
    }
}