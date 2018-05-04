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
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Cryptography;


namespace EaseFilter.CommonObjects
{

    static public class FilterAPI
    {
        public delegate Boolean FilterDelegate(IntPtr sendData, IntPtr replyData);
        public delegate void DisconnectDelegate();
        static GCHandle gchFilter;
        static GCHandle gchDisconnect;
        static bool isFilterStarted = false;
        public const int MAX_FILE_NAME_LENGTH = 1024;
        public const int MAX_SID_LENGTH = 256;
        public const int MAX_MESSAGE_LENGTH = 65536;
        public const int MAX_PATH = 260;
        public const int MAX_ERROR_MESSAGE_SIZE = 1024;
        public const uint MESSAGE_SEND_VERIFICATION_NUMBER = 0xFF000001;        
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint AES_TAG_KEY = 0xccb76e80;

        public const uint FILE_FLAG_OPEN_REPARSE_POINT =  0x00200000;
        public const uint FILE_FLAG_OPEN_NO_RECALL =  0x00100000;
        public const uint FILE_FLAG_NO_BUFFERING =  0x20000000;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT =  (uint)FileAttributes.ReparsePoint;

        static Dictionary<byte[], string> userNameTable = new Dictionary<byte[], string>(new ByteArrayComparer());
        static Dictionary<uint, string> processNameTable = new Dictionary<uint, string>();

        //for encryption default IV key
        public static byte[] DEFAULT_IV_TAG = { 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff };

        public enum FilterType : uint
        {
            FILE_SYSTEM_MONITOR = 0,
            FILE_SYSTEM_CONTROL = 1,
            FILE_SYSTEM_ENCRYPTION = 2,
            FILE_SYSTEM_CONTROL_ENCRYPTION = 3,
            FILE_SYSTEM_MONITOR_ENCRYPTION = 4,
            FILE_SYSTEM_EASE_FILTER_ALL = 5,
            FILE_SYSTEM_MONITOR_CONTROL = 0x8,
        }

        public enum BooleanConfig : uint
        {
            ENABLE_NO_RECALL_FLAG = 0x00000001, //for easetag, if it is true, after the reparsepoint file was opened, it won't restore data back for read and write.
            DISABLE_FILTER_UNLOAD_FLAG = 0x00000002, //if it is true, the filter driver can't be unloaded.
            ENABLE_SET_OFFLINE_FLAG = 0x00000004, //for virtual file, it will set offline attribute if it is true.
            ENABLE_DEFAULT_IV_TAG = 0x00000008, //for encryption, it is true, it will use the default IV tag to encrypt the files.
            ENABLE_PROTECTION_IN_BOOT_TIME = 0x00000080,
        }


        public enum FilterCommand
        {
            FILTER_SEND_FILE_CHANGED_EVENT = 0x00010001,
            FILTER_REQUEST_USER_PERMIT = 0x00010002,
            FILTER_REQUEST_ENCRYPTION_KEY = 0x00010003,
            FILTER_REQUEST_ENCRYPTION_IV_AND_KEY = 0x00010004,

        }

        /// <summary>
        /// the message type of the filter driver send request 
        /// </summary>
        public enum MessageType : uint
        {           
            PRE_CREATE = 0x00000001,
            POST_CREATE = 0x00000002,
            PRE_FASTIO_READ = 0x00000004,
            POST_FASTIO_READ = 0x00000008,
            PRE_CACHE_READ = 0x00000010,
            POST_CACHE_READ = 0x00000020,
            PRE_NOCACHE_READ = 0x00000040,
            POST_NOCACHE_READ = 0x00000080,
            PRE_PAGING_IO_READ = 0x00000100,
            POST_PAGING_IO_READ = 0x00000200,
            PRE_FASTIO_WRITE = 0x00000400,
            POST_FASTIO_WRITE = 0x00000800,
            PRE_CACHE_WRITE = 0x00001000,
            POST_CACHE_WRITE = 0x00002000,
            PRE_NOCACHE_WRITE = 0x00004000,
            POST_NOCACHE_WRITE = 0x00008000,
            PRE_PAGING_IO_WRITE = 0x00010000,
            POST_PAGING_IO_WRITE = 0x00020000,
            PRE_QUERY_INFORMATION = 0x00040000,
            POST_QUERY_INFORMATION = 0x00080000,
            PRE_SET_INFORMATION = 0x00100000,
            POST_SET_INFORMATION = 0x00200000,
            PRE_DIRECTORY = 0x00400000,
            POST_DIRECTORY = 0x00800000,
            PRE_QUERY_SECURITY = 0x01000000,
            POST_QUERY_SECURITY = 0x02000000,
            PRE_SET_SECURITY = 0x04000000,
            POST_SET_SECURITY = 0x08000000,
            PRE_CLEANUP = 0x10000000,
            POST_CLEANUP = 0x20000000,
            PRE_CLOSE = 0x40000000,
            POST_CLOSE = 0x80000000,

        }


