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
using Core.Utility;
using Core.Validator.Unit;
namespace Service.Integration.Schema {
[Serializable]
public sealed class FieldSchema<T> : BaseFieldSchema where T : IComparable {
    public T fieldValue {
        get;
        set;
    }
    private CompareValidatorUnit<T> validator {
        get;
        set;
    }
    public FieldSchema() {
    }
    public FieldSchema(T fieldValue) {
        this.fieldValue = fieldValue;
        this.validator = new CompareValidatorUnit<T>();
        this.validator.compareValue = this.fieldValue;
    }
    public override bool Equal(object fieldValue) {
        T compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
        this.validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Equal;
        return this.validator.IsValid(compareValue);
    }
    public override bool MoreThan(object fieldValue) {
        T compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
        this.validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
        return this.validator.IsValid(compareValue);
    }
    public override bool MoreThanEqual(object fieldValue) {
        T compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
        this.validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
        this.validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
        return this.validator.IsValid(compareValue);
    }
    public override bool LessThan(object fieldValue) {
        T compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
        this.validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
        return this.validator.IsValid(compareValue);
    }
    public override bool LessThanEqual(object fieldValue) {
        T compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
        this.validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
        this.validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
        return this.validator.IsValid(compareValue);
    }
}
}