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
using Core.Utility;
using Core.Validator.Unit;

namespace Service.Integration.Schema
{
    [Serializable]
    public sealed class FieldSchema<T> : BaseFieldSchema where T : IComparable
    {
        public FieldSchema()
        {
        }

        public FieldSchema(T fieldValue)
        {
            this.fieldValue = fieldValue;
            validator = new CompareValidatorUnit<T>();
            validator.compareValue = this.fieldValue;
        }

        public T fieldValue { get; set; }

        private CompareValidatorUnit<T> validator { get; set; }

        public override bool Equal(object fieldValue)
        {
            var compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Equal;
            return validator.IsValid(compareValue);
        }

        public override bool MoreThan(object fieldValue)
        {
            var compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
            return validator.IsValid(compareValue);
        }

        public override bool MoreThanEqual(object fieldValue)
        {
            var compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Large;
            validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
            return validator.IsValid(compareValue);
        }

        public override bool LessThan(object fieldValue)
        {
            var compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
            return validator.IsValid(compareValue);
        }

        public override bool LessThanEqual(object fieldValue)
        {
            var compareValue = ConvertUtility.ToGenerics<T>(fieldValue);
            validator.compareTypeOption = CompareValidatorUnit<T>.CompareTypeOption.Small;
            validator.addEqualOption = CompareValidatorUnit<T>.AddEqualOption.AddEqual;
            return validator.IsValid(compareValue);
        }
    }
}