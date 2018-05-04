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

namespace EaseFilter.CommonObjects
{
    public partial class FilterRuleForm : Form
    {
        public FilterRuleForm(FilterRule filterRule)
        {
            InitializeComponent();

            textBox_IncludeFilterMask.Text = filterRule.IncludeFileFilterMask;
            textBox_ExcludeFilterMask.Text = filterRule.ExcludeFileFilterMasks;
            textBox_FileAccessFlags.Text = filterRule.AccessFlag.ToString();
            textBox_PassPhrase.Text = filterRule.EncryptionPassPhrase;
            textBox_SelectedEvents.Text = filterRule.EventType.ToString();
            textBox_HiddenFilterMask.Text = filterRule.HiddenFileFilterMasks;
            textBox_ReparseFileFilterMask.Text = filterRule.ReparseFileFilterMasks;
            textBox_IncludePID.Text = filterRule.IncludeProcessIds;
            textBox_ExcludePID.Text = filterRule.ExcludeProcessIds;
            textBox_ExcludeProcessNames.Text = filterRule.ExcludeProcessNames;
            textBox_IncludeProcessNames.Text = filterRule.IncludeProcessNames;
            textBox_ExcludeUserNames.Text = filterRule.ExcludeUserNames;
            textBox_IncludeUserNames.Text = filterRule.IncludeUserNames;
            textBox_MonitorIO.Text = filterRule.MonitorIO.ToString();
            textBox_ControlIO.Text = filterRule.ControlIO.ToString();
            checkBox_EnableProtectionInBootTime.Checked = filterRule.IsResident;

            textBox_ProcessRights.Text = filterRule.ProcessRights;
            textBox_UserRights.Text = filterRule.UserRights;

            if (GlobalConfig.filterType == FilterAPI.FilterType.FILE_SYSTEM_MONITOR)
            {
                groupBox_AccessControl.Visible = false;
            }
            else
            {
                SetCheckBoxValue();
            }
        }


