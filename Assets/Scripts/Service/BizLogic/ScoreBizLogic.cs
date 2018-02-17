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
using System.Collections;
using System.Collections.Generic;
using Service.Integration;
using Service.Integration.Table;
using Service.Integration.Schema;
using Service.Integration.Query.Expression;
using Core.Utility;
namespace Service.BizLogic {
public sealed class ScoreBizLogic : BaseBizLogic {
    public ScoreBizLogic() {
    }
    public int GetClearCount(int userId) {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        ConditionExpression condition = new ConditionExpression("userId", "==", new FieldSchema<int>(userId));
        List<TScoreTable> ret = dao.FindBy(condition);
        int clearCount = 0;
        if (0 < ret.Count) {
            clearCount = ret[0].clearCount;
        }
        return clearCount;
    }
    public bool AddClearCount(int userId, int clearCount) {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        ConditionExpression condition = new ConditionExpression("userId", "==", new FieldSchema<int>(userId));
        List<TScoreTable> ret = dao.FindBy(condition);
        if (0 < ret.Count) {
            ret[0].clearCount += clearCount;
            return dao.Update(ret[0]);
        }
        return false;
    }
    public bool UpdateClearCount(int userId, int clearCount) {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        ConditionExpression condition = new ConditionExpression("userId", "==", new FieldSchema<int>(userId));
        List<TScoreTable> ret = dao.FindBy(condition);
        if (0 < ret.Count) {
            ret[0].clearCount = clearCount;
            return dao.Update(ret[0]);
        }
        return false;
    }
    public int GetBestClearCount() {
        int bestClearCount = PlayerPrefs.GetInt("bestclearcount");
        return bestClearCount;
    }
    public bool UpdateBestClearCount(int clearCount) {
        int bestClearCount = this.GetBestClearCount();
        if (bestClearCount < clearCount) {
            PlayerPrefs.SetInt("bestclearcount", clearCount);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
    public bool Clear() {
        UserBizLogic ubl = new UserBizLogic();
        MUserTable mut = ubl.GetPlayer();
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        ConditionExpression condition = new ConditionExpression("userId", "==", new FieldSchema<int>(mut.id));
        List<TScoreTable> ret = dao.FindBy(condition);
        if (0 < ret.Count) {
            ret[0].clearCount = 0;
            return dao.Update(ret[0]);
        }
        return false;
    }
    public bool AddNewUserScore(int userId) {
        TScoreTable table = new TScoreTable();
        table.userId = userId;
        return this.AddNewUserScore(table);
    }
    public bool AddNewUserScore(TScoreTable table) {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        ConditionExpression condition = new ConditionExpression("userId", "==", new FieldSchema<int>(table.userId));
        List<TScoreTable> ret = dao.FindBy(condition);
        if (0 != ret.Count) {
            return false;
        }
        return dao.Save(table);
    }
    public void AddNewUserScore(List<TScoreTable> tableList) {
        UnitOfWork<TScoreTable> muow = new UnitOfWork<TScoreTable>();
        muow.addRecordList = tableList;
        muow.Commit();
    }
    public List<TScoreTable> GetRankingList() {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        List<TScoreTable> recordList = dao.FindAll();
        for (int i = 0; i < recordList.Count; i++) {
            for (int j = recordList.Count - 1; j > i; j--) {
                int previousIndex = j - 1;
                TScoreTable recordA = recordList[j];
                TScoreTable recordB = recordList[previousIndex];
                if (recordA.clearCount > recordB.clearCount) {
                    TScoreTable tmp = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = tmp;
                }
            }
        }
        return recordList.GetRange(0, 5);
    }
}
}
