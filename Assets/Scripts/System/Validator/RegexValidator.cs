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
using System.Xml;
using UnityEngine;
using Core.Validator.Unit;
using Core.Validator.Entity;
using Core.Validator.Factory;
using Core.Validator.Config;
using Core.Validator.Message;
namespace Core.Validator {
public sealed class RegexValidator : BaseValidator {
    public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList) {
        ValidatorResponse response = new ValidatorResponse();
        foreach (string typeName in validatorList.Keys) {
            object validatorUnit = validatorList[typeName];
            BaseValidatorUnit<string> validator = validatorUnit as RegexValidatorUnit;
            bool ret = validator.IsValid(validateValue.ToString());
            ValidatorResponseEntity entity = new ValidatorResponseEntity();
            entity.result = ret;
            if (!ret) {
                if (this.compareOption == CompareOption.Or) {
                    if (0 == response.responseList.Count) {
                        entity.message = validator.validateMessage;
                        response.responseList.Add(entity);
                    }
                } else {
                    entity.message = validator.validateMessage;
                    response.responseList.Add(entity);
                }
            } else {
                if (this.compareOption == CompareOption.Or) {
                    response.responseList.Clear();
                    break;
                }
            }
        }
        return response;
    }
}
}
