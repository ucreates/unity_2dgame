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
            daoDictionary = new Dictionary<string, BaseDao>
            {
                { "TScoreTable", new Dao<TScoreTable>() },
                { "TSummaryTable", new Dao<TSummaryTable>() },
                { "TLoadingTable", new Dao<TLoadingTable>() },
                { "MUserTable", new Dao<MUserTable>() },
                { "MCorporateTable", new Dao<MCorporateTable>() },
                { "MItemTable", new Dao<MItemTable>() },
                { "TItemTable", new Dao<TItemTable>() }
            };
        }

        public Dictionary<string, BaseDao> daoDictionary { get; }

        public static DataBase instance { get; private set; }

        public static DataBase GetInstance()
        {
            instance ??= new DataBase();
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
                return dao;
            }

            return null;
        }
    }
}