//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Validator.Entity;
using UnityEngine;

namespace Core.Validator
{
    public sealed class ScreenValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue)
        {
            var position = (Vector3)validateValue;
            var viewportPoint = Camera.main.WorldToViewportPoint(position);
            var response = new ValidatorResponse();
            var entity = new ValidatorResponseEntity();
            entity.result = !(0.0f < viewportPoint.x && viewportPoint.x < 1.0f && 0.0f < viewportPoint.y &&
                              viewportPoint.y < 1.0f);
            response.responseList.Add(entity);
            return response;
        }
    }
}