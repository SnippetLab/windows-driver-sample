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
using System.Xml.Serialization;

namespace EaseFilter.CommonObjects
{
    public enum AESFlags : uint
    {
        Flags_Enabled_Expire_Time = 0x00000010,
        Flags_Enabled_Check_ProcessName = 0x00000020,
        Flags_Enabled_Check_UserName = 0x00000040,
        Flags_Enabled_Check_AccessFlags = 0x00000080,
        Flags_Enabled_Check_User_Permit = 0x00000100,
        Flags_AES_Key_Was_Embedded = 0x00000200,
        Flags_Enabled_Request_IV_And_Key = 0x00000400,
        Flags_Enabled_Revoke_Access_Control = 0x00000800,
        Flags_Enabled_Check_Computer_Id = 0x00001000,
        Flags_Enabled_Check_User_Password = 0x00002000, 

    }

    /// <summary>
    /// This is the DR info meta data which will be stored in server if revoke access control is enabled.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class DRPolicy
    {
        /// <summary>
        /// If it is not empty, only the processes in the process list can access the files.
        /// </summary>
        public string IncludeProcessNames;
        /// <summary>
        /// If it is not empty, all the processes in the process list can not access the files.
        /// </summary>
        public string ExcludeProcessNames;
        /// <summary>
        /// If it is not empty, only the users in the user name list can access the files.
        /// </summary>
        public string IncludeUserNames;
        /// <summary>
        /// If it is not empty, all the useres in the user name list can not access the files.
        /// </summary>
        public string ExcludeUserNames;
        /// <summary>
        /// If it is not empty, only the computer in the computer id list can access the files.
        /// </summary>
        public string IncludeComputerIds;
        /// <summary>
        /// the password of the shared file.
        /// </summary>
        public string UserPassword;
        /// <summary>
        /// the access flags of the shared file.
        /// </summary>
        public string AccessFlags;
        /// <summary>
        /// The file will be expired after the expire time, and it can't be accessed.           
        /// </summary>
        public long ExpireTime;
        /// <summary>
        /// The time of the encrypted file was created.
        /// </summary>
        public long CreationTime;
        /// <summary>
        /// the file name which was applied with policy.
        /// </summary>
        public string FileName;
    }    

