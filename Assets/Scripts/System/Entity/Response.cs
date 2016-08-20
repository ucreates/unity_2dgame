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
public sealed class Response {
    public enum ServiceStatus {
        SUCCESS = 0,
        FAILED = 0,
    }
    public Dictionary<string, BaseValue> responseDictionary {
        get;
        private set;
    }
    public string errorMessage {
        get;
        set;
    }
    public ServiceStatus resultStatus {
        get;
        set;
    }
    public Response() {
        this.responseDictionary = new Dictionary<string, BaseValue> ();
    }
    public T Get<T>(string name) {
        if (false != this.responseDictionary.ContainsKey(name)) {
            Value<T> ret = this.responseDictionary [name] as Value<T>;
            return ret.value;
        }
        return default(T);
    }
    public bool Set<T>(string name, T responseValue) {
        if (false == this.responseDictionary.ContainsKey(name)) {
            Value<T> value = new Value<T> ();
            value.value = responseValue;
            this.responseDictionary.Add(name, value);
            return true;
        }
        return false;
    }
}
}