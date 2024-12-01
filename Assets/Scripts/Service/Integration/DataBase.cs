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
using Service.Integration.Table;

namespace Service.Integration
{
    public sealed class DataBase
    {
        private DataBase()
        {
            daoList = new Dictionary<string, BaseDao>();
            daoList.Add("TScoreTable", new Dao<TScoreTable>());
            daoList.Add("TSummaryTable", new Dao<TSummaryTable>());
            daoList.Add("TLoadingTable", new Dao<TLoadingTable>());
            daoList.Add("MUserTable", new Dao<MUserTable>());
            daoList.Add("MCorporateTable", new Dao<MCorporateTable>());
            daoList.Add("MItemTable", new Dao<MItemTable>());
            daoList.Add("TItemTable", new Dao<TItemTable>());
        }

        public Dictionary<string, BaseDao> daoList { get; }

        public static DataBase instance { get; private set; }

        public static DataBase GetInstance()
        {
            if (null == instance) instance = new DataBase();
            return instance;
        }

        public void Clear()
        {
            foreach (var key in daoList.Keys) daoList[key].Clear();
        }

        public Dao<T> FindBy<T>() where T : BaseTable, new()
        {
            var type = typeof(T);
            var daoName = type.Name;
            if (daoList.ContainsKey(daoName))
            {
                var dao = daoList[daoName] as Dao<T>;
                dao.Reset();
                return dao;
            }

            return null;
        }
    }
}