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
using Core.Utility;
namespace Core.Validator.Builder {
public sealed class CompareValidatorUnitBuilder : BaseValidatorUnitBuilder {
    public override string type {
        get;
        set;
    }
    public string compareValue {
        get;
        set;
    }
    public string copmareTypeOption {
        get;
        set;
    }
    public string equalOption {
        get;
        set;
    }
    public CompareValidatorUnitBuilder() {
    }
    public CompareValidatorUnitBuilder AddType(string type) {
        this.type = type.ToLower();
        return this;
    }
    public CompareValidatorUnitBuilder AddCompareValue(string greater) {
        this.compareValue = greater.ToLower();
        return this;
    }
    public CompareValidatorUnitBuilder AddCompareTypeOption(string option) {
        this.copmareTypeOption = option.ToLower();
        return this;
    }
    public CompareValidatorUnitBuilder AddEqualOption(string option) {
        this.equalOption = option.ToLower();
        return this;
    }
    public override object Build() {
        object validator = null;
        if (this.type.Equals("int") || this.type.Equals("string")) {
            validator = this.UnitFactoryMethod<int>();
        } else if (this.type.Equals("long")) {
            validator = this.UnitFactoryMethod<string>();
        } else if (this.type.Equals("float")) {
            validator = this.UnitFactoryMethod<float>();
        } else if (this.type.Equals("double")) {
            validator = this.UnitFactoryMethod<double>();
        }
        return validator;
    }
    protected override BaseValidatorUnit<T> UnitFactoryMethod<T>() {
        CompareValidatorUnit<T> validator = new CompareValidatorUnit<T>();
        validator.compareValue = ConvertUtility.ToGenerics<T>(this.compareValue);
        validator.validateMessage = this.message;
        if (this.copmareTypeOption.Equals("large")) {
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
        } else {
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
        }
        if (equalOption.Equals("addequal")) {
            validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
        } else {
            validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.Default;
        }
        return validator;
    }
}
}
