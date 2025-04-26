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

namespace Service.BizLogic
{
    public sealed class UserBizLogic : BaseBizLogic
    {
        public bool AddNewUser(string nickName, string password, int gender, string mailOrPhone, int coin,
            bool isPlayer)
        {
            var master = FindByNickName(nickName);
            if (null != master) return false;
            master = new MUserTable();
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
            var userMaster = FindByNickName(master.nickName);
            if (null != userMaster) return false;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MUserTable>();
            dao?.recordList.ForEach(record => { record.isPlayer = false; });
            return dao?.Save(master) ?? false;
        }

        public void AddNewUser(List<MUserTable> masterList)
        {
            var uow = new UnitOfWork<MUserTable>();
            uow.addRecordList = masterList;
            uow.Commit();
        }

        public bool UseCoin(int diffCoin)
        {
            var master = GetPlayer();
            if (master.coin < diffCoin) return false;
            if (master.coin > diffCoin)
                master.coin = master.coin - diffCoin;
            else
                master.coin = 0;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MUserTable>();
            return dao?.Update(master) ?? false;
        }

        public MUserTable GetPlayer()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MUserTable>();
            var masterList = dao?.FindBy(record => record.isPlayer);
            return masterList.FirstOrDefault();
        }

        public MUserTable GetUser(int userId)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MUserTable>();
            var master = dao?.FindBy(userId) ?? null;
            return master?.record;
        }

        public MUserTable FindByNickName(string nickName)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MUserTable>();
            var masterList = dao?.FindBy(record => record.nickName == nickName);
            return masterList.FirstOrDefault();
        }
    }
}