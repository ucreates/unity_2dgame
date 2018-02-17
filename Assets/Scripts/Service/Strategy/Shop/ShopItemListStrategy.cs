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
using Service;
using Service.Integration;
using Service.Integration.Table;
using Service.BizLogic;
using Core.Entity;
namespace Service.Strategy {
public sealed class ShopItemListStrategy : BaseStrategy {
    public override Response Get(Parameter parameter = null) {
        Response ret = new Response();
        UserBizLogic ubl = new UserBizLogic();
        ItemBizLogic ibl = new ItemBizLogic();
        MUserTable mut = ubl.GetPlayer();
        List<string> hadItemIdList = new List<string>();
        List<MItemTable> recordList =  ibl.GetAllItemMaster();
        foreach (MItemTable record in recordList) {
            if (ibl.HasItem(mut.id, record.id)) {
                hadItemIdList.Add(record.name);
            }
        }
        List<MItemTable> itemMasterList = ibl.GetAllItemMaster();
        ret.Set<List<string>>("itemidlist", hadItemIdList);
        ret.Set<List<MItemTable>>("itemmasterlist", itemMasterList);
        ret.Set<int>("coin", mut.coin);
        return ret;
    }
}
}
