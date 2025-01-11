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
            var dao = db.FindBy<TScoreTable>();
            var ret = dao.FindBy(record => record.userId == userId);
            var clearCount = 0;
            if (0 < ret.Count) clearCount = ret.FirstOrDefault()?.clearCount ?? 0;
            return clearCount;
        }

        public bool AddClearCount(int userId, int clearCount)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var ret = dao.FindBy(record => record.userId == userId);
            if (0 < ret.Count)
            {
                var record = ret.FirstOrDefault();
                record.clearCount += clearCount;
                return dao.Update(record);
            }

            return false;
        }

        public bool UpdateClearCount(int userId, int clearCount)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var ret = dao.FindBy(record => record.userId == userId);
            if (0 < ret.Count)
            {
                var record = ret.FirstOrDefault();
                record.clearCount = clearCount;
                return dao.Update(record);
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
            var mut = ubl.GetPlayer();
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var ret = dao.FindBy(record => record.userId == mut.id);
            if (0 < ret.Count)
            {
                var record = ret.FirstOrDefault();
                record.clearCount = 0;
                return dao.Update(record);
            }

            return false;
        }

        public bool AddNewUserScore(int userId)
        {
            var table = new TScoreTable();
            table.userId = userId;
            return AddNewUserScore(table);
        }

        public bool AddNewUserScore(TScoreTable table)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var ret = dao.FindBy(record => record.userId == table.userId);
            if (0 != ret.Count) return false;
            return dao.Save(table);
        }

        public void AddNewUserScore(List<TScoreTable> tableList)
        {
            var muow = new UnitOfWork<TScoreTable>();
            muow.addRecordList = tableList;
            muow.Commit();
        }

        public List<TScoreTable> GetRankingList()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var recordList = dao.FindAll();
            for (var i = 0; i < recordList.Count; i++)
            for (var j = recordList.Count - 1; j > i; j--)
            {
                var previousIndex = j - 1;
                var recordA = recordList[j];
                var recordB = recordList[previousIndex];
                if (recordA.clearCount > recordB.clearCount)
                {
                    var tmp = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = tmp;
                }
            }

            return recordList.GetRange(0, 5);
        }
    }
}