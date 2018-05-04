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
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Diagnostics;
using System.Management;
using System.Collections;
using System.Windows.Forms;

using EaseFilter.CommonObjects;

namespace SecureAgent
{
    public class CacheUserAccessInfo
    {
        public string index = string.Empty;
        public bool accessStatus = false;
        public DateTime lastAccessTime = DateTime.MinValue;
        public string iv = string.Empty;
        public string key = string.Empty;
        public AutoResetEvent syncEvent = new AutoResetEvent(true);

    }

    public class DRM
    {
        Dictionary<string, CacheUserAccessInfo> userAccessCache = new Dictionary<string, CacheUserAccessInfo>();
        int cacheTimeOutInSeconds = 120;
        System.Timers.Timer deleteCachedItemTimer = new System.Timers.Timer();

        public DRM()
        {
            deleteCachedItemTimer.Interval = cacheTimeOutInSeconds * 1000 / 4; //millisecond
            deleteCachedItemTimer.Start();
            deleteCachedItemTimer.Enabled = true;
            deleteCachedItemTimer.Elapsed += new System.Timers.ElapsedEventHandler(deleteCachedItemTimer_Elapsed);
        }

        private bool GetDRPolicyDataFromDataBuffer(Byte[] drDataBuffer, uint drBufferLength, ref DRPolicyData drPolicyData, ref string lastError)
        {
            Boolean retVal = false;

            try
            {

                MemoryStream ms = new MemoryStream(drDataBuffer);
                BinaryReader br = new BinaryReader(ms);

                drPolicyData.AESVerificationKey = br.ReadUInt32();

                if (FilterAPI.AES_TAG_KEY != drPolicyData.AESVerificationKey)
                {
                    lastError = "The DRPolicyData buffer was corrupted.";
                    return false;
                }

                drPolicyData.AESFlags = (AESFlags)br.ReadUInt32();
                drPolicyData.IVLength = br.ReadUInt32();
                drPolicyData.IV = br.ReadBytes(16);
                drPolicyData.EncryptionKeyLength = br.ReadUInt32();
                drPolicyData.EncryptionKey = br.ReadBytes(32);
                drPolicyData.CreationTime = br.ReadInt64();
                drPolicyData.ExpireTime = br.ReadInt64();
                drPolicyData.AccessFlags = br.ReadUInt32();
                drPolicyData.FileSize = br.ReadInt64();

                drPolicyData.LengthOfIncludeProcessNames = br.ReadUInt32();
                drPolicyData.OffsetOfIncludeProcessNames = br.ReadUInt32();
                drPolicyData.LengthOfExcludeProcessNames = br.ReadUInt32();
                drPolicyData.OffsetOfExcludeProcessNames = br.ReadUInt32();
                drPolicyData.LengthOfIncludeUserNames = br.ReadUInt32();
                drPolicyData.OffsetOfIncludeUserNames = br.ReadUInt32();
                drPolicyData.LengthOfExcludeUserNames = br.ReadUInt32();
                drPolicyData.OffsetOfExcludeUserNames = br.ReadUInt32();
                drPolicyData.LengthOfAccountName = br.ReadUInt32();
                drPolicyData.OffsetOfAccountName = br.ReadUInt32();
                drPolicyData.LengthOfComputerIds = br.ReadUInt32();
                drPolicyData.OffsetOfComputerIds = br.ReadUInt32();
                drPolicyData.LengthOfUserPassword = br.ReadUInt32();
                drPolicyData.OffsetOfUserPassword = br.ReadUInt32();

                if (drPolicyData.LengthOfIncludeProcessNames > 0 && drPolicyData.OffsetOfIncludeProcessNames > 0)
                {
                    ms.Position = drPolicyData.OffsetOfIncludeProcessNames;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfIncludeProcessNames);
                    drPolicyData.IncludeProcessNames = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfExcludeProcessNames > 0 && drPolicyData.OffsetOfExcludeProcessNames > 0)
                {
                    ms.Position = drPolicyData.OffsetOfExcludeProcessNames;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfExcludeProcessNames);
                    drPolicyData.ExcludeProcessNames = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfIncludeUserNames > 0 && drPolicyData.OffsetOfIncludeUserNames > 0)
                {
                    ms.Position = drPolicyData.OffsetOfIncludeUserNames;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfIncludeUserNames);
                    drPolicyData.IncludeUserNames = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfExcludeUserNames > 0 && drPolicyData.OffsetOfExcludeUserNames > 0)
                {
                    ms.Position = drPolicyData.OffsetOfExcludeUserNames;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfExcludeUserNames);
                    drPolicyData.ExcludeUserNames = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfAccountName > 0 && drPolicyData.OffsetOfAccountName > 0)
                {
                    ms.Position = drPolicyData.OffsetOfAccountName;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfAccountName);
                    drPolicyData.AccountName = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfComputerIds > 0 && drPolicyData.OffsetOfComputerIds > 0)
                {
                    ms.Position = drPolicyData.OffsetOfComputerIds;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfComputerIds);
                    drPolicyData.ComputerIds = UnicodeEncoding.Unicode.GetString(buffer);
                }

                if (drPolicyData.LengthOfUserPassword > 0 && drPolicyData.OffsetOfUserPassword > 0)
                {
                    ms.Position = drPolicyData.OffsetOfUserPassword;
                    byte[] buffer = br.ReadBytes((int)drPolicyData.LengthOfUserPassword);
                    drPolicyData.UserPassword = UnicodeEncoding.Unicode.GetString(buffer);
                }

                retVal = true;

            }
            catch (Exception ex)
            {
                retVal = false;
                lastError = "Get DRPolicyData failed with error " + ex.Message;
            }

            return retVal;
        }

        private bool GetAccessPermissionFromServer(FilterAPI.MessageSendData messageSend,
                                                            DRPolicyData drPolicyData,
                                                            string userName,
                                                            string processName,
                                                            string userPassword,
                                                            ref CacheUserAccessInfo cacheUserAccessInfo)
        {
            Boolean retVal = true;
            string fileName = messageSend.FileName;
            string lastError = string.Empty;

            try
            {
                UserInfo userInfo = new UserInfo();
                string keyStr = string.Empty;
                string ivStr = string.Empty;

                userInfo.FileName = Path.GetFileName(messageSend.FileName) + DigitalRightControl.SECURE_SHARE_FILE_EXTENSION;
                userInfo.AccountName = drPolicyData.AccountName;
                userInfo.ProcessName = processName;
                userInfo.UserName = userName;
                userInfo.UserPassword = userPassword;
                userInfo.CreationTime = drPolicyData.CreationTime;

                byte[] computerId = new byte[52];
                uint computerIdLength = (uint)computerId.Length;
                IntPtr computerIdPtr = Marshal.UnsafeAddrOfPinnedArrayElement(computerId, 0);
                retVal = FilterAPI.GetUniqueComputerId(computerIdPtr, ref computerIdLength);

                if (!retVal)
                {
                    string message = "Get computerId failed,return error:" + FilterAPI.GetLastErrorMessage();
                    EventManager.WriteMessage(366, "GetAccessPermissionFromServer", EventLevel.Error, message);

                    return retVal;
                }

                Array.Resize(ref computerId, (int)computerIdLength);

                userInfo.ComputerId = UnicodeEncoding.Unicode.GetString(computerId);

                string userInfoStr = DigitalRightControl.EncryptObjectToStr<UserInfo>(userInfo);

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                //retVal = WebFormServices.GetFileKey(userInfoStr, ref keyStr, ref ivStr, ref lastError);

                stopWatch.Stop();

                if (!retVal)
                {
                    string message = "Get file " + messageSend.FileName + " permission from server return error:" + lastError;
                    EventManager.WriteMessage(293, "GetAccessPermissionFromServer", EventLevel.Error, message);

                    return retVal;
                }
                else
                {
                    string message = "Get file " + messageSend.FileName + " permission frome server return succeed, spent " + stopWatch.ElapsedMilliseconds + " milliseconds.";
                    EventManager.WriteMessage(208, "GetAccessPermissionFromServer", EventLevel.Verbose, message);
                }

                cacheUserAccessInfo.key = keyStr;
                cacheUserAccessInfo.iv = ivStr;

            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(286, "GetAccessPermissionFromServer", EventLevel.Error, "Get file " + messageSend.FileName + "permission failed with exception:" + ex.Message);
                retVal = false;
            }

            return retVal;

        }

        public bool GetUserPermission(FilterAPI.MessageSendData messageSend, ref FilterAPI.MessageReplyData messageReply)
        {
            Boolean retVal = true;
            string userPassword = string.Empty;
            string fileName = messageSend.FileName;
            string lastError = string.Empty;
            string processName = string.Empty;
            string userName = string.Empty;
            bool isFirstAccess = false;
            CacheUserAccessInfo cacheUserAccessInfo = new CacheUserAccessInfo();

            try
            {

                FilterAPI.DecodeProcessName(messageSend.ProcessId, out processName);
                FilterAPI.DecodeUserName(messageSend.Sid, out userName);

                string index = (userName + "_" + processName + "_" + fileName).ToLower();

                //cache the same user/process/filename access.
                lock (userAccessCache)
                {
                    if (userAccessCache.ContainsKey(index))
                    {
                        cacheUserAccessInfo = userAccessCache[index];
                        EventManager.WriteMessage(446, "GetUserPermission", EventLevel.Verbose, "Thread" + Thread.CurrentThread.ManagedThreadId + ",userInfoKey " + index + " exists in the cache table.");
                    }
                    else
                    {
                        isFirstAccess = true;
                        cacheUserAccessInfo.index = index;
                        cacheUserAccessInfo.lastAccessTime = DateTime.Now;
                        userAccessCache.Add(index, cacheUserAccessInfo);
                        EventManager.WriteMessage(435, "GetUserPermission", EventLevel.Verbose, "Thread" + Thread.CurrentThread.ManagedThreadId + ",add userInfoKey " + index + " to the cache table.");
                    }
                }

                //synchronize the same file access.
                if (!cacheUserAccessInfo.syncEvent.WaitOne(new TimeSpan(0, 0, (int)GlobalConfig.ConnectionTimeOut)))
                {
                    string info = "User name: " + userName + ",processname:" + processName + ",file name:" + fileName + " wait for permission timeout.";
                    EventManager.WriteMessage(402, "GetUserPermission", EventLevel.Warning, info);
                }

                TimeSpan timeSpan = DateTime.Now - cacheUserAccessInfo.lastAccessTime;

                if (!isFirstAccess && timeSpan.TotalSeconds < cacheTimeOutInSeconds)
                {                                     
                    //the access was cached, return the last access status.
                    retVal = cacheUserAccessInfo.accessStatus;

                    string info =  "thread" + Thread.CurrentThread.ManagedThreadId + ",  Cached userInfoKey " + index + " in the cache table,return " + retVal;
                    EventManager.WriteMessage(451, "GetUserPermission", EventLevel.Verbose, info);

                    return retVal;
                }


                DRPolicyData drPolicyData = new DRPolicyData();
                retVal = GetDRPolicyDataFromDataBuffer(messageSend.DataBuffer, messageSend.Length, ref drPolicyData, ref lastError);
                if (!retVal)
                {
                    EventManager.WriteMessage(258, "GetUserPermission", EventLevel.Error, "Process encrypted file failed because of error:" + lastError);
                }
                else
                {
                    if ((drPolicyData.AESFlags & AESFlags.Flags_Enabled_Check_User_Password) == AESFlags.Flags_Enabled_Check_User_Password)
                    {
                        string messageInfo = "User name: " + userName + ",processname:" + processName + ",file name:" + fileName + "\n\n Enter password in password windows.";
                        EventManager.WriteMessage(301, "Request user password.", EventLevel.Verbose,messageInfo);

                        UserPasswordForm userPasswordForm = new UserPasswordForm(userName, processName, fileName);
                        userPasswordForm.BringToFront();
                        userPasswordForm.Focus();
                        userPasswordForm.TopMost = true;

                        if (userPasswordForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            userPassword = userPasswordForm.userPassword;
                        }
                    }

                    if ((drPolicyData.AESFlags & AESFlags.Flags_Enabled_Revoke_Access_Control) == AESFlags.Flags_Enabled_Revoke_Access_Control)
                    {
                        retVal = GetAccessPermissionFromServer(messageSend, drPolicyData, userName, processName, userPassword, ref cacheUserAccessInfo);
                    }
                    else
                    {
                        if (drPolicyData.UserPassword.Length > 0)
                        {
                            if (!string.Equals(userPassword, drPolicyData.UserPassword))
                            {
                                retVal = false;
                            }
                        }
                    }
                }
               
                cacheUserAccessInfo.accessStatus = retVal;

            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(340, "GetUserPermission", EventLevel.Error, "filter callback exception." + ex.Message);
                retVal = false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(cacheUserAccessInfo.key))
                {
                    byte[] encryptKey = Utils.ConvertHexStrToByteArray(cacheUserAccessInfo.key);
                    byte[] encryptIV = Utils.ConvertHexStrToByteArray(cacheUserAccessInfo.iv);


                    //write the iv and key to the reply data buffer with format FilterAPI.AESDataBuffer
                    MemoryStream ms = new MemoryStream(messageReply.DataBuffer);
                    BinaryWriter bw = new BinaryWriter(ms);
                    bw.Write(encryptIV);
                    bw.Write(encryptKey.Length);
                    bw.Write(encryptKey);

                    messageReply.DataBufferLength = (uint)ms.Length;
                }

                cacheUserAccessInfo.lastAccessTime = DateTime.Now;
                cacheUserAccessInfo.syncEvent.Set();

       
            }

            return retVal;

        }

        private void deleteCachedItemTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                List<string> keysToRemove = new List<string>();

                foreach (KeyValuePair<string, CacheUserAccessInfo> userItem in userAccessCache)
                {

                    TimeSpan tsSinceLastAccess = DateTime.Now - userItem.Value.lastAccessTime;

                    if (tsSinceLastAccess.TotalSeconds >= cacheTimeOutInSeconds)
                    {
                        keysToRemove.Add(userItem.Key);
                    }
                }

                foreach (string key in keysToRemove)
                {
                    lock (userAccessCache)
                    {
                        userAccessCache.Remove(key);

                        EventManager.WriteMessage(573, "deleteCachedItemTimer_Elapsed", EventLevel.Verbose, "Delete cached item " + key);
                    }
                }
            }
            catch (System.Exception ex)
            {
                EventManager.WriteMessage(46, "deleteCachedItemTimer_Elapsed", EventLevel.Error, "Delete cached item failed with error:" + ex.Message);
            }

        }
    }
}
