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
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;

namespace EaseFilter.CommonObjects
{

    public class GlobalConfig
    {
        //Purchase a license key with the link: http://www.easefilter.com/Order.htm
        //Email us to request a trial key: info@easefilter.com //free email is not accepted.
        public static string registerKey = "************************************";

        static Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
        public static string AssemblyPath = Path.GetDirectoryName(assembly.Location);

        //the message output level. It will output the messages which less than this level.
        static EventLevel eventLevel = EventLevel.Information;
        static bool[] selectedDisplayEvents = new bool[] { false, true, true, true, false, false };
        static EventOutputType eventOutputType = EventOutputType.Console;
        //The log file name if outputType is ToFile.
        static string eventLogFileName = "EventLog.txt";
        static int maxEventLogFileSize = 4 * 1024 * 1024; //4MB
        static string eventSource = "EaseFilter";
        static string eventLogName = "EaseFilter";

        static uint filterConnectionThreads = 5;
        static uint connectionTimeOut = 30; //seconds
        static private Dictionary<string, FilterRule> filterRules = new Dictionary<string, FilterRule>();
        static List<uint> includePidList = new List<uint>();
        static List<uint> excludePidList = new List<uint>();
        static List<uint> protectPidList = new List<uint>();
        static string includedUsers = string.Empty;
        static string excludedUsers = string.Empty;

        static uint requestIORegistration = 0;
        static uint displayEvents = 0;

        static string accountName = "guest";
        static string masterPassword = string.Empty;
        static string activatedLicense = string.Empty;

        static int maximumFilterMessages = 500;
        static string filterMessageLogName = "filterMessage.log";
        static long filterMessageLogFileSize = 10 * 1024 * 1024;
        static bool enableLogTransaction = false;
        static bool outputMessageToConsole = true;
        static bool enableNotification = true;

        static string configFileName = ConfigSetting.GetFilePath();

        //the filter driver will use the default IV to encrypt the new file if it is true.
        static bool enableDefaultIVKey = false;

        static uint currentPid = (uint)System.Diagnostics.Process.GetCurrentProcess().Id;

        public static bool isRunning = true;
        public static ManualResetEvent stopEvent = new ManualResetEvent(false);

        public static FilterAPI.FilterType filterType = FilterAPI.FilterType.FILE_SYSTEM_MONITOR;

        static GlobalConfig()
        {
            Load();
        }

        public static void Load()
        {
            filterRules.Clear();

            try
            {
                filterRules = ConfigSetting.GetFilterRules();
                filterConnectionThreads = ConfigSetting.Get("filterConnectionThreads", filterConnectionThreads);
                requestIORegistration = ConfigSetting.Get("requestIORegistration", requestIORegistration);
                displayEvents = ConfigSetting.Get("displayEvents", displayEvents);
                filterMessageLogName = ConfigSetting.Get("filterMessageLogName", filterMessageLogName);
                filterMessageLogFileSize = ConfigSetting.Get("filterMessageLogFileSize", filterMessageLogFileSize);
                maximumFilterMessages = ConfigSetting.Get("maximumFilterMessages", maximumFilterMessages);
                enableLogTransaction = ConfigSetting.Get("enableLogTransaction", enableLogTransaction);
                activatedLicense = ConfigSetting.Get("activatedLicense", activatedLicense);
                enableDefaultIVKey = ConfigSetting.Get("enableDefaultIVKey", enableDefaultIVKey);
                accountName = ConfigSetting.Get("accountName", accountName);

                outputMessageToConsole = ConfigSetting.Get("outputMessageToConsole", outputMessageToConsole);
                enableNotification = ConfigSetting.Get("enableNotification", enableNotification);
                eventLevel = (EventLevel)ConfigSetting.Get("eventLevel", (uint)eventLevel);

                masterPassword = ConfigSetting.Get("masterPassword", masterPassword);
                masterPassword = FilterAPI.AESEncryptDecryptStr(masterPassword, FilterAPI.EncryptType.Decryption);

                includedUsers = ConfigSetting.Get("includedUsers", includedUsers);
                excludedUsers = ConfigSetting.Get("excludedUsers", excludedUsers);

            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(176, "LoadConfigSetting", CommonObjects.EventLevel.Error, "Load config file " + configFileName + " failed with error:" + ex.Message);
            }
        }

