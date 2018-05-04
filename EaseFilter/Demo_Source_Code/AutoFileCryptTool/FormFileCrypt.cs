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
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using EaseFilter.CommonObjects;

namespace AutoFileCryptTool
{
    public partial class Form_FileCrypt : Form
    {
        bool isServiceRunning = true;

        public Form_FileCrypt()
        {
            GlobalConfig.filterType = FilterAPI.FilterType.FILE_SYSTEM_CONTROL_ENCRYPTION;

            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            string lastError = string.Empty;
            if (!FilterDriverService.StartFilterService(out lastError))
            {
                isServiceRunning = false;

                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Start encryption service failed with error:" + lastError + ", auto folder encryption service will stop.", "Auto FileCrypt Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         
            InitializeFileCrypt();

        }

        private void InitializeFileCrypt()
        {
            if (GlobalConfig.FilterRules.Count > 0)
            {
                //delete the predefined items for users.
                listView_Folders.Items.Clear();
            }

            foreach (string folder in GlobalConfig.FilterRules.Keys)
            {
                string folderName = folder;
                if (folderName.EndsWith("\\*"))
                {
                    folderName = folderName.Substring(0, folderName.Length - 2);
                }

                ListViewItem item = new ListViewItem(folderName);
                item.ImageIndex = 0;
                listView_Folders.Items.Add(item);
            }

            foreach (uint pid in GlobalConfig.IncludePidList)
            {
                if (textBox_IncludePID.Text.Length > 0)
                {
                    textBox_IncludePID.Text += ";";
                }

                textBox_IncludePID.Text += pid.ToString();
            }

            foreach (uint pid in GlobalConfig.ExcludePidList)
            {
                if (textBox_ExcludePID.Text.Length > 0)
                {
                    textBox_ExcludePID.Text += ";";
                }

                textBox_ExcludePID.Text += pid.ToString();
            }

            textBox_ExcludedUsers.Text = GlobalConfig.ExcludedUsers;
            textBox_IncludedUsers.Text = GlobalConfig.IncludedUsers;

            label_VersionInfo.Text = "Current version:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ",©  2016EaseFilter Inc. All rights reserved.";

            if (!isServiceRunning)
            {
                listView_Folders.Items.Clear();
                string warningMessage = "The encryption service failed to start.";
                string message2 = "Please run with administrator permission.";
                ListViewItem item = new ListViewItem(warningMessage);
                listView_Folders.Items.Add(item);
                item = new ListViewItem(message2);
                listView_Folders.Items.Add(item);
            }
               

        }

        private bool AddEncyrptFolder(string folderName)
        {
            if (GlobalConfig.FilterRules.Count == 0)
            {
                //delete the predefined items for users.
                listView_Folders.Items.Clear();
            }

            uint accessFlags = FilterAPI.ALLOW_MAX_RIGHT_ACCESS|(uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE;
            string includeFilterMask = folderName + "\\*";

            if (!GlobalConfig.IsFilterRuleExist(includeFilterMask))
            {
                FilterRule filterRule = new FilterRule();
                filterRule.IncludeFileFilterMask = includeFilterMask;
                filterRule.ExcludeFileFilterMasks = "";
                filterRule.EncryptionPassPhrase = GlobalConfig.MasterPassword;
                filterRule.IncludeProcessNames = "";
                filterRule.ExcludeProcessNames = "";
                filterRule.IncludeProcessIds = "";
                filterRule.ExcludeProcessIds = "";
                filterRule.HiddenFileFilterMasks = "";
                filterRule.AccessFlag = accessFlags;
                filterRule.EventType = 0;
                GlobalConfig.AddFilterRule(filterRule);

                GlobalConfig.SaveConfigSetting();

                ListViewItem item = new ListViewItem(folderName);
                item.ImageIndex = 0;
                listView_Folders.Items.Add(item);

                listView_Folders.EnsureVisible(listView_Folders.Items.Count - 1);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void button_AddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdDiaglog = new FolderBrowserDialog();
            if (fdDiaglog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderName = fdDiaglog.SelectedPath;
                AddEncyrptFolder(folderName);
            }
        }

        private void RemoveEncyrptFolder(string folderName)
        {
            GlobalConfig.RemoveFilterRule(folderName + "\\*");
            GlobalConfig.SaveConfigSetting();
        }

        private void button_RemoveFolder_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_Folders.SelectedItems)
            {
                string folderName = item.Text;
                RemoveEncyrptFolder(folderName);

                listView_Folders.Items.Remove(item);
            }
        }

        private void listView_Folders_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (null != fileList)
            {
                foreach (string folder in fileList)
                {
                    if (Directory.Exists(folder))
                    {
                        AddEncyrptFolder(folder);
                    }
                }
            }

        }

        private void listView_Folders_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void button_StartToEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = textBox_FileNameToEncrypt.Text;
                string passwordPhrase = textBox_PasswordToEncrypt.Text;
                byte[] key = Utils.GetKeyByPassPhrase(passwordPhrase);

