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

namespace Service.Strategy
{
    public sealed class ShopConfirmStrategy : BaseStrategy
    {
        public override Response Get(Parameter parameter = null)
        {
            var itemId = parameter.Get<int>("itemId");
            var sret = new Response();
            var ibl = new ItemBizLogic();
            var mit = ibl.GetMasterByItemId(itemId);
            sret.Set("itemmaster", mit);
            return sret;
        }
    }
}