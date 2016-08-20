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
using Core.Validator.Message;
namespace Core.Validator.Entity {
public sealed class ValidatorResponseEntity {
    public bool result {
        get;
        set;
    }
    public BaseValidateMessage message {
        get;
        set;
    }
    public ValidatorResponseEntity() {
        this.result = false;
        this.message = null;
    }
}
}
