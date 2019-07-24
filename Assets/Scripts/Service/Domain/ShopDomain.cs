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
namespace Service.Domain
{
    public sealed class ShopDomain : BaseDomain {
    public ShopDomain() {
        this.strategyDictionary.Add("buy", new ShopBuyStrategy());
        this.strategyDictionary.Add("list", new ShopItemListStrategy());
        this.strategyDictionary.Add("confirm", new ShopConfirmStrategy());
    }
}
}
