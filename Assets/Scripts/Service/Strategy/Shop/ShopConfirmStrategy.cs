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
using Core.Extensions;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class ShopConfirmStrategy : BaseStrategy
    {
        public override Response Get(in object parameter = null)
        {
            var itemId = parameter.ToInt32();
            var sret = new Response();
            var ibl = new ItemBizLogic();
            var mit = ibl?.GetMasterByItemId(itemId) ?? null;
            sret.Set("itemmaster", mit);
            return sret;
        }
    }
}