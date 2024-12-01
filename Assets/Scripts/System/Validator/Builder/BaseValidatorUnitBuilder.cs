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
using Core.Validator.Unit;

namespace Core.Validator.Builder
{
    public abstract class BaseValidatorUnitBuilder
    {
        public BaseValidateMessage message { get; set; }

        public virtual string type { get; set; }

        public BaseValidatorUnitBuilder AddMessage(BaseValidateMessage message)
        {
            this.message = message;
            return this;
        }

        public virtual object Build()
        {
            return null;
        }

        protected virtual BaseValidatorUnit<T> UnitFactoryMethod<T>() where T : IComparable
        {
            return null;
        }
    }
}