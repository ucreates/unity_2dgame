//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Core.Math
{
    public sealed class Figure
    {
        public static int CountFigure(int number, int count = 1)
        {
            var nCount = count;
            var result = number / 10;
            if (result <= 0) return nCount;

            return CountFigure(result, ++nCount);
        }

        public static int GetSpeciFigureValue(int number, int figure = 1)
        {
            var result = number / (int)System.Math.Pow(10d, figure - 1);
            result = result % 10;
            return result;
        }
    }
}