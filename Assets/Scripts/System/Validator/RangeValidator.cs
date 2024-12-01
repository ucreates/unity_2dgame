//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Validator.Entity;
using Core.Validator.Message;
using Core.Validator.Unit;

namespace Core.Validator
{
    public sealed class RangeValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList)
        {
            var response = new ValidatorResponse();
            foreach (var typeName in validatorList.Keys)
            {
                var validatorUnit = validatorList[typeName];
                if (typeName.Equals("int"))
                {
                    response = IsValid<int>(validatorUnit, validateValue, typeName);
                }
                else if (typeName.Equals("long"))
                {
                    response = IsValid<long>(validatorUnit, validateValue, typeName);
                }
                else if (typeName.Equals("float"))
                {
                    response = IsValid<float>(validatorUnit, validateValue, typeName);
                }
                else if (typeName.Equals("double"))
                {
                    response = IsValid<double>(validatorUnit, validateValue, typeName);
                }
                else if (typeName.Equals("string"))
                {
                    var validator = validatorUnit as BaseValidatorUnit<int>;
                    var ret = validator.IsValid(validateValue.ToString().Length);
                    BaseValidateMessage message = null;
                    if (!ret)
                        message = validator.validateMessage;
                    else
                        message = new SuccessValidateMessage();
                    var entity = new ValidatorResponseEntity();
                    entity.result = ret;
                    entity.message = message;
                    response.responseList.Add(entity);
                }

                if (compareOption == CompareOption.Or)
                {
                    response.responseList.Clear();
                    break;
                }
            }

            return response;
        }
    }
}