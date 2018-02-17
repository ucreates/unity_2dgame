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
using System.Xml;
using System.Collections.Generic;
using Core.Validator.Unit;
using Core.Validator.Message;
using Core.Validator.Factory;
using Core.Validator.Entity;
using Core.Validator.Config;
using Core.Utility;
namespace Core.Validator {
public sealed class CompareValidator : BaseValidator {
    public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList) {
        ValidatorResponse response = new ValidatorResponse();
        foreach (string typeName in validatorList.Keys) {
            object validatorUnit = validatorList[typeName];
            if (typeName.Equals("int")) {
                response = this.IsValid<int>(validatorUnit, validateValue, typeName);
            } else if (typeName.Equals("long")) {
                response = this.IsValid<long>(validatorUnit, validateValue, typeName);
            } else if (typeName.Equals("float")) {
                response = this.IsValid<float>(validatorUnit, validateValue, typeName);
            } else if (typeName.Equals("double")) {
                response = this.IsValid<double>(validatorUnit, validateValue, typeName);
            } else if (typeName.Equals("string")) {
                BaseValidatorUnit<int> validator = validatorUnit as BaseValidatorUnit<int>;
                bool ret = validator.IsValid(validateValue.ToString().Length);
                BaseValidateMessage message = null;
                if (!ret) {
                    message = validator.validateMessage;
                } else {
                    message = new SuccessValidateMessage();
                }
                ValidatorResponseEntity entity = new ValidatorResponseEntity();
                entity.result = ret;
                entity.message = message;
                response.responseList.Add(entity);
            }
            if (this.compareOption == CompareOption.Or) {
                response.responseList.Clear();
                break;
            }
        }
        return response;
    }
}
}
