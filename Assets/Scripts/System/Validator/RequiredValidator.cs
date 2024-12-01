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

namespace Core.Validator
{
    public sealed class RequiredValidator : BaseValidator
    {
        public override ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorList)
        {
            var response = new ValidatorResponse();
            foreach (var typeName in validatorList.Keys)
            {
                var lowerTypeName = typeName.ToLower();
                var validatorUnit = validatorList[typeName];
                if (lowerTypeName.Equals("int"))
                    response = IsValid<int>(validatorUnit, validateValue, typeName);
                else if (lowerTypeName.Equals("long"))
                    response = IsValid<long>(validatorUnit, validateValue, typeName);
                else if (lowerTypeName.Equals("float"))
                    response = IsValid<float>(validatorUnit, validateValue, typeName);
                else if (lowerTypeName.Equals("double"))
                    response = IsValid<double>(validatorUnit, validateValue, typeName);
                else if (lowerTypeName.Equals("string"))
                    response = IsValid<string>(validatorUnit, validateValue, typeName);
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