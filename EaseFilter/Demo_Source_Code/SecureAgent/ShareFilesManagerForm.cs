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
using System.Windows.Forms;
using System.IO;

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public partial class ShareFilesManagerForm : Form
    {
        DRPolicy selectedDRPolicy = new DRPolicy();
        public Dictionary<string, DRPolicy> sharedFileList = new Dictionary<string, DRPolicy>();

        public ShareFilesManagerForm()
        {
            InitializeComponent();
            InitListView();
        }

        public void InitListView()
        {
            listView_SharedFiles.Clear();
            //create column header for ListView
            listView_SharedFiles.Columns.Add("fileName", 250, System.Windows.Forms.HorizontalAlignment.Left);
            listView_SharedFiles.Columns.Add("CreationTime", 300, System.Windows.Forms.HorizontalAlignment.Left);

            try
            {
                foreach (KeyValuePair<string, DRPolicy> entry in sharedFileList)
                {
                    ListViewItem lvItem = new ListViewItem();
                    string[] listData = new string[listView_SharedFiles.Columns.Count];
                    listData[0] = entry.Value.FileName.Replace(DigitalRightControl.SECURE_SHARE_FILE_EXTENSION, "");;

                    long creationTimeN = long.Parse(entry.Key);
                    DateTime creationTime = DateTime.FromFileTime(creationTimeN).ToLocalTime();
                    listData[1] = String.Format("{0:F}", creationTime);

                    lvItem = new ListViewItem(listData, 0);
                    lvItem.Tag = entry.Key;

                    listView_SharedFiles.Items.Add(lvItem);

                }
            }
            catch
            {
            }
        }

        private void button_CreateShareEncryptedFile_Click(object sender, EventArgs e)
        {
            ShareFileForm shareFileForm = new ShareFileForm();
            shareFileForm.ShowDialog();

            if (shareFileForm.isNewFileAddedToServer)
            {
                sharedFileList.Add(shareFileForm.selectedDRPolicy.CreationTime.ToString(), shareFileForm.selectedDRPolicy);
                InitListView();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            button_Delete.Enabled = false;

            try
            {
                if (listView_SharedFiles.SelectedItems.Count != 1)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Please select a file.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string creationTimeStr = (string)listView_SharedFiles.SelectedItems[0].Tag;

                if (sharedFileList.ContainsKey(creationTimeStr))
                {
                    DRPolicy drPolicy = sharedFileList[creationTimeStr];

                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    if (MessageBox.Show("Are you sure you want to delete the file " + drPolicy.FileName + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }

                    string fileName = drPolicy.FileName + "." + creationTimeStr;
                    string lastError = string.Empty;

                    //if (!WebFormServices.DeleteShareFile(AccountForm.accountName, AccountForm.password, fileName, ref lastError))
                    //{
                    //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    //    MessageBox.Show("Delete shared file " + selectedDRPolicy.FileName + " failed with error:" + lastError, "DeleteSharedFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    sharedFileList.Remove(creationTimeStr);
                }

                InitListView();

            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Delete shared file " + selectedDRPolicy.FileName + " failed with error " + ex.Message, "DeleteSharedFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_Delete.Enabled = true;
            }
        }


        private void button_EditSharedFile_Click(object sender, EventArgs e)
        {
            if (listView_SharedFiles.SelectedItems.Count != 1)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Please select a file.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string creationTimeStr = (string)listView_SharedFiles.SelectedItems[0].Tag;

            if (sharedFileList.ContainsKey(creationTimeStr))
            {
                DRPolicy drPolicy = sharedFileList[creationTimeStr];

                if (drPolicy.ExpireTime == 0)
                {
                    string fileName = drPolicy.FileName;
                    long creationTime = drPolicy.CreationTime;
                    string lastError = string.Empty;
                    string encryptedDRPolicy = string.Empty;

                    //bool retVal = WebFormServices.GetFileDRInfo(AccountForm.accountName, AccountForm.password, fileName, creationTime, ref encryptedDRPolicy, ref lastError);
                    //if (!retVal)
                    //{
                    //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    //    MessageBox.Show("Get digital right information for file " + fileName + " failed with error:" + lastError, "GetFileDRInfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    drPolicy = DigitalRightControl.DecryptStrToObject<DRPolicy>(encryptedDRPolicy);
                    drPolicy.CreationTime = creationTime;

                    sharedFileList[creationTimeStr] = drPolicy;

                }

                ShareFileForm shareFileForm = new ShareFileForm(drPolicy);
                shareFileForm.ShowDialog();

            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {

            button_Refresh.Enabled = false;

            try
            {
                if (AccountForm.accountName.Length == 0)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("There are no shared file list for guest user.", "RefreshFileList", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (GetSharedFileList())
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Shared file list was refreshed.", "RefreshFileList", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
            finally
            {
                button_Refresh.Enabled = true;
            }
        }

        public bool GetSharedFileList()
        {
            string lastError = string.Empty;
            string encryptFileList = string.Empty;
            Dictionary<string, DRPolicy> shareList = new Dictionary<string, DRPolicy>();

            //bool retVal = WebFormServices.GetFileList(AccountForm.accountName, AccountForm.password, ref encryptFileList, ref lastError);
            //if (!retVal)
            //{
            //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            //    MessageBox.Show(lastError, "GetFileList", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return false;
            //}
            //else
            {
                if (encryptFileList.Length > 0)
                {

                    List<string> decrypFileList = DigitalRightControl.DecryptStrToObject<List<string>>(encryptFileList);

                    foreach (string name in decrypFileList)
                    {
                        if (name.Length > 0)
                        {
                            //the extension of the file is the creation time.
                            string creationTimeStr = Path.GetExtension(name).Substring(1);
                            string fileName = Path.GetFileNameWithoutExtension(name);

                            DRPolicy drPolicy = new DRPolicy();
                            drPolicy.FileName = fileName;
                            drPolicy.CreationTime = long.Parse(creationTimeStr);
                            drPolicy.ExpireTime = 0;

                            shareList.Add(creationTimeStr, drPolicy);


                        }
                    }
                }


                sharedFileList = shareList;
                InitListView();
            }

            return true;
        }

        private void listView_SharedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
           
         

        }
    }
}
