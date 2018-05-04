///////////////////////////////////////////////////////////////////////////////
//
//    (C) Copyright 2014 EaseFilter Technologies Inc.
//    All Rights Reserved
//
//    This software is part of a licensed software product and may
//    only be used or copied in accordance with the terms of that license.
//
///////////////////////////////////////////////////////////////////////////////

#ifndef __SHARE_TYPE_H__
#define __SHARE_TYPE_H__

#define DOWNLOADED_BITMAP_NAME				L":EASECLOUD_BITMAP"
#define MESSAGE_SEND_VERIFICATION_NUMBER	0xFF000001
#define BLOCK_SIZE							65536
#define MAX_FILE_NAME_LENGTH				1024
#define MAX_SID_LENGTH						256
#define MAX_EXCLUDED_PROCESS_ID				200	
#define MAX_INCLUDED_PROCESS_ID				200
#define MAX_PROTECTED_PROCESS_ID			200
#define MAX_BLOCK_SAVEAS_PROCESS_ID			200
#define MAX_PATH							260
#define	MAX_ERROR_MESSAGE_SIZE				1024

//Default encryption IV key
static const UCHAR DEFAULT_IV_TAG[] = {0xf0,0xf1,0xf2,0xf3,0xf4,0xf5,0xf6,0xf7,0xf8,0xf9,0xfa,0xfb,0xfc,0xfd,0xfe,0xff};


//-----------reparse file specified definition-------------------------------------------

#define IO_REPARSE_TAG_EASEVAULT_HSM        (0x0000003EL)
// {BBA65D6F-F8A0-48cc-B748-DBD5FFFCCFB1}
static const GUID REPARSE_GUID = 
{ 0xbba65d6f, 0xf8a0, 0x48cc, { 0xb7, 0x48, 0xdb, 0xd5, 0xff, 0xfc, 0xcf, 0xb1 } };

#define EASETAG_KEY		0xbba65d6f

typedef struct _EASETAG_DATA 
{
	ULONG		EaseTagKey;
	ULONG		Flags;
	ULONG		FileNameLength;
	WCHAR		FileName[1];
	
} EASETAG_DATA, *PEASETAG_DATA;

#define AES_TAG_KEY							0xccb76e80

//the flags for encrypted file,indicates the action to the encrypted file open I/O.
typedef enum _AESFlags
{
	Flags_Enabled_Expire_Time			=	0x00000010, 
	Flags_Enabled_Check_ProcessName		=	0x00000020, 
	Flags_Enabled_Check_UserName		=	0x00000040, 
	Flags_Enabled_Check_AccessFlags		=	0x00000080, 
	Flags_Enabled_Check_User_Permit		=	0x00000100, 
	Flags_AES_Key_Was_Embedded			=	0x00000200, 
	Flags_Enabled_Request_IV_And_Key	=	0x00000400, 
	Flags_Enabled_Revoke_Access_Control	=	0x00000800, 
	Flags_Enabled_Check_Computer_Id		=	0x00001000, 
	Flags_Enabled_Check_User_Password	=	0x00002000, 

}AESFlags;

//the structure of control meta data of the encrypted file.
#pragma pack (push,1)
typedef struct _EASETAG_AES_DATA 
{
	ULONG		EaseTagKey;
	ULONG		AESFlags;
	ULONG		IVLength;
	UCHAR		IV[16];
	ULONG		EncryptionKeyLength;
	UCHAR		EncryptionKey[32];
	LONGLONG    CreationTime;
	LONGLONG    ExpireTime;
	ULONG		AccessFlags;
	LONGLONG	FileSize;
	ULONG		LengthOfIncludeProcessNames;
	ULONG		OffsetOfIncludeProcessNames;
	ULONG		LengthOfExcludeProcessNames;
	ULONG		OffsetOfExcludeProcessNames;
	ULONG		LengthOfIncludeUserNames;
	ULONG		OffsetOfIncludeUserNames;
	ULONG		LengthOfExcludeUserNames;
	ULONG		OffsetOfExcludeUserNames;
	ULONG		LengthOfAccountName;
	ULONG		OffsetOfAccountName;
	ULONG		LengthOfComputerId;
	ULONG		OffsetOfComputerId;
	ULONG		LengthOfUserPassword;
	ULONG		OffsetOfUserPassword;
	//the data store here.
	//IncludeProcessNames;
	//ExcludeProcessNames;
	//IncludeUserNames;
	//ExcludeUserNames;
	//AccountNames;
	//ComputerId;
	//UserPassword;
	ULONG		TotalSizeOfAESData;
	
} EASETAG_AES_DATA, *PEASETAG_AES_DATA;
#pragma pack(pop)

