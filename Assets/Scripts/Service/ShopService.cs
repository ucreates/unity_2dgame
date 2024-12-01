//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Service.Strategy;

namespace Service
{
    public sealed class ShopService : BaseService
    {
        public ShopService()
        {
            strategyDictionary.Add("buy", new ShopBuyStrategy());
            strategyDictionary.Add("list", new ShopItemListStrategy());
            strategyDictionary.Add("confirm", new ShopConfirmStrategy());
        }
    }
}