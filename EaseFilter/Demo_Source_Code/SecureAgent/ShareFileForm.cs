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
using System.Collections;
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
    public partial class ShareFileForm : Form
    {
      
        public DRPolicy selectedDRPolicy = new DRPolicy();
        public bool isNewFileAddedToServer = false;
        bool isNewFile = false;

        public ShareFileForm(DRPolicy _selectedDRPolicy)
        {
            InitializeComponent();               

            this.selectedDRPolicy = _selectedDRPolicy;
            InitializeDRSetting();

        }

        public ShareFileForm()
        {
            isNewFile = true;

            InitializeComponent();
            InitializeDRSetting();            
        }

        private void InitializeDRSetting()
        {
            textBox_FileName.Text = string.Empty; ;
            textBox_OutputFilePath.Text = selectedDRPolicy.FileName;

            if (!isNewFile)
            {
                try
                {
                    textBox_FileName.Enabled = false;
                    textBox_ExcludeProcessNames.Text = selectedDRPolicy.ExcludeProcessNames;
                    dateTimePicker_ExpireDate.Value = DateTime.FromFileTime(selectedDRPolicy.ExpireTime);
                    dateTimePicker_ExpireTime.Value = DateTime.FromFileTime(selectedDRPolicy.ExpireTime);
                    textBox_IncludeProcessNames.Text = selectedDRPolicy.IncludeProcessNames;
                    textBox_IncludeUserNames.Text = selectedDRPolicy.IncludeUserNames;
                    textBox_ExcludeUserNames.Text = selectedDRPolicy.ExcludeUserNames;
                    textBox_IncludeComputerIds.Text = selectedDRPolicy.IncludeComputerIds;
                    textBox_UserPassword.Text = selectedDRPolicy.UserPassword;
                    checkBox_RevokeControl.Checked = true;
                    checkBox_RevokeControl.Enabled = false;
                }
                catch
                {
                }
            }
            else
            {
                textBox_FileName.Enabled = true;
                checkBox_RevokeControl.Enabled = true;

                if (AccountForm.isGuest)
                {
                    checkBox_RevokeControl.Enabled = false;
                    checkBox_RevokeControl.Checked = false;
                }
                else
                {
                    checkBox_RevokeControl.Checked = true;
                }

                dateTimePicker_ExpireDate.Value = DateTime.Now.AddDays(1);
                textBox_ExcludeProcessNames.Text = "explorer.exe;";
            }            


        }

        private DRPolicy GetDRSetting()
        {
            DRPolicy drPolicy = new DRPolicy();

            try
            {
                drPolicy.IncludeProcessNames = textBox_IncludeProcessNames.Text.Trim().ToLower();
                drPolicy.ExcludeProcessNames = textBox_ExcludeProcessNames.Text.Trim().ToLower();
                drPolicy.IncludeUserNames = textBox_IncludeUserNames.Text.Trim().ToLower();
                drPolicy.ExcludeUserNames = textBox_ExcludeUserNames.Text.Trim().ToLower();
                drPolicy.IncludeComputerIds = textBox_IncludeComputerIds.Text.Trim();
                DateTime expireDate = dateTimePicker_ExpireDate.Value.Date + dateTimePicker_ExpireTime.Value.TimeOfDay;
                drPolicy.ExpireTime = expireDate.ToUniversalTime().ToFileTime();
                drPolicy.FileName = Path.GetFileName(textBox_OutputFilePath.Text);
                drPolicy.UserPassword = textBox_UserPassword.Text.Trim();
            }
            catch (Exception ex)
            {
               throw new Exception("Apply digital right failed with error:" + ex.Message);
            }

            return drPolicy;
        }

        private bool AddNewFileDRInfoToServer(ref string iv, ref string key, ref long creationTime)
        {
            bool retVal = false;
            string lastError = string.Empty;

            try
            {

                iv = string.Empty;
                key = string.Empty;
                creationTime = 0;

                if (AccountForm.accountName.Length == 0 || AccountForm.password.Length == 0)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Can't enable revoke access feature for guest user!", "Create share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return retVal;
                }

                selectedDRPolicy = GetDRSetting();

                string encryptedDRPolicy = DigitalRightControl.EncryptObjectToStr<DRPolicy>(selectedDRPolicy);

                //retVal = WebFormServices.AddNewFile(AccountForm.accountName, AccountForm.password, encryptedDRPolicy, ref creationTime, ref key, ref iv, ref lastError);
                //if (!retVal)
                //{
                //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                //    MessageBox.Show("Create share encrypted file failed with error:" + lastError, "Process share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return retVal;
                //}
                //else
                {
                    selectedDRPolicy.CreationTime = creationTime;
                    isNewFileAddedToServer = true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Get encryption key info failed with error:" + ex.Message, "GetEncryptionKeyAndIVFromServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retVal;
        }

        private DRPolicyData GetDRPolicyData()
        {
            DRPolicyData policyData = new DRPolicyData();

            policyData.AESVerificationKey = FilterAPI.AES_TAG_KEY;

            policyData.AccessFlags = FilterAPI.ALLOW_MAX_RIGHT_ACCESS;


            if (textBox_IncludeProcessNames.Text.Trim().Length > 0 || textBox_ExcludeProcessNames.Text.Trim().Length > 0)
            {
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_ProcessName;
            }

            if (textBox_IncludeUserNames.Text.Trim().Length > 0 || textBox_ExcludeUserNames.Text.Trim().Length > 0)
            {
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_UserName;
            }

            if (textBox_IncludeComputerIds.Text.Trim().Length > 0)
            {
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_Computer_Id;
            }

            if (textBox_UserPassword.Text.Trim().Length > 0)
            {
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_User_Permit;
            }

            DateTime expireDateTime = dateTimePicker_ExpireDate.Value.Date + dateTimePicker_ExpireTime.Value.TimeOfDay;
            policyData.ExpireTime = expireDateTime.ToFileTime();

            if (checkBox_RevokeControl.Checked )
            {
                //get encryption key and iv from server.
                policyData.AESFlags |= AESFlags.Flags_Enabled_Revoke_Access_Control;
            }
            else
            {
                policyData.IncludeProcessNames = textBox_IncludeProcessNames.Text.Trim();
                policyData.LengthOfIncludeProcessNames = (uint)textBox_IncludeProcessNames.Text.Length * 2;
                policyData.ExcludeProcessNames = textBox_ExcludeProcessNames.Text.Trim();
                policyData.LengthOfExcludeProcessNames = (uint)textBox_ExcludeProcessNames.Text.Length * 2;
                policyData.IncludeUserNames = textBox_IncludeUserNames.Text.Trim();
                policyData.LengthOfIncludeUserNames = (uint)textBox_IncludeUserNames.Text.Length * 2;
                policyData.ExcludeUserNames = textBox_ExcludeUserNames.Text.Trim();
                policyData.LengthOfExcludeUserNames = (uint)textBox_ExcludeUserNames.Text.Length * 2;
                policyData.ComputerIds = textBox_IncludeComputerIds.Text.Trim();
                policyData.LengthOfComputerIds = (uint)policyData.ComputerIds.Length * 2;                
                policyData.CreationTime = DateTime.Now.ToFileTime();
                policyData.UserPassword = textBox_UserPassword.Text.Trim();
                policyData.LengthOfUserPassword = (uint)policyData.UserPassword.Length * 2;

                //notify the filter driver to check AccessFlags for permission, if the file was expired and get encryption key here.
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_AccessFlags | AESFlags.Flags_Enabled_Expire_Time | AESFlags.Flags_AES_Key_Was_Embedded;
            }

            if (textBox_UserPassword.Text.Trim().Length > 0)
            {                
                policyData.AESFlags |= AESFlags.Flags_Enabled_Check_User_Password;
            }

            policyData.AccountName = AccountForm.accountName;
            policyData.LengthOfAccountName = (uint)policyData.AccountName.Length * 2;

            return policyData;

        }


        private bool CreateShareEncryptFile()
        {
            string lastError = string.Empty;
            string key = string.Empty;
            string iv = string.Empty;
            long creationTime = DateTime.Now.ToFileTime();

            try
            {
                if (textBox_FileName.Text.Trim().Length == 0)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The file name can't be empty.", "Create share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DateTime expireDateTime = dateTimePicker_ExpireDate.Value.Date + dateTimePicker_ExpireTime.Value.TimeOfDay;
                if (expireDateTime <= DateTime.Now)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The expire time can't be less than current time.", "Create share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DRPolicyData policyData = GetDRPolicyData();

                if (((uint)policyData.AESFlags & (uint)AESFlags.Flags_Enabled_Revoke_Access_Control) == (uint)AESFlags.Flags_Enabled_Revoke_Access_Control)
                {
                    if (!AddNewFileDRInfoToServer(ref iv, ref key, ref creationTime))
                    {
                        return false;
                    }

                    policyData.CreationTime = creationTime;
                }

                byte[] encryptIV = null;
                byte[] encryptKey = null;

                if (iv.Length > 0 && key.Length > 0)
                {
                    encryptIV = Utils.ConvertHexStrToByteArray(iv);
                    encryptKey = Utils.ConvertHexStrToByteArray(key);
                }
                else
                {
                    encryptIV = Utils.GetRandomIV();
                    encryptKey = Utils.GetRandomKey();

                    policyData.IV = encryptIV;
                    policyData.IVLength = (uint)encryptIV.Length;
                    policyData.EncryptionKey = encryptKey;
                    policyData.EncryptionKeyLength = (uint)encryptKey.Length;
                }


                if (!DigitalRightControl.EncryptFileWithEmbeddedDRPolicy(textBox_FileName.Text, textBox_OutputFilePath.Text, encryptIV, encryptKey, policyData, out lastError))
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Create share encrypted file " + textBox_FileName.Text + " failed with error:" + lastError, "Process share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                else
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Create encrypted file " + textBox_OutputFilePath.Text + " succeeded.", "Process share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedDRPolicy.CreationTime = creationTime;
                    selectedDRPolicy.FileName = Path.GetFileName(textBox_OutputFilePath.Text);

                    isNewFileAddedToServer = true;

                    return true;

                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Create share file failed with error " + ex.Message, "Create share encrypted file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }


        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_FileName.Text = openFileDialog.FileName;
                textBox_OutputFilePath.Text = openFileDialog.FileName + DigitalRightControl.SECURE_SHARE_FILE_EXTENSION;
            }
        }

        private void button_GetComputerId_Click(object sender, EventArgs e)
        {
            bool retVal = false;
            string myComputerId = string.Empty;
            string lastError = string.Empty;

            retVal = FilterAPI.GetUniqueComputerId(ref myComputerId, ref lastError);

            if (retVal)
            {
                textBox_IncludeComputerIds.Text = myComputerId;
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("This is your local computer unique id:\r\n\r\n" + myComputerId, "Get computer info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show(lastError, "Get computer info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;
        }

        private void textBox_FileName_TextChanged(object sender, EventArgs e)
        {
            textBox_OutputFilePath.Text = textBox_FileName.Text + DigitalRightControl.SECURE_SHARE_FILE_EXTENSION;
            checkBox_RevokeControl.Enabled = true;
        }    

        private void button_Apply_Click(object sender, EventArgs e)
        {
            button_Apply.Enabled = false;

            try
            {
                string lastError = string.Empty;

                if (isNewFile)
                {
                    if (CreateShareEncryptFile())
                    {
                        this.Close();
                    }
                }
                else
                {
                    DRPolicy newDRPolicy = GetDRSetting();
                    newDRPolicy.CreationTime = selectedDRPolicy.CreationTime;
                    string encryptedDRPolicy = string.Empty;

                    encryptedDRPolicy = DigitalRightControl.EncryptObjectToStr<DRPolicy>(newDRPolicy);

                    //if (!WebFormServices.SetFileDRInfo(AccountForm.accountName, AccountForm.password, encryptedDRPolicy, ref lastError))
                    //{
                    //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    //    MessageBox.Show("Apply digital right information for file " + selectedDRPolicy.FileName + " failed with error " + lastError, "Apply", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    {
                        MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                        MessageBox.Show("Apply digital right information for file " + selectedDRPolicy.FileName + " succeeded.", "Apply", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();

                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Apply digital right information for file " + selectedDRPolicy.FileName + " failed with error " + ex.Message, "Apply", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_Apply.Enabled = true;
            }

        }

        private void checkBox_RevokeControk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AccountForm.accountName) || string.IsNullOrEmpty(AccountForm.password))
            {
                //this is a guest user.
                if (checkBox_RevokeControl.Checked)
                {
                    checkBox_RevokeControl.Checked = false;

                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The guest user can't enable the revoke access control feature.", "revoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
