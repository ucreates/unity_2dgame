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
using System.Text.RegularExpressions;
using UnityEngine;
namespace Core.Validator.Unit {
public sealed class RegexValidatorUnit : BaseValidatorUnit<string> {
    public string pattern {
        get;
        set;
    }
    private RequiredValidatorUnit<string> required {
        get;
        set;
    }
    public RegexValidatorUnit() : this(string.Empty) {
    }
    public RegexValidatorUnit(string pattern) {
        this.pattern = pattern;
        this.required = new RequiredValidatorUnit<string>();
    }
    public override bool IsValid(string value) {
        if (!this.required.IsValid(value)) {
            return false;
        }
        if (this.pattern.Equals(string.Empty)) {
            return false;
        }
        return Regex.IsMatch(value.ToString(), this.pattern);
    }
}
}
