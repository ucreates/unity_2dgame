//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
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

        public override ValidatorResponse IsValid(Parameter parameter)
        {
            var response = new ValidatorResponse();
            foreach (var validateControlName in parameter.parameterList.Keys)
            {
                var validateValue = parameter.parameterList[validateControlName] as Value<string>;
                var ctrlVldRes = IsValid(validateControlName, validateValue.value);
                response.responseList.AddRange(ctrlVldRes.responseList);
            }

            return response;
        }

        public ValidatorResponse IsValid(string validateControlName, object validateValue)
        {
            var response = new ValidatorResponse();
            if (!config.ruleNodeList.ContainsKey(validateControlName))
            {
                var entity = new ValidatorResponseEntity();
                entity.result = false;
                entity.message = new ErrorValidateMessage("nothing validate parameter.");
                response.responseList.Add(entity);
                return response;
            }

            var nodeList = config.ruleNodeList[validateControlName];
            var unitEntity = ValidatorUnitFactory.FactoryMethod(nodeList);
            foreach (var validatorType in unitEntity.validatorUnitList.Keys)
            {
                var validatorUnitList = unitEntity.validatorUnitList[validatorType];
                var validator = ValidatorFactory.FactoryMethod(validatorType);
                var unitResponse = validator.IsValid(validateValue, validatorUnitList);
                foreach (var entity in unitResponse.responseList)
                    if (!entity.result)
                    {
                        response.responseList.Add(entity);
                        break;
                    }

                if (0 != response.responseList.Count) break;
            }

            return response;
        }
    }
}