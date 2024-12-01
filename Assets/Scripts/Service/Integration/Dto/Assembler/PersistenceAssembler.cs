//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Service.Integration.Table;
using UnityEngine;

namespace Service.Integration.Dto.Assembler
{
    public sealed class PersistenceAssembler<T> : BaseAssembler<T> where T : BaseTable, new()
    {
        public PersistenceAssembler(string daoName)
        {
            this.daoName = daoName;
        }

        public string daoName { get; }

        public override List<T> WriteToTableList()
        {
            if (!PlayerPrefs.HasKey(daoName)) return null;
            var formatter = new BinaryFormatter();
            var recordList = PlayerPrefs.GetString(daoName);
            var stream = new MemoryStream(Convert.FromBase64String(recordList));
            var ret = formatter.Deserialize(stream) as List<T>;
            return ret;
        }

        public bool WriteToPlayerPrefs(List<T> recordList)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, recordList);
            var tmp = Convert.ToBase64String(stream.ToArray());
            try
            {
                PlayerPrefs.SetString(daoName, tmp);
                PlayerPrefs.Save();
            }
            catch (PlayerPrefsException)
            {
                return false;
            }

            return true;
        }
    }
}