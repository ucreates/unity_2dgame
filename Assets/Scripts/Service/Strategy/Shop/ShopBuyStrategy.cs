//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Service.BizLogic;
using Service.Integration.Dto.Assembler;

namespace Service.Strategy
{
    public sealed class ShopBuyStrategy : BaseStrategy
    {
        public override Response Update(Parameter parameter)
        {
            var itemId = parameter.Get<int>("itemId");
            var amount = parameter.Get<int>("amount");
            var sret = new Response();
            var ubl = new UserBizLogic();
            var ibl = new ItemBizLogic();
            var price = ibl.GetPriceByItemId(itemId);
            var mut = ubl.GetPlayer();
            if (ibl.HasItem(mut.id, itemId))
            {
                sret.Set("message", StoreAssembler.VALID_PURCHASE_FAILD_HAD_ITEM);
                sret.resultStatus = Response.ServiceStatus.FAILED;
                return sret;
            }

            var ret = ubl.UseCoin(price);
            if (ret)
            {
                ibl.BuyItem(mut.id, itemId, amount);
                sret.Set("message", StoreAssembler.VALID_PURCHASE_SUCCESS);
                sret.resultStatus = Response.ServiceStatus.SUCCESS;
            }
            else
            {
                sret.Set("message", StoreAssembler.VALID_PURCHASE_FAILD_NO_COIN);
                sret.resultStatus = Response.ServiceStatus.FAILED;
            }

            return sret;
        }
    }
}