///////////////////////////////////////////////////////////////////////////////
//
//    (C) Copyright 2011 EaseFilter Technologies Inc.
//    All Rights Reserved
//
//    This software is part of a licensed software product and may
//    only be used or copied in accordance with the terms of that license.
//
///////////////////////////////////////////////////////////////////////////////

// CPlusPlusDemo.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Tools.h"
#include "FilterAPI.h"
#include "UnitTest.h"
#include "AESEncryption.h"
#include "WindowsService.h"
#include "FilterWorker.h"
#include <Sddl.h>

#define	MAX_ERROR_MESSAGE_SIZE	1024

#define PrintMessage	wprintf //ToDebugger


VOID
Usage (
    VOID
    )
/*++

Routine Description

    Prints usage

Arguments

    None

Return Value

    None

--*/
{
	WCHAR	computerId[26];
	ULONG	bufferLength = sizeof(computerId);

	if( GetUniqueComputerId((BYTE*)computerId,&bufferLength) )
	{
		wprintf(L"\nComputerId:%ws\n\n\n",computerId );
	}
	else
	{
		PrintLastErrorMessage( L"GetUniqueComputerId failed.");
		printf( "\n\n" );
	}

	printf( "\nUsage:		EaseFltCPPDemo  [command] <FilterFolder> <IoRegistration> <AccessFlag>\n" );
	printf( "\nCommands:\n" );
	printf( "		i ----- Install Driver\n" );
	printf( "		u ----- UnInstall Driver\n" );
	printf( "		t ----- Driver UnitTest\n" );
	printf( "		m ----- Start monitor filter,require FilterFolder,IoRgistration, if it is empty,it will use default value.\n" );
	printf( "		c ----- Start control filter,require FilterFolder,IoRgistration,AccessFlag if it is empty,it will use default value.\n" );
	printf( "		e ----- Start encryption filter driver test,require FilterFolder,if it is empty,it will use default value.\n" );
	printf( "\n		[FilterFolder]---- the folder mask which will be monitored. By default is c:\\filterTest\\*\n" );
	printf( "		[IoRegistration]---- the I/O requests register to filter. By default is all post I/O.\n" );
	printf( "		[AccessFlag]---- the I/O access flag,only affect control filter. By default is ALLOW_MAX_RIGHT_ACCESS.\n" );
	printf( "\r\nExample: EaseFltCPPDemo i   ----- Install Driver\r\n" ); 
	printf( "EaseFltCPPDemo u   ----- UnInstall Driver\r\n" ); 
	printf( "EaseFltCPPDemo t   ----- Driver UnitTest\r\n" ); 
	printf( "EaseFltCPPDemo m c:\\filterTest\\*    ----- run the monitor filter driver\r\n" );
	
}


