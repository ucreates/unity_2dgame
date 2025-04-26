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
using Core.Extensions;
using Service.Integration;
using Service.Integration.Table;

namespace Service.BizLogic
{
    public sealed class ItemBizLogic : BaseBizLogic
    {
        public void InitializeMaster(Dictionary<string, int> masterDictionary)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MItemTable>();
            if (0 == dao?.recordList.Count)
            {
                var uow = new UnitOfWork<MItemTable>();
                masterDictionary.ForEach(pair => { uow.addRecordList.Add(new MItemTable(pair.Key, pair.Value)); });
                uow.Commit();
            }
        }

        public List<MItemTable> GetAllItemMaster()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MItemTable>();
            return dao?.recordList;
        }

        public MItemTable GetMasterByItemId(int itemId)
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<MItemTable>();
            var master = dao?.FindBy(itemId);
            return master?.record;
        }

        public int GetPriceByItemId(int itemId)
        {
            var master = GetMasterByItemId(itemId);
            return master.price;
        }

        public bool BuyItem(int userId, int itemId, int amont)
        {
            var ret = false;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TItemTable>();
            var transaction = dao?.FindBy(record => record.userId == userId && record.itemId == itemId);
            if (0 == transaction.Count)
            {
                var record = new TItemTable();
                record.userId = userId;
                record.itemId = itemId;
                record.amount = amont;
                ret = dao?.Save(record) ?? false;
            }
            else
            {
                var record = transaction.FirstOrDefault();
                record.amount += amont;
                ret = dao?.Update(record) ?? false;
            }

            return ret;
        }

        public bool HasItem(int userId, int itemId)
        {
            var ret = false;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TItemTable>();
            var transaction = dao?.FindBy(record => record.userId == userId && record.itemId == itemId);
            if (0 < transaction.Count) ret = true;
            return ret;
        }
    }
}