#pragma pack (push,1)
typedef struct _EASETAG_EMBEDDED_DATA 
{
	ULONG		VerificationNumber;
	LONGLONG	FileSize;
	UCHAR		IV[16];
	LONGLONG    ExpireTime;
	
} EASETAG_EMBEDDED_DATA, *PEASETAG_EMBEDDED_DATA;
#pragma pack(pop)
//---------------------------------------------------------------------------------------

typedef enum _FilterType 
{
    FILE_SYSTEM_MONITOR					= 0, 
    FILE_SYSTEM_CONTROL					= 0x1,     
	FILE_SYSTEM_ENCRYPTION				= 0x2,    	
	FILE_SYSTEM_CONTROL_ENCRYPTION		= 0x3,    
	FILE_SYSTEM_MONITOR_ENCRYPTION		= 0x4,  
	FILE_SYSTEM_EASE_FILTER_ALL			= 0x5,  
	FILE_SYSTEM_HSM						= 0x10,    
	FILE_SYSTEM_CLOUD					= 0x20,    

} FilterType;


#define MAX_REQUEST_TYPE 32

//the commands of filter driver to user application.
typedef enum _FilterCommand
{	
	MESSAGE_TYPE_RESTORE_BLOCK_OR_FILE	=						0x00000001,
	//restore file to the original folder
	MESSAGE_TYPE_RESTORE_FILE_TO_ORIGINAL_FOLDER =				0x00000002,
	MESSAGE_TYPE_GET_FILE_LIST =								0x00000004,
	MESSAGE_TYPE_RESTORE_FILE_TO_CACHE =						0x00000008,
	MESSAGE_TYPE_SEND_EVENT_NOTIFICATION =						0x00000010,
	MESSAGE_TYPE_DELETE_FILE =									0x00000020,
	MESSAGE_TYPE_RENAME_FILE =									0x00000040,
	MESSAGE_TYPE_FILE_CHANGED_PERSISTENT_FILENAME =				0x00000080,
	FILTER_SEND_FILE_CHANGED_EVENT =							0x00010001,
    FILTER_REQUEST_USER_PERMIT =								0x00010002,
    FILTER_REQUEST_ENCRYPTION_KEY =								0x00010003,
    FILTER_REQUEST_ENCRYPTION_IV_AND_KEY =						0x00010004,

}FilterCommand;

//the I/O types of the monitor or control filter can intercept.
typedef enum _MessageType
{
	
	PRE_CREATE							= 0x00000001,
	POST_CREATE							= 0x00000002,
	PRE_FASTIO_READ						= 0x00000004,
	POST_FASTIO_READ					= 0x00000008,
	PRE_CACHE_READ						= 0x00000010,
	POST_CACHE_READ						= 0x00000020,
	PRE_NOCACHE_READ					= 0x00000040,
	POST_NOCACHE_READ					= 0x00000080,
	PRE_PAGING_IO_READ					= 0x00000100,
	POST_PAGING_IO_READ					= 0x00000200,
	PRE_FASTIO_WRITE					= 0x00000400,
	POST_FASTIO_WRITE					= 0x00000800,
	PRE_CACHE_WRITE						= 0x00001000,
	POST_CACHE_WRITE					= 0x00002000,
	PRE_NOCACHE_WRITE					= 0x00004000,
	POST_NOCACHE_WRITE					= 0x00008000,
	PRE_PAGING_IO_WRITE					= 0x00010000,
	POST_PAGING_IO_WRITE				= 0x00020000,
	PRE_QUERY_INFORMATION				= 0x00040000,
	POST_QUERY_INFORMATION				= 0x00080000,
	PRE_SET_INFORMATION					= 0x00100000,
	POST_SET_INFORMATION				= 0x00200000,
	PRE_DIRECTORY						= 0x00400000,
	POST_DIRECTORY						= 0x00800000,
	PRE_QUERY_SECURITY					= 0x01000000,	
	POST_QUERY_SECURITY					= 0x02000000,
	PRE_SET_SECURITY					= 0x04000000,
	POST_SET_SECURITY					= 0x08000000,
	PRE_CLEANUP							= 0x10000000,
	POST_CLEANUP						= 0x20000000,
	PRE_CLOSE							= 0x40000000,
	POST_CLOSE							= 0x80000000UL, 

}MessageType;

