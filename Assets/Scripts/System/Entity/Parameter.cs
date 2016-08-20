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
using System;
using System.Collections.Generic;
namespace Core.Entity {
public sealed class Parameter {
    public Dictionary<string, BaseValue> parameterList {
        get;
        private set;
    }
    public Parameter() {
        this.parameterList = new Dictionary<string, BaseValue> ();
    }
    public T Get<T>(string parameterName) {
        if (false != this.parameterList.ContainsKey(parameterName)) {
            Value<T> ret = this.parameterList [parameterName] as Value<T>;
            return ret.value;
        }
        return default(T);
    }
    public bool Set<T>(string parameterName, T notifyValue) {
        if (false == this.parameterList.ContainsKey(parameterName)) {
            Value<T> value = new Value<T> ();
            value.value = notifyValue;
            this.parameterList.Add(parameterName, value);
            return true;
        }
        return false;
    }
}
}