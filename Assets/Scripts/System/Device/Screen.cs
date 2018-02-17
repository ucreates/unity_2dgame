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
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
namespace Core.Device {
public sealed class Screen {
    public static float HalfWidth {
        get {
            float val = (float)UnityEngine.Screen.width / 2f;
            return val;
        }
    }
    public static float HalfHeight {
        get {
            float val = (float)UnityEngine.Screen.height / 2f;
            return val;
        }
    }
}
}
