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
using Core.Validator.Unit;

namespace Core.Validator
{
    public sealed class RegexValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList)
        {
            var response = new ValidatorResponse();
            foreach (var typeName in validatorList.Keys)
            {
                var validatorUnit = validatorList[typeName];
                BaseValidatorUnit<string> validator = validatorUnit as RegexValidatorUnit;
                var ret = validator.IsValid(validateValue.ToString());
                var entity = new ValidatorResponseEntity();
                entity.result = ret;
                if (!ret)
                {
                    if (compareOption == CompareOption.Or)
                    {
                        if (0 == response.responseList.Count)
                        {
                            entity.message = validator.validateMessage;
                            response.responseList.Add(entity);
                        }
                    }
                    else
                    {
                        entity.message = validator.validateMessage;
                        response.responseList.Add(entity);
                    }
                }
                else
                {
                    if (compareOption == CompareOption.Or)
                    {
                        response.responseList.Clear();
                        break;
                    }
                }
            }

            return response;
        }
    }
}