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
namespace Frontend.Component.Property {
public class BaseProperty {
    public virtual string category {
        get {
            return string.Empty;
        }
    }
    public string type {
        get;
        protected set;
    }
    public string name {
        get;
        protected set;
    }
    public int id {
        get;
        protected set;
    }
    public BaseProperty(MonoBehaviour behaviour) {
        this.type = behaviour.tag;
        this.name = behaviour.name;
        this.id = behaviour.GetInstanceID();
    }
}
}
