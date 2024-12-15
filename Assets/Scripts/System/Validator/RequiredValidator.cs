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

namespace Core.Validator
{
    public sealed class RequiredValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorDictionary)
        {
            var response = new ValidatorResponse();
            validatorDictionary.ForEach(pair =>
            {
                var lowerTypeName = pair.Key.ToLower();
                if (lowerTypeName.Equals("int"))
                    response = IsValid<int>(pair.Value, validateValue, pair.Key);
                else if (lowerTypeName.Equals("long"))
                    response = IsValid<long>(pair.Value, validateValue, pair.Key);
                else if (lowerTypeName.Equals("float"))
                    response = IsValid<float>(pair.Value, validateValue, pair.Key);
                else if (lowerTypeName.Equals("double"))
                    response = IsValid<double>(pair.Value, validateValue, pair.Key);
                else if (lowerTypeName.Equals("string"))
                    response = IsValid<string>(pair.Value, validateValue, pair.Key);
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