                if (!FilterAPI.AESEncryptFile(fileName, (uint)key.Length, key, (uint)FilterAPI.DEFAULT_IV_TAG.Length, FilterAPI.DEFAULT_IV_TAG, false))
                {
                    string lastError = "Encrypt file " + fileName + " failed with error:" + FilterAPI.GetLastErrorMessage();
                    ShowMessage(lastError, MessageBoxIcon.Error);
                }
                else
                {
                    string lastError = "Encrypt file " + fileName + " succeeded.";
                    ShowMessage(lastError, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string lastError = "Encrypt file failed with error:" + ex.Message;
                ShowMessage(lastError, MessageBoxIcon.Error);
            }
        }

        private void button_BrowseEncryptFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDiag  = new OpenFileDialog();
            if (fileDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_FileNameToEncrypt.Text = fileDiag.FileName;
            }
        }

        private void button_BrowseFileToDecrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            if (fileDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_FileNameToDecrypt.Text = fileDiag.FileName;
            }
        }

        private void button_StartToDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = textBox_FileNameToDecrypt.Text;
                string passwordPhrase = textBox_PasswordToDecrypt.Text;
                byte[] key = Utils.GetKeyByPassPhrase(passwordPhrase);

                if (!FilterAPI.AESEncryptFile(fileName, (uint)key.Length, key, (uint)FilterAPI.DEFAULT_IV_TAG.Length, FilterAPI.DEFAULT_IV_TAG, false))
                {
                    string lastError = "Decrypt file " + fileName + " failed with error:" + FilterAPI.GetLastErrorMessage();
                    ShowMessage(lastError, MessageBoxIcon.Error);
                }
                else
                {
                    string lastError = "Decrypt file " + fileName + " succeeded.";
                    ShowMessage(lastError, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string lastError = "Decrypt file failed with error:" + ex.Message;
                ShowMessage(lastError, MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(string message,MessageBoxIcon messageIcon)
        {
            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            MessageBox.Show(message, "Message", MessageBoxButtons.OK, messageIcon);
        }

        private void checkBox_ShowPasswordToEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ShowPasswordToEncrypt.Checked)
            {
                textBox_PasswordToEncrypt.UseSystemPasswordChar = false;
            }
            else
            {
                textBox_PasswordToEncrypt.UseSystemPasswordChar = true;
            }
        }

        private void checkBox_ShowPasswordToDecrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ShowPasswordToDecrypt.Checked)
            {
                textBox_PasswordToDecrypt.UseSystemPasswordChar = false;
            }
            else
            {
                textBox_PasswordToDecrypt.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel_Report_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.easefilter.com/ReportIssue.htm");
        }

        private void linkLabel_SDK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://easefilter.com/Forums_Files/Transparent_Encryption_Filter_Driver.htm");
        }

        private void button_Activate_Click(object sender, EventArgs e)
        {
        }

        private void Form_FileCrypt_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            if (MessageBox.Show("Do you want to minimize to system tray?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

            }
            else
            {
                GlobalConfig.Stop();
                FilterAPI.StopFilter();

                Application.Exit();
            }
        }

        private void button_ChangePassword_Click(object sender, EventArgs e)
        {
            SetupPasswordForm setupForm = new SetupPasswordForm();
            setupForm.ShowDialog();
        }


        private void button_SelectIncludePID_Click(object sender, EventArgs e)
        {

            OptionForm optionForm = new OptionForm(OptionForm.OptionType.ProccessId, textBox_IncludePID.Text);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_IncludePID.Text = optionForm.ProcessId;
            }
        }

        private void button_SelectExcludePID_Click(object sender, EventArgs e)
        {

            OptionForm optionForm = new OptionForm(OptionForm.OptionType.ProccessId, textBox_ExcludePID.Text);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_ExcludePID.Text = optionForm.ProcessId;
            }
        }


        private void button_ApplySetting_Click(object sender, EventArgs e)
        {
            List<uint> inPids = new List<uint>();
            if (textBox_IncludePID.Text.Length > 0)
            {
                if (textBox_IncludePID.Text.EndsWith(";"))
                {
                    textBox_IncludePID.Text = textBox_IncludePID.Text.Remove(textBox_IncludePID.Text.Length - 1);
                }

                string[] pids = textBox_IncludePID.Text.Split(new char[] { ';' });
                for (int i = 0; i < pids.Length; i++)
                {
                    inPids.Add(uint.Parse(pids[i].Trim()));
                }
            }

            GlobalConfig.IncludePidList = inPids;

            List<uint> exPids = new List<uint>();
            if (textBox_ExcludePID.Text.Length > 0)
            {
                if (textBox_ExcludePID.Text.EndsWith(";"))
                {
                    textBox_ExcludePID.Text = textBox_ExcludePID.Text.Remove(textBox_ExcludePID.Text.Length - 1);
                }

                string[] pids = textBox_ExcludePID.Text.Split(new char[] { ';' });
                for (int i = 0; i < pids.Length; i++)
                {
                    exPids.Add(uint.Parse(pids[i].Trim()));
                }
            }

            GlobalConfig.ExcludePidList = exPids;

            GlobalConfig.IncludedUsers = textBox_IncludedUsers.Text;
            GlobalConfig.ExcludedUsers = textBox_ExcludedUsers.Text;

            GlobalConfig.SaveConfigSetting();

            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            MessageBox.Show("Your setting was saved.", "Apply settings", MessageBoxButtons.OK,  MessageBoxIcon.Information);

        }
       

    }
}
