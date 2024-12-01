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
    public sealed class ItemBizLogic : BaseBizLogic
    {
        public void InitializeMaster(Dictionary<string, int> masterList)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MItemTable>();
            if (0 == dao.recordList.Count)
            {
                var uow = new UnitOfWork<MItemTable>();
                foreach (var name in masterList.Keys)
                {
                    var price = masterList[name];
                    uow.addRecordList.Add(new MItemTable(name, price));
                }

                uow.Commit();
            }
        }

        public List<MItemTable> GetAllItemMaster()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MItemTable>();
            return dao.recordList;
        }

        public MItemTable GetMasterByItemId(int itemId)
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MItemTable>();
            var mit = dao.FindBy(itemId);
            return mit;
        }

        public int GetPriceByItemId(int itemId)
        {
            var mit = GetMasterByItemId(itemId);
            return mit.price;
        }

        public bool BuyItem(int userId, int itemId, int amont)
        {
            var ret = false;
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TItemTable>();
            var condition = new AndExpression();
            condition.conditionList.Add(new ConditionExpression("userId", "==", new FieldSchema<int>(userId)));
            condition.conditionList.Add(new ConditionExpression("itemId", "==", new FieldSchema<int>(itemId)));
            var titList = dao.FindBy(condition);
            if (0 == titList.Count)
            {
                var record = new TItemTable();
                record.userId = userId;
                record.itemId = itemId;
                record.amount = amont;
                ret = dao.Save(record);
            }
            else
            {
                var record = titList[0];
                record.amount += amont;
                ret = dao.Update(record);
            }

            return ret;
        }

        public bool HasItem(int userId, int itemId)
        {
            var ret = false;
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TItemTable>();
            var condition = new AndExpression();
            condition.conditionList.Add(new ConditionExpression("userId", "==", new FieldSchema<int>(userId)));
            condition.conditionList.Add(new ConditionExpression("itemId", "==", new FieldSchema<int>(itemId)));
            var titList = dao.FindBy(condition);
            if (0 < titList.Count) ret = true;
            return ret;
        }
    }
}