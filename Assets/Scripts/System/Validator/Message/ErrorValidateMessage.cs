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
public sealed class ErrorValidateMessage : BaseValidateMessage {
    public override string messageType {
        get {
            return "[!]";
        }
    }
    public ErrorValidateMessage() : base(string.Empty) {}
    public ErrorValidateMessage(string message) : base(message) {}
}
}
