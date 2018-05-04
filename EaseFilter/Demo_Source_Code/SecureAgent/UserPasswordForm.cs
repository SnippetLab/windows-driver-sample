
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
using System.Runtime.InteropServices;
using System.Diagnostics;

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public partial class UserPasswordForm : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        void ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
                SetForegroundWindow(p[0].MainWindowHandle);
        }

        public string userPassword = string.Empty;
        private DateTime currentTime = DateTime.Now;
        private delegate void FormDlgt();

        public UserPasswordForm(string userName, string processName, string fileName)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            label_FileName.Text = "File Name: " + fileName;
            label_ProcessName.Text = "Process Name: " + processName;
            label_UserName.Text = "User Name: " + userName;

            System.Timers.Timer closeFormTimer = new System.Timers.Timer();
            closeFormTimer.Interval = 100; //millisecond
            closeFormTimer.Start();
            closeFormTimer.Enabled = true;
            closeFormTimer.Elapsed += new System.Timers.ElapsedEventHandler(CloseFormTimer_Elapsed);

            Process p = Process.GetCurrentProcess();

            // Activate the first application we find with this name
            //if (null != p)
            //    SetForegroundWindow(p.MainWindowHandle);
           // SetForegroundWindow(this.Handle);
        }

        private void UserPasswordForm_Load(object sender, EventArgs e)
        {
           // SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            SetForegroundWindow(this.Handle);
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            userPassword = textBox_Password.Text;
            this.Close();
        }

        private void CloseFormTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //ShowForm();

            if ((DateTime.Now - currentTime).TotalSeconds > GlobalConfig.ConnectionTimeOut)
            {
                //there are no one input the password.
                ClosePasswordForm();
            }

        }

        private void ShowForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FormDlgt(ShowForm), new Object[] { });
            }
            else
            {

                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                this.Focus();
                this.TopMost = true;
                
                //SetForegroundWindow(this.Handle);

                this.ShowDialog();
            }
        }

        private void ClosePasswordForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FormDlgt(ClosePasswordForm), new Object[] {});
            }
            else
            {
                this.Close();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
