﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using NuGet.Services.Configuration;

namespace NuGet.Services.Storage
{
    public class StorageHub
    {
        public StorageAccountHub Primary { get; private set; }
        public StorageAccountHub Backup { get; private set; }

        public StorageHub(ConfigurationHub configuration)
        {
            Primary = TryLoadAccount(configuration, KnownStorageAccount.Primary);
            Backup = TryLoadAccount(configuration, KnownStorageAccount.Backup);
        }

        private StorageAccountHub TryLoadAccount(ConfigurationHub configuration, KnownStorageAccount account)
        {
            string str = configuration.Storage.GetConnectionString(account);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }
            return new StorageAccountHub(CloudStorageAccount.Parse(str));
        }
    }
}
