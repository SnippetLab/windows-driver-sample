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
using System.Linq;
using System.Text;
using System.Configuration;

namespace EaseFilter.CommonObjects
{
    public class FilterRuleSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public FilterRuleCollection Instances
        {
            get { return (FilterRuleCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class FilterRuleCollection : ConfigurationElementCollection
    {
        public FilterRule this[int index]
        {
            get { return (FilterRule)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(FilterRule filterRule)
        {
            BaseAdd(filterRule);
        }

        public void Clear()
        {
            BaseClear();
        }

        public void Remove(FilterRule filterRule)
        {
            BaseRemove(filterRule.IncludeFileFilterMask);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FilterRule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((FilterRule)element).IncludeFileFilterMask;
        }
    }

    public class FilterRule : ConfigurationElement
    {
        //A filter rule must have a unique include file filter mask, 
        //A filter rule can have multiple exclude file filter masks.
        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty("includeFileFilterMask", IsKey = true, IsRequired = true)]
        public string IncludeFileFilterMask
        {
            get { return (string)base["includeFileFilterMask"]; }
            set { base["includeFileFilterMask"] = value; }
        }

        [ConfigurationProperty("excludeFileFilterMasks", IsRequired = false)]
        public string ExcludeFileFilterMasks
        {
            get { return (string)base["excludeFileFilterMasks"]; }
            set { base["excludeFileFilterMasks"] = value; }
        }

        [ConfigurationProperty("includeProcessNames", IsRequired = false)]
        public string IncludeProcessNames
        {
            get { return (string)base["includeProcessNames"]; }
            set { base["includeProcessNames"] = value; }
        }

        [ConfigurationProperty("excludeProcessNames", IsRequired = false)]
        public string ExcludeProcessNames
        {
            get { return (string)base["excludeProcessNames"]; }
            set { base["excludeProcessNames"] = value; }
        }

        [ConfigurationProperty("includeUserNames", IsRequired = false)]
        public string IncludeUserNames
        {
            get { return (string)base["includeUserNames"]; }
            set { base["includeUserNames"] = value; }
        }

        [ConfigurationProperty("excludeUserNames", IsRequired = false)]
        public string ExcludeUserNames
        {
            get { return (string)base["excludeUserNames"]; }
            set { base["excludeUserNames"] = value; }
        }

        [ConfigurationProperty("includeProcessIds", IsKey = true, IsRequired = true)]
        public string IncludeProcessIds
        {
            get { return (string)base["includeProcessIds"]; }
            set { base["includeProcessIds"] = value; }
        }

        [ConfigurationProperty("excludeProcessIds", IsRequired = false)]
        public string ExcludeProcessIds
        {
            get { return (string)base["excludeProcessIds"]; }
            set { base["excludeProcessIds"] = value; }
        }

        [ConfigurationProperty("hiddenFileFilterMasks", IsRequired = false)]
        public string HiddenFileFilterMasks
        {
            get { return (string)base["hiddenFileFilterMasks"]; }
            set { base["hiddenFileFilterMasks"] = value; }
        }

        [ConfigurationProperty("reparseFileFilterMasks", IsRequired = false)]
        public string ReparseFileFilterMasks
        {
            get { return (string)base["reparseFileFilterMasks"]; }
            set { base["reparseFileFilterMasks"] = value; }
        }

        [ConfigurationProperty("userRights", IsRequired = false)]
        public string UserRights
        {
            get { return (string)base["userRights"]; }
            set { base["userRights"] = value; }
        }

        [ConfigurationProperty("processRights", IsRequired = false)]
        public string ProcessRights
        {
            get { return (string)base["processRights"]; }
            set { base["processRights"] = value; }
        }

        [ConfigurationProperty("encryptionPassPhrase", IsRequired = false)]
        public string EncryptionPassPhrase
        {
            get
            {
                string key = (string)base["encryptionPassPhrase"];
                if (Utils.IsBase64String(key))
                {
                    key = FilterAPI.AESEncryptDecryptStr(key, FilterAPI.EncryptType.Decryption);
                }

                return key;
            }
            set 
            {
                string key = value.Trim();

                if (key.Length > 0)
                {
                    key = FilterAPI.AESEncryptDecryptStr(key, FilterAPI.EncryptType.Encryption);
                }

                base["encryptionPassPhrase"] = key; 
            }
        }

        [ConfigurationProperty("accessFlag", IsRequired = true)]
        public uint AccessFlag
        {
            get { return (uint)base["accessFlag"]; }
            set { base["accessFlag"] = value; }
        }

        /// <summary>
        /// The register the file I/O events
        /// </summary>
        [ConfigurationProperty("eventType", IsRequired = false)]
        public uint EventType
        {
            get { return (uint)base["eventType"]; }
            set { base["eventType"] = value; }
        }

        /// <summary>
        /// register monitor I/O requests
        /// </summary>
        [ConfigurationProperty("monitorIO", IsRequired = false)]
        public uint MonitorIO
        {
            get { return (uint)base["monitorIO"]; }
            set { base["monitorIO"] = value; }
        }

        /// <summary>
        /// register control I/O requests, the filter driver will block and wait for the response.
        /// </summary>
        [ConfigurationProperty("controlIO", IsRequired = false)]
        public uint ControlIO
        {
            get { return (uint)base["controlIO"]; }
            set { base["controlIO"] = value; }
        }

        /// <summary>
        /// Enable the filter rule in boot time for control filter if it is true.
        /// </summary>
        [ConfigurationProperty("isResident", IsRequired = false)]
        public bool IsResident
        {
            get { return (bool)base["isResident"]; }
            set { base["isResident"] = value; }
        } 
    }


}