//the flags of the access control to the file.
typedef enum _AccessFlag
{
	EXCLUDE_FILTER_RULE								= 0x00000000,
	EXCLUDE_FILE_ACCESS								= 0x00000001,
	REPARSE_FILE_OPEN								= 0x00000002,
	HIDE_FILES_IN_DIRECTORY_BROWSING				= 0x00000004,
	FILE_ENCRYPTION_RULE							= 0x00000008,
	ALLOW_OPEN_WTIH_ACCESS_SYSTEM_SECURITY			= 0x00000010,
	ALLOW_OPEN_WITH_READ_ACCESS						= 0x00000020,
	ALLOW_OPEN_WITH_WRITE_ACCESS					= 0x00000040,
	ALLOW_OPEN_WITH_CREATE_OR_OVERWRITE_ACCESS		= 0x00000080,
	ALLOW_OPEN_WITH_DELETE_ACCESS					= 0x00000100,
	ALLOW_READ_ACCESS								= 0x00000200,
	ALLOW_WRITE_ACCESS								= 0x00000400,
	ALLOW_QUERY_INFORMATION_ACCESS					= 0x00000800,
	//allow to change file time and file attributes
	ALLOW_SET_INFORMATION							= 0x00001000,
	ALLOW_FILE_RENAME								= 0x00002000,
	ALLOW_FILE_DELETE								= 0x00004000,
	ALLOW_FILE_SIZE_CHANGE							= 0x00008000,
	ALLOW_QUERY_SECURITY_ACCESS						= 0x00010000,
    ALLOW_SET_SECURITY_ACCESS						= 0x00020000,
	ALLOW_DIRECTORY_LIST_ACCESS						= 0x00040000,
	ALLOW_FILE_ACCESS_FROM_NETWORK					= 0x00080000,
	ALLOW_NEW_FILE_ENCRYPTION						= 0x00100000,
	ALLOW_ALL_SAVE_AS								= 0x00200000,
	ALLOW_INCLUDE_PROCESS_SAVE_AS					= 0x00400000,
	ALLOW_MAX_RIGHT_ACCESS							= 0xfffffff0,
	
}AccessFlag;


typedef enum _ConnectionPortType 
{
    ClientMessagePort       = 0, 
    ClientControlPort	    = 1,     

} ConnectionPortType;

typedef enum _EVENT_OUTPUT_TYPE 
{
    OUTPUT_NONE			= 0, 
    OUTPUT_DEBUGGER	    = 1,     
	OUTPUT_EVENTLOG	    = 2,     
	OUTPUT_FILE			= 3,     

} EVENT_OUTPUT_TYPE;

typedef enum _EVENT_LEVEL 
{
    EVENT_LEVEL_NONE		=   0,   // Tracing is not on
	EVENT_LEVEL_CRITICAL    =	1,   // Abnormal exit or termination
	EVENT_LEVEL_ERROR       =	2,   // Severe errors that need logging
	EVENT_LEVEL_WARNING     =	3,   // Warnings such as allocation failure
	EVENT_LEVEL_INFORMATION =	4,   // Includes non-error cases(e.g.,Entry-Exit)
	EVENT_LEVEL_VERBOSE     =	5,   // Detailed traces from intermediate steps

} EVENT_LEVEL;

