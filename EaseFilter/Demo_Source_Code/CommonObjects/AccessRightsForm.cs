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
    public partial class Form_AccessRights : Form
    {
        bool isProcessRights = true;
        string accessRights = string.Empty;
        uint accessFlags = FilterAPI.ALLOW_MAX_RIGHT_ACCESS;

        public string accessRightText = string.Empty;

        public Form_AccessRights(bool _isProcessRights)
        {
            InitializeComponent();

            isProcessRights = _isProcessRights;
            groupBox_UserName.Location = groupBox_ProcessName.Location;

            if (isProcessRights)
            {
                groupBox_ProcessName.Visible = true;
                groupBox_UserName.Visible = false;
            }
            else
            {
                groupBox_ProcessName.Visible = false;
                groupBox_UserName.Visible = true;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (!checkBox_Creation.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS);
            }

            if (!checkBox_Delete.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_OPEN_WITH_DELETE_ACCESS);
            }

            if (!checkBox_QueryInfo.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_QUERY_INFORMATION_ACCESS);
            }

            if (!checkBox_QuerySecurity.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_QUERY_SECURITY_ACCESS);
            }

            if (!checkBox_Read.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_READ_ACCESS);
            }

            if (!checkBox_Rename.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_FILE_RENAME);
            }

            if (!checkBox_SetInfo.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_SET_INFORMATION);
            }

            if (!checkBox_SetSecurity.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_SET_SECURITY_ACCESS);
            }

            if (!checkBox_Write.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_WRITE_ACCESS);
            }

            if (!checkBox_Execution.Checked)
            {
                accessFlags &= (uint)(~FilterAPI.AccessFlag.ALLOW_FILE_MEMORY_MAPPED);
            }

            if (isProcessRights && textBox_ProcessName.Text.Trim().Length > 0 )
            {
                string[] processNames =  textBox_ProcessName.Text.Trim().Split(new char[] { ';' });
                if (processNames.Length > 0)
                {
                    foreach (string processName in processNames)
                    {
                        if (processName.Trim().Length > 0)
                        {
                            if (accessRightText.Length > 0)
                            {
                                accessRightText += ";";
                            }
                            
                            accessRightText += processName.Trim() + ":" + accessFlags.ToString();
                        }
                    }
                }
            }
            else if (!isProcessRights && textBox_UserName.Text.Trim().Length > 0)
            {
                string[] userNames = textBox_UserName.Text.Trim().Split(new char[] { ';' });
                if (userNames.Length > 0)
                {
                    foreach (string userName in userNames)
                    {
                        if (userName.Trim().Length > 0)
                        {
                            if (accessRightText.Length > 0)
                            {
                                accessRightText += ";";
                            }

                            accessRightText += userName.Trim() + ":" + accessFlags.ToString();
                        }
                    }
                }
            }
        }
    }
}
