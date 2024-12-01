//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Service.Integration.Schema;

namespace Service.Integration.Table
{
    public sealed class TItemTable : BaseTable
    {
        public TItemTable()
        {
            userId = 0;
            itemId = 0;
            amount = 0;
        }

        [PrimaryKey] public int userId { get; set; }

        [PrimaryKey] public int itemId { get; set; }

        public int amount { get; set; }

        public override BaseTable Clone()
        {
            return MemberwiseClone() as TItemTable;
        }
    }
}