        private void SetCheckBoxValue()
        {

            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if ((accessFlags & (uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE) > 0 && textBox_PassPhrase.Text.Length > 0 )
            {
                checkBox_Encryption.Checked = true;
                textBox_PassPhrase.ReadOnly = false;
            }
            else
            {
                checkBox_Encryption.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_FILE_ACCESS_FROM_NETWORK) > 0)
            {
                checkBox_AllowRemoteAccess.Checked = true;
            }
            else
            {
                checkBox_AllowRemoteAccess.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_FILE_DELETE) > 0)
            {
                checkBox_AllowDelete.Checked = true;
            }
            else
            {
                checkBox_AllowDelete.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_FILE_RENAME) > 0)
            {
                checkBox_AllowRename.Checked = true;
            }
            else
            {
                checkBox_AllowRename.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_WRITE_ACCESS) > 0
                && (accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_SET_INFORMATION) > 0)
            {
                checkBox_AllowChange.Checked = true;
            }
            else
            {
                checkBox_AllowChange.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS) > 0)
            {
                checkBox_AllowNewFileCreation.Checked = true;
            }
            else
            {
                checkBox_AllowNewFileCreation.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED) > 0)
            {
                checkBox_AllowExecution.Checked = true;
            }
            else
            {
                checkBox_AllowExecution.Checked = false;
            }

            if ((accessFlags & (uint)FilterAPI.AccessFlag.ALLOW_SET_SECURITY_ACCESS) > 0)
            {
                checkBox_AllowSetSecurity.Checked = true;
            }
            else
            {
                checkBox_AllowSetSecurity.Checked = false;
            }

        }

        private void button_FileAccessFlags_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(OptionForm.OptionType.Access_Flag, textBox_FileAccessFlags.Text);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (optionForm.AccessFlags > 0)
                {
                    textBox_FileAccessFlags.Text = optionForm.AccessFlags.ToString();                    
                }
                else
                {
                    //if the accessFlag is 0, it is exclude filter rule,this is not what we want, so we need to include this flag.
                    textBox_FileAccessFlags.Text = ((uint)FilterAPI.AccessFlag.LAST_ACCESS_FLAG).ToString();
                }

                SetCheckBoxValue();
            }
        }


        private void button_SaveFilter_Click(object sender, EventArgs e)
        {
            if (textBox_IncludeFilterMask.Text.Trim().Length == 0)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("The include filter mask can't be empty.", "Add Filter Rule", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //include filter mask must be unique, but it can have multiple exclude filter masks associate to the same include filter mask.
            string encryptionPassPhrase = string.Empty;
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (checkBox_Encryption.Checked)
            {
                encryptionPassPhrase = textBox_PassPhrase.Text;

                //enable encryption for this filter rule.
                accessFlags = accessFlags|(uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE;
            }

            if (textBox_HiddenFilterMask.Text.Trim().Length > 0)
            {
                //enable hidden filter mask for this filter rule.
                accessFlags = accessFlags | (uint)FilterAPI.AccessFlag.HIDE_FILES_IN_DIRECTORY_BROWSING;
            }

            FilterRule filterRule = new FilterRule();
            filterRule.IncludeFileFilterMask = textBox_IncludeFilterMask.Text;
            filterRule.ExcludeFileFilterMasks = textBox_ExcludeFilterMask.Text;
            filterRule.EncryptionPassPhrase = encryptionPassPhrase;
            filterRule.IncludeProcessNames = textBox_IncludeProcessNames.Text;
            filterRule.ExcludeProcessNames = textBox_ExcludeProcessNames.Text;
            filterRule.IncludeUserNames = textBox_IncludeUserNames.Text;
            filterRule.ExcludeUserNames = textBox_ExcludeUserNames.Text;
            filterRule.IncludeProcessIds = textBox_IncludePID.Text;
            filterRule.ExcludeProcessIds = textBox_ExcludePID.Text;
            filterRule.HiddenFileFilterMasks = textBox_HiddenFilterMask.Text;
            filterRule.ReparseFileFilterMasks = textBox_ReparseFileFilterMask.Text;
            filterRule.AccessFlag = accessFlags;
            filterRule.EventType = uint.Parse(textBox_SelectedEvents.Text);
            filterRule.MonitorIO = uint.Parse(textBox_MonitorIO.Text);
            filterRule.ControlIO = uint.Parse(textBox_ControlIO.Text);
            filterRule.IsResident = checkBox_EnableProtectionInBootTime.Checked;
            filterRule.UserRights = textBox_UserRights.Text;
            filterRule.ProcessRights = textBox_ProcessRights.Text;

            GlobalConfig.AddFilterRule(filterRule);

            this.Close();
        }

        private void checkBox_Encryption_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (checkBox_Encryption.Checked)
            {
                textBox_PassPhrase.ReadOnly = false;
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE);
            }
            else
            {
                textBox_PassPhrase.ReadOnly = true;
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowDelete_CheckedChanged(object sender, EventArgs e)
        {

            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowDelete.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_FILE_DELETE);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_FILE_DELETE);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowChange_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);
            if (!checkBox_AllowChange.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_WRITE_ACCESS) & ((uint)~FilterAPI.AccessFlag.ALLOW_SET_INFORMATION);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_WRITE_ACCESS) | ((uint)FilterAPI.AccessFlag.ALLOW_SET_INFORMATION);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowRename_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowRename.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_FILE_RENAME);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_FILE_RENAME);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowRemoteAccess_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowRemoteAccess.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_FILE_ACCESS_FROM_NETWORK);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_FILE_ACCESS_FROM_NETWORK);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowNewFileCreation_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowNewFileCreation.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowExecution_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowExecution.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }

        private void checkBox_AllowSetSecurity_CheckedChanged(object sender, EventArgs e)
        {
            uint accessFlags = uint.Parse(textBox_FileAccessFlags.Text);

            if (!checkBox_AllowSetSecurity.Checked)
            {
                accessFlags = accessFlags & ((uint)~FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED);
            }
            else
            {
                accessFlags = accessFlags | ((uint)FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED);
            }

            textBox_FileAccessFlags.Text = accessFlags.ToString();
        }


        private void checkBox_DisplayPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DisplayPassword.Checked)
            {
                textBox_PassPhrase.UseSystemPasswordChar = false;
            }
            else
            {
                textBox_PassPhrase.UseSystemPasswordChar = true;
            }
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

        private void button_SelectedEvents_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(OptionForm.OptionType.EventNotification, textBox_SelectedEvents.Text);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_SelectedEvents.Text = optionForm.EventNotification.ToString();
            }
        }

        private void button_RegisterMonitorIO_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(OptionForm.OptionType.Register_Request, textBox_MonitorIO.Text,true);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_MonitorIO.Text = optionForm.RequestRegistration.ToString();
            }
        }

        private void button_RegisterControlIO_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(OptionForm.OptionType.Register_Request, textBox_ControlIO.Text);

            if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_ControlIO.Text = optionForm.RequestRegistration.ToString();
            }
        }

        private void button_AddProcessRights_Click(object sender, EventArgs e)
        {
            Form_AccessRights accessRightsForm = new Form_AccessRights(true);

            if (accessRightsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox_ProcessRights.Text.Trim().Length > 0)
                {
                    textBox_ProcessRights.Text += ";" + accessRightsForm.accessRightText;
                }
                else
                {
                    textBox_ProcessRights.Text = accessRightsForm.accessRightText;
                }
            }
        }

        private void button_AddUserRights_Click(object sender, EventArgs e)
        {
            Form_AccessRights accessRightsForm = new Form_AccessRights(false);

            if (accessRightsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox_UserRights.Text.Trim().Length > 0)
                {
                    textBox_UserRights.Text += ";" + accessRightsForm.accessRightText;
                }
                else
                {
                    textBox_UserRights.Text = accessRightsForm.accessRightText;
                }
            }
        }

    
       

    }
}
