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
using Core.Entity;
using Service;
using Service.BizLogic;
using Service.Integration;
using Service.Integration.Table;
namespace Service.Strategy {
public sealed class ShopConfirmStrategy : BaseStrategy {
    public override Response Get(Parameter parameter = null) {
        int itemId = parameter.Get<int>("itemId");
        Response sret = new Response();
        ItemBizLogic ibl = new ItemBizLogic();
        MItemTable mit = ibl.GetMasterByItemId(itemId);
        sret.Set<MItemTable>("itemmaster", mit);
        return sret;
    }
}
}
