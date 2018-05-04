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
using System.Threading;
using System.Reflection;

using EaseFilter.CommonObjects;

namespace FileProtectorCS
{
    public class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] left, byte[] right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }
            if (left.Length != right.Length)
            {
                return false;
            }
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            int sum = 0;
            foreach (byte cur in key)
            {
                sum += cur;
            }
            return sum;
        }
    }

    public class FilterMessage : IDisposable
    {
        Thread messageThread = null;
        Queue<FilterAPI.MessageSendData> messageQueue = new Queue<FilterAPI.MessageSendData>();

        static Dictionary<string, DateTime> readFileCacheTable = new Dictionary<string, DateTime>();
        static Dictionary<string, DateTime> writeFileCacheTable = new Dictionary<string, DateTime>();
        static int cacheTimeOutInSeconds = 30;
        static System.Timers.Timer deleteCachedItemTimer = new System.Timers.Timer();

        delegate void LogFileEventDlg(FileEvent fileEvent);
        static event LogFileEventDlg OnFileEvent = new LogFileEventDlg(FileEventHandler.LogFileEvent);

        AutoResetEvent autoEvent = new AutoResetEvent(false);
        bool disposed = false;


        public FilterMessage()
        {
            deleteCachedItemTimer.Interval = cacheTimeOutInSeconds * 1000 / 4; //millisecond
            deleteCachedItemTimer.Start();
            deleteCachedItemTimer.Enabled = true;
            deleteCachedItemTimer.Elapsed += new System.Timers.ElapsedEventHandler(deleteCachedItemTimer_Elapsed);

            messageThread = new Thread(new ThreadStart(ProcessMessage));
            messageThread.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
            }

            autoEvent.Set();
            messageThread.Abort();
            disposed = true;
        }

        ~FilterMessage()
        {
            Dispose(false);
        }


        private static void deleteCachedItemTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                List<string> keysToRemove = new List<string>();

                foreach (KeyValuePair<string, DateTime> userItem in readFileCacheTable)
                {

                    TimeSpan tsSinceLastAccess = DateTime.Now - userItem.Value;

                    if (tsSinceLastAccess.TotalSeconds >= cacheTimeOutInSeconds)
                    {
                        EventManager.WriteMessage(124, "deleteCachedItemTimer_Elapsed", EventLevel.Verbose, "Remove read key " + userItem.Key);
                        keysToRemove.Add(userItem.Key);
                    }
                }

                foreach (string key in keysToRemove)
                {
                    lock (readFileCacheTable)
                    {
                        readFileCacheTable.Remove(key);
                    }
                }

                keysToRemove.Clear();

                foreach (KeyValuePair<string, DateTime> userItem in writeFileCacheTable)
                {

                    TimeSpan tsSinceLastAccess = DateTime.Now - userItem.Value;

                    if (tsSinceLastAccess.TotalSeconds >= cacheTimeOutInSeconds)
                    {
                        EventManager.WriteMessage(145, "deleteCachedItemTimer_Elapsed", EventLevel.Verbose, "Remove write key " + userItem.Key );

                        keysToRemove.Add(userItem.Key);
                    }
                }

                foreach (string key in keysToRemove)
                {
                    lock (writeFileCacheTable)
                    {
                        writeFileCacheTable.Remove(key);
                    }
                }
            }
            catch (System.Exception ex)
            {
                EventManager.WriteMessage(46, "deleteCachedItemTimer_Elapsed", EventLevel.Error, "Delete cached item failed with error:" + ex.Message);
            }

        }

        public void AddMessage(FilterAPI.MessageSendData messageSend)
        {
            lock (messageQueue)
            {
                if (messageQueue.Count > GlobalConfig.MaximumFilterMessages)
                {
                    messageQueue.Clear();
                }

                messageQueue.Enqueue(messageSend);
            }

            autoEvent.Set();


        }


        void ProcessMessage()
        {
           
            WaitHandle[] waitHandles = new WaitHandle[] { autoEvent, GlobalConfig.stopEvent };

            while (GlobalConfig.isRunning)
            {
                if (messageQueue.Count == 0)
                {
                    int result = WaitHandle.WaitAny(waitHandles);
                    if (!GlobalConfig.isRunning)
                    {
                        return;
                    }
                }

                while (messageQueue.Count > 0)
                {
                    FilterAPI.MessageSendData messageSend;

                    lock (messageQueue)
                    {
                        messageSend = (FilterAPI.MessageSendData)messageQueue.Dequeue();
                    }

                    var fileEvent = DecodeFilterMessage(messageSend);

                    if (null != fileEvent && null != OnFileEvent)
                    {
                        OnFileEvent(fileEvent);
                    }
                }

            }


        }

   

        FileEvent DecodeFilterMessage(FilterAPI.MessageSendData messageSend)
        {


            try
            {

                string userName = string.Empty;
                string processName = string.Empty;
                string fileName = messageSend.FileName;
                string description = string.Empty;
                FileAttributes fileAttributes = (FileAttributes)messageSend.FileAttributes;
                DateTime timestamp = DateTime.FromFileTime(messageSend.TransactionTime);
                FileEventResult result = (messageSend.Status == (uint)FilterAPI.NTSTATUS.STATUS_SUCCESS) ? FileEventResult.SUCCESS : FileEventResult.FAILURE;

                FilterAPI.DecodeUserName(messageSend.Sid, out userName);
                FilterAPI.DecodeProcessName(messageSend.ProcessId, out processName);

                FilterAPI.EVENTTYPE eventType = (FilterAPI.EVENTTYPE)messageSend.InfoClass;

                if ((eventType & FilterAPI.EVENTTYPE.RENAMED) == FilterAPI.EVENTTYPE.RENAMED)
                {
                    description = "file was renamed to " + Encoding.Unicode.GetString(messageSend.DataBuffer);
                    description = description.Substring(0, description.IndexOf('\0'));
                }


                if (eventType != FilterAPI.EVENTTYPE.NONE)
                {
                    FileEvent fileEvent = new FileEvent();

                    fileEvent.User = userName;
                    fileEvent.Process = processName;
                    fileEvent.Resource = fileName;
                    fileEvent.Result = result;
                    fileEvent.Timestamp = timestamp;
                    fileEvent.Type = eventType;
                    fileEvent.Description = description;
                    fileEvent.Attributes = fileAttributes;

                    return fileEvent;

                }

            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(296, "DecodeFilterMessage", EventLevel.Error, "Decode filter message failed because of error:" + ex.Message);
            }

            return null;
        }


      

    }
}