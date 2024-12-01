//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Validator.Unit;

namespace Core.Validator.Builder
{
    public sealed class RegexValidatorUnitBuilder : BaseValidatorUnitBuilder
    {
        public override string type => "string";

        public string pattern { get; set; }

        public RegexValidatorUnitBuilder AddPattern(string pattern)
        {
            this.pattern = pattern;
            return this;
        }

        public override object Build()
        {
            var validator = new RegexValidatorUnit();
            validator.pattern = pattern;
            validator.validateMessage = message;
            return validator;
        }
    }
}