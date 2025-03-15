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
using Core.Validator.Config;
using Core.Validator.Entity;
using Core.Validator.Factory;
using Core.Validator.Message;

namespace Core.Validator
{
    public sealed class ValidatorGateway : BaseValidator
    {
        private ValidatorGateway()
        {
            var configEntity = new ValidationConfigEntity();
            configEntity.xmlSouceName = "Config/validation";
            config = new ValidationConfig();
            config.Load(configEntity.xmlSouceName, configEntity.sourceType);
        }

        private static ValidatorGateway instance { get; set; }

        private ValidationConfig config { get; }

        public static ValidatorGateway GetInstance()
        {
            if (null == instance) instance = new ValidatorGateway();
            return instance;
        }

        public override ValidatorResponse IsValid(object parameter)
        {
            var response = new ValidatorResponse();
            (parameter as Dictionary<string, object>).ForEach(pair =>
            {
                var ctrlVldRes = IsValid(pair.Key.ToLower(), pair.Value.ToString());
                response.responseList.AddRange(ctrlVldRes.responseList);
            });
            return response;
        }

        public ValidatorResponse IsValid(string validateControlName, object validateValue)
        {
            var response = new ValidatorResponse();
            if (!config.ruleNodeDictionary.ContainsKey(validateControlName))
            {
                var entity = new ValidatorResponseEntity();
                entity.result = false;
                entity.message = new ErrorValidateMessage($"{validateControlName} is nothing validate parameter.");
                response.responseList.Add(entity);
                return response;
            }

            var nodeDictionary = config.ruleNodeDictionary[validateControlName];
            var unitEntity = ValidatorUnitFactory.FactoryMethod(nodeDictionary);
            unitEntity.validatorUnitDictionary.ForEach(pair =>
            {
                var validatorUnitList = pair.Value;
                var validator = ValidatorFactory.FactoryMethod(pair.Key);
                var unitResponse = validator.IsValid(validateValue, validatorUnitList);
                unitResponse.responseList.ForEach(respoinse =>
                {
                    if (!respoinse.result)
                    {
                        response.responseList.Add(respoinse);
                        return false;
                    }

                    return true;
                });
                if (0 != response.responseList.Count) return false;
                return true;
            });
            return response;
        }
    }
}