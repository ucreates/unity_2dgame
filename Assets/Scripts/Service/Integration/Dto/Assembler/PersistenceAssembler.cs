//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Service.Integration.Table;
using UnityEngine;
namespace Service.Integration.Dto.Assembler
{
    public sealed class PersistenceAssembler<T> : BaseAssembler<T> where T : BaseTable, new() {
    public string daoName {
        get;
        private set;
    }
    public PersistenceAssembler(string daoName) {
        this.daoName = daoName;
    }
    public override List<T> WriteToTableList() {
        if (!PlayerPrefs.HasKey(daoName)) {
            return null;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string recordList = PlayerPrefs.GetString(this.daoName);
        MemoryStream stream = new MemoryStream(System.Convert.FromBase64String(recordList));
        List<T> ret = formatter.Deserialize(stream) as List<T>;
        return ret;
    }
    public bool WriteToPlayerPrefs(List<T> recordList) {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, recordList);
        string tmp = System.Convert.ToBase64String(stream.ToArray());
        try {
            PlayerPrefs.SetString(this.daoName, tmp);
            PlayerPrefs.Save();
        } catch (PlayerPrefsException) {
            return false;
        }
        return true;
    }
}
}