//the commands from user mode app to the filter driver.
typedef enum _ControlType 
{
	CONTROL_SET_INTEGER_DATA = 1,
	CONTROL_SET_STRING_DATA ,
	CONTROL_REGISTER_REQUEST,
	CONTROL_ADD_FILTER_RULE ,
	CONTROL_REMOVE_FILTER_RULE,
	CONTROL_ADD_EXCLUDED_PID,
	CONTROL_REMOVE_EXCLUDED_PID,
	CONTROL_RESET_CONFIG_DATA,
	CONTROL_GET_FILE_HANDLE,
	CONTROL_CLOSE_FILE_HANDLE,
	CONTROL_ADD_INCLUDED_PID,
	CONTROL_REMOVE_INCLUDED_PID,
	CONTROL_ADD_PROTECTED_PID,
	CONTROL_REMOVE_PROTECTED_PID,
	CONTROL_ADD_BLOCK_SAVEAS_PID,
	CONTROL_REMOVE_BLOCK_SAVEAS_PID,
	CONTROL_MAX_TYPE,       

} ControlType;

// the user mode app sends the integer data to filter driver, this is the index of the integer data.
typedef enum _DataControlId 
{
	FILTER_TYPE_ID = 1,			//The filter driver type.
	EVENT_OUTPUT_TYPE_ID,		//Control send the event output type.
	EVENT_LEVEL_ID,				//Control send event level.
	EVENT_FLAGS_ID,				//Control send the event modules
	CONNECTION_TIMEOUT_ID,		//Control send client connection timout in seconds.
	BOOLEAN_CONFIG_ID,			//All the boolean config data setting
	WAIT_BLOCK_DATA_INTERVAL,	//the interval time in milliseconds to wait for the block data download
	WAIT_BLOCK_DATA_TIMEOUT,	//the timeout in milliseconds to wait for the block data ready
	DIR_CACHE_TIMEOUT,			//the directory cache file list time to live in milliseconds
	MAX_TOTAL_DIR_CACHE_SIZE,   //the total size of the dir info buffer
	DELETE_NO_ACCESS_DIR_INFO_IN_SECONDS,   //delete the directory info if there are no access more than this value.
	FILE_CHANGED_QUEUE_TTL_IN_SECONDS,		//set the file changed queue time to live.
	MAX_CHANGED_FILES_IN_QUEUE, //set the maximum files can be kept in queue.

	MAX_DATA_CONTROL_ID,

} DataControlId;

//the filter rule commands 
typedef enum _FilterRuleId 
{
	ADD_NEW_FILTER_RULE = 0x100,						
	FILTER_RULE_ADD_EXCLUDE_FILE_FILTER_MASK,
	FILTER_RULE_REMOVE_EXLCUDE_FILE_FILTER_MASK,
	FILTER_RULE_ADD_HIDDEN_FILE_FILTER_MASK,
	FILTER_RULE_REMOVE_HIDDEN_FILE_FILTER_MASK,
	FILTER_RULE_ADD_REPARSE_FILTER_MASK,
	FILTER_RULE_REMOVE_REPARSE_FILTER_MASK,
	FILTER_RULE_ADD_ENCRYPTION_KEY,
	FILTER_RULE_ADD_INCLUDE_PROCESS_ID,
	FILTER_RULE_REMOVE_INCLUDE_PROCESS_ID,
	FILTER_RULE_ADD_EXCLUDE_PROCESS_ID,
	FILTER_RULE_REMOVE_EXCLUDE_PROCESS_ID,
	FILTER_RULE_REGISTER_EVENTYPE,
	FILTER_RULE_UNREGISTER_EVENTYPE,
	FILTER_RULE_ADD_INCLUDE_PROCESS_NAME,
	FILTER_RULE_REMOVE_INCLUDE_PROCESS_NAME,
	FILTER_RULE_ADD_EXCLUDE_PROCESS_NAME,
	FILTER_RULE_REMOVE_EXCLUDE_PROCESS_NAME,
	FILTER_RULE_ADD_INCLUDE_USER_NAME,
	FILTER_RULE_REMOVE_INCLUDE_USER_NAME,
	FILTER_RULE_ADD_EXCLUDE_USER_NAME,
	FILTER_RULE_REMOVE_EXCLUDE_USER_NAME,
	FILTER_RULE_REGISTER_MONITOR_IO,
	FILTER_RULE_REGISTER_CONTROL_IO,

}FilterRuleId;

//the user mode app sends the string data to the filter driver,
//this is the index of the string data.
typedef enum _StringControlId 
{
	STRING_TRASACTION_FOLDER_ID = 1,
	STRING_REALTIME_JOB_SUFFIX_ID = 2,

	MAX_STRING_CONTROL_ID,

} StringControlId;

