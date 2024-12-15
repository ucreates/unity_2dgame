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
using Core.Extensions;
using Service.Integration.Table;

namespace Service.Integration
{
    public sealed class DataBase
    {
        private DataBase()
        {
            daoDictionary = new Dictionary<string, BaseDao>();
            daoDictionary.Add("TScoreTable", new Dao<TScoreTable>());
            daoDictionary.Add("TSummaryTable", new Dao<TSummaryTable>());
            daoDictionary.Add("TLoadingTable", new Dao<TLoadingTable>());
            daoDictionary.Add("MUserTable", new Dao<MUserTable>());
            daoDictionary.Add("MCorporateTable", new Dao<MCorporateTable>());
            daoDictionary.Add("MItemTable", new Dao<MItemTable>());
            daoDictionary.Add("TItemTable", new Dao<TItemTable>());
        }

        public Dictionary<string, BaseDao> daoDictionary { get; }

        public static DataBase instance { get; private set; }

        public static DataBase GetInstance()
        {
            if (null == instance) instance = new DataBase();
            return instance;
        }

        public void Clear()
        {
            daoDictionary.ForEach(pair => { pair.Value.Clear(); });
        }

        public Dao<T> FindBy<T>() where T : BaseTable, new()
        {
            var type = typeof(T);
            var daoName = type.Name;
            if (daoDictionary.ContainsKey(daoName))
            {
                var dao = daoDictionary[daoName] as Dao<T>;
                dao.Reset();
                return dao;
            }

            return null;
        }
    }
}