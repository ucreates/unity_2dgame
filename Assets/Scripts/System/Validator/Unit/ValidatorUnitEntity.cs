//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System;
using System.Collections.Generic;
namespace Core.Validator.Unit {
public sealed class ValidatorUnitEntity {
    public Dictionary<string, Dictionary<string, object>> validatorUnitList {
        get;
        set;
    }
    public ValidatorUnitEntity() {
        this.validatorUnitList = new Dictionary<string, Dictionary<string, object>>();
        this.validatorUnitList.Add("required", new Dictionary<string, object>());
        this.validatorUnitList.Add("range", new Dictionary<string, object>());
        this.validatorUnitList.Add("compare", new Dictionary<string, object>());
        this.validatorUnitList.Add("regex", new Dictionary<string, object>());
        this.validatorUnitList.Add("mailandphone", new Dictionary<string, object>());
    }
}
}