//this is the boolean data of the user mode app sending to the filter.
//this is the boolean configuration of the filter driver.
typedef enum _BooleanConfig 
{
	ENABLE_NO_RECALL_FLAG = 0x00000001, //for easetag, if it was true, after the reparsepoint file was opened, it won't restore data back for read and write.
	DISABLE_FILTER_UNLOAD_FLAG = 0x00000002, //if it is true, the filter driver can't be unloaded.
	ENABLE_SET_OFFLINE_FLAG = 0x00000004, //for virtual file, it will set offline attribute if it is true.
	ENABLE_DEFAULT_IV_TAG = 0x00000008, //for encryption, it is true, it will use the default IV tag to encrypt the files.
	ENABLE_ADD_CHANGED_FILE_TO_QUEUE = 0x00000010, //for file changed event, if it is enabled, it will save the file name to a persistent file, or it will send the event to service right away. 
	ENABLE_ENCRYPTION_KEY_FOR_NEW_FILE = 0x0000020, //for encryption rule, get the encryption key from user mode for the new file creation.
	ENABLE_ENCRYPTION_KEY_AND_IV_FOR_NEW_FILE = 0x00000040,  //for encryption rule, get the encryption key and IV from user mode for the new file creation.

} BooleanConfig;

//this is the data structure which send control message to kernel from user mode.
//first it needs to set the control type which shows as above enumeration.
//the second is the control id for integer data.
//the third is the integer data.
typedef struct _CONTROL_DATA 
{
	ULONG		ControlType;
	ULONG		ControlId;
	LONGLONG	IntegerData;
	ULONG		StringLength1;
	WCHAR		StringData1[MAX_PATH];
	ULONG		StringLength2;
	WCHAR		StringData2[MAX_PATH];
	ULONG		KeyLength;
	UCHAR		Key[MAX_PATH];
	
} CONTROL_DATA, *PCONTROL_DATA;

//the file was changed, this is the meta data of the file information.
typedef struct _FILE_CHANGED_DATA 
{
	ULONG		SizeOfEntry;
	ULONG		FileEventType;
	LONGLONG	LastWriteTime;
	ULONG		FileNameLength;
	WCHAR		FileName[1];
	//the whole file name path is appended here.

} FILE_CHANGED_DATA, *PFILE_CHANGED_DATA;


//this is the data structure of the filter driver sending data to the user mode app.
typedef struct _MESSAGE_SEND_DATA 
{
	ULONG			MessageId;
	PVOID			FileObject;
	PVOID			FsContext;
	ULONG			MessageType;	
	ULONG			ProcessId;
    ULONG			ThreadId;   
	LONGLONG		Offset; // read/write offset 
	ULONG			Length; //read/write length
	LONGLONG		FileSize;
	LONGLONG		TransactionTime;
	LONGLONG		CreationTime;
	LONGLONG		LastAccessTime;
	LONGLONG		LastWriteTime;
	ULONG			FileAttributes;
	//The disired access,share access and disposition for Create request.
	ULONG			DesiredAccess;
	ULONG			Disposition;
	ULONG			ShareAccess;
	ULONG			CreateOptions;
	ULONG			CreateStatus;

	//For QueryInformation,SetInformation,Directory request it is information class
	//For QuerySecurity and SetSecurity request,it is securityInformation.
	ULONG			InfoClass; 

	ULONG			Status;
	ULONG			FileNameLength;
	WCHAR			FileName[MAX_FILE_NAME_LENGTH];
	ULONG			SidLength;
    UCHAR			Sid[MAX_SID_LENGTH];
	ULONG			DataBufferLength;
	UCHAR			DataBuffer[BLOCK_SIZE];

	ULONG			VerificationNumber;

} MESSAGE_SEND_DATA, *PMESSAGE_SEND_DATA;

