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
using System.Collections;
using System.Collections.Generic;
using Core.Utility;
namespace Core.Entity {
public sealed class Session {
    private static Session instance {
        get;
        set;
    }
    private Dictionary<string, object> valueList {
        get;
        set;
    }
    private Session() {
        this.valueList = new Dictionary<string, object>();
    }
    public static Session GetInstance() {
        if (null == Session.instance) {
            Session.instance = new Session();
        }
        return Session.instance;
    }
    public T Get<T>(string key, bool delete = false) {
        if (this.valueList.ContainsKey(key)) {
            Type type = typeof(T);
            if (type == typeof(string) || type is System.Object) {
                T ret = (T)this.valueList[key];
                if (delete) {
                    this.valueList.Remove(key);
                }
                return ret;
            } else {
                T ret = ConvertUtility.ToGenerics<T>(this.valueList[key]);
                if (delete) {
                    this.valueList.Remove(key);
                }
                return ret;
            }
        }
        return default(T);
    }
    public bool Add(string key , object value) {
        if (this.valueList.ContainsKey(key)) {
            this.valueList[key] = value;
            return true;
        }
        this.valueList.Add(key, value);
        return false;
    }
}
}