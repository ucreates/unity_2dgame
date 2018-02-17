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
using Service.Integration.Schema;
using Frontend.Component.Vfx;
namespace Service.Integration.Table {
public sealed class TLoadingTable : BaseTable {
    public int downloadedDataCount {
        get;
        set;
    }
    public int totalDataCount {
        get;
        set;
    }
    public TLoadingTable() {
        this.downloadedDataCount = 0;
        this.totalDataCount = 0;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as TLoadingTable;
    }
}
}