//The status return to filter,instruct filter driver what action needs to be done.
typedef enum _FilterStatus 
{
	FILTER_MESSAGE_IS_DIRTY			= 0x00000001, //Set this flag if the reply message need to be processed.
	FILTER_COMPLETE_PRE_OPERATION	= 0x00000002, //Set this flag if complete the pre operation. 
	FILTER_DATA_BUFFER_IS_UPDATED	= 0x00000004, //Set this flag if the databuffer was updated.
	FILTER_BLOCK_DATA_WAS_RETURNED	= 0x00000008, //Set this flag if return read block databuffer to filter.
	FILTER_CACHE_FILE_WAS_RETURNED	= 0x00000010, //Set this flag if the stub file was restored.

} FilterStatus, *PFilterStatus;

//this is the enumeration of the file I/O events.
typedef enum _FileEventType
{
	//kernal internal use
	CONTEXT_WAS_REMOVED				= 0x00000001,

	FILE_WAS_CREATED				= 0x00000020,
	FILE_WAS_WRITTEN				= 0x00000040,
	FILE_WAS_RENAMED				= 0x00000080,
	FILE_WAS_DELETED				= 0x00000100,
	FILE_SECURITY_CHANGED			= 0x00000200,
	FILE_INFO_CHANGED				= 0x00000400,
	FILE_WAS_READ					= 0x00000800,

} FileEventType, *PFileEventType;


//This is the return data structure from user mode to the filter driver.
typedef struct _MESSAGE_REPLY_DATA 
{
	ULONG		MessageId;
	ULONG		MessageType;	
	ULONG		ReturnStatus;
	ULONG		FilterStatus;
	union {
		struct {
				ULONG		DataBufferLength;
				UCHAR		DataBuffer[BLOCK_SIZE];		
		} Data;
		struct {
				ULONG		AESDataLength;
				UCHAR		IV[16];
				ULONG		EncryptionKeyLength;
				UCHAR		EncryptionKey[1];				
		} AESData;
		struct {
				ULONG		UserNameLength;
				WCHAR		UserName[1];				
		} UserInfo;
		struct {
				ULONG		FileNameLength;
				WCHAR		FileName[1];				
		} FileInfo;

	}ReplyData;
  
} MESSAGE_REPLY_DATA, *PMESSAGE_REPLY_DATA;



#define STATUS_ACCESS_DENIED				0xC0000022L

extern "C" __declspec(dllexport) 
BOOL 
InstallDriver();

extern "C" __declspec(dllexport) 
BOOL
UnInstallDriver();

extern "C" __declspec(dllexport) 
BOOL
SetRegistrationKey(char* key);

typedef BOOL (__stdcall *Proto_Message_Callback)(
   IN		PMESSAGE_SEND_DATA pSendMessage,
   IN OUT	PMESSAGE_REPLY_DATA pReplyMessage);

typedef VOID (__stdcall *Proto_Disconnect_Callback)();

extern "C" __declspec(dllexport) 
BOOL
RegisterMessageCallback(
	ULONG ThreadCount,
	Proto_Message_Callback MessageCallback,
	Proto_Disconnect_Callback DisconnectCallback );

extern "C" __declspec(dllexport) 
VOID
Disconnect();

extern "C" __declspec(dllexport) 
BOOL
GetLastErrorMessage(WCHAR* Buffer, PULONG BufferLength);

extern "C" __declspec(dllexport)
BOOL
SetIntegerData(ULONG dataControlId, LONGLONG data );

extern "C" __declspec(dllexport)
BOOL
SetStringData(ULONG stringControlId, WCHAR* stringData);

extern "C" __declspec(dllexport)
BOOL
ResetConfigData();

extern "C" __declspec(dllexport)  
BOOL
ProtectCurrentProcess();

extern "C" __declspec(dllexport)  
BOOL
StopProtectCurrentProcess();

extern "C" __declspec(dllexport)  
BOOL
SetFilterType(ULONG FilterType);

extern "C" __declspec(dllexport)  
BOOL
SetBooleanConfig(ULONG booleanConfig);

extern "C" __declspec(dllexport)  
BOOL
SetConnectionTimeout(ULONG TimeOutInSeconds);

extern "C" __declspec(dllexport) 
BOOL 
AddFilterRule(ULONG AccessFlag, WCHAR* FilterMask, WCHAR* FilterMask2 = NULL,ULONG keyLength = 0,PUCHAR key = NULL);

extern "C" __declspec(dllexport) 
BOOL 
AddNewFilterRule(ULONG accessFlag, WCHAR* filterMask,BOOL isResident = FALSE);;

