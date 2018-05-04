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
#include "tools.h"
#include "FilterAPI.h"

WCHAR* testFolder = L"c:\\filterTest";
WCHAR* testFile = L"c:\\filterTest\\testEncryptfile1.bin";
WCHAR* copyFile = L"c:\\filterTest\\testEncryptfile1.copy.bin";

//Add clear text data to the file.
unsigned char iv[] = {0xf0,0xf1,0xf2,0xf3,0xf4,0xf5,0xf6,0xf7,0xf8,0xf9,0xfa,0xfb,0xfc,0xfd,0xfe,0xff};// Initialization vector
unsigned char clearText[] = {0x6b,0xc1,0xbe,0xe2,0x2e,0x40,0x9f,0x96,0xe9,0x3d,0x7e,0x11,0x73,0x93,0x17,0x2a};// clear text

//256 bit test key
unsigned char key[] = {0x60,0x3d,0xeb,0x10,0x15,0xca,0x71,0xbe,0x2b,0x73,0xae,0xf0,0x85,0x7d,0x77,0x81,0x1f,0x35,0x2c,0x07,0x3b,0x61,0x08,0xd7,0x2d,0x98,0x10,0xa3,0x09,0x14,0xdf,0xf4};// 32bytes encrytpion key
unsigned char cipherText[] = {0x60,0x1e,0xc3,0x13,0x77,0x57,0x89,0xa5,0xb7,0xa7,0xf5,0x04,0xbb,0xf3,0xd2,0x28};// cipher text
	

BOOL
CreateTestFile(BOOL bypassFilterDriver )
{
	LARGE_INTEGER ByteOffset = {0};
    OVERLAPPED Overlapped = {0};
    DWORD dwTransferred = 0;
    int nError = ERROR_SUCCESS;
	BOOL ret = FALSE;
    DWORD dwFlagsAndAttributes = FILE_FLAG_NO_BUFFERING;

	//Create the test folder.
 	ret = CreateDirectory(testFolder,NULL);

	if( ret == 0 )
	{
		nError = GetLastError();
		if( nError != ERROR_ALREADY_EXISTS )
		{
			PrintErrorMessage(L"Create test folder failed.", nError);
			return FALSE;
		}
	}

	if( bypassFilterDriver )
	{
		dwFlagsAndAttributes |= FILE_ATTRIBUTE_REPARSE_POINT|
								FILE_FLAG_OPEN_REPARSE_POINT|FILE_FLAG_NO_BUFFERING|FILE_FLAG_OPEN_NO_RECALL;
	}

	//Create the test file,if it exist, overwrite it.
	HANDLE pFile = CreateFile(testFile,GENERIC_WRITE,NULL,NULL,CREATE_ALWAYS,dwFlagsAndAttributes,NULL);

	if( pFile == INVALID_HANDLE_VALUE )
	{
		PrintErrorMessage(L"Create test file failed.", GetLastError());
		return FALSE;
	}

	//since we open the file without buffering,we need to write the file with sector align length
	DWORD bufferLength = 65536;
	unsigned char* buffer = (unsigned char*)_aligned_malloc(bufferLength,bufferLength);
	if( NULL == buffer )
	{
		ret = FALSE;
		PrintErrorMessage(L"Allocate memory failed with insufficient resources.",0);
		goto EXIT;
	}

	ZeroMemory(buffer,65536);

	RtlCopyMemory(buffer,clearText,sizeof(clearText));

	// Write test data to the test file.
	if(!WriteFile(pFile, buffer,bufferLength, &dwTransferred, NULL))
	{
		nError = GetLastError();
		PrintErrorMessage(L"WriteFile failed.", nError);
		ret = FALSE;
		goto EXIT;
	}

	SetFileSize(pFile,sizeof(clearText));

	ret = TRUE;

EXIT:

	CloseHandle(pFile);

	if( buffer != NULL )
	{
		_aligned_free( buffer);
	}

	return ret;

}

