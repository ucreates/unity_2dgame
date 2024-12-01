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
    [Serializable]
    public sealed class CompareValidatorUnit<T> : BaseValidatorUnit<T> where T : IComparable
    {
        public T compareValue { get; set; }

        public int compareTypeOption { get; set; }

        public int addEqualOption { get; set; }

        public override bool IsValid(T value)
        {
            if (compareTypeOption == CompareTypeOption.Equal) return value.CompareTo(compareValue) == 0;

            if (compareTypeOption == CompareTypeOption.Large)
            {
                if (addEqualOption == AddEqualOption.AddEqual) return value.CompareTo(compareValue) >= 0;

                return value.CompareTo(compareValue) > 0;
            }

            if (compareTypeOption == CompareTypeOption.Small)
            {
                if (addEqualOption == AddEqualOption.AddEqual) return value.CompareTo(compareValue) <= 0;

                return value.CompareTo(compareValue) < 0;
            }

            return false;
        }

        public bool IsValid(T value, T compareValue, int compareTypeOption = CompareTypeOption.Large,
            int addEqualOption = AddEqualOption.Default)
        {
            if (compareTypeOption == CompareTypeOption.Equal) return value.CompareTo(compareValue) == 0;

            if (compareTypeOption == CompareTypeOption.Large)
            {
                if (this.addEqualOption == AddEqualOption.AddEqual) return value.CompareTo(compareValue) >= 0;

                return value.CompareTo(compareValue) > 0;
            }

            if (compareTypeOption == CompareTypeOption.Small)
            {
                if (this.addEqualOption == AddEqualOption.AddEqual) return value.CompareTo(compareValue) <= 0;

                return value.CompareTo(compareValue) < 0;
            }

            return false;
        }

        public sealed class CompareTypeOption
        {
            public const int Small = 0;
            public const int Large = 1;
            public const int Equal = 2;
        }

        public sealed class AddEqualOption
        {
            public const int Default = 0;
            public const int AddEqual = 1;
        }
    }
}