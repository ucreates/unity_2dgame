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
            var serviceParams = ((int itemId, int amount, string messsage))parameter;
            var response = new Response();
            var ubl = new UserBizLogic();
            var ibl = new ItemBizLogic();
            var price = ibl?.GetPriceByItemId(serviceParams.itemId) ?? 0;
            var userMaster = ubl?.GetPlayer() ?? null;
            if (ibl.HasItem(userMaster.id, serviceParams.itemId))
            {
                response.data = StoreAssembler.VALID_PURCHASE_FAILD_HAD_ITEM;
                response.resultStatus = Response.ServiceStatus.FAILED;
                return response;
            }

            var result = ubl.UseCoin(price);
            if (result)
            {
                ibl.BuyItem(userMaster.id, serviceParams.itemId, serviceParams.amount);
                var itemMaster = ibl.GetMasterByItemId(serviceParams.itemId);
                var message = $"{itemMaster.name} を{serviceParams.amount}個 購入しました";
                response.data = message;
                response.resultStatus = Response.ServiceStatus.SUCCESS;
            }
            else
            {
                response.data = StoreAssembler.VALID_PURCHASE_FAILD_NO_COIN;
                response.resultStatus = Response.ServiceStatus.FAILED;
            }

            return response;
        }
    }
}