        public enum EVENTTYPE : uint
        {
            NONE = 0,
            CREATED = 0x00000020,
            WRITTEN = 0x00000040,
            RENAMED = 0x00000080,
            DELETED = 0x00000100,
            SECURITY_CHANGED = 0x00000200,
            INFO_CHANGED = 0x00000400,
            READ = 0x00000800,
        }

        public enum NTSTATUS : uint
        {
            STATUS_SUCCESS = 0,
            STATUS_UNSUCCESSFUL = 0xc0000001,
            STATUS_ACCESS_DENIED = 0xC0000022,
        }


        public const uint ALLOW_MAX_RIGHT_ACCESS = 0xfffffff0;

        /// <summary>
        /// control the access of the files
        /// </summary>
        public enum AccessFlag : uint
        {
            /// <summary>
            /// Filter driver will skip all the IO if the file name match the include file mask.
            /// </summary>
            EXCLUDE_FILTER_RULE = 0X00000000,
            /// <summary>
            /// Block the file open. 
            /// </summary>
            EXCLUDE_FILE_ACCESS = 0x00000001,
            /// <summary>
            /// Reparse the file open to the new file name if the reparse file mask was added.
            /// </summary>
            REPARSE_FILE_OPEN = 0x00000002,
            /// <summary>
            /// Hide the files from the folder directory list if the hide file mask was added.
            /// </summary>
            HIDE_FILES_IN_DIRECTORY_BROWSING = 0x00000004,
            /// <summary>
            /// Enable the transparent file encryption if the encryption key was added.
            /// </summary>
            FILE_ENCRYPTION_RULE = 0x00000008,
            /// <summary>
            /// Allow the file open to access the file's security information.
            /// </summary>
            ALLOW_OPEN_WTIH_ACCESS_SYSTEM_SECURITY = 0x00000010,
            /// <summary>
            /// Allow the file open for read access.
            /// </summary>
            ALLOW_OPEN_WITH_READ_ACCESS = 0x00000020,
            /// <summary>
            /// Allow the file open for write access.
            /// </summary>
            ALLOW_OPEN_WITH_WRITE_ACCESS = 0x00000040,
            /// <summary>
            /// Allow the file open for create new file or overwrite access.
            /// </summary>
            ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS = 0x00000080,
            /// <summary>
            /// Allow the file open for delete.
            /// </summary>
            ALLOW_OPEN_WITH_DELETE_ACCESS = 0x00000100,
            /// <summary>
            /// Allow to read the file data.
            /// </summary>
            ALLOW_READ_ACCESS = 0x00000200,
            /// <summary>
            /// Allow write data to the file.
            /// </summary>
            ALLOW_WRITE_ACCESS = 0x00000400,
            /// <summary>
            /// Allow to query file information.
            /// </summary>
            ALLOW_QUERY_INFORMATION_ACCESS = 0x00000800,
           /// <summary>
           /// Allow to change the file information:file attribute,file size,file name,delete file
           /// </summary>
            ALLOW_SET_INFORMATION = 0x00001000,
            /// <summary>
            /// Allow to rename the file.
            /// </summary>
            ALLOW_FILE_RENAME = 0x00002000,
            /// <summary>
            /// Allow to delete the file.
            /// </summary>
            ALLOW_FILE_DELETE = 0x00004000,
            /// <summary>
            /// Allow to change file size.
            /// </summary>
            ALLOW_FILE_SIZE_CHANGE = 0x00008000,
            /// <summary>
            /// Allow query the file security information.
            /// </summary>
            ALLOW_QUERY_SECURITY_ACCESS = 0x00010000,
            /// <summary>
            /// Allow change the file security information.
            /// </summary>
            ALLOW_SET_SECURITY_ACCESS = 0x00020000,
            /// <summary>
            /// Allow to browse the directory file list.
            /// </summary>
            ALLOW_DIRECTORY_LIST_ACCESS = 0x00040000,
            /// <summary>
            /// Allow the remote access via share folder.
            /// </summary>
            ALLOW_FILE_ACCESS_FROM_NETWORK = 0x00080000,
            /// <summary>
            /// Allow to encrypt the new file if the encryption filter rule is enabled.
            /// </summary>
            ALLOW_NEW_FILE_ENCRYPTION = 0x00100000,
            /// <summary>
            /// Allow the application to create a new file after it opened the protected file.
            /// </summary>
            ALLOW_ALL_SAVE_AS = 0x00200000,
            /// <summary>
            /// Allow the application in the inlcude process list to create a new file after it opened the protected file.
            /// </summary>
            ALLOW_INCLUDE_PROCESS_SAVE_AS = 0x00400000,
            /// <summary>
            /// Allow the file to be executed.
            /// </summary>
            ALLOW_FILE_MEMORY_MAPPED = 0x00800000,
            /// <summary>
            /// If it is not exclude filter rule,the access flag can't be 0, at least you need to include this flag
            /// for filter driver to process this filter rule.
            /// </summary>
            LAST_ACCESS_FLAG = 0xf0000000,
          //  ALLOW_MAX_RIGHT_ACCESS = 0xfffffff0,
        }


