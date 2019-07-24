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
    public sealed class ItemBizLogic : BaseBizLogic {
    public ItemBizLogic() {
    }
    public void InitializeMaster(Dictionary<string, int> masterList) {
        DataBase db = DataBase.GetInstance();
        Dao<MItemTable> dao = db.FindBy<MItemTable>();
        if (0 == dao.recordList.Count) {
            UnitOfWork<MItemTable> uow = new UnitOfWork<MItemTable>(DataBase.GetInstance());
            foreach (string name in masterList.Keys) {
                int price = masterList[name];
                uow.addRecordList.Add(new MItemTable(name, price));
            }
            uow.Commit();
        }
    }
    public List<MItemTable> GetAllItemMaster() {
        DataBase db = DataBase.GetInstance();
        Dao<MItemTable> dao = db.FindBy<MItemTable>();
        return dao.recordList;
    }
    public MItemTable GetMasterByItemId(int itemId) {
        DataBase db = DataBase.GetInstance();
        Dao<MItemTable> dao = db.FindBy<MItemTable>();
        MItemTable mit = dao.FindBy(itemId);
        return mit;
    }
    public int GetPriceByItemId(int itemId) {
        MItemTable mit = this.GetMasterByItemId(itemId);
        return mit.price;
    }
    public bool BuyItem(int userId, int itemId, int amont) {
        bool ret = false;
        DataBase db = DataBase.GetInstance();
        Dao<TItemTable> dao = db.FindBy<TItemTable>();
        AndExpression condition = new AndExpression();
        condition.conditionList.Add(new ConditionExpression("userId", "==", new FieldSchema<int>(userId)));
        condition.conditionList.Add(new ConditionExpression("itemId", "==", new FieldSchema<int>(itemId)));
        List<TItemTable> titList = dao.FindBy(condition);
        if (0 == titList.Count) {
            TItemTable record = new TItemTable();
            record.userId = userId;
            record.itemId = itemId;
            record.amount = amont;
            ret = dao.Save(record);
        } else {
            TItemTable record = titList[0];
            record.amount += amont;
            ret = dao.Update(record);
        }
        return ret;
    }
    public bool HasItem(int userId, int itemId) {
        bool ret = false;
        DataBase db = DataBase.GetInstance();
        Dao<TItemTable> dao = db.FindBy<TItemTable>();
        AndExpression condition = new AndExpression();
        condition.conditionList.Add(new ConditionExpression("userId", "==", new FieldSchema<int>(userId)));
        condition.conditionList.Add(new ConditionExpression("itemId", "==", new FieldSchema<int>(itemId)));
        List<TItemTable> titList = dao.FindBy(condition);
        if (0 < titList.Count) {
            ret = true;
        }
        return ret;
    }
}
}
