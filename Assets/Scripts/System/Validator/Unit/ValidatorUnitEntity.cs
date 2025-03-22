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

namespace Core.Validator.Unit
{
    public sealed class ValidatorUnitEntity
    {
        public ValidatorUnitEntity()
        {
            validatorUnitDictionary = new Dictionary<string, Dictionary<string, object>>
            {
                { "required", new Dictionary<string, object>() },
                { "range", new Dictionary<string, object>() },
                { "compare", new Dictionary<string, object>() },
                { "regex", new Dictionary<string, object>() },
                { "mailandphone", new Dictionary<string, object>() }
            };
        }

        public Dictionary<string, Dictionary<string, object>> validatorUnitDictionary { get; set; }
    }
}