        public static void Stop()
        {
            isRunning = false;
            stopEvent.Set();
            EventManager.Stop();
        }
     
        public static bool SaveConfigSetting()
        {
            bool ret = true;

            try
            {
                ConfigSetting.Save();
                SendConfigSettingsToFilter();
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(235, "SaveConfigSetting", CommonObjects.EventLevel.Error, "Save config file " + configFileName + " failed with error:" + ex.Message);
                ret = false;
            }

            return ret;
        }

        static public string  ConfigFilePath
        {
            get { return configFileName; }
            set {  configFileName = value; }
        }


        static public bool IsRunning
        {
            get { return isRunning; }
        }

        static public ManualResetEvent StopEvent
        {
            get { return stopEvent; }
        }

        static public bool[] SelectedDisplayEvents
        {
            get
            {
                return selectedDisplayEvents;
            }
            set
            {
                selectedDisplayEvents = value;
            }
        }

        static public EventLevel EventLevel
        {
            get
            {
                return eventLevel;
            }
            set
            {
                eventLevel = value;
                ConfigSetting.Set("eventLevel", ((uint)value).ToString());
            }
        }

        static public EventOutputType EventOutputType
        {
            get
            {
                return eventOutputType;
            }
            set
            {
                eventOutputType = value;
            }
        }

        static public string EventLogFileName
        {
            get
            {
                return eventLogFileName;
            }
            set
            {
                eventLogFileName = value;
            }
        }

        static public int MaxEventLogFileSize
        {
            get
            {
                return maxEventLogFileSize;
            }
            set
            {
                maxEventLogFileSize = value;
            }
        }

        static public string EventSource
        {
            get
            {
                return eventSource;
            }
            set
            {
                eventSource = value;
            }
        }


        static public string EventLogName
        {
            get
            {
                return eventLogName;
            }
            set
            {
                eventLogName = value;
            }
        }


        public static uint FilterConnectionThreads
        {
            get { return filterConnectionThreads; }
            set
            { 
                filterConnectionThreads = value;
                ConfigSetting.Set("filterConnectionThreads", value.ToString());
            }
        }

        public static uint RequestIORegistration
        {
            get { return requestIORegistration; }
            set 
            { 
                requestIORegistration = value;
                ConfigSetting.Set("requestIORegistration", value.ToString());
            }
        }

        public static uint DisplayEvents
        {
            get { return displayEvents; }
            set 
            { 
                displayEvents = value;
                ConfigSetting.Set("displayEvents", value.ToString());
            }
        }

        public static string FilterMessageLogName
        {
            get { return filterMessageLogName; }
            set 
            { 
                filterMessageLogName = value;
                ConfigSetting.Set("filterMessageLogName", value.ToString());
            }
        }

        public static long FilterMessageLogFileSize
        {
            get { return filterMessageLogFileSize; }
            set 
            {
                filterMessageLogFileSize = value;
                ConfigSetting.Set("filterMessageLogFileSize", value.ToString());
            }
        }

        public static int MaximumFilterMessages
        {
            get { return maximumFilterMessages; }
            set
            { 
                maximumFilterMessages = value;
                ConfigSetting.Set("maximumFilterMessages", value.ToString());
            }
        }

        public static bool EnableLogTransaction
        {
            get { return enableLogTransaction; }
            set 
            { 
                enableLogTransaction = value;
                ConfigSetting.Set("enableLogTransaction", value.ToString());
            }
        }

        public static bool OutputMessageToConsole
        {
            get { return outputMessageToConsole; }
            set
            {
                outputMessageToConsole = value;
                ConfigSetting.Set("outputMessageToConsole", value.ToString());
            }
        }

        public static bool EnableNotification
        {
            get { return enableNotification; }
            set
            {
                enableNotification = value;
                ConfigSetting.Set("enableNotification", value.ToString());
            }
        }

        public static List<uint> IncludePidList
        {
            get { return includePidList; }
            set { includePidList = value; }
        }