extern "C" __declspec(dllexport) 
BOOL 
AddEncryptionKeyToFilterRule(WCHAR* filterMask,ULONG encryptionKeyLength,PUCHAR encryptionKey);

extern "C" __declspec(dllexport) 
BOOL 
AddReparseFileMaskToFilterRule(WCHAR* filterMask,  WCHAR* reparseFilterMask);

extern "C" __declspec(dllexport) 
BOOL 
AddHiddenFileMaskToFilterRule(WCHAR* filterMask,  WCHAR* hiddenFileFilterMask);
extern "C" __declspec(dllexport) 
BOOL 
AddExcludeFileMaskToFilterRule(WCHAR* filterMask,  WCHAR* excludeFileFilterMask);

extern "C" __declspec(dllexport) 
BOOL 
AddExcludeProcessIdToFilterRule(WCHAR* filterMask, ULONG excludePID);

extern "C" __declspec(dllexport) 
BOOL 
AddIncludeProcessIdToFilterRule(WCHAR* filterMask, ULONG includePID);

extern "C" __declspec(dllexport) 
BOOL 
RegisterEventTypeToFilterRule(WCHAR* filterMask, ULONG eventType);

extern "C" __declspec(dllexport) 
BOOL 
RegisterMoinitorIOToFilterRule(WCHAR* filterMask, ULONG registerIO);

extern "C" __declspec(dllexport) 
BOOL 
RegisterControlIOToFilterRule(WCHAR* filterMask, ULONG registerIO);

extern "C" __declspec(dllexport) 
BOOL 
AddIncludeProcessNameToFilterRule(WCHAR* filterMask,  WCHAR* processName);

extern "C" __declspec(dllexport) 
BOOL 
AddExcludeProcessNameToFilterRule(WCHAR* filterMask,  WCHAR* processName);

extern "C" __declspec(dllexport) 
BOOL 
AddIncludeUserNameToFilterRule(WCHAR* filterMask,  WCHAR* userName);

extern "C" __declspec(dllexport) 
BOOL 
AddExcludeUserNameToFilterRule(WCHAR* filterMask,  WCHAR* processName);

extern "C" __declspec(dllexport) 
BOOL 
RemoveFilterRule(WCHAR* FilterMask);

extern "C" __declspec(dllexport) 
BOOL 
AddExcludedProcessId(ULONG ProcessId);

extern "C" __declspec(dllexport) 
BOOL 
RemoveExcludeProcessId(ULONG ProcessId);

extern "C" __declspec(dllexport)
BOOL 
AddIncludedProcessId(ULONG processId);

extern "C" __declspec(dllexport) 
BOOL 
RemoveIncludeProcessId(ULONG processId);

extern "C" __declspec(dllexport)
BOOL 
AddProtectedProcessId(ULONG processId);

extern "C" __declspec(dllexport) 
BOOL 
RemoveProtectedProcessId(ULONG processId);

extern "C" __declspec(dllexport)
BOOL 
AddBlockSaveAsProcessId(ULONG processId);

extern "C" __declspec(dllexport) 
BOOL 
RemoveBlockSaveAsProcessId(ULONG processId);

extern "C" __declspec(dllexport) 
BOOL 
RegisterIoRequest(ULONG RequestRegistration);

extern "C" __declspec(dllexport) 
BOOL	
GetFileHandleInFilter(WCHAR* FileName,ULONG  DesiredAccess,HANDLE*	FileHandle);

extern "C" __declspec(dllexport) 
BOOL
CloseFileHandleInFilter(HANDLE hFile);

extern "C" __declspec(dllexport) 
BOOL
IsDriverServiceRunning();

extern "C" __declspec(dllexport) 
BOOL
OpenStubFile(
    LPCTSTR fileName,
    DWORD   dwDesiredAccess,
    DWORD   dwShareMode,
    PHANDLE pHandle );

extern "C" __declspec(dllexport) 
BOOL
CreateFileAPI(
	LPCTSTR		fileName,
	DWORD		dwDesiredAccess,
	DWORD		dwShareMode,
	DWORD		dwCreationDisposition,
	DWORD		dwFlagsAndAttributes,
	PHANDLE		pHandle );

