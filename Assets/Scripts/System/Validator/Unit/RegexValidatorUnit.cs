//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Text.RegularExpressions;

namespace Core.Validator.Unit
{
    public sealed class RegexValidatorUnit : BaseValidatorUnit<string>
    {
        public RegexValidatorUnit() : this(string.Empty)
        {
        }

        public RegexValidatorUnit(string pattern)
        {
            this.pattern = pattern;
            required = new RequiredValidatorUnit<string>();
        }

        public string pattern { get; set; }

        private RequiredValidatorUnit<string> required { get; }

        public override bool IsValid(string value)
        {
            if (!required.IsValid(value)) return false;
            if (pattern.Equals(string.Empty)) return false;
            return Regex.IsMatch(value, pattern);
        }
    }
}