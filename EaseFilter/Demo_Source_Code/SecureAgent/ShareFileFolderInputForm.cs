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

namespace SecureAgent
{
    public partial class ShareFileFolderInputForm : Form
    {
        public string dropFolder = string.Empty;

        public ShareFileFolderInputForm(string _dropFolder)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.textBox_DropFolder.Text = _dropFolder;
        }

        private void button_BrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_DropFolder.Text = fbd.SelectedPath;
            }
        }

        private void button_SetDropFolder_Click(object sender, EventArgs e)
        {
            dropFolder = textBox_DropFolder.Text;
            this.Close();
        }
    }
}
