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
using Service.Integration;
using Service.Integration.Query.Expression;
using Service.Integration.Schema;
using Service.Integration.Table;

namespace Service.BizLogic
{
    public sealed class UserBizLogic : BaseBizLogic
    {
        public bool AddNewUser(string nickName, string password, int gender, string mailOrPhone, int coin,
            bool isPlayer)
        {
            var mut = FindByNickName(nickName);
            if (null != mut) return false;
            var master = new MUserTable();
            master.nickName = nickName;
            master.password = password;
            master.gender = gender;
            master.mailOrPhone = mailOrPhone;
            master.coin = coin;
            master.isPlayer = isPlayer;
            return AddNewUser(master);
        }

        public bool AddNewUser(MUserTable master)
        {
            var mut = FindByNickName(master.nickName);
            if (null != mut) return false;
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MUserTable>();
            foreach (var table in dao.recordList) table.isPlayer = false;
            return dao.Save(master);
        }

        public void AddNewUser(List<MUserTable> masterList)
        {
            var muow = new UnitOfWork<MUserTable>();
            muow.addRecordList = masterList;
            muow.Commit();
        }

        public bool UseCoin(int diffCoin)
        {
            var mut = GetPlayer();
            if (mut.coin < diffCoin) return false;
            if (mut.coin > diffCoin)
                mut.coin = mut.coin - diffCoin;
            else
                mut.coin = 0;
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MUserTable>();
            return dao.Update(mut);
        }

        public MUserTable GetPlayer()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MUserTable>();
            var condition = new ConditionExpression("isPlayer", "==", new FieldSchema<bool>(true));
            var userList = dao.FindBy(condition);
            if (0 < userList.Count) return userList[0];
            return null;
        }

        public MUserTable GetUser(int userId)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MUserTable>();
            var mut = dao.FindBy(userId);
            return mut;
        }

        public MUserTable FindByNickName(string nickName)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MUserTable>();
            var condition = new ConditionExpression("nickName", "==", new FieldSchema<string>(nickName));
            var userList = dao.FindBy(condition);
            if (0 < userList.Count) return userList[0];
            return null;
        }
    }
}