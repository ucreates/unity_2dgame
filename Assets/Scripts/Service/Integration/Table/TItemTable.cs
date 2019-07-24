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
    public sealed class TItemTable : BaseTable {
    [PrimaryKey]
    public int userId {
        get;
        set;
    }
    [PrimaryKey]
    public int itemId {
        get;
        set;
    }
    public int amount {
        get;
        set;
    }
    public TItemTable() {
        this.userId = 0;
        this.itemId = 0;
        this.amount = 0;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as TItemTable;
    }
}
}
