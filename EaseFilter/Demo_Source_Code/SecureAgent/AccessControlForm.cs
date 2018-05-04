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
using System.Windows.Forms;
using System.Runtime.InteropServices;

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public partial class AccessControlForm : Form
    {
       
        FilterWorker filterWorker = null;

        public AccessControlForm()
        {
            GlobalConfig.filterType = FilterAPI.FilterType.FILE_SYSTEM_EASE_FILTER_ALL;

            InitializeComponent();

            filterWorker = new FilterWorker(listView_Info);

            DisplayInfo();
         
        }

      
        private void DisplayInfo()
        {
            bool retVal = false;
            string myComputerId = string.Empty;
            string lastError = string.Empty;

            retVal = FilterAPI.GetUniqueComputerId(ref myComputerId, ref lastError);
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (retVal)
            {
                this.Text += "         ComputerId:  " + myComputerId + "    Version:  " + version;
            }
        }


        private void toolStripButton_StartFilter_Click(object sender, EventArgs e)
        {
            StartFilterService();
        }

        public bool StartFilterService()
        {
            string lastError = string.Empty;
            if (filterWorker.StartService(ref lastError))
            {
                toolStripButton_StartFilter.Enabled = false;
                toolStripButton_Stop.Enabled = true;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void StopFilterService()
        {
            filterWorker.StopService();

            toolStripButton_StartFilter.Enabled = true;
            toolStripButton_Stop.Enabled = false;

        }

        private void toolStripButton_Stop_Click(object sender, EventArgs e)
        {
            StopFilterService();
        }

        private void toolStripButton_ClearMessage_Click(object sender, EventArgs e)
        {
            filterWorker.ClearMessage();
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

      
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void unInstallFilterDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterAPI.StopFilter();
            FilterAPI.UnInstallDriver();
        }

        private void createSecureShareFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShareFileForm shareFileForm = new ShareFileForm();

            if (!AccountForm.isAuthorized || AccountForm.isGuest)
            {
                AccountForm accountForm = new AccountForm();
                accountForm.ShowDialog();
            }

            shareFileForm.ShowDialog();
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.assurefiles.com/company.htm");
        }

        private void onlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.assurefiles.com/Forums_Files/secure_file_sharing.htm");
        }

        private void sDKManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.assurefiles.com/info/easefilter_manual.pdf");
        }

        private void toolStripButton_LoadMessage_Click(object sender, EventArgs e)
        {
            filterWorker.filterMessage.LoadMessageFromLogToConsole();
        }

    }
}
