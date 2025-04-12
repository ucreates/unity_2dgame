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
        public override Response Update(in object parameter)
        {
            var paramBody = ((int itemId, int amount, string messsage))parameter;
            var sret = new Response();
            var ubl = new UserBizLogic();
            var ibl = new ItemBizLogic();
            var price = ibl?.GetPriceByItemId(paramBody.itemId) ?? 0;
            var mut = ubl?.GetPlayer() ?? null;
            if (ibl.HasItem(mut.id, paramBody.itemId))
            {
                sret.Set("message", StoreAssembler.VALID_PURCHASE_FAILD_HAD_ITEM);
                sret.resultStatus = Response.ServiceStatus.FAILED;
                return sret;
            }

            var ret = ubl.UseCoin(price);
            if (ret)
            {
                ibl.BuyItem(mut.id, paramBody.itemId, paramBody.amount);
                var item = ibl.GetMasterByItemId(paramBody.itemId);
                var message = $"{item.name} を{paramBody.amount}個 購入しました";
                sret.Set("message", message);
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