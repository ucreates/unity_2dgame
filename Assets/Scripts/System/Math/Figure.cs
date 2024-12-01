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
            var lcount = count;
            var ret = number / 10;
            if (ret <= 0) return lcount;

            return CountFigure(ret, ++lcount);
        }

        public static int GetSpeciFigureValue(int number, int figure = 1)
        {
            var ret = number / (int)System.Math.Pow(10d, figure - 1);
            ret = ret % 10;
            return ret;
        }
    }
}