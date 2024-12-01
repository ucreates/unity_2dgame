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

namespace Core.Validator.Unit
{
    public sealed class RangeValidatorUnit<T> : BaseValidatorUnit<T> where T : IComparable
    {
        public enum Option
        {
            Default = 0,
            AddEqual = 1
        }

        public T min { get; set; }

        public T max { get; set; }

        public Option option { get; set; }

        public override bool IsValid(T value)
        {
            if (option == Option.Default) return value.CompareTo(min) > 0 && value.CompareTo(max) < 0;

            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public bool IsValid(T value, T pmin, T pmax, Option option = Option.Default)
        {
            if (option == Option.Default) return value.CompareTo(pmin) > 0 && value.CompareTo(pmax) < 0;

            return value.CompareTo(pmin) >= 0 && value.CompareTo(pmax) <= 0;
        }
    }
}