    /// <summary>
    /// This the DR data which will be embedded to the encyrpted file
    /// if the revoke access control flag is not enabled, or the dr info
    /// will be stored in the server.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DRPolicyData
    {
        public uint AESVerificationKey;
        public AESFlags AESFlags;
        public uint IVLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] IV;
        public uint EncryptionKeyLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] EncryptionKey;
        public long CreationTime;
        public long ExpireTime;
        public uint AccessFlags;
        public long FileSize;
        public uint LengthOfIncludeProcessNames;
        public uint OffsetOfIncludeProcessNames;
        public uint LengthOfExcludeProcessNames;
        public uint OffsetOfExcludeProcessNames;
        public uint LengthOfIncludeUserNames;
        public uint OffsetOfIncludeUserNames;
        public uint LengthOfExcludeUserNames;
        public uint OffsetOfExcludeUserNames;
        public uint LengthOfAccountName;
        public uint OffsetOfAccountName;
        public uint LengthOfComputerIds;
        public uint OffsetOfComputerIds;
        public uint LengthOfUserPassword;
        public uint OffsetOfUserPassword;
        public string IncludeProcessNames;
        public string ExcludeProcessNames;
        public string IncludeUserNames;
        public string ExcludeUserNames;
        public string AccountName;
        public string ComputerIds;
        public string UserPassword;
        public uint SizeOfAESData;

    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct UserInfo
    {
        /// <summary>
        /// the owner account name
        /// </summary>
        public string AccountName;
        /// <summary>
        /// the password of the account
        /// </summary>
        public string AccountPassword;
        /// <summary>
        /// the process name which access the file.
        /// </summary>
        public string ProcessName;
        /// <summary>
        /// the user name who access the file.
        /// </summary>
        public string UserName;
        /// <summary>
        /// the computer information which access the file.
        /// </summary>
        public string ComputerId;
        /// <summary>
        /// the encrypted file which was accessed.
        /// </summary>
        public string FileName;
        /// <summary>
        /// the creation time of the file which was accessed.
        /// </summary>
        public long CreationTime;
        /// <summary>
        /// the password of the user input.
        /// </summary>
        public string UserPassword;
    }


    public class DigitalRightControl
    {
        public static string SECURE_SHARE_FILE_EXTENSION = ".psf";
        public const uint AES_VERIFICATION_KEY = 0xccb76e80;
        public static string WorkingFolder = string.Empty;
        public static string PassPhrase = string.Empty;        

        private static byte[] GetPolicyBuffer(long fileSize,DRPolicyData policy,byte[] iv, byte[] encryptionKey)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(AES_VERIFICATION_KEY);
            bw.Write((uint)(policy.AESFlags));
            bw.Write(iv.Length);
            bw.Write(iv);
            bw.Write(encryptionKey.Length);

            if (encryptionKey.Length < 32)
            {
                //the struture always keep 32 bytes for encryption key.
                Array.Resize(ref encryptionKey, 32);
            }

            bw.Write(encryptionKey);
            bw.Write(policy.CreationTime);
            bw.Write(policy.ExpireTime);
            bw.Write((uint)policy.AccessFlags);
            bw.Write(fileSize);
            bw.Write(policy.LengthOfIncludeProcessNames);
            
            //the first offset lenght = current position + 13*4
            //    sizeof(OffsetOfIncludeProcessNames)  +  sizeof(OffsetOfExcludeProcessNames) + sizeof(LengthOfExcludeProcessNames)
            // +  sizeof(OffsetOfIncludeUserNames) + sizeof(LengthOfIncludeUserNames) +  sizeof(OffsetOfExcludeUserNames) + sizeof(LengthOfExcludeUserNames)
            // +  sizeof(OffsetOfAccountName) + sizeof(LengthOfAccountName) +  sizeof(OffsetOfComputerIds) + sizeof(LengthOfComputerIds)
            // +  sizeof(LengthOfUserPassword) + sizeof(OffsetOfUserPassword) 
            
            policy.OffsetOfIncludeProcessNames = (uint)ms.Length + 13 * 4;
            bw.Write(policy.OffsetOfIncludeProcessNames);

            bw.Write(policy.LengthOfExcludeProcessNames);
            policy.OffsetOfExcludeProcessNames = policy.OffsetOfIncludeProcessNames + policy.LengthOfIncludeProcessNames;
            bw.Write(policy.OffsetOfExcludeProcessNames);

            bw.Write(policy.LengthOfIncludeUserNames);
            policy.OffsetOfIncludeUserNames = policy.OffsetOfExcludeProcessNames + policy.LengthOfExcludeProcessNames;
            bw.Write(policy.OffsetOfIncludeUserNames);

            bw.Write(policy.LengthOfExcludeUserNames);
            policy.OffsetOfExcludeUserNames = policy.OffsetOfIncludeUserNames + policy.LengthOfIncludeUserNames;
            bw.Write(policy.OffsetOfExcludeUserNames);
            
            bw.Write(policy.LengthOfAccountName);
            policy.OffsetOfAccountName = policy.OffsetOfExcludeUserNames + policy.LengthOfExcludeUserNames;
            bw.Write(policy.OffsetOfAccountName);

            bw.Write(policy.LengthOfComputerIds);
            policy.OffsetOfComputerIds = policy.OffsetOfAccountName + policy.LengthOfAccountName;
            bw.Write(policy.OffsetOfComputerIds);

            bw.Write(policy.LengthOfUserPassword);
            policy.OffsetOfUserPassword = policy.OffsetOfComputerIds + policy.LengthOfComputerIds;
            bw.Write(policy.OffsetOfUserPassword);


            byte[] strBuffer;
            if (policy.LengthOfIncludeProcessNames > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.IncludeProcessNames);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfExcludeProcessNames > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.ExcludeProcessNames);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfIncludeUserNames > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.IncludeUserNames);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfExcludeUserNames > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.ExcludeUserNames);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfAccountName > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.AccountName);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfComputerIds > 0)
            {

                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.ComputerIds);
                bw.Write(strBuffer);
            }

            if (policy.LengthOfUserPassword > 0)
            {
                strBuffer = UnicodeEncoding.Unicode.GetBytes(policy.UserPassword);
                bw.Write(strBuffer);
            }

            byte[] AESBuffer = ms.ToArray();

            //encrypt the access policy except the sizeOfAESData;
            FilterAPI.AESEncryptDecryptBuffer(AESBuffer, 0, null, null);

            return AESBuffer;
        }

        /// <summary>
        /// Create an encrypted file with embedded digital right policy, distribute the encrypted file via internet, 
        /// only the authorized users and processes can access the encrypted file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="passPhrase"></param>
        /// <param name="policy"></param>
        /// <param name="lastError"></param>
        /// <returns></returns>
        public static bool EncryptFileWithEmbeddedDRPolicy(string sourceFileName, string destFileName, byte[] encryptIV, byte[] encryptKey,DRPolicyData policy, out string lastError)
        {
            bool ret = false;
            FileStream fs = null;
            lastError = string.Empty;

            try
            {
                if (!File.Exists(sourceFileName))
                {
                    lastError = sourceFileName + " doesn't exist.";
                    return false;
                }

                FileInfo fileInfo = new FileInfo(sourceFileName);
                long fileSize = fileInfo.Length;

                byte[] AESBuffer = GetPolicyBuffer(fileSize, policy, encryptIV, encryptKey);


                //encrypt the file with encryption key and a iv key.
                ret = FilterAPI.AESEncryptFileToFile(sourceFileName, destFileName, (uint)encryptKey.Length, encryptKey, (uint)encryptIV.Length, encryptIV, false);
                if (!ret)
                {
                    lastError = "Create encrypt file " + destFileName + " failed with error:" + FilterAPI.GetLastErrorMessage();
                    return ret;
                }

                fs = new FileStream(destFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

                //append the DR policy to the encrypted file.
                fs.Write(AESBuffer, 0, AESBuffer.Length);

                //append the sizeof the DR policy
                fs.Write(BitConverter.GetBytes(AESBuffer.Length + 4 ), 0, 4);                

            }
            catch (Exception ex)
            {
                ret = false;
                lastError = "Encrypt file " + sourceFileName + " failed with error:" + ex.Message;
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
            }

            return ret;
        }

        /// <summary>
        /// Process the encrypted file's embedded access policy, remove embedded information, add AESTagData to encrypted file, 
        /// Create a filter driver aware encrypted file. Then you can read the encrypted file transparently via filter driver encryption engine.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="passPhrase"></param>
        /// <param name="lastError"></param>
        /// <returns></returns>
        public static bool ProcessSecureShareFile(string fileName, out string lastError)
        {
            bool ret = false;
            lastError = string.Empty;

            try
            {
                if (!File.Exists(fileName))
                {
                    lastError = fileName + " doesn't exist.";
                    return false;
                }

                if (!fileName.EndsWith(SECURE_SHARE_FILE_EXTENSION))
                {
                    lastError = fileName + " extension is not correct.";
                    return false;
                }

                FileAttributes attributes = File.GetAttributes(fileName);
                attributes = (~FileAttributes.ReadOnly) & attributes;
                File.SetAttributes(fileName, attributes);

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                long fileSize = fs.Length;

                //read the last 4 bytes data, it is the total size of the embedded data.

                fs.Position = fileSize - 4;
                BinaryReader br = new BinaryReader(fs);
                uint sizeOfAESData = br.ReadUInt32();

                if (sizeOfAESData >= fileSize)
                {
                    lastError = fileName + " is not valid share encrypted file, the sizeOfAESData:" + sizeOfAESData + " >= file size:" + fileSize;
                    return false;
                }

                fs.Position = fileSize - sizeOfAESData;

                //Read the embedded data 
                byte[] AESBuffer = new byte[sizeOfAESData];
                fs.Read(AESBuffer, 0, (int)sizeOfAESData);

                //decrypt the embedded data, since the last 4 bytes is not encrypted, after decryption,need to write the clear size back.
                FilterAPI.AESEncryptDecryptBuffer(AESBuffer, 0, null, null);

                //since the last 4 bytes for sizeOfAESData is not encrypted, we need to put back the clear value back.
                MemoryStream ms = new MemoryStream(AESBuffer);
                ms.Position = 0;
                br = new BinaryReader(ms);
                uint verificationKey = br.ReadUInt32();

                //verify if this is the valid embedded data.
                if (verificationKey != AES_VERIFICATION_KEY)
                {
                    lastError = fileName + " is not valid share encrypted file, the encryption key:" + verificationKey + " is not valid.";
                    return false;
                }

                //write back the size of embedded data here.
                ms.Position = ms.Length - 4;
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(sizeOfAESData);

                //Remove the embedded data, this is the original file size without the embedded information.
                fs.SetLength(fileSize - sizeOfAESData);

                fs.Close();
                fs = null;

                string newFileName = fileName.Remove(fileName.Length - SECURE_SHARE_FILE_EXTENSION.Length);

                File.Move(fileName, newFileName);
                //add the DR data to the encrypted file as a tag data.
                ret = FilterAPI.EmbedDRPolicyDataToFile(newFileName, AESBuffer, out lastError);

            }
            catch (Exception ex)
            {
                ret = false;
                lastError = "ProcessSecureShareFile " + fileName + " failed with error:" + ex.Message;
            }


            return ret;
        }


        public static T DecryptStrToObject<T>( string toDeserialize)
        {
            string decryptedStr = FilterAPI.AESEncryptDecryptStr(toDeserialize, FilterAPI.EncryptType.Decryption);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader textReader = new StringReader(decryptedStr);
            return (T)xmlSerializer.Deserialize(textReader);
        }

        public static string EncryptObjectToStr<T>( T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);

            string encryptedText = FilterAPI.AESEncryptDecryptStr(textWriter.ToString(), FilterAPI.EncryptType.Encryption);

            return encryptedText;
        }

     
    }
}