        public static List<uint> ExcludePidList
        {
            get { return excludePidList; }
            set { excludePidList = value; }
        }

        public static List<uint> ProtectPidList
        {
            get { return protectPidList; }
            set { protectPidList = value; }
        }


        public static uint ConnectionTimeOut
        {
            get { return connectionTimeOut; }
            set 
            {
                connectionTimeOut = value;
                ConfigSetting.Set("connectionTimeOut", value.ToString());
            }
        }

        public static string ActivatedLisense
        {
            get { return activatedLicense; }
            set
            { 
                activatedLicense = value;
                ConfigSetting.Set("activatedLicense", value.ToString());
            }
        }

        public static bool EnableDefaultIVKey
        {
            get { return enableDefaultIVKey; }
            set
            {
                enableDefaultIVKey = value;
                ConfigSetting.Set("enableDefaultIVKey", value.ToString());
            }
        }

        public static string AccountName
        {
            get { return accountName; }
            set
            {
                accountName = value;
                ConfigSetting.Set("accountName", value.ToString());
            }
        }


        public static string MasterPassword
        {
            get
            {
                return masterPassword; 
            }
            set
            {
                masterPassword = value;
                string encryptedPassword = FilterAPI.AESEncryptDecryptStr(value.ToString(), FilterAPI.EncryptType.Encryption);
                ConfigSetting.Set("masterPassword", encryptedPassword);
            }
        }

        public static string IncludedUsers
        {
            get { return includedUsers; }
            set
            {
                includedUsers = value;
                ConfigSetting.Set("includedUsers", value.ToString());
            }
        }

        public static string ExcludedUsers
        {
            get { return excludedUsers; }
            set
            {
                excludedUsers = value;
                ConfigSetting.Set("excludedUsers", value.ToString());
            }
        }

        public static bool AddFilterRule( FilterRule newRule)
        {
            if (filterRules.ContainsKey(newRule.IncludeFileFilterMask))
            {
                //the exist filter rule already there,remove it
                filterRules.Remove(newRule.IncludeFileFilterMask);
                ConfigSetting.RemoveFilterRule(newRule.IncludeFileFilterMask);
            }

            filterRules.Add(newRule.IncludeFileFilterMask, newRule);

            ConfigSetting.AddFilterRule(newRule);

            return true;
        }

        public static void RemoveFilterRule(string includeFilterMask)
        {
            if (filterRules.ContainsKey(includeFilterMask))
            {
                filterRules.Remove(includeFilterMask);
                ConfigSetting.RemoveFilterRule(includeFilterMask);
            }

        }

        public static bool IsFilterRuleExist(string includeFilterMask)
        {
            if (filterRules.ContainsKey(includeFilterMask))
            {
                return true;
            }

            return false;
        }

        public static Dictionary<string, FilterRule> FilterRules
        {
            get { return filterRules; }
        }

