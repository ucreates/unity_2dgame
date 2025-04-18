﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Entity;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class ShopItemListStrategy : BaseStrategy
    {
        public override Response Get(in object parameter = null)
        {
            var ret = new Response();
            var ubl = new UserBizLogic();
            var ibl = new ItemBizLogic();
            var mut = ubl?.GetPlayer();
            var hadItemIdList = new List<string>();
            var recordList = ibl?.GetAllItemMaster();
            recordList.ForEach(record =>
            {
                if (ibl.HasItem(mut.id, record.id))
                    hadItemIdList.Add(record.name);
            });
            var itemMasterList = ibl.GetAllItemMaster();
            ret.data = (hadItemIdList, itemMasterList, mut.coin);
            return ret;
        }
    }
}