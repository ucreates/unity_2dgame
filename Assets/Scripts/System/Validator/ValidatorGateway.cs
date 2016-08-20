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
using Core.Entity;
using Core.Validator.Unit;
using Core.Validator.Entity;
using Core.Validator.Factory;
using Core.Validator.Mapper;
using Core.Validator.Config;
using Core.Validator.Message;
using Service;
namespace Core.Validator {
public sealed class ValidatorGateway : BaseValidator {
    private static ValidatorGateway instance {
        get;
        set;
    }
    private ValidationConfig config {
        get;
        set;
    }
    private ValidatorGateway() {
        ValidationConfigEntity configEntity = new ValidationConfigEntity();
        configEntity.xmlSouceName = "Config/validation";
        this.config = new ValidationConfig();
        this.config.Load(configEntity.xmlSouceName, configEntity.sourceType);
    }
    public static ValidatorGateway GetInstance() {
        if (null == ValidatorGateway.instance) {
            ValidatorGateway.instance = new ValidatorGateway();
        }
        return ValidatorGateway.instance;
    }
    public override ValidatorResponse IsValid(Parameter parameter) {
        ValidatorResponse response = new ValidatorResponse();
        foreach (string validateControlName in parameter.parameterList.Keys) {
            Value<string> validateValue = parameter.parameterList[validateControlName] as Value<string>;
            ValidatorResponse ctrlVldRes = this.IsValid(validateControlName, validateValue.value);
            response.responseList.AddRange(ctrlVldRes.responseList);
        }
        return response;
    }
    public ValidatorResponse IsValid(string validateControlName, object validateValue) {
        ValidatorResponse response = new ValidatorResponse();
        if (!this.config.ruleNodeList.ContainsKey(validateControlName)) {
            ValidatorResponseEntity entity = new ValidatorResponseEntity();
            entity.result = false;
            entity.message = new ErrorValidateMessage("nothing validate parameter.");
            response.responseList.Add(entity);
            return response;
        }
        XmlNodeList nodeList =  this.config.ruleNodeList[validateControlName];
        ValidatorUnitEntity unitEntity = ValidatorUnitFactory.FactoryMethod(nodeList);
        foreach (string validatorType in unitEntity.validatorUnitList.Keys) {
            Dictionary<string, object> validatorUnitList = unitEntity.validatorUnitList[validatorType];
            BaseValidator validator = ValidatorFactory.FactoryMethod(validatorType);
            ValidatorResponse unitResponse = validator.IsValid(validateValue, validatorUnitList);
            foreach (ValidatorResponseEntity entity in unitResponse.responseList) {
                if (!entity.result) {
                    response.responseList.Add(entity);
                    break;
                }
            }
            if (0 != response.responseList.Count) {
                break;
            }
        }
        return response;
    }
}
}
