namespace EaseFilter.CommonObjects
{
    partial class Form_AccessRights
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
            this.button_Add = new System.Windows.Forms.Button();
            this.groupBox_ProcessName = new System.Windows.Forms.GroupBox();
            this.textBox_ProcessName = new System.Windows.Forms.TextBox();
            this.label_AccessFlags = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_SetSecurity = new System.Windows.Forms.CheckBox();
            this.checkBox_QueryInfo = new System.Windows.Forms.CheckBox();
            this.checkBox_Read = new System.Windows.Forms.CheckBox();
            this.checkBox_QuerySecurity = new System.Windows.Forms.CheckBox();
            this.checkBox_SetInfo = new System.Windows.Forms.CheckBox();
            this.checkBox_Write = new System.Windows.Forms.CheckBox();
            this.checkBox_Delete = new System.Windows.Forms.CheckBox();
            this.checkBox_Rename = new System.Windows.Forms.CheckBox();
            this.checkBox_Creation = new System.Windows.Forms.CheckBox();
            this.groupBox_UserName = new System.Windows.Forms.GroupBox();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_Execution = new System.Windows.Forms.CheckBox();
            this.groupBox_ProcessName.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_UserName.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Add.Location = new System.Drawing.Point(728, 486);
            this.button_Add.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(112, 35);
            this.button_Add.TabIndex = 25;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // groupBox_ProcessName
            // 
            this.groupBox_ProcessName.Controls.Add(this.textBox_ProcessName);
            this.groupBox_ProcessName.Controls.Add(this.label_AccessFlags);
            this.groupBox_ProcessName.Location = new System.Drawing.Point(64, 30);
            this.groupBox_ProcessName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_ProcessName.Name = "groupBox_ProcessName";
            this.groupBox_ProcessName.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_ProcessName.Size = new System.Drawing.Size(776, 74);
            this.groupBox_ProcessName.TabIndex = 26;
            this.groupBox_ProcessName.TabStop = false;
            // 
            // textBox_ProcessName
            // 
            this.textBox_ProcessName.Location = new System.Drawing.Point(224, 24);
            this.textBox_ProcessName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_ProcessName.Name = "textBox_ProcessName";
            this.textBox_ProcessName.Size = new System.Drawing.Size(445, 26);
            this.textBox_ProcessName.TabIndex = 27;
            this.textBox_ProcessName.Text = "notepad.exe;   cmd.exe;";
            // 
            // label_AccessFlags
            // 
            this.label_AccessFlags.AutoSize = true;
            this.label_AccessFlags.Location = new System.Drawing.Point(13, 30);
            this.label_AccessFlags.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_AccessFlags.Name = "label_AccessFlags";
            this.label_AccessFlags.Size = new System.Drawing.Size(116, 20);
            this.label_AccessFlags.TabIndex = 28;
            this.label_AccessFlags.Text = "Process Name ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_Execution);
            this.groupBox2.Controls.Add(this.checkBox_SetSecurity);
            this.groupBox2.Controls.Add(this.checkBox_QueryInfo);
            this.groupBox2.Controls.Add(this.checkBox_Read);
            this.groupBox2.Controls.Add(this.checkBox_QuerySecurity);
            this.groupBox2.Controls.Add(this.checkBox_SetInfo);
            this.groupBox2.Controls.Add(this.checkBox_Write);
            this.groupBox2.Controls.Add(this.checkBox_Delete);
            this.groupBox2.Controls.Add(this.checkBox_Rename);
            this.groupBox2.Controls.Add(this.checkBox_Creation);
            this.groupBox2.Location = new System.Drawing.Point(64, 207);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(776, 246);
            this.groupBox2.TabIndex = 76;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acess Rights";
            // 
            // checkBox_SetSecurity
            // 
            this.checkBox_SetSecurity.AutoSize = true;
            this.checkBox_SetSecurity.Checked = true;
            this.checkBox_SetSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SetSecurity.Location = new System.Drawing.Point(534, 100);
            this.checkBox_SetSecurity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_SetSecurity.Name = "checkBox_SetSecurity";
            this.checkBox_SetSecurity.Size = new System.Drawing.Size(216, 24);
            this.checkBox_SetSecurity.TabIndex = 29;
            this.checkBox_SetSecurity.Text = "Allow file security changing";
            this.checkBox_SetSecurity.UseVisualStyleBackColor = true;
            // 
            // checkBox_QueryInfo
            // 
            this.checkBox_QueryInfo.AutoSize = true;
            this.checkBox_QueryInfo.Checked = true;
            this.checkBox_QueryInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_QueryInfo.Location = new System.Drawing.Point(258, 67);
            this.checkBox_QueryInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_QueryInfo.Name = "checkBox_QueryInfo";
            this.checkBox_QueryInfo.Size = new System.Drawing.Size(236, 24);
            this.checkBox_QueryInfo.TabIndex = 24;
            this.checkBox_QueryInfo.Text = "Allow file information querying";
            this.checkBox_QueryInfo.UseVisualStyleBackColor = true;
            // 
            // checkBox_Read
            // 
            this.checkBox_Read.AutoSize = true;
            this.checkBox_Read.Checked = true;
            this.checkBox_Read.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Read.Location = new System.Drawing.Point(16, 67);
            this.checkBox_Read.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Read.Name = "checkBox_Read";
            this.checkBox_Read.Size = new System.Drawing.Size(146, 24);
            this.checkBox_Read.TabIndex = 26;
            this.checkBox_Read.Text = "Allow file reading";
            this.checkBox_Read.UseVisualStyleBackColor = true;
            // 
            // checkBox_QuerySecurity
            // 
            this.checkBox_QuerySecurity.AutoSize = true;
            this.checkBox_QuerySecurity.Checked = true;
            this.checkBox_QuerySecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_QuerySecurity.Location = new System.Drawing.Point(536, 67);
            this.checkBox_QuerySecurity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_QuerySecurity.Name = "checkBox_QuerySecurity";
            this.checkBox_QuerySecurity.Size = new System.Drawing.Size(211, 24);
            this.checkBox_QuerySecurity.TabIndex = 25;
            this.checkBox_QuerySecurity.Text = "Allow file security querying";
            this.checkBox_QuerySecurity.UseVisualStyleBackColor = true;
            // 
            // checkBox_SetInfo
            // 
            this.checkBox_SetInfo.AutoSize = true;
            this.checkBox_SetInfo.Checked = true;
            this.checkBox_SetInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SetInfo.Location = new System.Drawing.Point(258, 100);
            this.checkBox_SetInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_SetInfo.Name = "checkBox_SetInfo";
            this.checkBox_SetInfo.Size = new System.Drawing.Size(241, 24);
            this.checkBox_SetInfo.TabIndex = 28;
            this.checkBox_SetInfo.Text = "Allow file information changing";
            this.checkBox_SetInfo.UseVisualStyleBackColor = true;
            // 
            // checkBox_Write
            // 
            this.checkBox_Write.AutoSize = true;
            this.checkBox_Write.Checked = true;
            this.checkBox_Write.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Write.Location = new System.Drawing.Point(17, 101);
            this.checkBox_Write.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Write.Name = "checkBox_Write";
            this.checkBox_Write.Size = new System.Drawing.Size(138, 24);
            this.checkBox_Write.TabIndex = 15;
            this.checkBox_Write.Text = "Allow file writing";
            this.checkBox_Write.UseVisualStyleBackColor = true;
            // 
            // checkBox_Delete
            // 
            this.checkBox_Delete.AutoSize = true;
            this.checkBox_Delete.Checked = true;
            this.checkBox_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Delete.Location = new System.Drawing.Point(534, 135);
            this.checkBox_Delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Delete.Name = "checkBox_Delete";
            this.checkBox_Delete.Size = new System.Drawing.Size(149, 24);
            this.checkBox_Delete.TabIndex = 17;
            this.checkBox_Delete.Text = "Allow file deletion";
            this.checkBox_Delete.UseVisualStyleBackColor = true;
            // 
            // checkBox_Rename
            // 
            this.checkBox_Rename.AutoSize = true;
            this.checkBox_Rename.Checked = true;
            this.checkBox_Rename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Rename.Location = new System.Drawing.Point(258, 135);
            this.checkBox_Rename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Rename.Name = "checkBox_Rename";
            this.checkBox_Rename.Size = new System.Drawing.Size(159, 24);
            this.checkBox_Rename.TabIndex = 16;
            this.checkBox_Rename.Text = "Allow file renaming";
            this.checkBox_Rename.UseVisualStyleBackColor = true;
            // 
            // checkBox_Creation
            // 
            this.checkBox_Creation.AutoSize = true;
            this.checkBox_Creation.Checked = true;
            this.checkBox_Creation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Creation.Location = new System.Drawing.Point(17, 135);
            this.checkBox_Creation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Creation.Name = "checkBox_Creation";
            this.checkBox_Creation.Size = new System.Drawing.Size(183, 24);
            this.checkBox_Creation.TabIndex = 22;
            this.checkBox_Creation.Text = "Allow new file creation";
            this.checkBox_Creation.UseVisualStyleBackColor = true;
            // 
            // groupBox_UserName
            // 
            this.groupBox_UserName.Controls.Add(this.textBox_UserName);
            this.groupBox_UserName.Controls.Add(this.label1);
            this.groupBox_UserName.Location = new System.Drawing.Point(64, 105);
            this.groupBox_UserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_UserName.Name = "groupBox_UserName";
            this.groupBox_UserName.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_UserName.Size = new System.Drawing.Size(776, 71);
            this.groupBox_UserName.TabIndex = 30;
            this.groupBox_UserName.TabStop = false;
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Location = new System.Drawing.Point(224, 24);
            this.textBox_UserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(445, 26);
            this.textBox_UserName.TabIndex = 27;
            this.textBox_UserName.Text = "domain1\\user1;  domain2\\user2;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "User Name ";
            // 
            // checkBox_Execution
            // 
            this.checkBox_Execution.AutoSize = true;
            this.checkBox_Execution.Checked = true;
            this.checkBox_Execution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Execution.Location = new System.Drawing.Point(16, 169);
            this.checkBox_Execution.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Execution.Name = "checkBox_Execution";
            this.checkBox_Execution.Size = new System.Drawing.Size(161, 24);
            this.checkBox_Execution.TabIndex = 30;
            this.checkBox_Execution.Text = "Allow file execution";
            this.checkBox_Execution.UseVisualStyleBackColor = true;
            // 
            // Form_AccessRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 598);
            this.Controls.Add(this.groupBox_UserName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.groupBox_ProcessName);
            this.Name = "Form_AccessRights";
            this.Text = "Add Access Rights";
            this.groupBox_ProcessName.ResumeLayout(false);
            this.groupBox_ProcessName.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_UserName.ResumeLayout(false);
            this.groupBox_UserName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.GroupBox groupBox_ProcessName;
        private System.Windows.Forms.TextBox textBox_ProcessName;
        private System.Windows.Forms.Label label_AccessFlags;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_Write;
        private System.Windows.Forms.CheckBox checkBox_Delete;
        private System.Windows.Forms.CheckBox checkBox_Rename;
        private System.Windows.Forms.CheckBox checkBox_Creation;
        private System.Windows.Forms.GroupBox groupBox_UserName;
        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_SetSecurity;
        private System.Windows.Forms.CheckBox checkBox_QueryInfo;
        private System.Windows.Forms.CheckBox checkBox_Read;
        private System.Windows.Forms.CheckBox checkBox_QuerySecurity;
        private System.Windows.Forms.CheckBox checkBox_SetInfo;
        private System.Windows.Forms.CheckBox checkBox_Execution;
    }
}