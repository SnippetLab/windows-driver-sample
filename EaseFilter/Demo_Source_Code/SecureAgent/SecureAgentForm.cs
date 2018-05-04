///////////////////////////////////////////////////////////////////////////////
//
//    (C) Copyright 2011 EaseFilter Technologies Inc.
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
using System.IO;
using System.Windows.Forms;

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public partial class SecureAgentForm : Form
    {
        ShareFilesManagerForm shareFileManagerForm = new ShareFilesManagerForm();
        AccountForm accountForm = new AccountForm();
        SharedFileAccessLogForm sharedFileAccessLogForm = new SharedFileAccessLogForm();
        AccessControlForm accessControlForm = null;

        string accountName = string.Empty;
        string password = string.Empty;        

        public SecureAgentForm()
        {
            Utils.CopyOSPlatformDependentFiles();
            InitializeComponent();

            GlobalConfig.filterType = FilterAPI.FilterType.FILE_SYSTEM_EASE_FILTER_ALL;

            EventManager.ShowNotificationDlgt showNotificationDlgt = new EventManager.ShowNotificationDlgt(ShowNotification);
            EventManager.showNotificationDlgt = showNotificationDlgt;

            accessControlForm = new AccessControlForm();

        
        }

        private void SecureForm_Load(object sender, EventArgs e)
        {           
            this.Hide();
            this.notifyIcon.Visible = true;

            ShowNotification("SecureAgent started successfully,\r\nRight click the SecureAgent icon to open the menu context.", false);

        }

        private void SecureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalConfig.Stop();
            FilterAPI.StopFilter();
        }

      

        private void toolStripMenuItem_Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == toolStripMenuItem_Start.Tag || (bool)toolStripMenuItem_Start.Tag == false)
                {
                    string secureFolder = string.Empty;

                    if (GlobalConfig.FilterRules.Count > 0)
                    {
                        string includeFilterMask = GlobalConfig.FilterRules.First().Key;

                        if (includeFilterMask.EndsWith("\\*"))
                        {
                            secureFolder = includeFilterMask.Remove(includeFilterMask.IndexOf("\\*"));
                        }
                    }

                    ShareFileFolderInputForm secureFolderForm = new ShareFileFolderInputForm(secureFolder);

                    if (secureFolderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        secureFolder = secureFolderForm.dropFolder;
                    }

                    if (!Directory.Exists(secureFolder))
                    {
                        ShowNotification("The folder " + secureFolder + " doesn't exist.", true);
                        return;
                    }


                    FilterRule filterRule = new FilterRule();

                    filterRule.IncludeFileFilterMask = secureFolder + "\\*";
                    filterRule.AccessFlag |= (uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE | FilterAPI.ALLOW_MAX_RIGHT_ACCESS;
                    GlobalConfig.AddFilterRule(filterRule);

                    FilterRule filterRule2 = new FilterRule();
                    filterRule2.IncludeFileFilterMask = secureFolder + "\\*" + DigitalRightControl.SECURE_SHARE_FILE_EXTENSION;
                    filterRule2.AccessFlag = FilterAPI.ALLOW_MAX_RIGHT_ACCESS;
                    filterRule2.EventType = (uint)FilterAPI.EVENTTYPE.CREATED | (uint)FilterAPI.EVENTTYPE.RENAMED;
                    GlobalConfig.AddFilterRule(filterRule2);
                  
                    GlobalConfig.SaveConfigSetting();

                    if (!accessControlForm.StartFilterService())
                    {
                        return;
                    }
                    else
                    {
                        toolStripMenuItem_Start.Tag = true;
                        toolStripMenuItem_Start.Text = "Stop secure agent";
                    }
                }
                else
                {
                    FilterAPI.StopFilter();

                    toolStripMenuItem_Start.Tag = false;
                    toolStripMenuItem_Start.Text = "Start secure agent";

                }
            }
            catch (Exception ex)
            {
                FilterAPI.StopFilter();
                EventManager.WriteMessage(157, "start service", EventLevel.Error, "start service failed with error:" + ex.Message);
            }
        }

        private void toolStripMenuItem_ShareFileManager_Click(object sender, EventArgs e)
        {

            if (!AccountForm.isAuthorized || AccountForm.isGuest)
            {
                accountForm.ShowDialog();
            }

            if (!AccountForm.isGuest)
            {
                shareFileManagerForm.GetSharedFileList();
            }

            shareFileManagerForm.StartPosition = FormStartPosition.CenterScreen;
            shareFileManagerForm.ShowDialog();
        }

        private void toolStripMenuItem_AccessLogViewer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AccountForm.isAuthorized || AccountForm.isGuest)
                {
                    accountForm.ShowDialog();
                }

                if (!sharedFileAccessLogForm.initialized)
                {
                    string accessLogStr = string.Empty;

                    if (AccountForm.isGuest)
                    {
                        ShowNotification("The guest user can't access the log file.", true);
                        return;
                    }

                    string accountName = AccountForm.accountName;
                    string password = AccountForm.password;
                    string lastError = string.Empty;

                    sharedFileAccessLogForm.initialized = true;

                    //bool retVal = WebFormServices.GetAccessLog(accountName, password, ref accessLogStr, ref lastError);

                    //if (retVal)
                    //{
                    //    sharedFileAccessLogForm.accessLogStr = accessLogStr;
                    //}
                    //else
                    //{
                    //    lastError = "Get access log failed,system return error:" + lastError;
                    //    EventManager.WriteMessage(180, "AccessViewer", EventLevel.Error, lastError);
                    //    ShowNotification(lastError, true);
                    //    return;
                    //}
                }
                sharedFileAccessLogForm.StartPosition = FormStartPosition.CenterScreen;
                sharedFileAccessLogForm.ShowDialog();
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(190, "AccessViewer", EventLevel.Error, "Get access log failed,system return error:" + ex.Message);
                return;
            }

        }

        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            if (!accessControlForm.Visible)
            {
                accessControlForm.StartPosition = FormStartPosition.CenterScreen;
                accessControlForm.ShowDialog();
            }
        }

        private void toolStripMenuItem_Help_Click(object sender, EventArgs e)
        {
           System.Diagnostics.Process.Start("http://www.easefilter.com/Forums_Files/AssureFiles_Secure_File_Sharing.htm");

        }

        private void exitToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            GlobalConfig.Stop();
            FilterAPI.StopFilter();

            Application.Exit();
        }

        private void ShowNotification(string message, bool isErrorMessage)
        {
            if (!GlobalConfig.EnableNotification)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new EventManager.ShowNotificationDlgt(ShowNotification), new Object[] { message, isErrorMessage });
            }
            else
            {
                popupNotifier1.ShowInternalImage(isErrorMessage, !isErrorMessage);
                popupNotifier1.TitleText = "";
                popupNotifier1.ContentText = message;
                popupNotifier1.Popup();
            }

        }

        private void eventLogViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventForm.DisplayEventForm();
        }

    
       
    }
}
