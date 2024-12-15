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
using Core.Extensions;
using Core.Validator.Entity;
using Core.Validator.Message;
using Core.Validator.Unit;

namespace Core.Validator
{
    public sealed class RangeValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorDictionary)
        {
            var response = new ValidatorResponse();
            validatorDictionary.ForEach(pair =>
            {
                if (pair.Key.Equals("int"))
                {
                    response = IsValid<int>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("long"))
                {
                    response = IsValid<long>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("float"))
                {
                    response = IsValid<float>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("double"))
                {
                    response = IsValid<double>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("string"))
                {
                    var validator = pair.Value as BaseValidatorUnit<int>;
                    var ret = validator?.IsValid(validateValue.ToString().Length) ?? false;
                    BaseValidateMessage message = null;
                    if (!ret)
                        message = validator?.validateMessage;
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
                    return false;
                }

                return true;
            });

            validatorDictionary.ForEach(pair =>
            {
                if (pair.Key.Equals("int"))
                {
                    response = IsValid<int>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("long"))
                {
                    response = IsValid<long>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("float"))
                {
                    response = IsValid<float>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("double"))
                {
                    response = IsValid<double>(pair.Value, validateValue, pair.Key);
                }
                else if (pair.Key.Equals("string"))
                {
                    var validator = pair.Value as BaseValidatorUnit<int>;
                    var ret = validator?.IsValid(validateValue.ToString().Length) ?? false;
                    BaseValidateMessage message = null;
                    if (!ret)
                        message = validator?.validateMessage;
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
                    return false;
                }

                return true;
            });
            return response;
        }
    }
}