BOOL
VerifyRawData(BOOL isEncrypted)
{
	HANDLE pFile = INVALID_HANDLE_VALUE;

	ULONG ivLength = 16;
	BYTE ivTag[16];
	BOOL ret = FALSE;

	if(isEncrypted )
	{
		 ret = GetIVTag(testFile,&ivLength,ivTag);
		if(!ret)
		{
			PrintLastErrorMessage( L"121 VerifyRawData GetIVTag failed.");
			return FALSE;
		}	
	}

	//open test file bypass the filter driver.
	pFile = CreateFile( testFile,
						GENERIC_READ,NULL,NULL,
						OPEN_EXISTING,
						FILE_ATTRIBUTE_ENCRYPTED|FILE_ATTRIBUTE_REPARSE_POINT|
						FILE_FLAG_OPEN_REPARSE_POINT|FILE_FLAG_NO_BUFFERING|FILE_FLAG_OPEN_NO_RECALL,
						NULL);

	if( pFile == INVALID_HANDLE_VALUE )
	{
		PrintErrorMessage(L"Open test file failed.", GetLastError());
		return FALSE;
	}

	//since we open the file without buffering,we need to read the file with sector align length
	DWORD bufferLength = 65536;
	unsigned char* buffer = (unsigned char*)_aligned_malloc(bufferLength,bufferLength);
	if( NULL == buffer )
	{
		ret = FALSE;
		PrintErrorMessage(L"Allocate memory failed with insufficient resources.",0);
		goto EXIT;
	}

	ret = ReadFile(pFile,buffer,bufferLength,&bufferLength,NULL);
	if(0 == ret)
	{
		PrintErrorMessage(L"Read test file failed.",GetLastError());
		goto EXIT;
	}

	if( isEncrypted && ivLength == 0)
	{
		ret = FALSE;
		PrintErrorMessage(L"VerifyRawData failed, the encrypted file doesn't have iv tag.",0);
		goto EXIT;
	}
	
	if( isEncrypted )
	{
		if( memcmp(buffer,cipherText,bufferLength) == 0)
		{
			ret = TRUE;
		}
		else
		{
			printf("Compare encrypted data failed.\r\nCipher data:");
			for(int i = 0; i < sizeof(cipherText); i++)
			{
				printf("%2x",cipherText[i]);
			}

			printf("\r\nRaw data:");
			for(int i = 0; i < sizeof(cipherText); i++)
			{
				printf("%2x",buffer[i]);
			}

			printf("\r\n");

			ret = FALSE;
		}
	}
	else
	{
		if( memcmp(buffer,clearText,bufferLength) == 0)
		{
			ret = TRUE;
		}
		else
		{
			printf("Compare decrypted data failed.\r\nClear data:");
			for(int i = 0; i < sizeof(clearText); i++)
			{
				printf("%2x",clearText[i]);
			}

			printf("\r\nRaw data:");
			for(int i = 0; i < sizeof(clearText); i++)
			{
				printf("%2x",buffer[i]);
			}

			printf("\r\n");

			ret = FALSE;
		}
	}

EXIT:
	
	if( pFile != INVALID_HANDLE_VALUE )
	{
		CloseHandle(pFile);
	}

	if( buffer != NULL )
	{
		_aligned_free( buffer);
	}


	return ret;

}

