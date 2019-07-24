//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Service.Domain;
using Service.Strategy;
namespace Service
{
    public sealed class ServiceGateway : BaseServiceGateway {
    public static ServiceGateway instance {
        get;
        private set;
    }
    public static ServiceGateway GetInstance() {
        if (null == ServiceGateway.instance) {
            ServiceGateway.instance = new ServiceGateway();
        }
        return ServiceGateway.instance;
    }
    protected override void Register() {
        this.serviceDictionary.Add("player", new PlayerDomain());
        this.serviceDictionary.Add("stats", new StatsDomain());
        this.serviceDictionary.Add("master", new MasterDomain());
        this.serviceDictionary.Add("shop", new ShopDomain());
        return;
    }
}
}
