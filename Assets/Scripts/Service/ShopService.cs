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
using Service.BizLogic;
using Service.Strategy;
namespace Service {
public sealed class ShopService : BaseService {
    public ShopService() {
        this.strategyDictionary.Add("buy", new ShopBuyStrategy());
        this.strategyDictionary.Add("list", new ShopItemListStrategy());
        this.strategyDictionary.Add("confirm", new ShopConfirmStrategy());
    }
}
}
