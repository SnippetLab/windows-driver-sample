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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

using EaseFilter.CommonObjects;

namespace FileProtector
{
    public partial class ProtectorForm : Form
    {
        //Purchase a license key with the link: http://www.easefilter.com/Order.htm
        //Email us to request a trial key: info@easefilter.com //free email is not accepted.
        string registerKey = GlobalConfig.registerKey;

        FilterMessage filterMessage = null;

        public ProtectorForm()
        {
            GlobalConfig.filterType = FilterAPI.FilterType.FILE_SYSTEM_EASE_FILTER_ALL;

            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            filterMessage = new FilterMessage(listView_Info);

            DisplayVersion();

         
        }

        ~ProtectorForm()
        {
            GlobalConfig.Stop();
        }

        private void DisplayVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                string filterDllPath = Path.Combine(GlobalConfig.AssemblyPath, "FilterAPI.Dll");
                version = FileVersionInfo.GetVersionInfo(filterDllPath).ProductVersion;
            }
            catch( Exception ex)
            {
                EventManager.WriteMessage(43, "LoadFilterAPI Dll", EventLevel.Error, "FilterAPI.dll can't be found." + ex.Message);
            }

            this.Text += "    Version:  " + version;
        }


        private void toolStripButton_StartFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string lastError = string.Empty;

                bool ret = FilterAPI.StartFilter( (int)GlobalConfig.FilterConnectionThreads
                                            , registerKey
                                            , new FilterAPI.FilterDelegate(FilterCallback)
                                            , new FilterAPI.DisconnectDelegate(DisconnectCallback)
                                            , ref lastError);
                if (!ret)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Start filter failed." + lastError);
                    return;
                }

                toolStripButton_StartFilter.Enabled = false;
                toolStripButton_Stop.Enabled = true;


                if (GlobalConfig.FilterRules.Count == 0 && null != sender )
                {
                    FilterRule filterRule = new FilterRule();
                    filterRule.IncludeFileFilterMask = "c:\\test\\*";
                    filterRule.ExcludeFileFilterMasks = "c:\\windows*";
                    filterRule.EventType = (uint)(FilterAPI.EVENTTYPE.WRITTEN | FilterAPI.EVENTTYPE.CREATED | FilterAPI.EVENTTYPE.DELETED | FilterAPI.EVENTTYPE.RENAMED); 
                    filterRule.AccessFlag = (uint)FilterAPI.ALLOW_MAX_RIGHT_ACCESS;
                    GlobalConfig.FilterRules.Add(filterRule.IncludeFileFilterMask, filterRule);

                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("You don't have any monitor folder setup, add c:\\test\\* as your default test folder, I/Os from c:\\test\\* will show up in the console.");
                }

               

                GlobalConfig.SendConfigSettingsToFilter();

                EventManager.WriteMessage(102, "StartFilter", EventLevel.Information, "Start filter service succeeded.");
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(104, "StartFilter", EventLevel.Error, "Start filter service failed with error " + ex.Message);
            }

        }

        private void toolStripButton_Stop_Click(object sender, EventArgs e)
        {

            FilterAPI.StopFilter();

            toolStripButton_StartFilter.Enabled = true;
            toolStripButton_Stop.Enabled = false;
        }

        private void toolStripButton_ClearMessage_Click(object sender, EventArgs e)
        {
            filterMessage.InitListView();
        }

        Boolean FilterCallback(IntPtr sendDataPtr, IntPtr replyDataPtr)
        {
            Boolean ret = true;

            try
            {
                FilterAPI.MessageSendData messageSend = (FilterAPI.MessageSendData)Marshal.PtrToStructure(sendDataPtr, typeof(FilterAPI.MessageSendData));

                if (FilterAPI.MESSAGE_SEND_VERIFICATION_NUMBER != messageSend.VerificationNumber)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Received message corrupted.Please check if the MessageSendData structure is correct.");

                    EventManager.WriteMessage(139, "FilterCallback", EventLevel.Error, "Received message corrupted.Please check if the MessageSendData structure is correct.");
                    return false;
                }

                EventManager.WriteMessage(149, "FilterCallback", EventLevel.Verbose, "Received message Id#" + messageSend.MessageId + " type:" + messageSend.MessageType
                    + " CreateOptions:" + messageSend.CreateOptions.ToString("X") + " infoClass:" + messageSend.InfoClass + " fileName:" + messageSend.FileName );

                filterMessage.AddMessage(messageSend);

                FileProtectorUnitTest.FileIOEventHandler(messageSend);

                if (replyDataPtr.ToInt64() != 0)
                {
                    FilterAPI.MessageReplyData messageReply = (FilterAPI.MessageReplyData)Marshal.PtrToStructure(replyDataPtr, typeof(FilterAPI.MessageReplyData));

                    //here you can control the IO behaviour and modify the data.
                    if (!FileProtectorUnitTest.UnitTestCallbackHandler(messageSend) || !FilterService.IOAccessControl(messageSend, ref messageReply) )
                    {
                        //to comple the PRE_IO
                        messageReply.ReturnStatus = (uint)FilterAPI.NTSTATUS.STATUS_ACCESS_DENIED;
                        messageReply.FilterStatus = (uint)FilterAPI.FilterStatus.FILTER_COMPLETE_PRE_OPERATION;

                        EventManager.WriteMessage(160, "FilterCallback", EventLevel.Error, "Return error for I/O request:" + ((FilterAPI.MessageType)messageSend.MessageType).ToString() + 
                            ",fileName:" + messageSend.FileName );
                    }
                    else
                    {

                        messageReply.MessageId = messageSend.MessageId;
                        messageReply.MessageType = messageSend.MessageType;
                        messageReply.ReturnStatus = (uint)FilterAPI.NTSTATUS.STATUS_SUCCESS;

                    }

                    Marshal.StructureToPtr(messageReply, replyDataPtr, true);

                }

                string info = "FileProtector process request " + FilterMessage.FormatIOName(messageSend) + ",pid:" + messageSend.ProcessId +
                            " ,filename:" + messageSend.FileName + ",return status:" + FilterMessage.FormatStatus(messageSend.Status);

                if (messageSend.Status == (uint)NtStatus.Status.Success)
                {
                    ret = false;
                    EventManager.WriteMessage(98, "FilterCallback", EventLevel.Verbose, info);
                }
                else
                {
                    ret = true;
                    EventManager.WriteMessage(98, "FilterCallback", EventLevel.Error, info);
                }

                return ret;
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(134, "FilterCallback", EventLevel.Error, "filter callback exception." + ex.Message);
                return false;
            }

        }

        void DisconnectCallback()
        {
            EventManager.WriteMessage(190, "DisconnectCallback", EventLevel.Information, "Filter Disconnected." + FilterAPI.GetLastErrorMessage());
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingForm = new SettingsForm();
            settingForm.StartPosition = FormStartPosition.CenterParent;
            settingForm.ShowDialog();
        }

        private void reportAProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.easefilter.com/ReportIssue.htm");
        }

        private void helpTopicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.easefilter.com/Forums_Files/FileProtector.htm");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            EventForm.DisplayEventForm();
        }

        private void encryptFileWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EncryptedFileForm encryptForm = new EncryptedFileForm("Encrypt file", FilterAPI.EncryptType.Encryption);
            encryptForm.ShowDialog();
        }

        private void decryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EncryptedFileForm encryptForm = new EncryptedFileForm("Decrypt file", FilterAPI.EncryptType.Decryption);
            encryptForm.ShowDialog();
        }

        private void getEncryptedFileIVTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm("Input file name", "Plase input file name", "");

            if (inputForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = inputForm.InputText;
                byte[] iv = null;
                string lastError = string.Empty;

                if (FilterAPI.GetIVTag(fileName, ref iv, out lastError))
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Get encrypted file " + fileName + " iv tag:" + Utils.ByteArrayToHexStr(iv), "IV Tag", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Get encrypted file " + fileName + " iv tag failed with error " + lastError, "IV Tag", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

  
        private void ProtectorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            if (MessageBox.Show("Do you want to minimize to system tray?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

            }
            else
            {
                GlobalConfig.Stop();
                Application.Exit();
            }
        }

        private void unInstallFilterDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterAPI.StopFilter();
            FilterAPI.UnInstallDriver();
        }

     

        private void protectorTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TutorialForm tutorialForm = new TutorialForm();
            tutorialForm.ShowDialog();
        }

        private void toolStripButton_LoadMessage_Click(object sender, EventArgs e)
        {
            filterMessage.LoadMessageFromLogToConsole();
        }

        private void toolStripButton_UnitTest_Click(object sender, EventArgs e)
        {
            toolStripButton_StartFilter_Click(null, null);
           // System.Threading.Tasks.Task.Factory.StartNew(() => { ProtectorUnitTest(); });
            ProtectorUnitTest();

        }

        private void ProtectorUnitTest()
        {
            FileProtectorUnitTest fileProtectorUnitTest = new FileProtectorUnitTest();
            fileProtectorUnitTest.ShowDialog();
        }

        private void activateLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

    }
}