int _tmain(int argc, _TCHAR* argv[])
{
    DWORD	threadCount = 5;
	ULONG	filterType = FILE_SYSTEM_MONITOR;
	BOOL	ret = FALSE;
	
	//Purchase a license key with the link: http://www.EaseFilter.com/Order.htm
    //Email us to request a trial key: info@EaseFilter.com //free email is not accepted.
	#define	registerKey "************************************"

	if(argc <= 1)
	{
		Usage();
		return 1;
	}

	TCHAR op = *argv[1];

	switch(op)
	{
	
			/*How to run as windows service, do the following steps:
			1.Create windows service:  sc create filterService binpath=  c:\easefilter\x64\cplusplusdemo.exe w,
									   replace the path with your own path,Note: binpath=(space)binarypath
			2. cplusplusdemo.exe i  //install the driver manully. 
			3. Sc start easefilter  //start the windows service.
			*/
		case 'w':  //start windows service for the monitor filter
				  {
					  StartWindowsService();
					  break;
				  }

		case 'i': //install driver
		{
			//Install the driver only once.
			//After the installation, you can use command "fltmc unload EaseFlt" to unload the driver
			//or "fltmc load EaseFlt" to load the driver, or "fltmc" to check the status of the driver.
			ret = InstallDriver();	
			if( !ret )
			{
				PrintLastErrorMessage( L"InstallDriver failed.");
				return 1;
			}
			break;
		}

		case 'u': //uninstall driver
		{
			ret = UnInstallDriver();
			if( !ret )
			{
				PrintLastErrorMessage( L"UnInstallDriver failed.");
				return 1;
			}

			break;

		}

		case 't': //driver unit test 
		{
			ret = SetRegistrationKey(registerKey);
			if( !ret )
			{
				PrintLastErrorMessage( L"SetRegistrationKey failed.");
				return 1;
			}

			ret = RegisterMessageCallback(threadCount,MessageCallback,DisconnectCallback);

			if( !ret )
			{
				PrintLastErrorMessage( L"RegisterMessageCallback failed.");
				return 1;
			}

			//this the demo how to use the control filter driver SDK.
			ControlFilterUnitTest();


			Disconnect();

			break;

		}

		case 'c': filterType = FILE_SYSTEM_CONTROL; //start control filter		
		case 'm':
		{

			/*ret = InstallDriver();	
			if( !ret )
			{
				PrintLastErrorMessage( L"InstallDriver failed.");
				return 1;
			}*/

			TCHAR* filterFolder = GetFilterMask();
			ULONG ioRegistration = 0;
			ULONG accessFlag = ALLOW_MAX_RIGHT_ACCESS;			

			if( argc >= 3 )
			{
				filterFolder = argv[2];
			}
			
			if( argc >= 4 )
			{
				ioRegistration = _ttoi(argv[3]);
			}
			else
			{
				//Register the I/O request,which will be monitored or will be called back from filter.
				for (int i = 0; i < MAX_REQUEST_TYPE; i++ )
				{
					//register all post request
					if( (double)i/2 != i/2 )
					{
						ioRegistration |= 1<<i;
					}
				}     
			}

			if( argc >= 5 )
			{
				accessFlag = _ttoi(argv[4]);
			}


			_tprintf(_T("Start Monitor %s  ioregistration:0X%0X accessFlag:0X%0X\n\n Press any key to stop.\n"),filterFolder,ioRegistration,accessFlag);

			 //Reset all filter confing setting.
			ResetConfigData();

			ret = SetRegistrationKey(registerKey);
			if( !ret )
			{
				PrintLastErrorMessage( L"SetRegistrationKey failed.");
				return 1;
			}

			ret = RegisterMessageCallback(threadCount,MessageCallback,DisconnectCallback);

			if( !ret )
			{
				PrintLastErrorMessage( L"RegisterMessageCallback failed.");
				return 1;
			}

			//this the demo how to use the control filter.
			SendConfigInfoToFilter(filterType,filterFolder,ioRegistration,accessFlag);

			//prevent the current process from being terminated.
			AddProtectedProcessId(GetCurrentProcessId());

			system("pause");

			//the process can be termiated now.
			RemoveProtectedProcessId(GetCurrentProcessId());



			Disconnect();

			break;

		}
		case 'e':  //encryption filter driver test
		{

			UnInstallDriver();
			
			ret = InstallDriver();	
			if( !ret )
			{
				PrintLastErrorMessage( L"InstallDriver failed.");
				return 1;
			}

			TCHAR* filterFolder = GetFilterMask();
			ULONG ioRegistration = 0;
			ULONG accessFlag = ALLOW_MAX_RIGHT_ACCESS|FILE_ENCRYPTION_RULE;			
			
			filterType = FILE_SYSTEM_ENCRYPTION;	

			if( argc >= 3 )
			{
				filterFolder = argv[2];
			}			

			//Reset all filter confing setting.
			ResetConfigData();

			ret = SetRegistrationKey(registerKey);
			if( !ret )
			{
				PrintLastErrorMessage( L"SetRegistrationKey failed.");
				return 1;
			}

			ret = RegisterMessageCallback(threadCount,MessageCallback,DisconnectCallback);

			if( !ret )
			{
				PrintLastErrorMessage( L"RegisterMessageCallback failed.");
				return 1;
			}

			//prevent the current process from being terminated.
			//AddProtectedProcessId(GetCurrentProcessId());

			// 32bytes encrytpion key
			UCHAR key[] = {0x60,0x3d,0xeb,0x10,0x15,0xca,0x71,0xbe,0x2b,0x73,0xae,0xf0,0x85,0x7d,0x77,0x81,0x1f,0x35,0x2c,0x07,0x3b,0x61,0x08,0xd7,0x2d,0x98,0x10,0xa3,0x09,0x14,0xdf,0xf4};

			//this the demo how to use the control filter.
			SendConfigInfoToFilter(filterType,filterFolder,ioRegistration,accessFlag,key,sizeof(key));

			//this the demo how to use the control filter.
			EncryptionUnitTest();

			_tprintf(_T("\n\nStart Encryption for folder %s,\r\nAll new created files in this folder will be encrypted by filter driver. When filter driver is stopped, the encrypted files can't be read. \n\n Press any key to stop the filter driver.\n"),filterFolder);
			system("pause");

			//the process can be termiated now.
			//RemoveProtectedProcessId(GetCurrentProcessId());

			Disconnect();

			break;

		}
		default:
			{
				Usage(); 
				return 1;
			}


	}

		
	return 0;
}

