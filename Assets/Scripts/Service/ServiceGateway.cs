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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Service.Strategy;
namespace Service {
public sealed class ServiceGateway {
    private static ServiceGateway instance {
        get;
        set;
    }
    private Dictionary<string, BaseService> serviceDictionary {
        get;
        set;
    }
    private ServiceGateway() {
        this.serviceDictionary = new Dictionary<string, BaseService>();
        this.serviceDictionary.Add("player", new PlayerService());
        this.serviceDictionary.Add("stats", new StatsService());
        this.serviceDictionary.Add("master", new MasterService());
        this.serviceDictionary.Add("shop", new ShopService());
    }
    public static ServiceGateway GetInstance() {
        if (null == ServiceGateway.instance) {
            ServiceGateway.instance = new ServiceGateway();
        }
        return ServiceGateway.instance;
    }
    public BaseStrategy Request(string domainName) {
        string protocol = @"service://";
        Regex regex = new Regex(protocol);
        if (false == regex.IsMatch(domainName)) {
            return null;
        }
        domainName = domainName.Replace(protocol, "");
        char[] delimiter = new char[] {'/'};
        string[] schema = domainName.Split(delimiter);
        if (0 == schema.Length) {
            return null;
        }
        string serviceName = schema[0];
        if (false == this.serviceDictionary.ContainsKey(serviceName)) {
            return null;
        }
        BaseService service = this.serviceDictionary[serviceName];
        string strategyName = "";
        for (int i = 1; i < schema.Length; i++) {
            strategyName += schema[i];
            if (i < schema.Length - 1) {
                strategyName += "/";
            }
        }
        return service.Create(strategyName);
    }
}
}
