//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using Service.Integration.Table;
using Service.Integration.Schema;
namespace Service.Integration.Table {
public sealed class MCorporateTable : BaseTable {
    public string buisiness {
        get;
        set;
    }
    public string campanyName {
        get;
        set;
    }
    public string copyright {
        get;
        set;
    }
    public MCorporateTable() {
        this.buisiness = string.Empty;
        this.campanyName = string.Empty;
        this.copyright = string.Empty;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as MCorporateTable;
    }
}
}
