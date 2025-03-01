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
using System.Linq;
using System.Text.RegularExpressions;
using Service.Strategy;

namespace Service
{
    public sealed class ServiceGateway
    {
        private ServiceGateway()
        {
            serviceDictionary = new Dictionary<string, BaseService>();
            serviceDictionary.Add("player", new PlayerService());
            serviceDictionary.Add("stats", new StatsService());
            serviceDictionary.Add("master", new MasterService());
            serviceDictionary.Add("shop", new ShopService());
        }

        private static ServiceGateway instance { get; set; }

        private Dictionary<string, BaseService> serviceDictionary { get; }

        public static ServiceGateway GetInstance()
        {
            if (null == instance) instance = new ServiceGateway();
            return instance;
        }

        public BaseStrategy Request(string domainName)
        {
            var protocol = @"service://";
            var regex = new Regex(protocol);
            if (!regex.IsMatch(domainName)) return null;
            domainName = domainName.Replace(protocol, "");
            var delimiter = new[] { '/' };
            var schema = domainName.Split(delimiter);
            if (0 == schema.Length) return null;
            var serviceName = schema.FirstOrDefault();
            if (!serviceDictionary.ContainsKey(serviceName)) return null;
            var service = serviceDictionary[serviceName];
            var strategyName = schema.Aggregate((result, name) => string.Join(result, name)).Replace(serviceName, string.Empty);
            return service.Create(strategyName);
        }
    }
}