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
using Core.Entity;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class ShopItemListStrategy : BaseStrategy
    {
        public override Response Get(in object parameter = null)
        {
            var response = new Response();
            var ubl = new UserBizLogic();
            var ibl = new ItemBizLogic();
            var userMaster = ubl?.GetPlayer();
            var hadItemIdList = new List<string>();
            var itemMasterList = ibl?.GetAllItemMaster();
            itemMasterList.ForEach(record =>
            {
                if (ibl.HasItem(userMaster.id, record.id))
                    hadItemIdList.Add(record.name);
            });
            itemMasterList = ibl.GetAllItemMaster();
            response.data = (hadItemIdList, itemMasterList, userMaster.coin);
            return response;
        }
    }
}