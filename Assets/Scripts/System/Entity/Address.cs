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
namespace Core.Entity {
public sealed class Address {
    public static Address zero = new Address(0, 0, 0);
    public int x {
        get;
        set;
    }
    public int y {
        get;
        set;
    }
    public int z {
        get;
        set;
    }
    public Address() {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }
    public Address(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
}