BOOL
CopyEncryptedFile()
{
	HANDLE pFile = INVALID_HANDLE_VALUE;
	HANDLE pFile2 = INVALID_HANDLE_VALUE;
	BOOL ret = FALSE;
	ULONG ivLength = 16;
	BYTE ivTag[16];
	unsigned char* buffer = NULL;
	
	ret = GetIVTag(testFile,&ivLength,ivTag);
	if(!ret)
	{
		PrintLastErrorMessage(L"GetIVTag failed.");
		return FALSE;
	}

	if( ivLength == 0 )
	{
		PrintErrorMessage(L"The file is not encrypted.",0);
		return FALSE;
	}

	//open test file bypass the filter driver.
	pFile = CreateFile( testFile,
						GENERIC_READ,NULL,NULL,
						OPEN_EXISTING,
						FILE_ATTRIBUTE_ENCRYPTED|FILE_ATTRIBUTE_REPARSE_POINT|
						FILE_FLAG_OPEN_REPARSE_POINT|FILE_FLAG_NO_BUFFERING|FILE_FLAG_OPEN_NO_RECALL,
						NULL);

	if( pFile == INVALID_HANDLE_VALUE )
	{
		PrintErrorMessage(L"Open test file failed.", GetLastError());
		return FALSE;
	}

	LARGE_INTEGER fileSize;
	if( !GetFileSizeEx(pFile,&fileSize))
	{
		PrintErrorMessage(L"Get test file size failed.", GetLastError());
		ret = FALSE;
		goto EXIT;
	}

	//Create test file bypass the filter driver.
	pFile2 = CreateFile( copyFile,
						GENERIC_WRITE,NULL,NULL,
						CREATE_ALWAYS,
						FILE_ATTRIBUTE_ENCRYPTED|FILE_ATTRIBUTE_REPARSE_POINT|
						FILE_FLAG_OPEN_REPARSE_POINT|FILE_FLAG_NO_BUFFERING|FILE_FLAG_OPEN_NO_RECALL,
						NULL);

	if( pFile2 == INVALID_HANDLE_VALUE )
	{
		PrintErrorMessage(L"Create test copy file failed.", GetLastError());
		return FALSE;
	}

	DWORD dwTransferred = 0;
	DWORD bufferLength = 65536;
	buffer = (unsigned char*)_aligned_malloc(bufferLength,bufferLength);
	if( NULL == buffer )
	{
		ret = FALSE;
		PrintErrorMessage(L"Allocate memory failed with insufficient resources.",0);
		goto EXIT;
	}

	ZeroMemory(buffer,65536);

	ret = ReadFile(pFile,buffer,bufferLength,&dwTransferred,NULL);
	if(0 == ret)
	{
		PrintErrorMessage(L"Read test file failed.",GetLastError());
		goto EXIT;
	}		

	// Write test data to the test file.
	if(!WriteFile(pFile2, buffer, bufferLength, &dwTransferred, NULL))
	{
		PrintErrorMessage(L"WriteFile failed.", GetLastError());
		ret = FALSE;
		goto EXIT;
	}

	//set back the correct file size 
	ret = SetFileSize(pFile2,fileSize.QuadPart);
	if(!ret)
	{
		PrintLastErrorMessage( L"SetFileSize failed.");
	}

	if(pFile2 != INVALID_HANDLE_VALUE)
	{
		CloseHandle(pFile2);
	}

	//add the iv tag to the encrypted file, or the filter driver can't recognize the encrypted file.
	ret = AddIVTag(copyFile,ivLength,ivTag);
	if(!ret)
	{
		PrintLastErrorMessage( L"AddIVTag failed.");
	}

EXIT:

	if(pFile != INVALID_HANDLE_VALUE)
	{
		CloseHandle(pFile);
	}

	if(pFile2 != INVALID_HANDLE_VALUE)
	{
		CloseHandle(pFile2);
	}

	if( buffer != NULL )
	{
		_aligned_free( buffer);
	}

	return ret;
}

