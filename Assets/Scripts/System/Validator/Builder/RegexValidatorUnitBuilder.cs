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
using Core.Validator;
using Core.Validator.Unit;
namespace Core.Validator.Builder {
public sealed class RegexValidatorUnitBuilder : BaseValidatorUnitBuilder {
    public override string type {
        get {
            return "string";
        }
    }
    public string pattern {
        get;
        set;
    }
    public RegexValidatorUnitBuilder() {
    }
    public RegexValidatorUnitBuilder AddPattern(string pattern) {
        this.pattern = pattern;
        return this;
    }
    public override object Build() {
        RegexValidatorUnit validator = new RegexValidatorUnit();
        validator.pattern = this.pattern;
        validator.validateMessage = this.message;
        return validator;
    }
}
}
