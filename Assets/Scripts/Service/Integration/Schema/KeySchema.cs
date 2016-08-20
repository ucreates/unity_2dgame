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
namespace Service.Integration.Schema {
[Serializable]
public sealed class KeySchema {
    public string keyCache {
        get;
        set;
    }
    public Dictionary<string, string> keyHolder {
        get;
        set;
    }
    public KeySchema() {
        this.keyHolder = new Dictionary<string, string> ();
        this.keyCache = string.Empty;
    }
    public string Get() {
        if (false == string.IsNullOrEmpty(this.keyCache)) {
            return this.keyCache;
        }
        foreach (string fieldName in this.keyHolder.Keys) {
            this.keyCache += this.keyHolder[fieldName];
        }
        return this.keyCache;
    }
    public string Get(string fieldName) {
        if (false != this.keyHolder.ContainsKey(fieldName)) {
            return this.keyHolder [fieldName];
        }
        return string.Empty;
    }
    public int GetId() {
        string id = this.Get("id");
        if (string.Empty == id) {
            return -1;
        }
        return System.Convert.ToInt32(id);
    }
    public bool Set(string fieldName, object fieldValue) {
        bool ret = false;
        if (false == this.keyHolder.ContainsKey(fieldName)) {
            this.keyHolder.Add(fieldName, fieldValue.ToString());
            ret = true;
        }
        return ret;
    }
    public override bool Equals(object obj) {
        if (this.GetType() != obj.GetType()) {
            return false;
        }
        KeySchema right = obj as KeySchema;
        int matchCount = 0;
        foreach (var leftElement in this.keyHolder) {
            foreach (var rightElement in right.keyHolder) {
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value) {
                    matchCount++;
                    break;
                }
            }
        }
        if (matchCount != this.keyHolder.Count) {
            return false;
        }
        return true;
    }
    public override int GetHashCode() {
        return base.GetHashCode();
    }
    public static bool operator ==(KeySchema left , KeySchema right) {
        int matchCount = 0;
        foreach (var leftElement in left.keyHolder) {
            foreach (var rightElement in right.keyHolder) {
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value) {
                    matchCount++;
                    break;
                }
            }
        }
        if (matchCount != left.keyHolder.Count) {
            return false;
        }
        return true;
    }
    public static bool operator !=(KeySchema left , KeySchema right) {
        int matchCount = 0;
        foreach (var leftElement in left.keyHolder) {
            foreach (var rightElement in right.keyHolder) {
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value) {
                    matchCount++;
                    break;
                }
            }
        }
        if (matchCount != left.keyHolder.Count) {
            return true;
        }
        return false;
    }
}
}