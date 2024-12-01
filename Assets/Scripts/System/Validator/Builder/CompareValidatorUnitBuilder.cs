//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Utility;
using Core.Validator.Unit;

namespace Core.Validator.Builder
{
    public sealed class CompareValidatorUnitBuilder : BaseValidatorUnitBuilder
    {
        public override string type { get; set; }

        public string compareValue { get; set; }

        public string copmareTypeOption { get; set; }

        public string equalOption { get; set; }

        public CompareValidatorUnitBuilder AddType(string type)
        {
            this.type = type.ToLower();
            return this;
        }

        public CompareValidatorUnitBuilder AddCompareValue(string greater)
        {
            compareValue = greater.ToLower();
            return this;
        }

        public CompareValidatorUnitBuilder AddCompareTypeOption(string option)
        {
            copmareTypeOption = option.ToLower();
            return this;
        }

        public CompareValidatorUnitBuilder AddEqualOption(string option)
        {
            equalOption = option.ToLower();
            return this;
        }

        public override object Build()
        {
            object validator = null;
            if (type.Equals("int") || type.Equals("string"))
                validator = UnitFactoryMethod<int>();
            else if (type.Equals("long"))
                validator = UnitFactoryMethod<string>();
            else if (type.Equals("float"))
                validator = UnitFactoryMethod<float>();
            else if (type.Equals("double")) validator = UnitFactoryMethod<double>();
            return validator;
        }

        protected override BaseValidatorUnit<T> UnitFactoryMethod<T>()
        {
            var validator = new CompareValidatorUnit<T>();
            validator.compareValue = ConvertUtility.ToGenerics<T>(compareValue);
            validator.validateMessage = message;
            if (copmareTypeOption.Equals("large"))
                validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
            else
                validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
            if (equalOption.Equals("addequal"))
                validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
            else
                validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.Default;
            return validator;
        }
    }
}