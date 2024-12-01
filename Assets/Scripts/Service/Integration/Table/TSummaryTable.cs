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

namespace Service.Integration.Table
{
    [Serializable]
    public sealed class TSummaryTable : BaseTable
    {
        public TSummaryTable()
        {
            bestClearCount = 0;
        }

        public int bestClearCount { get; set; }

        public override BaseTable Clone()
        {
            return MemberwiseClone() as TSummaryTable;
        }
    }
}