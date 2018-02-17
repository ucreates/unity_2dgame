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
using Service.Strategy;
namespace Service {
public abstract class BaseService {
    protected Dictionary<string, BaseStrategy> strategyDictionary {
        get;
        set;
    }
    public BaseService() {
        this.strategyDictionary = new Dictionary<string, BaseStrategy>();
    }
    public BaseStrategy Create(string strategyName) {
        if (false != this.strategyDictionary.ContainsKey(strategyName)) {
            return this.strategyDictionary[strategyName];
        }
        return null;
    }
}
}
