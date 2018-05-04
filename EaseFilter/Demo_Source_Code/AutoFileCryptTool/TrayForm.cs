///////////////////////////////////////////////////////////////////////////////
//
//    (C) Copyright 2012 EaseFilter Technologies Inc.
//    All Rights Reserved
//
//    This software is part of a licensed software product and may
//    only be used or copied in accordance with the terms of that license.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using EaseFilter.CommonObjects;

namespace AutoFileCryptTool
{
    public partial class TrayForm: Form
    {
        Form_FileCrypt fileCryptForm = null;

        public TrayForm()
        {
              
            InitializeComponent();
            Utils.CopyOSPlatformDependentFiles();

            if (!VerifyPassword())
            {
                return;
            }

            this.Hide();

            fileCryptForm = new Form_FileCrypt();
            
        }

        private void TrayForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.notifyIcon.Visible = true;
            fileCryptForm.ShowDialog();
        }

        private bool VerifyPassword()
        {
            if (string.IsNullOrEmpty(GlobalConfig.MasterPassword))
            {
                SetupPasswordForm passForm = new SetupPasswordForm();
                passForm.ShowDialog();

                return passForm.isPasswordMatched;
            }
            else
            {
                VerifyPasswordForm verifyForm = new VerifyPasswordForm();
                verifyForm.ShowDialog();

                return verifyForm.isPasswordMatched;
            }
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyPassword())
            {
                return;
            }

            if (!fileCryptForm.Visible)
            {
                fileCryptForm.StartPosition = FormStartPosition.CenterScreen;
                fileCryptForm.ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingForm = new SettingsForm();
            settingForm.StartPosition = FormStartPosition.CenterScreen;
            settingForm.ShowDialog();
        }


        private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.easefilter.com/ReportIssue.htm");
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GlobalConfig.Stop();
            fileCryptForm.Close();

            Application.Exit();
        }

        private void sdkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.easefilter.com/info/easefilter_manual.pdf");
        }

     
        private void openProtectorSourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            string AssemblyPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "Demo");
            System.Diagnostics.Process.Start("explorer.exe", AssemblyPath);
        }

        private void toolStripMenuItemEncryptInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://easefilter.com/Forums_Files/Transparent_Encryption_Filter_Driver.htm");
        }

  
    
    }
}