        public static void SendConfigSettingsToFilter()
        {
            try
            {
                if (!FilterAPI.IsFilterStarted)
                {
                    EventManager.WriteMessage(479, "SetFilterType", CommonObjects.EventLevel.Error, "SendConfigSettingsToFilter failed, the filter driver is not loaded.");
                    return;
                }

                FilterAPI.ResetConfigData();

                FilterAPI.SetConnectionTimeout(connectionTimeOut);

                if (!FilterAPI.SetFilterType((uint)filterType))
                {
                    EventManager.WriteMessage(443, "SetFilterType", CommonObjects.EventLevel.Error, "SetFilterType " + filterType.ToString() + " failed:" + FilterAPI.GetLastErrorMessage());
                }
                else
                {
                    EventManager.WriteMessage(447, "SetFilterType", CommonObjects.EventLevel.Information, "SetFilterType " + filterType.ToString() + " succeeded.");
                }

                //if you want the filter driver to use the devault IV key, you need to set this setting:
                if (enableDefaultIVKey)
                {
                    uint boolConfig = (uint)FilterAPI.BooleanConfig.ENABLE_DEFAULT_IV_TAG;
                    FilterAPI.SetBooleanConfig(boolConfig);
                }

                foreach (FilterRule filterRule in filterRules.Values)
                {
                    //add filter rule to filter driver here, the filter rule is unique with the include file filter mask.
                    //you can't have the mutiple filter rules with the same include file filter mask,if there are the same 
                    //one exist, the new one with accessFlag will overwrite the old accessFlag.
                    //for control filter, if isResident is true, the access control will be enabled in boot time.
                    if (!FilterAPI.AddNewFilterRule((uint)filterRule.AccessFlag, filterRule.IncludeFileFilterMask, filterRule.IsResident))
                    {
                        EventManager.WriteMessage(456, "SendFilterRule", CommonObjects.EventLevel.Error, "Send filter rule failed:" + FilterAPI.GetLastErrorMessage());
                    }
                    else
                    {
                        EventManager.WriteMessage(460, "SendFilterRule", CommonObjects.EventLevel.Information, "Send filter rule:" + filterRule.IncludeFileFilterMask);
                    }

                    if (!FilterAPI.RegisterEventTypeToFilterRule(filterRule.IncludeFileFilterMask,(uint)filterRule.EventType))
                    {
                        EventManager.WriteMessage(478, "SendFilterRule", CommonObjects.EventLevel.Error, "Register event type:" + (FilterAPI.EVENTTYPE)filterRule.EventType +" failed:" + FilterAPI.GetLastErrorMessage());
                    }
                    else
                    {
                        EventManager.WriteMessage(482, "SendFilterRule", CommonObjects.EventLevel.Information, "Register event type:" + (FilterAPI.EVENTTYPE)filterRule.EventType +" succeed.");
                    }

                    if (!FilterAPI.RegisterMoinitorIOToFilterRule(filterRule.IncludeFileFilterMask, filterRule.MonitorIO))
                    {
                        EventManager.WriteMessage(499, "SendFilterRule", CommonObjects.EventLevel.Error, "Register monitor IO:" + filterRule.MonitorIO + " failed:" + FilterAPI.GetLastErrorMessage());
                    }
                    else
                    {
                        EventManager.WriteMessage(503, "SendFilterRule", CommonObjects.EventLevel.Information, "Register monitor IO:" + filterRule.MonitorIO + " succeed.");
                    }

                    if (!FilterAPI.RegisterControlIOToFilterRule(filterRule.IncludeFileFilterMask, filterRule.ControlIO))
                    {
                        EventManager.WriteMessage(508, "SendFilterRule", CommonObjects.EventLevel.Error, "Register control IO:" + filterRule.ControlIO + " failed:" + FilterAPI.GetLastErrorMessage());
                    }
                    else
                    {
                        EventManager.WriteMessage(512, "SendFilterRule", CommonObjects.EventLevel.Information, "Register control IO:" + filterRule.ControlIO + " succeed.");
                    }

                    //every filter rule can have multiple exclude file filter masks, you can exclude the files 
                    //which matches the exclude filter mask.
                    string[] excludeFilterMasks = filterRule.ExcludeFileFilterMasks.Split(new char[] { ';' });
                    if (excludeFilterMasks.Length > 0)
                    {
                        foreach (string excludeFilterMask in excludeFilterMasks)
                        {
                            if (excludeFilterMask.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddExcludeFileMaskToFilterRule(filterRule.IncludeFileFilterMask, excludeFilterMask.Trim()))
                                {
                                    EventManager.WriteMessage(496, "AddExcludeFileMaskToFilterRule", CommonObjects.EventLevel.Error, "AddExcludeFileMaskToFilterRule " + excludeFilterMask + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(500, "AddExcludeFileMaskToFilterRule", CommonObjects.EventLevel.Information, "AddExcludeFileMaskToFilterRule " + excludeFilterMask + " succeeded.");
                                }
                            }
                        }

                    }

                    byte[] encryptionKey = null;
                    uint encryptionKeyLength = 0;

                    //you can enable the encryption per filter rule, set the FILE_ENCRYPTION_RULE to accessFlag, and add encryption key to the filter rule too.
                    if ((filterRule.AccessFlag & (uint)FilterAPI.AccessFlag.FILE_ENCRYPTION_RULE) > 0 && filterRule.EncryptionPassPhrase.Length > 0)
                    {
                        encryptionKey = Utils.GetKeyByPassPhrase(filterRule.EncryptionPassPhrase);
                        encryptionKeyLength = (uint)encryptionKey.Length;

                        if (!FilterAPI.AddEncryptionKeyToFilterRule(filterRule.IncludeFileFilterMask, encryptionKeyLength, encryptionKey))
                        {
                            EventManager.WriteMessage(482, "AddEncryptionKeyToFilterRule", CommonObjects.EventLevel.Error, "AddEncryptionKeyToFilterRule " + filterRule.IncludeFileFilterMask + " failed:" + FilterAPI.GetLastErrorMessage());
                        }
                        else
                        {
                            EventManager.WriteMessage(482, "AddEncryptionKeyToFilterRule", CommonObjects.EventLevel.Information, "AddEncryptionKeyToFilterRule succeeded.");
                        }

                     
                    }


                    string[] includeProcessNames = filterRule.IncludeProcessNames.Split(new char[] { ';' });
                    if (includeProcessNames.Length > 0)
                    {
                        foreach (string includeProcessName in includeProcessNames)
                        {
                            if (includeProcessName.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddIncludeProcessNameToFilterRule(filterRule.IncludeFileFilterMask, includeProcessName.Trim()))
                                {
                                    EventManager.WriteMessage(536, "AddIncludeProcessNameToFilterRule", CommonObjects.EventLevel.Error, "AddIncludeProcessNameToFilterRule " + includeProcessName + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(540, "AddIncludeProcessNameToFilterRule", CommonObjects.EventLevel.Information, "AddIncludeProcessNameToFilterRule " + includeProcessName + " succeeded.");
                                }
                            }
                        }

                    }

                    string[] excludeProcessNames = filterRule.ExcludeProcessNames.Split(new char[] { ';' });
                    if (excludeProcessNames.Length > 0)
                    {
                        foreach (string excludeProcessName in excludeProcessNames)
                        {
                            if (excludeProcessName.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddExcludeProcessNameToFilterRule(filterRule.IncludeFileFilterMask, excludeProcessName.Trim()))
                                {
                                    EventManager.WriteMessage(556, "AddExcludeProcessNameToFilterRule", CommonObjects.EventLevel.Error, "AddExcludeProcessNameToFilterRule " + excludeProcessName + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(560, "AddExcludeProcessNameToFilterRule", CommonObjects.EventLevel.Information, "AddExcludeProcessNameToFilterRule " + excludeProcessName + " succeeded.");
                                }
                            }
                        }

                    }

                    //set include process list for this filter rule.
                    string[] includePidListInFilterRule = filterRule.IncludeProcessIds.Split(new char[] { ';' });
                    if (includePidListInFilterRule.Length > 0 )
                    {
                        foreach (string inPidstr in includePidListInFilterRule)
                        {
                            if (inPidstr.Trim().Length > 0)
                            {
                                uint inPid = uint.Parse(inPidstr.Trim());
                                if (!FilterAPI.AddIncludeProcessIdToFilterRule(filterRule.IncludeFileFilterMask, inPid))
                                {
                                    EventManager.WriteMessage(523, "AddIncludeProcessIdToFilterRule", CommonObjects.EventLevel.Error, "AddIncludeProcessIdToFilterRule " + filterRule.IncludeFileFilterMask + " PID:" + inPidstr + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(527, "AddIncludeProcessIdToFilterRule", CommonObjects.EventLevel.Information, "AddIncludeProcessIdToFilterRule " + filterRule.IncludeFileFilterMask + " PID:" + inPidstr + " succeeded.");
                                }
                            }
                        }

                    }

                    //set exclude process list for this filter rule.
                    string[] excludePidListInFilterRule = filterRule.ExcludeProcessIds.Split(new char[] { ';' });
                    if (excludePidListInFilterRule.Length > 0)
                    {
                        foreach (string exPidstr in excludePidListInFilterRule)
                        {
                            if (exPidstr.Trim().Length > 0)
                            {
                                uint exPid = uint.Parse(exPidstr.Trim());
                                if (!FilterAPI.AddExcludeProcessIdToFilterRule(filterRule.IncludeFileFilterMask, exPid))
                                {
                                    EventManager.WriteMessage(545, "AddExcludeProcessIdToFilterRule", CommonObjects.EventLevel.Error, "AddExcludeProcessIdToFilterRule " + filterRule.IncludeFileFilterMask + " PID:" + exPidstr + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(549, "AddExcludeProcessIdToFilterRule", CommonObjects.EventLevel.Information, "AddExcludeProcessIdToFilterRule " + filterRule.IncludeFileFilterMask + " PID:" + exPidstr + " succeeded.");
                                }
                            }

                        }
                    }


                    string[] includeUserNames = filterRule.IncludeUserNames.Split(new char[] { ';' });
                    if (includeUserNames.Length > 0)
                    {
                        foreach (string includeUserName in includeUserNames)
                        {
                            if (includeUserName.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddIncludeUserNameToFilterRule(filterRule.IncludeFileFilterMask, includeUserName.Trim()))
                                {
                                    EventManager.WriteMessage(536, "AddIncludeUserNameToFilterRule", CommonObjects.EventLevel.Error, "AddIncludeUserNameToFilterRule " + includeUserName + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(540, "AddIncludeUserNameToFilterRule", CommonObjects.EventLevel.Information, "AddIncludeUserNameToFilterRule " + includeUserName + " succeeded.");
                                }
                            }
                        }

                    }

                    string[] excludeUserNames = filterRule.ExcludeUserNames.Split(new char[] { ';' });
                    if (excludeUserNames.Length > 0)
                    {
                        foreach (string excludeUserName in excludeUserNames)
                        {
                            if (excludeUserName.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddExcludeUserNameToFilterRule(filterRule.IncludeFileFilterMask, excludeUserName.Trim()))
                                {
                                    EventManager.WriteMessage(556, "AddExcludeUserNameToFilterRule", CommonObjects.EventLevel.Error, "AddExcludeUserNameToFilterRule " + excludeUserName + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(560, "AddExcludeUserNameToFilterRule", CommonObjects.EventLevel.Information, "AddExcludeUserNameToFilterRule " + excludeUserName + " succeeded.");
                                }
                            }
                        }

                    }

                    string[] processRights = filterRule.ProcessRights.Split(new char[] { ';' });
                    if (processRights.Length > 0)
                    {
                        foreach (string processRight in processRights)
                        {
                            if (processRight.Trim().Length > 0)
                            {
                                string processName = processRight.Substring(0, processRight.IndexOf(':'));
                                uint accessFlags = uint.Parse(processRight.Substring(processRight.IndexOf(':') + 1) );

                                if (!FilterAPI.AddProcessRightsToFilterRule(filterRule.IncludeFileFilterMask, processName.Trim(), accessFlags))
                                {
                                    EventManager.WriteMessage(725, "AddProcessRightsToFilterRule", CommonObjects.EventLevel.Error, "AddProcessRightsToFilterRule " + filterRule.IncludeFileFilterMask + ",processName:" + processName + ",accessFlags:" + accessFlags + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(729, "AddProcessRightsToFilterRule", CommonObjects.EventLevel.Information, "AddProcessRightsToFilterRule " + filterRule.IncludeFileFilterMask + ",processName:" + processName + ",accessFlags:" + accessFlags + " succeeded.");
                                }
                            }
                        }

                    }

                    string[] userRights = filterRule.UserRights.Split(new char[] { ';' });
                    if (userRights.Length > 0)
                    {
                        foreach (string userRight in userRights)
                        {
                            if (userRight.Trim().Length > 0)
                            {
                                string userName = userRight.Substring(0, userRight.IndexOf(':'));
                                uint accessFlags = uint.Parse(userRight.Substring(userRight.IndexOf(':') + 1));

                                if (!FilterAPI.AddUserRightsToFilterRule(filterRule.IncludeFileFilterMask, userName.Trim(), accessFlags))
                                {
                                    EventManager.WriteMessage(748, "AddUserRightsToFilterRule", CommonObjects.EventLevel.Error, "AddUserRightsToFilterRule " + filterRule.IncludeFileFilterMask + ",userName:" + userName + ",accessFlags:" + accessFlags + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(752, "AddUserRightsToFilterRule", CommonObjects.EventLevel.Information, "AddUserRightsToFilterRule " + filterRule.IncludeFileFilterMask + ",userName:" + userName + ",accessFlags:" + accessFlags + " succeeded.");
                                }
                            }
                        }

                    }

                    //Hide the files which match the hidden file filter masks when the user browse the managed directory.
                    string[] hiddenFileFilterMasks = filterRule.HiddenFileFilterMasks.Split(new char[] { ';' });
                    if ((filterRule.AccessFlag & (uint)FilterAPI.AccessFlag.HIDE_FILES_IN_DIRECTORY_BROWSING) > 0 && hiddenFileFilterMasks.Length > 0)
                    {
                        foreach (string hiddenFilterMask in hiddenFileFilterMasks)
                        {
                            if (hiddenFilterMask.Trim().Length > 0)
                            {
                                if (!FilterAPI.AddHiddenFileMaskToFilterRule(filterRule.IncludeFileFilterMask, hiddenFilterMask.Trim()))
                                {
                                    EventManager.WriteMessage(567, "AddHiddenFileMaskToFilterRule", CommonObjects.EventLevel.Error, "AddHiddenFileMaskToFilterRule " + filterRule.IncludeFileFilterMask + " hiddenFilterMask:" + hiddenFilterMask + " failed:" + FilterAPI.GetLastErrorMessage());
                                }
                                else
                                {
                                    EventManager.WriteMessage(567, "AddHiddenFileMaskToFilterRule", CommonObjects.EventLevel.Information, "AddHiddenFileMaskToFilterRule " + filterRule.IncludeFileFilterMask + " hiddenFilterMask:" + hiddenFilterMask + " succeeded.");
                                }
                            }
                        }

                    }

                    //reparse the file open to another file with the filter mask.
                    //For example:
                    //FilterMask = c:\test\*txt
                    //ReparseFilterMask = d:\reparse\*doc
                    //If you open file c:\test\MyTest.txt, it will reparse to the file d:\reparse\MyTest.doc.

                    string reparseFileFilterMask = filterRule.ReparseFileFilterMasks;
                    if ((filterRule.AccessFlag & (uint)FilterAPI.AccessFlag.REPARSE_FILE_OPEN) > 0 && reparseFileFilterMask.Trim().Length > 0)
                    {
                        if (!FilterAPI.AddReparseFileMaskToFilterRule(filterRule.IncludeFileFilterMask, reparseFileFilterMask.Trim()))
                        {
                            EventManager.WriteMessage(791, "AddReparseFileMaskToFilterRule", CommonObjects.EventLevel.Error, "AddReparseFileMaskToFilterRule " + filterRule.IncludeFileFilterMask + " reparseFileFilterMask:" + reparseFileFilterMask + " failed:" + FilterAPI.GetLastErrorMessage());
                        }
                        else
                        {
                            EventManager.WriteMessage(791, "AddReparseFileMaskToFilterRule", CommonObjects.EventLevel.Information, "AddReparseFileMaskToFilterRule " + filterRule.IncludeFileFilterMask + " reparseFileFilterMask:" + reparseFileFilterMask + " succeeded.");
                        }

                    }
                }



                //below is the global setting.

                //if you send the include process Id to filter driver, then only the include processes can
                //apply to the filter rules, all other processes will be skipped.
                foreach (uint includedPid in includePidList)
                {
                    FilterAPI.AddIncludedProcessId(includedPid);
                }

                //if the exclude process list is not empty, all process in this list will be skipped by filter driver.
                foreach (uint excludedPid in excludePidList)
                {
                    uint currentPid = FilterAPI.GetCurrentProcessId();
                    FilterAPI.AddExcludedProcessId(currentPid);

                    FilterAPI.AddExcludedProcessId(excludedPid);
                }

                FilterAPI.RegisterIoRequest(requestIORegistration);

                foreach (uint protectPid in protectPidList)
                {
                    FilterAPI.AddProtectedProcessId(protectPid);
                }
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(502, "SendConfigSettingsToFilter", CommonObjects.EventLevel.Error, "Send config settings to filter failed with error " + ex.Message);
            }

           
        }
    }
}
