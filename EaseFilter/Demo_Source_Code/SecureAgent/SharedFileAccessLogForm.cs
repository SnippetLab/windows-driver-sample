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
    public partial class SharedFileAccessLogForm : Form
    {
        public string accessLogStr = string.Empty;
        public bool initialized = false;

        public SharedFileAccessLogForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            InitListView();
        }

        public void InitListView()
        {
        
            listView_AccessLog.Clear();
            //create column header for ListView            
            listView_AccessLog.Columns.Add("Status", 80, System.Windows.Forms.HorizontalAlignment.Left);
            listView_AccessLog.Columns.Add("AccessTime",160, System.Windows.Forms.HorizontalAlignment.Left);            
            listView_AccessLog.Columns.Add("UserName",100, System.Windows.Forms.HorizontalAlignment.Left);
            listView_AccessLog.Columns.Add("ProcessName",100, System.Windows.Forms.HorizontalAlignment.Left);
            listView_AccessLog.Columns.Add("FileName",150, System.Windows.Forms.HorizontalAlignment.Left);
            listView_AccessLog.Columns.Add("Location", 200, System.Windows.Forms.HorizontalAlignment.Left);
            listView_AccessLog.Columns.Add("Description", 350, System.Windows.Forms.HorizontalAlignment.Left);

            try
            {
                if( accessLogStr.Length == 0 )
                {
                    return;
                }

                string accessLog = FilterAPI.AESEncryptDecryptStr(accessLogStr, FilterAPI.EncryptType.Decryption);
                StringReader sr = new StringReader(accessLog);

                while (true)
                {
                    string accessInfo = sr.ReadLine().Trim();
                    if (string.IsNullOrEmpty(accessInfo))
                    {
                        break;
                    }

                    try
                    {

                        string[] infos = accessInfo.Split(new char[] { '|' });

                        if (infos.Length < 7)
                        {
                            continue;
                        }

                        ListViewItem lvItem = new ListViewItem();
                        string[] listData = new string[listView_AccessLog.Columns.Count];

                        if (infos[0].Trim() == "1")
                        {
                            listData[0] = "Authorized";
                        }
                        else
                        {
                            listData[0] = "Denied";
                            listData[6] = infos[6].Trim();
                        }

                        long accessTimeL = 0;

                        if (long.TryParse(infos[1].Trim(), out accessTimeL))
                        {
                            DateTime accessTime = DateTime.FromFileTime(accessTimeL);
                            listData[1] = accessTime.ToString("F");
                        }

                        listData[2] = infos[2].Trim();
                        listData[3] = infos[3].Trim();
                        listData[4] = infos[4].Trim();
                        listData[5] = infos[5].Trim();

                        lvItem = new ListViewItem(listData, 0);

                        if (infos[0].Trim() == "0")
                        {
                            lvItem.BackColor = Color.LightGray;
                            lvItem.ForeColor = Color.Red;
                        }
                      

                        listView_AccessLog.Items.Add(lvItem);
                    }
                    catch
                    {
                    }
                }

            }
            catch
            {
            }
        }

        private void AccessLogForm_Shown(object sender, EventArgs e)
        {
            InitListView();
        }

 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string logStr = string.Empty;
            string lastError = string.Empty;

            //bool retVal = WebFormServices.GetAccessLog(AccountForm.accountName, AccountForm.password, ref logStr, ref lastError);

          //  if (retVal)
            {
                accessLogStr = logStr;
                InitListView();
            }
          
        }

        private void toolStripButton_ClearMessage_Click(object sender, EventArgs e)
        {
            string lastError = string.Empty;

            //bool retVal = WebFormServices.ClearAccessLog(AccountForm.accountName, AccountForm.password,ref lastError);

            //if (retVal)
            {
                accessLogStr = string.Empty;
                InitListView();
            }
        }

     
    }
}
