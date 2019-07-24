//======================================================================
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
    public sealed class TPlayerTable : BaseTable {
    public int userId {
        get;
        set;
    }
    public TPlayerTable() {
        this.userId = 0;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as TPlayerTable;
    }
}
}
