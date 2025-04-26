//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Collections.Generic;
using Core.Utility;
using Core.Validator.Entity;
using Core.Validator.Message;
using Core.Validator.Unit;

namespace Core.Validator
{
    public abstract class BaseValidator
    {
        public enum CompareOption
        {
            And = 0,
            Or = 1
        }

        public CompareOption compareOption { get; set; }

        public virtual ValidatorResponse IsValid(object parameter)
        {
            return new ValidatorResponse();
        }

        public virtual ValidatorResponse IsValid(object validateValue, Dictionary<string, object> validatorDictionary)
        {
            return new ValidatorResponse();
        }

        protected ValidatorResponse IsValid<T>(object validatorUnit, object validateValue, string typeName)
            where T : IComparable
        {
            var response = new ValidatorResponse();
            var result = false;
            BaseValidateMessage message = null;
            var type = typeof(T);
            if (type == typeof(string))
            {
                var validator = validatorUnit as BaseValidatorUnit<string>;
                result = validator.IsValid(validateValue.ToString());
                message = validator.validateMessage;
            }
            else
            {
                var validator = validatorUnit as BaseValidatorUnit<T>;
                var convertedValue = ConvertUtility.ToGenerics<T>(validateValue);
                result = validator.IsValid(convertedValue);
                message = validator.validateMessage;
            }

            if (result) message = new SuccessValidateMessage();
            var entity = new ValidatorResponseEntity();
            entity.result = result;
            entity.message = message;
            response.responseList.Add(entity);
            return response;
        }
    }
}