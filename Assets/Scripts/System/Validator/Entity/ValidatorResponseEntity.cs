//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Validator.Message;

namespace Core.Validator.Entity
{
    public sealed class ValidatorResponseEntity
    {
        public ValidatorResponseEntity()
        {
            result = false;
            message = null;
        }

        public bool result { get; set; }

        public BaseValidateMessage message { get; set; }
    }
}