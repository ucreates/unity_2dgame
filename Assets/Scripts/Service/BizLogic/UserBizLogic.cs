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
namespace Service.BizLogic {
public sealed class UserBizLogic : BaseBizLogic {
    public UserBizLogic() {
    }
    public bool AddNewUser(string nickName, string password, int gender, string mailOrPhone, int coin, bool isPlayer) {
        MUserTable mut =  this.FindByNickName(nickName);
        if (null != mut) {
            return false;
        }
        MUserTable master = new MUserTable();
        master.nickName = nickName;
        master.password = password;
        master.gender = gender;
        master.mailOrPhone = mailOrPhone;
        master.coin = coin;
        master.isPlayer = isPlayer;
        return this.AddNewUser(master);
    }
    public bool AddNewUser(MUserTable master) {
        MUserTable mut =  this.FindByNickName(master.nickName);
        if (null != mut) {
            return false;
        }
        DataBase db = DataBase.GetInstance();
        Dao<MUserTable> dao = db.FindBy<MUserTable>();
        foreach (MUserTable table in dao.recordList) {
            table.isPlayer = false;
        }
        return dao.Save(master);
    }
    public void AddNewUser(List<MUserTable> masterList) {
        UnitOfWork<MUserTable> muow = new UnitOfWork<MUserTable>();
        muow.addRecordList = masterList;
        muow.Commit();
    }
    public bool UseCoin(int diffCoin) {
        MUserTable mut = this.GetPlayer();
        if (mut.coin < diffCoin) {
            return false;
        }
        if (mut.coin > diffCoin) {
            mut.coin = mut.coin - diffCoin;
        } else {
            mut.coin = 0;
        }
        DataBase db = DataBase.GetInstance();
        Dao<MUserTable> dao = db.FindBy<MUserTable>();
        return dao.Update(mut);
    }
    public MUserTable GetPlayer() {
        DataBase db = DataBase.GetInstance();
        Dao<MUserTable> dao = db.FindBy<MUserTable>();
        ConditionExpression condition = new ConditionExpression("isPlayer", "==", new FieldSchema<bool>(true));
        List<MUserTable> userList = dao.FindBy(condition);
        if (0 < userList.Count) {
            return userList[0];
        }
        return null;
    }
    public MUserTable GetUser(int userId) {
        DataBase db = DataBase.GetInstance();
        Dao<MUserTable> dao = db.FindBy<MUserTable>();
        MUserTable mut = dao.FindBy(userId);
        return mut;
    }
    public MUserTable FindByNickName(string nickName) {
        DataBase db = DataBase.GetInstance();
        Dao<MUserTable> dao = db.FindBy<MUserTable>();
        ConditionExpression condition = new ConditionExpression("nickName", "==", new FieldSchema<string>(nickName));
        List<MUserTable> userList = dao.FindBy(condition);
        if (0 < userList.Count) {
            return userList[0];
        }
        return null;
    }
}
}
