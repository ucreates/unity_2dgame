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
public sealed class ScreenValidator : BaseValidator {
    public override ValidatorResponse IsValid(object validateValue) {
        Vector3 position = (Vector3)validateValue;
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        ValidatorResponse response = new ValidatorResponse();
        ValidatorResponseEntity entity = new ValidatorResponseEntity();
        entity.result = !(0.0f < viewportPoint.x && viewportPoint.x < 1.0f && 0.0f < viewportPoint.y && viewportPoint.y < 1.0f);
        response.responseList.Add(entity);
        return response;
    }
}
}
