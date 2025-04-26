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
using System.Linq;
using Service.Integration;
using Service.Integration.Table;
using UnityEngine;

namespace Service.BizLogic
{
    public sealed class ScoreBizLogic : BaseBizLogic
    {
        public int GetClearCount(int userId)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var transaction = dao?.FindBy(record => record.userId == userId);
            var clearCount = 0;
            if (0 < transaction.Count) clearCount = transaction.FirstOrDefault()?.clearCount ?? 0;
            return clearCount;
        }

        public bool AddClearCount(int userId, int clearCount)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var transaction = dao?.FindBy(record => record.userId == userId);
            if (0 < transaction.Count)
            {
                var record = transaction.FirstOrDefault();
                record.clearCount += clearCount;
                return dao?.Update(record) ?? false;
            }

            return false;
        }

        public bool UpdateClearCount(int userId, int clearCount)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var transaction = dao?.FindBy(record => record.userId == userId);
            if (0 < transaction.Count)
            {
                var record = transaction.FirstOrDefault();
                record.clearCount = clearCount;
                return dao?.Update(record) ?? false;
            }

            return false;
        }

        public int GetBestClearCount()
        {
            var bestClearCount = PlayerPrefs.GetInt("bestclearcount");
            return bestClearCount;
        }

        public bool UpdateBestClearCount(int clearCount)
        {
            var bestClearCount = GetBestClearCount();
            if (bestClearCount < clearCount)
            {
                PlayerPrefs.SetInt("bestclearcount", clearCount);
                PlayerPrefs.Save();
                return true;
            }

            return false;
        }

        public bool Clear()
        {
            var ubl = new UserBizLogic();
            var userMaster = ubl.GetPlayer();
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var scoreTransaction = dao?.FindBy(record => record.userId == userMaster.id);
            if (0 < scoreTransaction.Count)
            {
                var record = scoreTransaction.FirstOrDefault();
                record.clearCount = 0;
                return dao?.Update(record) ?? false;
            }

            return false;
        }

        public bool AddNewUserScore(int userId)
        {
            var transaction = new TScoreTable();
            transaction.userId = userId;
            return AddNewUserScore(transaction);
        }

        public bool AddNewUserScore(TScoreTable table)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var transaction = dao?.FindBy(record => record.userId == table.userId);
            if (0 != transaction.Count) return false;
            return dao?.Save(table) ?? false;
        }

        public void AddNewUserScore(List<TScoreTable> tableList)
        {
            var uow = new UnitOfWork<TScoreTable>();
            uow.addRecordList = tableList;
            uow.Commit();
        }

        public List<TScoreTable> GetRankingList()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var transactionList = dao?.FindAll();
            for (var i = 0; i < transactionList.Count; i++)
            for (var j = transactionList.Count - 1; j > i; j--)
            {
                var previousIndex = j - 1;
                var recordA = transactionList[j];
                var recordB = transactionList[previousIndex];
                if (recordA.clearCount > recordB.clearCount)
                {
                    var tmp = transactionList[previousIndex];
                    transactionList[previousIndex] = transactionList[j];
                    transactionList[j] = tmp;
                }
            }

            return transactionList.GetRange(0, 5);
        }
    }
}