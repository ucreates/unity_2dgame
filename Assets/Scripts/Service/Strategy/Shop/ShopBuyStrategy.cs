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
using Core.Entity;
using Service;
using Service.BizLogic;
using Service.Integration;
using Service.Integration.Dto.Assembler;
using Service.Integration.Table;
namespace Service.Strategy {
public sealed class ShopBuyStrategy : BaseStrategy {
    public override Response Update(Parameter parameter) {
        int itemId = parameter.Get<int>("itemId");
        int amount = parameter.Get<int>("amount");
        Response sret = new Response();
        UserBizLogic ubl = new UserBizLogic();
        ItemBizLogic ibl = new ItemBizLogic();
        int price = ibl.GetPriceByItemId(itemId);
        MUserTable mut = ubl.GetPlayer();
        if (ibl.HasItem(mut.id, itemId)) {
            sret.Set<string>("message", StoreAssembler.VALID_PURCHASE_FAILD_HAD_ITEM);
            sret.resultStatus = Response.ServiceStatus.FAILED;
            return sret;
        }
        bool ret = ubl.UseCoin(price);
        if (false != ret) {
            ibl.BuyItem(mut.id, itemId, amount);
            sret.Set<string>("message", StoreAssembler.VALID_PURCHASE_SUCCESS);
            sret.resultStatus = Response.ServiceStatus.SUCCESS;
        } else {
            sret.Set<string>("message", StoreAssembler.VALID_PURCHASE_FAILD_NO_COIN);
            sret.resultStatus = Response.ServiceStatus.FAILED;
        }
        return sret;
    }
}
}
