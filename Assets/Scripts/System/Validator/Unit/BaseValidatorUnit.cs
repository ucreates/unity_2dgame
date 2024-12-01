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
using Core.Validator.Message;

namespace Core.Validator.Unit
{
    [Serializable]
    public abstract class BaseValidatorUnit<T> where T : IComparable
    {
        public BaseValidatorUnit()
        {
            validateMessage = new ErrorValidateMessage();
        }

        public BaseValidateMessage validateMessage { get; set; }

        public virtual bool IsValid(T value)
        {
            return true;
        }
    }
}