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

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public partial class AccountForm : Form
    {
        public  static string accountName = string.Empty;
        public  static string password = string.Empty;
        public  static bool isAuthorized = false;
        public static bool isGuest = true;

        public AccountForm()
        {           
            InitializeComponent();
            textBox_EmailAddress.Text = GlobalConfig.AccountName;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            GlobalConfig.AccountName = textBox_EmailAddress.Text;
            GlobalConfig.SaveConfigSetting();

            button_SignIn.Enabled = false;        

            try
            {
                if (string.IsNullOrEmpty(textBox_EmailAddress.Text))
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The account name can't be empty.", "account", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }

                accountName = textBox_EmailAddress.Text.ToLower().Trim();
                password = textBox_Password.Text.Trim();

                if (string.Compare(accountName, "guest") == 0)
                {
                    isAuthorized = true;
                    isGuest = true;
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                        MessageBox.Show("The password can't be empty.", "password", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }
                }

                string lastError = string.Empty;

                //isAuthorized = WebFormServices.SignInAccount(accountName, password, ref lastError);

                //if (!isAuthorized)
                //{
                //    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                //    MessageBox.Show(lastError, "SignIn", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //    return;
                //}

                isGuest = false;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Sign in account failed with error" + ex.Message, "SignIn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_SignIn.Enabled = true;

                if (isAuthorized)
                {
                    this.Close();
                }
            }
        }


        private void textBox_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_SignIn_Click(null, null);
            }

        }

        private void button_SignUp_Click(object sender, EventArgs e)
        {
            GlobalConfig.AccountName = textBox_EmailAddress.Text;
            GlobalConfig.SaveConfigSetting();

            button_SignUp.Enabled = false;

            try
            {
                if (string.IsNullOrEmpty(textBox_EmailAddress.Text))
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The account name can't be empty.", "account", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }

                accountName = textBox_EmailAddress.Text.ToLower().Trim();
                if (string.Compare(accountName, "guest") == 0)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("The guest account name can't be signed up to the server.", "account", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                string lastError = string.Empty;
                //bool retVal = WebFormServices.SignUpAccount(accountName, ref lastError);
                MessageBoxIcon icon = MessageBoxIcon.Information;
                //if (!retVal)
                {
                    icon = MessageBoxIcon.Error;
                }

                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show(lastError, "SignUp", MessageBoxButtons.OK, icon);

                return;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Sign up account failed with error" + ex.Message, "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_SignUp.Enabled = true;
                this.Close();
            }
        }

      
    }
}
