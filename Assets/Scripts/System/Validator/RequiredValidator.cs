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
using System.Collections.Generic;
using System.Xml;
using Core.Utility;
using Core.Validator.Unit;
using Core.Validator.Entity;
using Core.Validator.Factory;
using Core.Validator.Config;
using Core.Validator.Message;
namespace Core.Validator {
public sealed class RequiredValidator : BaseValidator {
    public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList) {
        ValidatorResponse response = new ValidatorResponse();
        foreach (string typeName in validatorList.Keys) {
            string lowerTypeName = typeName.ToLower();
            object validatorUnit = validatorList[typeName];
            if (lowerTypeName.Equals("int")) {
                response = this.IsValid<int>(validatorUnit, validateValue, typeName);
            } else if (lowerTypeName.Equals("long")) {
                response = this.IsValid<long>(validatorUnit, validateValue, typeName);
            } else if (lowerTypeName.Equals("float")) {
                response = this.IsValid<float>(validatorUnit, validateValue, typeName);
            } else if (lowerTypeName.Equals("double")) {
                response = this.IsValid<double>(validatorUnit, validateValue, typeName);
            } else if (lowerTypeName.Equals("string")) {
                response = this.IsValid<string>(validatorUnit, validateValue, typeName);
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