        /// <summary>
        /// for encrypted file digital access rights
        /// </summary>
        public enum SecureFileAccessRights : uint
        {
            ENABLE_REVOKE_ACCESS_CONTROL = 1,
            ENABLE_SAVE_AS = 2,
            ENABLE_EDITING = 4,            
        }
     
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MessageSendData
        {
            public uint MessageId;          //this is the request sequential number.
            public IntPtr FileObject;       //the address of FileObject,it is equivalent to file handle,it is unique per file stream open.
            public IntPtr FsContext;        //the address of FsContext,it is unique per file.
            public uint MessageType;        //the I/O request type.
            public uint ProcessId;          //the process ID for the process associated with the thread that originally requested the I/O operation.
            public uint ThreadId;           //the thread ID which requested the I/O operation.
            public long Offset;             //the read/write offset.
            public uint Length;             //the read/write length.
            public long FileSize;           //the size of the file for the I/O operation.
            public long TransactionTime;    //the transaction time in UTC of this request.
            public long CreationTime;       //the creation time in UTC of the file.
            public long LastAccessTime;     //the last access time in UTC of the file.
            public long LastWriteTime;      //the last write time in UTC of the file.
            public uint FileAttributes;     //the file attributes.
            public uint DesiredAccess;      //the DesiredAccess for file open, please reference CreateFile windows API.
            public uint Disposition;        //the Disposition for file open, please reference CreateFile windows API.
            public uint SharedAccess;       //the SharedAccess for file open, please reference CreateFile windows API.
            public uint CreateOptions;      //the CreateOptions for file open, please reference CreateFile windows API.
            public uint CreateStatus;       //the CreateStatus after file was openned, please reference CreateFile windows API.
            public uint InfoClass;          //the information class or security information
            public uint Status;             //the I/O status which returned from file system.
            public uint FileNameLength;     //the file name length in byte.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_FILE_NAME_LENGTH)]
            public string FileName;         //the file name of the I/O operation.
            public uint SidLength;          //the length of the security identifier.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SID_LENGTH)]
            public byte[] Sid;              //the security identifier data.
            public uint DataBufferLength;   //the data buffer length.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MESSAGE_LENGTH)]
            public byte[] DataBuffer;       //the data buffer which contains read/write/query information/set information data.
            public uint VerificationNumber; //the verification number which verifiys the data structure integerity.
        }

