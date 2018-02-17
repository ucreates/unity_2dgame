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
using Core.Validator.Unit;
using Core.Validator.Message;
using Core.Validator.Entity;
using Core.Utility;
using Core.Entity;
namespace Core.Validator {
public abstract class BaseValidator {
    public enum CompareOption {
        And = 0,
        Or = 1,
    }
    public CompareOption compareOption {
        get;
        set;
    }
    public virtual ValidatorResponse IsValid(object validateValue) {
        return new ValidatorResponse();
    }
    public virtual ValidatorResponse IsValid(Parameter parameter) {
        return new ValidatorResponse();
    }
    public virtual ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList) {
        return new ValidatorResponse();
    }
    protected ValidatorResponse IsValid<T>(object validatorUnit, object validateValue, string typeName) where T : IComparable {
        ValidatorResponse response = new ValidatorResponse();
        bool ret = false;
        BaseValidateMessage message = null;
        Type type = typeof(T);
        if (type == typeof(string)) {
            BaseValidatorUnit<string> validator = validatorUnit as BaseValidatorUnit<string>;
            ret = validator.IsValid(validateValue.ToString());
            message = validator.validateMessage;
        } else {
            BaseValidatorUnit<T> validator = validatorUnit as BaseValidatorUnit<T>;
            T cnvertedValue = ConvertUtility.ToGenerics<T>(validateValue);
            ret = validator.IsValid(cnvertedValue);
            message = validator.validateMessage;
        }
        if (false != ret) {
            message = new SuccessValidateMessage();
        }
        ValidatorResponseEntity entity = new ValidatorResponseEntity();
        entity.result = ret;
        entity.message = message;
        response.responseList.Add(entity);
        return response;
    }
}
}
