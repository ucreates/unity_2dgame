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
using Service.Strategy;

namespace Service
{
    public abstract class BaseService
    {
        public BaseService()
        {
            strategyDictionary = new Dictionary<string, BaseStrategy>();
        }

        protected Dictionary<string, BaseStrategy> strategyDictionary { get; set; }

        public BaseStrategy Create(string strategyName)
        {
            return strategyDictionary.FirstOrDefault(pair => pair.Key.Equals(strategyName)).Value;
        }
    }
}