        public enum FilterStatus : uint
        {
            FILTER_MESSAGE_IS_DIRTY = 0x00000001,           //the data buffer was updated.
            FILTER_COMPLETE_PRE_OPERATION = 0x00000002,     //ONLY FOR PRE CALL OPERATION,the IO won't pass down to the lower drivers and file system.
            FILTER_DATA_BUFFER_IS_UPDATED = 0x00000004,     //only for pre create,to reparse the file open to the new file name.	
            BLOCK_DATA_WAS_RETURNED = 0x00000008,           //Set this flag if return read block databuffer to filter.
            CACHE_FILE_WAS_RETURNED = 0x00000010,           //Set this flag if the stub file was restored.
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct AESDataBuffer
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] EncryptionIV;
            public uint EncryptionKeyLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] EncryptionKey;
		} 

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MessageReplyData
        {
            public uint MessageId;
            public uint MessageType;
            public uint ReturnStatus;
            public uint FilterStatus;
            public uint DataBufferLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65536)]
            public byte[] DataBuffer;
        }

        /// <summary>
        /// set the filter driver boolean config setting based on the enum booleanConfig
        /// </summary>
        /// <param name="booleanConfig"></param>
        /// <returns></returns>
        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool SetBooleanConfig(uint booleanConfig);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool InstallDriver();

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool UnInstallDriver();

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool IsDriverServiceRunning();

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool SetRegistrationKey([MarshalAs(UnmanagedType.LPStr)]string key);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool Disconnect();

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool GetLastErrorMessage(
            [MarshalAs(UnmanagedType.LPWStr)] 
            string lastError,
            ref int messageLength);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RegisterMessageCallback(
            int threadCount,
            IntPtr filterCallback,
            IntPtr disconnectCallback);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool ResetConfigData();

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool SetFilterType(uint filterType);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool SetConnectionTimeout(uint timeOutInSeconds);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddNewFilterRule(
         uint accessFlag,
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
         bool isResident );

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddEncryptionKeyToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
         uint encryptionKeyLength,
         byte[] encryptionKey);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddExcludeFileMaskToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string excludeFileFilterMask);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddHiddenFileMaskToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string hiddenFileFilterMask);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddReparseFileMaskToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string reparseFileFilterMask);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddIncludeProcessNameToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string processName);
        //process name format:  notepad.exe

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddExcludeProcessNameToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string processName);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddIncludeProcessIdToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        uint includeProcessId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddExcludeProcessIdToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        uint excludeProcessId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddIncludeUserNameToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string userName);
        //process name format:  notepad.exe

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddExcludeUserNameToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string userName);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RegisterEventTypeToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        uint eventType);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RegisterMoinitorIOToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        uint registerIO);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RegisterControlIOToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        uint registerIO);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddProcessRightsToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string processName,
        uint accessFlags);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddUserRightsToFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)]string filterMask,
        [MarshalAs(UnmanagedType.LPWStr)]string userName,
        uint accessFlags);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveFilterRule(
        [MarshalAs(UnmanagedType.LPWStr)] string filterMask);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddIncludedProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveIncludeProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddExcludedProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveExcludeProcessId(uint processId);

        /// <summary>
        /// protect the process  being terminated, only support OS vista or later version.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddProtectedProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveProtectedProcessId(uint processId);

        /// <summary>
        /// block the process with the pid creating the new file.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddBlockSaveAsProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveBlockSaveAsProcessId(uint processId);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RegisterIoRequest(uint requestRegistration);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool GetFileHandleInFilter(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             uint dwDesiredAccess,
             ref IntPtr fileHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool ConvertSidToStringSid(
            [In] IntPtr sid,
            [Out] out IntPtr sidString);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("kernel32", SetLastError = true)]
        public static extern uint GetCurrentProcessId();

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int QueryDosDeviceW(
        [MarshalAs(UnmanagedType.LPWStr)]string dosName,
        [MarshalAs(UnmanagedType.LPWStr)]ref string volumeName,
        int volumeNameLength);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        private static extern bool CreateFileAPI(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
              uint dwDesiredAccess,
              uint dwShareMode,
              uint dwCreationDisposition,
              uint dwFlagsAndAttributes,
              ref IntPtr fileHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetFileTime(SafeFileHandle hFile,
                                        [In] ref long lpCreationTime,
                                        [In] ref long lpLastAccessTime,
                                        [In] ref long lpLastWriteTime);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        private static extern bool CreateStubFile(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             long fileSize,  //if it is 0 and the file exist,it will use the current file size.
              uint fileAttributes, //if it is 0 and the file exist, it will use the current file attributes.
              uint tagDataLength, //if it is 0, then no reparsepoint will be created.
              IntPtr tagData,
              bool overwriteIfExist,
              ref IntPtr fileHandle);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool OpenStubFile(
            [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             FileAccess access,
             FileShare share,
             ref IntPtr fileHandle);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        private static extern bool QueryAllocatedRanges(
                IntPtr fileHandle,
                long queryOffset,
                long queryLength,
                IntPtr allocatedRangesBuffer,
                int allocatedRangesBufferSize,
                ref uint returnedLength);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        private static extern bool AESEncryptDecryptBuffer(
                IntPtr inputBuffer,
                IntPtr outputBuffer,
                uint bufferLength,
                long offset,
                byte[] encryptionKey,
                uint keyLength,
                byte[] iv,
                uint ivLength);


        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AESEncryptFile(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             uint keyLength,
             byte[] encryptionKey,
             uint ivLength,
             byte[] iv,
             bool addIVTag);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AESDecryptFile(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             uint keyLength,
             byte[] encryptionKey,
             uint ivLength,
             byte[] iv);


        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AESEncryptFileToFile(
             [MarshalAs(UnmanagedType.LPWStr)]string sourceFileName,
             [MarshalAs(UnmanagedType.LPWStr)]string destFileName,
             uint keyLength,
             byte[] encryptionKey,
             uint ivLength,
             byte[] iv,
             bool addIVTag);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AESDecryptFileToFile(
             [MarshalAs(UnmanagedType.LPWStr)]string sourceFileName,
             [MarshalAs(UnmanagedType.LPWStr)]string destFileName,
             uint keyLength,
             byte[] encryptionKey,
             uint ivLength,
             byte[] iv);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool ProcessEncryptedFile(
        [MarshalAs(UnmanagedType.LPWStr)]string sourceFileName,
        [MarshalAs(UnmanagedType.LPWStr)]string destFileName);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddIVAndExpireTimeTag(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             uint ivLength,
             byte[] iv,
             long expireTime);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddReparseTagData(
            [MarshalAs(UnmanagedType.LPWStr)]string fileName,
            int tagDataLength,
            IntPtr tagData);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool RemoveTagData(
              IntPtr fileHandle);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool AddTagData(
              IntPtr fileHandle,
              int tagDataLength,
              IntPtr tagData);

        public static bool EmbedDRPolicyDataToFile(
              string fileName,
              byte[] drPolicyData,
              out string lastError )
        {

            bool ret = false;
            lastError = string.Empty;

            try
            {              
                GCHandle pinnedArray = GCHandle.Alloc(drPolicyData, GCHandleType.Pinned);
                IntPtr pointer = pinnedArray.AddrOfPinnedObject();

                ret = AddReparseTagData(fileName, drPolicyData.Length, pointer);

                pinnedArray.Free();

                if (!ret)
                {
                    lastError = GetLastErrorMessage();
                }
            }
            catch (Exception ex)
            {
                ret = false;
                lastError = ex.Message;
            }
          
            return ret;

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Return true if it succeeds to check the iv tag, if ivLenght > 0, it returns ivTag, or there are no ivTag data.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ivLength"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        [DllImport("FilterAPI.dll", SetLastError = true)]
        private static extern bool GetIVTag(
             [MarshalAs(UnmanagedType.LPWStr)]string fileName,
             ref uint ivLength,
             IntPtr iv);

        /// <summary>
        /// the buffer length has to be 36 or more.
        /// </summary>
        /// <param name="outputBuffer"></param>
        /// <param name="outputBufferLength"></param>
        /// <returns></returns>
        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool GetUniqueComputerId(
                IntPtr outputBuffer,
                ref uint outputBufferLength);

        [DllImport("FilterAPI.dll", SetLastError = true)]
        public static extern bool ActivateLicense(
                IntPtr outputBuffer,
                uint outputBufferLength);

        public enum EncryptType
        {
            Decryption = 0,
            Encryption ,
        }

        public static bool GetUniqueComputerId(ref string myComputerId,ref string lastError)
        {
            bool retVal = false;
            byte[] computerId = new byte[52];
            GCHandle gcHandle = GCHandle.Alloc(computerId, GCHandleType.Pinned);

            try
            {
                uint computerIdLength = (uint)computerId.Length;
                IntPtr computerIdPtr = Marshal.UnsafeAddrOfPinnedArrayElement(computerId, 0);
                retVal = FilterAPI.GetUniqueComputerId(computerIdPtr, ref computerIdLength);

                if (!retVal || computerIdLength <= 0)
                {
                    lastError = GetLastErrorMessage();
                    return false;
                }

                Array.Resize(ref computerId, (int)computerIdLength);
                myComputerId = UnicodeEncoding.Unicode.GetString(computerId);

                return true;
            }
            catch (Exception ex)
            {
                lastError = "Get computerId got exception,system return error:" + ex.Message;
                return false;
            }
            finally
            {
                gcHandle.Free();
            }

        }

        /// <summary>
        /// To open encrypted file without the filter driver interception, read the raw data with the return file handle.
        /// The caller is reponsible to close the file handle.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileHandle"></param>
        /// <param name="lastError"></param>
        /// <returns></returns>
        public static bool OpenRawEnCyptedFile(string fileName, out IntPtr fileHandle, out string lastError)
        {
            fileHandle = IntPtr.Zero;
            lastError = string.Empty;
            uint bypassFilterFileAttributes = FILE_FLAG_OPEN_REPARSE_POINT|FILE_FLAG_OPEN_NO_RECALL|FILE_FLAG_NO_BUFFERING|FILE_ATTRIBUTE_REPARSE_POINT;

            try
            {               
                if (!CreateFileAPI(fileName, (uint)FileAccess.Read, (uint)FileShare.None, (uint)FileMode.Open, bypassFilterFileAttributes, ref fileHandle))
                {
                    lastError = FilterAPI.GetLastErrorMessage();
                    return false;
                }
            }
            catch (Exception ex)
            {
                lastError = "OpenRawEnCyptedFile " + fileName + " got exception,system return error:" + ex.Message;
                return false;
            }

            return true;
        }


        public static bool GetIVTag(string fileName, ref byte[] iv, out string lastError)
        {
            bool ret = false;
            IntPtr tagPtr = IntPtr.Zero;
            uint ivLength = 16;

            lastError = string.Empty;

            tagPtr = Marshal.AllocHGlobal((int)ivLength);
            ret = GetIVTag(fileName, ref ivLength, tagPtr);

            if (!ret)
            {
                lastError = GetLastErrorMessage();
            }
            else if (ivLength > 0)
            {
                iv = new byte[ivLength];
                Marshal.Copy(tagPtr, iv, 0, (int)ivLength);
            }
            else
            {
                iv = new byte[0]; ;
            }

            if (tagPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(tagPtr);
            }

            return ret;
        }


   
        public static string AESEncryptDecryptStr(string inStr, EncryptType encryptType)
        {
           
            if (string.IsNullOrEmpty(inStr))
            {
               return string.Empty;
            }

            byte[] inbuffer = null;

            if (encryptType == EncryptType.Encryption)
            {
                inbuffer = ASCIIEncoding.UTF8.GetBytes(inStr);
            }
            else if (encryptType == EncryptType.Decryption)
            {
                inbuffer = Convert.FromBase64String(inStr);
            }
            else
            {
                throw new Exception("Failed to encrypt decrypt string, the encryptType " + encryptType.ToString() + " doesn't know.");
            }

            byte[] outBuffer = new byte[inbuffer.Length];

            GCHandle gcHandleIn = GCHandle.Alloc(inbuffer, GCHandleType.Pinned);
            GCHandle gcHandleOut = GCHandle.Alloc(outBuffer, GCHandleType.Pinned);

            IntPtr inBufferPtr = Marshal.UnsafeAddrOfPinnedArrayElement(inbuffer, 0);
            IntPtr outBufferPtr = Marshal.UnsafeAddrOfPinnedArrayElement(outBuffer, 0);

            try
            {
                bool retVal = AESEncryptDecryptBuffer(inBufferPtr, outBufferPtr, (uint)inbuffer.Length, 0, null, 0, null, 0);

                if (encryptType == EncryptType.Encryption)
                {
                    return Convert.ToBase64String(outBuffer);
                }
                else //if (encryptType == EncryptType.Decryption)
                {
                    return ASCIIEncoding.UTF8.GetString(outBuffer);
                }
            }
            finally
            {
                gcHandleIn.Free();
                gcHandleOut.Free();
            }

        }

    
        public static void AESEncryptDecryptBuffer(byte[] inbuffer, long offset, byte[] key, byte[] IV)
        {
            if (null == inbuffer || inbuffer.Length == 0)
            {
                throw new Exception("Failed to encrypt decrypt buffer, the input buffer can't be null");
            }

            GCHandle gcHandle = GCHandle.Alloc(inbuffer, GCHandleType.Pinned);

            try
            {
                IntPtr inBufferPtr = Marshal.UnsafeAddrOfPinnedArrayElement(inbuffer, 0);

                uint keyLength = 0;
                uint IVLength = 0;

                if (key != null)
                {
                    keyLength = (uint)key.Length;
                }

                if (IV != null)
                {
                    IVLength = (uint)IV.Length;
                }


                bool retVal = AESEncryptDecryptBuffer(inBufferPtr, inBufferPtr, (uint)inbuffer.Length, offset, key, keyLength, IV, IVLength);

                if (!retVal)
                {
                    throw new Exception("Failed to encrypt buffer, return error:" + GetLastErrorMessage());
                }
            }
            finally
            {
                gcHandle.Free();
            }

            return ;
        }

        public static bool DecodeUserName(byte[]sid, out string userName)
        {
            bool ret = true;

            IntPtr sidStringPtr = IntPtr.Zero;
            string sidString = string.Empty;

            userName = string.Empty;
            
            try
            {
                lock (userNameTable)
                {
                    //check the user name cache table
                    if (userNameTable.ContainsKey(sid))
                    {
                        userName = userNameTable[sid];
                        return ret;
                    }
                }

                IntPtr sidBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(sid, 0);

                if (FilterAPI.ConvertSidToStringSid(sidBuffer, out sidStringPtr))
                {
                    sidString = Marshal.PtrToStringAuto(sidStringPtr);
                    SecurityIdentifier secIdentifier = new SecurityIdentifier(sidString);
                    IdentityReference reference = secIdentifier.Translate(typeof(NTAccount));
                    userName = reference.Value;

                    lock (userNameTable)
                    {
                        //check the user name cache table
                        if (!userNameTable.ContainsKey(sid))
                        {
                            userNameTable.Add(sid,userName);
                        }
                    }
                }
                else
                {
                    string errorMessage = "Convert sid to sid string failed with error " + Marshal.GetLastWin32Error();
                    Console.WriteLine(errorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Convert sid to user name got exception:{0}", ex.Message));
                ret = false;

            }
            finally
            {
                if (sidStringPtr != null && sidStringPtr != IntPtr.Zero)
                {
                    FilterAPI.LocalFree(sidStringPtr);
                }
            }

            return ret;
        }

        public static bool DecodeProcessName(uint processId, out string processName)
        {
            bool ret = true;

            processName = string.Empty;

            try
            {
                System.Diagnostics.Process requestProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                processName = requestProcess.ProcessName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Convert pid to process name got exception:{0}", ex.Message));
                ret = false;

            }

            return ret;
        }


        public static string GetLastErrorMessage()
        {
            int len = 1024;
            string lastError = new string((char)0, len);

            if (!GetLastErrorMessage(lastError, ref len))
            {
                lastError = new string((char)0, len);
                if (!GetLastErrorMessage(lastError, ref len))
                {
                    return "failed to get last error message.";
                }
            }

            if (lastError.IndexOf((char)0) >= 0)
            {
                lastError = lastError.Substring(0, lastError.IndexOf((char)0));
            }

            return lastError;
        }

        static bool IsDriverChanged()
        {
            bool ret = false;

            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
                string localPath = Path.GetDirectoryName(assembly.Location);
                string driverName = Path.Combine(localPath, "EaseFlt.sys");

                if (File.Exists(driverName))
                {
                    string driverInstalledPath = Path.Combine(Environment.SystemDirectory, "drivers\\easeflt.sys");

                    if (File.Exists(driverInstalledPath))
                    {
                        FileInfo fsInstalled = new FileInfo(driverInstalledPath);
                        FileInfo fsToInstall = new FileInfo(driverName);

                        if (fsInstalled.LastWriteTime < fsToInstall.LastWriteTime)
                        {
                            //it needs to install new the driver.
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                ret = false;

                EventManager.WriteMessage(630, "IsDriverChanged", EventLevel.Error, "Check IsDriverChanged failed with error:" + ex.Message);
            }

            return ret;
        }

        static public bool StartFilter(int threadCount, string registerKey,FilterDelegate filterCallback, DisconnectDelegate disconnectCallback,ref string lastError)
        {
          
            bool ret = true;

            try
            {
                if (IsDriverChanged() || !FilterAPI.IsDriverServiceRunning())
                {
                    FilterAPI.UnInstallDriver();

                    //wait for 3 seconds for the uninstallation completed.
                    System.Threading.Thread.Sleep(3000);

                    ret = FilterAPI.InstallDriver();
                    if (!ret)
                    {
                        lastError = "Installed driver failed with error:" + FilterAPI.GetLastErrorMessage();
                        return false;
                    }
                    else
                    {
                        isFilterStarted = false;
                        EventManager.WriteMessage(59, "InstallDriver", EventLevel.Information, "Install filter driver succeeded.");
                    }
                }


                if (!isFilterStarted)
                {

                    if (!SetRegistrationKey(registerKey))
                    {
                        lastError = "Set registration key failed with error:" + GetLastErrorMessage();
                        return false;
                    }

                    gchFilter = GCHandle.Alloc(filterCallback);
                    IntPtr filterCallbackPtr = Marshal.GetFunctionPointerForDelegate(filterCallback);

                    gchDisconnect = GCHandle.Alloc(disconnectCallback);
                    IntPtr disconnectCallbackPtr = Marshal.GetFunctionPointerForDelegate(disconnectCallback);

                    isFilterStarted = RegisterMessageCallback(threadCount, filterCallbackPtr, disconnectCallbackPtr);
                    if (!isFilterStarted)
                    {
                        lastError = "RegisterMessageCallback failed with error:" + GetLastErrorMessage();
                        return false;
                    }

                    ret = true;

                }
            }
            catch (Exception ex)
            {
                ret = false;
                lastError = "Start filter failed with error " + ex.Message;
            }
            finally
            {
                if (!ret)
                {
                    lastError = lastError + " Make sure you run this application as administrator.";
                }
            }

            return ret;
        }

        static public void StopFilter()
        {
            if (isFilterStarted)
            {
                Disconnect();
                gchFilter.Free();
                gchDisconnect.Free();
                isFilterStarted = false;
            }

            return;
        }

        static public bool IsFilterStarted
        {
            get { return isFilterStarted; }
        }

    }
}
