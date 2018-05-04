
///////////////////////////////////////////////////////////////////////////////
//
//    (C) Copyright 2011 EaseFilter Technologies Inc.
//    All Rights Reserved
//
//    This software is part of a licensed software product and may
//    only be used or copied in accordance with the terms of that license.
//
///////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "Tools.h"
#include "FilterAPI.h"
#include "FilterMessage.h"
#include "FilterWorker.h"
#include "ControlFilterHandler.h"

BOOL
__stdcall
MessageCallback(
   IN		PMESSAGE_SEND_DATA pSendMessage,
   IN OUT	PMESSAGE_REPLY_DATA pReplyMessage)
{

	BOOL	ret = TRUE;

	DisplayFilterMessageInfo(pSendMessage);

	if(pReplyMessage)
	{
		ret = ProcessControlFilter(pSendMessage,pReplyMessage);
	}

	return ret;
}

VOID
__stdcall
DisconnectCallback()
{
	printf("Filter connection was disconnected.\n");
	return;
}



void 
SendConfigInfoToFilter(ULONG FilterType,WCHAR* FilterFolder,ULONG IoRegistration ,ULONG AccessFlag,UCHAR* encryptionKey,ULONG keyLength)
{


	//Set the filter maximum wait time for response from the user mode call back function.
	SetConnectionTimeout(30);       

	//Set the filter type to file system monitor/call back filter.
    SetFilterType(FilterType);

	//Setup the filter rule,filter mask is the folder to be monitored,
	//accessFlag is the I/O access control of the folder, only affect callback filter,
	//reparseMask is the destination folder for reparse file open, only when the accessFlag = REPARSE_FILE_OPEN.

	ULONG accessFlag = AccessFlag;

	//test control filter to exclude the file access 
	//accessFlag = ALLOW_MAX_RIGHT_ACCESS | EXCLUDE_FILE_ACCESS;

	if( FilterFolder )
	{
		if( !AddNewFilterRule(accessFlag,FilterFolder))
		{
			PrintLastErrorMessage(L"AddFilterRule failed.");
			return;
		}

		if( keyLength > 0)
		{
			AddEncryptionKeyToFilterRule(FilterFolder,keyLength,encryptionKey);
		}
	}


	//Exclude the process Id from the filter.
 /*   if(!AddExcludedProcessId(GetCurrentProcessId()))
	{
		PrintLastErrorMessage(L"AddExcludedProcessId failed.");
		return;
	}*/
	
	//Only include process id will be watched,all others processes will be excluded.
	//AddIncludedProcessId(GetCurrentProcessId());
	
	if(!RegisterIoRequest(IoRegistration))
	{
		PrintLastErrorMessage(L"RegisterIoRequest failed.");
		return;
	}
}


