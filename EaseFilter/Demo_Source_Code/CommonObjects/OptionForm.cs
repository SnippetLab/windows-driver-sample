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
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace EaseFilter.CommonObjects
{
    public partial class OptionForm : Form
    {
        OptionType optionType = OptionType.Register_Request;
        string value = string.Empty;

        uint requestRegistration = 0;
        string processId = "0";
        uint accessFlags = 0;
        uint debugModules = 0xffff;
        uint filterStatus = 0;
        uint returnStatus = 0;
        uint eventNotification = 0;
        uint shareAccessFlags = 0;

        public bool isMonitorFilter = false;
     
        public enum OptionType
        {
            Register_Request = 0,
            ProccessId,
            Access_Flag,
            Filter_Status,
            Return_Status,
            EventNotification,
            ShareAccessOption,
        }

        public OptionForm(OptionType formType, string defaultValue, bool _isMoitorFilter)
        {
            this.optionType = formType;
            this.value = defaultValue;
            this.isMonitorFilter = _isMoitorFilter;

            InitializeComponent();
            InitForm();
        }

        public OptionForm(OptionType formType,string defaultValue)
        {
            this.optionType = formType;
            this.value = defaultValue;

            InitializeComponent();
            InitForm();
        }

        public uint ShareAccessFlags
        {
            get { return shareAccessFlags; }
        }

        public uint EventNotification
        {
            get { return eventNotification; }
        }

        public uint FilterStatus
        {
            get { return filterStatus; }
        }
        public uint ReturnStatus
        {
            get { return returnStatus; }
        }

        public uint RequestRegistration
        {
            get { return requestRegistration; }
        }

        public string ProcessId
        {
            get { return processId; }
        }


        public uint AccessFlags
        {
            get { return accessFlags; }
        }

        public uint DebugModules
        {
            get { return debugModules; }
        }


        void InitForm()
        {
            this.Text = optionType.ToString();

            switch (optionType)
            {
                case OptionType.EventNotification:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select file event type", 200, System.Windows.Forms.HorizontalAlignment.Left);

                        eventNotification = uint.Parse(value);

                        foreach (FilterAPI.EVENTTYPE eventType in Enum.GetValues(typeof(FilterAPI.EVENTTYPE)))
                        {

                            if (eventType == FilterAPI.EVENTTYPE.NONE)
                            {
                                continue;
                            }

                            string item = eventType.ToString();

                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = eventType;

                            if ((eventNotification & (uint)eventType) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }

                case OptionType.Register_Request:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select Register I/O type", 400, System.Windows.Forms.HorizontalAlignment.Left);

                        requestRegistration = uint.Parse(value);

                        foreach (FilterAPI.MessageType messageType in Enum.GetValues(typeof(FilterAPI.MessageType)))
                        {
                            string item = messageType.ToString();

                            if (item.ToLower().StartsWith("pre") && isMonitorFilter)
                            {
                                //for monitor filter, it only can register the post I/O request notification.
                                continue;
                            }

                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = messageType;

                            if ((requestRegistration & (uint)messageType) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }

                case OptionType.ProccessId:
                    {
                        Process[] processlist = Process.GetProcesses();

                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Process Id", 100, System.Windows.Forms.HorizontalAlignment.Left);
                        listView1.Columns.Add("Process Name", 300, System.Windows.Forms.HorizontalAlignment.Left);

                        List<uint> pidList = new List<uint>();
                  
                        string[] pids = value.Split(';');
                        foreach (string pid in pids)
                        {
                            if (!string.IsNullOrEmpty(pid))
                            {
                                pidList.Add(uint.Parse(pid));
                            }
                        }

                        
                        for (int i = 0; i < processlist.Length; i++)
                        {
                            string[] item = new string[2];
                            item[0] = processlist[i].Id.ToString();
                            item[1] = processlist[i].ProcessName;

                            ListViewItem lvItem = new ListViewItem(item, 0);
                            
                            lvItem.Tag = processlist[i].Id;

                            if (pidList.Contains((uint)(processlist[i].Id)))
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);

                        }

                        break;
                    }

                case OptionType.Access_Flag:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select AccessFlag", 400, System.Windows.Forms.HorizontalAlignment.Left);

                        accessFlags = uint.Parse(value);

                        foreach (FilterAPI.AccessFlag accessFlag in Enum.GetValues(typeof(FilterAPI.AccessFlag)))
                        {
                            if (accessFlag < FilterAPI.AccessFlag.REPARSE_FILE_OPEN || accessFlag == FilterAPI.AccessFlag.LAST_ACCESS_FLAG)
                            {
                                //this is special usage for the filter 
                                continue;
                            }

                            string item = accessFlag.ToString();
                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = accessFlag;

                            if (((uint)accessFlag & accessFlags) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }

                case OptionType.ShareAccessOption:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select AccessFlag", 400, System.Windows.Forms.HorizontalAlignment.Left);

                        shareAccessFlags = uint.Parse(value);

                        foreach (FilterAPI.SecureFileAccessRights accessFlag in Enum.GetValues(typeof(FilterAPI.SecureFileAccessRights)))
                        {
                            string item = accessFlag.ToString();
                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = accessFlag;

                            if (((uint)accessFlag & shareAccessFlags) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }
               

                case OptionType.Filter_Status:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select Filter Status", 400, System.Windows.Forms.HorizontalAlignment.Left);

                        filterStatus = uint.Parse(value);

                        foreach (FilterAPI.FilterStatus status in Enum.GetValues(typeof(FilterAPI.FilterStatus)))
                        {
                            string item = status.ToString();
                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = status;

                            if (((uint)status & filterStatus) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }

                case OptionType.Return_Status:
                    {
                        listView1.Clear();		//clear control
                        //create column header for ListView
                        listView1.Columns.Add("Select Only One Status", 400, System.Windows.Forms.HorizontalAlignment.Left);

                        returnStatus = uint.Parse(value);

                        foreach (NtStatus.Status status in Enum.GetValues(typeof(NtStatus.Status)))
                        {
                            string item = status.ToString();
                            ListViewItem lvItem = new ListViewItem(item, 0);
                            lvItem.Tag = status;

                            if (((uint)status & filterStatus) > 0)
                            {
                                lvItem.Checked = true;
                            }

                            listView1.Items.Add(lvItem);
                        }

                        break;
                    }
            }
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            requestRegistration = 0;
            processId = string.Empty;
            accessFlags = 0;
            debugModules = 0;
            eventNotification = 0;
            shareAccessFlags = 0;

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                switch (optionType)
                {
                    case OptionType.EventNotification:
                        eventNotification |= (uint)item.Tag;
                        break;

                    case OptionType.Register_Request:
                        requestRegistration |= (uint)item.Tag;
                        break;
                    case OptionType.ProccessId:
                        int pid = (int)item.Tag;
                        processId += pid.ToString() + ";";
                        break;

                    case OptionType.Access_Flag:
                        accessFlags |= (uint)item.Tag;
                        break;

                    case OptionType.ShareAccessOption:
                        shareAccessFlags |= (uint)item.Tag;
                        break;

                    case OptionType.Filter_Status:
                        filterStatus |= (uint)item.Tag;
                        break;

                    case OptionType.Return_Status:
                        returnStatus |= (uint)item.Tag;
                        return;
                }
            }

        }

        private void button_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = true;
            }
        }

        private void button_ClearAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;
            }
        }

        
    }
}
