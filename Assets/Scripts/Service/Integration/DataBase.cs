//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System;
using System.Collections.Generic;
using Service.Integration;
using Service.Integration.Table;
using Service.Integration.Schema;
namespace Service.Integration {
public sealed class DataBase {
    public Dictionary<string, BaseDao> daoList {
        get;
        private set;
    }
    public static DataBase instance {
        get;
        private set;
    }
    private DataBase() {
        this.daoList = new Dictionary<string, BaseDao>();
        this.daoList.Add("TScoreTable", new Dao<TScoreTable>());
        this.daoList.Add("TSummaryTable", new Dao<TSummaryTable>());
        this.daoList.Add("TLoadingTable", new Dao<TLoadingTable>());
        this.daoList.Add("MUserTable", new Dao<MUserTable>());
        this.daoList.Add("MCorporateTable", new Dao<MCorporateTable>());
        this.daoList.Add("MItemTable", new Dao<MItemTable>());
        this.daoList.Add("TItemTable", new Dao<TItemTable>());
    }
    public static DataBase GetInstance() {
        if (null == DataBase.instance) {
            DataBase.instance = new DataBase();
        }
        return DataBase.instance;
    }
    public void Clear() {
        foreach (string key in this.daoList.Keys) {
            this.daoList [key].Clear();
        }
    }
    public Dao<T> FindBy<T>() where T : BaseTable, new() {
        Type type = typeof(T);
        string daoName = type.Name;
        if (this.daoList.ContainsKey(daoName)) {
            Dao<T> dao = this.daoList[daoName] as Dao<T>;
            dao.Reset();
            return dao;
        }
        return null;
    }
}
}