BOOL
VerifyFilterDriverDecryptData()
{
	BOOL ret = FALSE;
	HANDLE pFile = INVALID_HANDLE_VALUE;

	pFile = CreateFile( copyFile,
						GENERIC_READ,NULL,NULL,
						OPEN_EXISTING,
						FILE_ATTRIBUTE_NORMAL,
						NULL);
	
	if( pFile == INVALID_HANDLE_VALUE )
	{
		PrintErrorMessage(L"Open encrypted file failed.", GetLastError());
		return FALSE;
	}

	DWORD bufferLength = 65536;
	unsigned char* buffer = (unsigned char*)_aligned_malloc(bufferLength,bufferLength);
	if( NULL == buffer )
	{
		ret = FALSE;
		PrintErrorMessage(L"Allocate memory failed with insufficient resources.",0);
		goto EXIT;
	}

	ZeroMemory(buffer,65536);

	ret = ReadFile(pFile,buffer,bufferLength,&bufferLength,NULL);
	if(0 == ret)
	{
		PrintErrorMessage(L"Read test file failed.",GetLastError());
		goto EXIT;
	}

	if( memcmp(buffer,clearText,bufferLength) == 0)
	{
		ret = TRUE;
	}
	else
	{
		printf("Verified filter driver decryption data failed.\r\nclearText data:");
		for(int i = 0; i < sizeof(clearText); i++)
		{
			printf("%2x",clearText[i]);
		}

		printf("\r\nReturn data:");
		for(int i = 0; i < sizeof(clearText); i++)
		{
			printf("%2x",buffer[i]);
		}

		printf("\r\n");

		ret = FALSE;

	}

EXIT:
	
	if( pFile != INVALID_HANDLE_VALUE )
	{
		CloseHandle(pFile);
	}

	if( buffer != NULL )
	{
		_aligned_free( buffer);
	}


	return ret;

}

VOID
EncryptionUnitTest()
{
	DeleteFile(testFile);
	DeleteFile(copyFile);

	//create a new test file
	if(!CreateTestFile(TRUE))
	{
		return;
	}
	else
	{
		PrintPassedMessage(L"Created test file passed.\n");
	}
	
	if(!AESEncryptFile(testFile,sizeof(key),key,sizeof(iv),iv,TRUE))
	{
		PrintLastErrorMessage( L"447 AESEncryptFile failed.");
		return;
	}
	else
	{
		PrintPassedMessage(L"452 Encrypted test file passed.\n");
	}

	if(!VerifyRawData(TRUE))
	{
		PrintFailedMessage(L"457 Verified AESEncryptFile test failed.\n");
		return;
	}
	else
	{
		PrintPassedMessage(L"462 Verified encrypted file data passed.\n");
	}

	if(!CopyEncryptedFile())
	{
		return;
	}
	else
	{
		PrintPassedMessage(L"471 Copied encrypted file passed.\n");
	}

	if(!AESDecryptFile(testFile,sizeof(key),key,sizeof(iv),iv))
	{
		PrintLastErrorMessage( L"476 AESDecryptFile failed.");
		return;
	}
	else
	{
		PrintPassedMessage(L"481 Decrypted file passed.\n");
	}

	if(!VerifyRawData(FALSE))
	{
		PrintFailedMessage(L"486 Verified AESDecryptFile test failed.\n");
		return;
	}
	else
	{
		PrintPassedMessage(L"491 Verified decrypted file data passed.\n");
	}

	if(!VerifyFilterDriverDecryptData())
	{
		PrintFailedMessage(L"496 Verified filter driver decryption test failed.\n");
		return;
	}
	else
	{
		PrintPassedMessage(L"501 Verified filter driver decryption test passed.\n");
	}


	//create a new test file
	//test the filter driver encrypt the file, the filter driver should be enabled, and didn't exclude the current process Id.
	if(!CreateTestFile(FALSE))
	{
		PrintFailedMessage(L"513 Created encrypted test file with filter driver failed.\n");
		return;
	}
	else
	{
		PrintPassedMessage(L"518 Created encrypted test file with filter driver passed.\n");
	}

	//decrypt the filter driver encrypted file with decryption API
	if(!AESDecryptFile(testFile,sizeof(key),key,0,NULL))
	{
		PrintLastErrorMessage( L"538 AESDecryptFile filter driver encrypted file failed.");
		return;
	}
	
	if(!VerifyRawData(FALSE))
	{
		PrintFailedMessage(L"548 Verified filter driver encryption test failed.\n");
		return;
	}
	else
	{
		PrintPassedMessage(L"553 Verified filter driver encryption test passed.\n");
	}

	PrintPassedMessage(L"Encryption unit test passed.\n");
	wprintf(L"\r\n");

}