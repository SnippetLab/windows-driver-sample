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
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;

using EaseFilter.CommonObjects;

namespace FileMonitorService
{
    //public enum FileEventType
    //{
    //    NONE = 0,
    //    READ = 1,
    //    CREATE = 2,
    //    MODIFY = 4,
    //    DELETE = 8,
    //    RENAME = ,
    //    PERM
    //}

    public enum FileEventResult
    {
        SUCCESS = 0,
        FAILURE
    }

    public class FileEvent : EventArgs
    {
        private FileAttributes fileAttributes = FileAttributes.Normal;
        // type of the event -can be an enum
        private FilterAPI.EVENTTYPE type = FilterAPI.EVENTTYPE.NONE;
        // timestamp of event
        private DateTime timestamp = DateTime.Now;
        // full path of the resource (/ as path separator)
        private string resource = string.Empty;
        // user id
        private string user = string.Empty;
        // process name
        private string process = string.Empty;
        // result - can be an enum
        private FileEventResult result = FileEventResult.SUCCESS;

        private string description = string.Empty;

        public FileEvent()
        {
        }

        public FileEvent(string _user,
            string _process,
            string _fileName,
            FileAttributes _fileAttributes,
            FilterAPI.EVENTTYPE _type,
            DateTime _timestamp,
            FileEventResult _result,
            string _description)
        {
            this.user = _user;
            this.process = _process;
            this.resource = _fileName;
            this.fileAttributes = _fileAttributes;
            this.type = _type;
            this.timestamp = _timestamp;
            this.result = _result;
            this.description = _description;
        }

        /// <summary>
        ///  the file attributes
        /// </summary>
        public FileAttributes Attributes
        {
            get
            {
                return fileAttributes;
            }

            set
            {
                fileAttributes = value;
            }
        }

        /// <summary>
        ///  type of the event -can be an enum
        /// </summary>
        public FilterAPI.EVENTTYPE Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }


        /// <summary>
        ///   // timestamp of event
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
            }
        }

        /// <summary>
        /// Full path of the file name
        /// </summary>
        public string Resource
        {
            get
            {
                return resource;
            }

            set
            {
                resource = value;
            }
        }

        /// <summary>
        /// User name
        /// </summary>
        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        /// <summary>
        /// The process name
        /// </summary>
        public string Process
        {
            get
            {
                return process;
            }

            set
            {
                process = value;
            }
        }

        /// <summary>
        /// The status of the result
        /// </summary>
        public FileEventResult Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

   
        /// <summary>
        /// The description of the IO
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }

    public static class FileEventHandler
    {

        static Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
        static string assemblyPath = Path.GetDirectoryName(assembly.Location);

        static string filterMessageLogName = GlobalConfig.FilterMessageLogName;

        static string logFileName = Path.Combine(assemblyPath, filterMessageLogName);
        static string logFolder = Path.GetDirectoryName(logFileName);

        static FileEventHandler()
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

  
        }


        private static string GetLogMessage(FileEvent fileEvent)
        {
            string retVal = string.Empty;
          
            string fileType = "FILE";

            if ((fileEvent.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                fileType = "DIRECTORY";
            }

            string eventType = string.Empty;
            foreach (FilterAPI.EVENTTYPE type in Enum.GetValues(typeof(FilterAPI.EVENTTYPE)))
            {
                if ((fileEvent.Type & type) == type && type != FilterAPI.EVENTTYPE.NONE )
                {
                    if (eventType.Length > 0)
                    {
                        eventType = eventType + ',' + type.ToString();
                    }
                    else
                    {
                        eventType = type.ToString();
                    }
                }
            }

            //the log message format
            //File,Attributes,Time, Action, Success/Failure,Process, User, description

            retVal = fileEvent.Resource + "|" + fileType + "|" + fileEvent.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss") + "|";
            retVal += eventType + "|" + fileEvent.Result.ToString() + "|" + fileEvent.Process + "|";
            retVal += fileEvent.User + "|" + fileEvent.Description;

            return retVal;

        }

        public static void LogFileEvent(FileEvent fileEvent)
        {
            try
            {             
                string logMessage = GetLogMessage(fileEvent);

                if (logMessage.Trim().Length > 0)
                {
                    if (File.Exists(logFileName))
                    {
                        FileInfo fileInfo = new FileInfo(logFileName);

                        if (fileInfo.Length > GlobalConfig.FilterMessageLogFileSize)
                        {
                            File.Delete(logFileName + ".bak");
                            File.Move(logFileName, logFileName + ".bak");
                        }
                    }

                    File.AppendAllText(logFileName, logMessage + "\r\n");
                }
            }
            catch (Exception ex)
            {
                EventManager.WriteMessage(172, "LogTrasaction", EventLevel.Error, "Log filter message failed with error " + ex.Message);
            }

        }


    }
}