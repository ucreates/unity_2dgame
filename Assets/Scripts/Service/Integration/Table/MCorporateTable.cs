﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Service.Integration.Table
{
    public sealed class MCorporateTable : BaseTable
    {
        public MCorporateTable()
        {
            buisiness = string.Empty;
            campanyName = string.Empty;
            copyright = string.Empty;
        }

        public string buisiness { get; set; }

        public string campanyName { get; set; }

        public string copyright { get; set; }

        public override BaseTable Clone()
        {
            return MemberwiseClone() as MCorporateTable;
        }
    }
}