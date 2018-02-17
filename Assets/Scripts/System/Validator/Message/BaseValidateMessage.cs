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
namespace Core.Validator.Message {
[Serializable]
public abstract class BaseValidateMessage {
    public virtual string messageType {
        get {
            return string.Empty;
        }
    }
    public string message {
        get;
        set;
    }
    public BaseValidateMessage() : this(string.Empty) {}
    public BaseValidateMessage(string message) {
        this.message = this.messageType + message;
    }
}
}
