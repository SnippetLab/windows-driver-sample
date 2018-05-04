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
   
    public class FilterWorker
    {
       
        public FilterMessage filterMessage = null;
        DRM digitalRightsManagement = null;

        public FilterWorker(ListView listView_Info)
        {
            this.filterMessage = new FilterMessage(listView_Info);
         
        }

        public bool StartService(ref string lastError)
        {
            //Purchase a license key with the link: http://www.easefilter.com/Order.htm
            //Email us to request a trial key: info@easefilter.com //free email is not accepted.
            string registerKey = GlobalConfig.registerKey;

            bool ret = false;
            lastError = string.Empty;

            try
            {
               
                ret = FilterAPI.StartFilter((int)GlobalConfig.FilterConnectionThreads
                                            , registerKey
                                            , new FilterAPI.FilterDelegate(FilterCallback)
                                            , new FilterAPI.DisconnectDelegate(DisconnectCallback)
                                            , ref lastError);
                if (!ret)
                {
                    lastError = "Start filter service failed with error " + lastError;
                    EventManager.WriteMessage(43, "StartFilter", EventLevel.Error, lastError);
                    return ret;
                }

                if (GlobalConfig.FilterRules.Count == 0)
                {
                    FilterRule filterRule = new FilterRule();
                    filterRule.IncludeFileFilterMask = "c:\\test\\*";
                    filterRule.ExcludeFileFilterMasks = "c:\\windows*";
                    filterRule.EventType = (uint)(FilterAPI.EVENTTYPE.WRITTEN | FilterAPI.EVENTTYPE.CREATED | FilterAPI.EVENTTYPE.DELETED | FilterAPI.EVENTTYPE.RENAMED);
                    filterRule.AccessFlag = (uint)FilterAPI.ALLOW_MAX_RIGHT_ACCESS;
                    GlobalConfig.FilterRules.Add(filterRule.IncludeFileFilterMask, filterRule);

                    MessageBox.Show("You don't have any monitor folder setup, add c:\\test\\* as your default test folder, I/Os from c:\\test\\* will show up in the console.");
                }


                GlobalConfig.SendConfigSettingsToFilter();             

                EventManager.WriteMessage(102, "StartFilter", EventLevel.Information, "Start filter service succeeded.");
            }
            catch (Exception ex)
            {
                lastError = "Start filter service failed with error " + ex.Message;
                EventManager.WriteMessage(104, "StartFilter", EventLevel.Error, lastError);
            }

            return ret;
        }

        static void DisconnectCallback()
        {
            EventManager.WriteMessage(697, "DisconnectCallback", EventLevel.Information, "Filter Disconnected." + FilterAPI.GetLastErrorMessage());
        }


        public  bool StopService()
        {
            FilterAPI.StopFilter();
            GlobalConfig.Stop();

            return true;
        }

        public void ClearMessage()
        {
            filterMessage.InitListView();
        }

        private  Boolean FilterCallback(IntPtr sendDataPtr, IntPtr replyDataPtr)
        {
            Boolean ret = true;

            try
            {
                FilterAPI.MessageSendData messageSend = new FilterAPI.MessageSendData();
                messageSend = (FilterAPI.MessageSendData)Marshal.PtrToStructure(sendDataPtr, typeof(FilterAPI.MessageSendData));

                if (FilterAPI.MESSAGE_SEND_VERIFICATION_NUMBER != messageSend.VerificationNumber)
                {
                    EventManager.WriteMessage(139, "FilterCallback", EventLevel.Error, "Received message corrupted.Please check if the MessageSendData structure is correct.");
                    return false;
                }

                ret = FilterRequestHandler(ref messageSend, replyDataPtr);

               
                string info = "SecureAgent process request " + FilterMessage.FormatIOName(messageSend) + ",pid:" + messageSend.ProcessId +
                              " ,filename:" + messageSend.FileName + ",return status:" + FilterMessage.FormatStatus(messageSend.Status);

                if( messageSend.Status == (uint)NtStatus.Status.Success )
                {
                    ret = false;
                    EventManager.WriteMessage(98, "FilterCallback", EventLevel.Verbose, info);
                }
                else
                {
                    ret = true;
                    EventManager.WriteMessage(98, "FilterCallback", EventLevel.Error, info);
                }

                return ret;
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(134, "FilterCallback", EventLevel.Error, "filter callback exception." + ex.Message);
                return false;
            }

        }

        private  bool FilterRequestHandler(ref FilterAPI.MessageSendData messageSend, IntPtr replyDataPtr)
        {
            Boolean retVal = true;
            string fileName = messageSend.FileName;
            string lastError = string.Empty;

            try
            {
              
                if ((replyDataPtr.ToInt64() != 0))
                {
                    FilterAPI.MessageReplyData messageReply = (FilterAPI.MessageReplyData)Marshal.PtrToStructure(replyDataPtr, typeof(FilterAPI.MessageReplyData));
                    messageReply.MessageId = messageSend.MessageId;
                    messageReply.MessageType = messageSend.MessageType;

                    if (    messageSend.MessageType == (uint)FilterAPI.FilterCommand.FILTER_REQUEST_USER_PERMIT
                        ||  messageSend.MessageType == (uint)FilterAPI.FilterCommand.FILTER_REQUEST_ENCRYPTION_IV_AND_KEY)
                    {
                        if (null == digitalRightsManagement)
                        {
                            digitalRightsManagement = new DRM();
                        }

                        //get permission for secure shared file
                        retVal = digitalRightsManagement.GetUserPermission(messageSend, ref messageReply);                      

                    }
                    else
                    {
                        //control the file I/O here
                        retVal = FilterService.IOAccessControl(messageSend, ref messageReply);
                    }

                    if (retVal)
                    {
                        messageReply.ReturnStatus = (uint)FilterAPI.NTSTATUS.STATUS_SUCCESS;
                    }
                    else
                    {
                        //comple the PRE_CREATE,user get access denied for the file open.
                        messageReply.ReturnStatus = (uint)FilterAPI.NTSTATUS.STATUS_ACCESS_DENIED;
                        messageReply.FilterStatus = (uint)FilterAPI.FilterStatus.FILTER_COMPLETE_PRE_OPERATION;
                    }

                    Marshal.StructureToPtr(messageReply, replyDataPtr, true);

                    if (!retVal)
                    {
                        messageSend.Status = (uint)FilterAPI.NTSTATUS.STATUS_ACCESS_DENIED;
                    }
                }
              

                filterMessage.AddMessage(messageSend);

                return retVal;
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(134, "FilterRequestHandler", EventLevel.Error, "filter callback exception." + ex.Message);
                return false;
            }
        }

       
    }


}