extern "C" __declspec(dllexport) 
BOOL
CreateStubFile(
	LPCTSTR		fileName,
	LONGLONG	fileSize,
	ULONG		fileAttributes,
	ULONG		tagDataLength,
	BYTE*		tagData,
	BOOL		overwriteIfExist,
	PHANDLE		pHandle );

extern "C" __declspec(dllexport) 
BOOL
GetTagData(
	HANDLE hFile,
	PULONG tagDataLength,
	BYTE*  tagData);

extern "C" __declspec(dllexport) 
BOOL  
RemoveTagData(
    HANDLE hFile ,
	BOOLEAN	updateTimeStamp = FALSE);

extern "C" __declspec(dllexport) 
BOOL 
AddTagData(
    HANDLE  hFile, 
    ULONG   tagDataLength,
	BYTE*	tagData );

extern "C" __declspec(dllexport) 
BOOL 
AddReparseTagData(
	LPCTSTR		fileName,
    ULONG		tagDataLength,
	BYTE*		tagData );

extern "C" __declspec(dllexport) 
BOOL  
QueryAllocatedRanges(
    IN HANDLE							hFile, 
    IN LONGLONG                         queryOffset,
    IN LONGLONG                         queryLength,
    IN OUT PFILE_ALLOCATED_RANGE_BUFFER allocatedBuffer,
    IN ULONG                            allocatedBufferSize,   
    OUT ULONG                           *returnBufferLength  );

extern "C" __declspec(dllexport) 
BOOL
SetFileSize(
	HANDLE			hFile,
	LONGLONG		fileSize);

extern "C" __declspec(dllexport) 
BOOL
AESEncryptFile(
	LPCTSTR					fileName,
	ULONG					keyLength,
	BYTE*					key,
	ULONG					ivLength = 0,
	BYTE*					iv = NULL,
	BOOLEAN					addIVTag = TRUE);

extern "C" __declspec(dllexport) 
BOOL
AESDecryptFile(
	LPCTSTR		fileName,
	ULONG		keyLength,
	BYTE*		key,
	ULONG		ivLength = 0,
	BYTE*		iv = NULL);

extern "C" __declspec(dllexport) 
BOOL
AESEncryptFileToFile(
	LPCTSTR					sourceFileName,
	LPCTSTR					destFileName,
	ULONG					keyLength,
	BYTE*					key,
	ULONG					ivLength,
	BYTE*					iv,
	BOOLEAN					addIVTag );

extern "C" __declspec(dllexport) 
BOOL
AESDecryptFileToFile(
	LPCTSTR					sourceFileName,
	LPCTSTR					destFileName,
	ULONG					keyLength,
	BYTE*					key,
	ULONG					ivLength,
	BYTE*					iv);

extern "C" __declspec(dllexport) 
BOOL
GetIVTag(
	LPCTSTR		fileName,
	PULONG		ivLength,
	BYTE*		ivTag);

extern "C" __declspec(dllexport) 
BOOL
AddIVTag(
	LPCTSTR		fileName,
	ULONG		ivLength,
	BYTE*		ivTag);

extern "C" __declspec(dllexport) 
BOOL
AddIVAndExpireTimeTag(
	LPCTSTR		fileName,
	ULONG		ivLength,
	BYTE*		ivTag,
	LONGLONG	expireTime,
	HANDLE		fileHandle);

extern "C" __declspec(dllexport) 
BOOL
ProcessEncryptedFile(
	LPCTSTR					sourceFileName,
	LPCTSTR					destFileName);

extern "C" __declspec(dllexport) 
BOOL
DeleteIVTag(
	LPCTSTR		fileName);

extern "C" __declspec(dllexport) 
BOOL
AESEncryptDecryptBuffer(
	BYTE*					inputBuffer,
	BYTE*					outputBuffer,
	ULONG					bufferLength,
	LONGLONG				offset,
	BYTE*					key,
	ULONG					keyLength,
	BYTE*					ivKey,
	ULONG					ivLength);

extern "C" __declspec(dllexport) 
BOOL
GetUniqueComputerId(
	BYTE*					buffer,
	PULONG					bufferLength  );


#endif//__SHARE_TYPE_H__
