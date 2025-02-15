//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      he code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using Core.Validator.Unit;

namespace Core.Validator.Builder
{
    public sealed class RequiredValidatorUnitBuilder : BaseValidatorUnitBuilder
    {
        public override string type { get; set; }

        public RequiredValidatorUnitBuilder AddType(string type)
        {
            this.type = type;
            return this;
        }

        public override object Build()
        {
            object validator = null;
            if (type.ToLower().Equals("int"))
                validator = UnitFactoryMethod<int>();
            else if (type.ToLower().Equals("long"))
                validator = UnitFactoryMethod<long>();
            else if (type.ToLower().Equals("float"))
                validator = UnitFactoryMethod<float>();
            else if (type.ToLower().Equals("double"))
                validator = UnitFactoryMethod<double>();
            else if (type.ToLower().Equals("string")) validator = UnitFactoryMethod<string>();
            return validator;
        }

        protected override BaseValidatorUnit<T> UnitFactoryMethod<T>()
        {
            var validator = new RequiredValidatorUnit<T>();
            validator.validateMessage = message;
            return validator;
        }
    }
}