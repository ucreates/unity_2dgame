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
using System.Collections;
namespace Core.Utility {
public sealed class ConditionUtility {
    public static bool ByRandom(int min = 0, int max = 10, int division = 2) {
        int cond = UnityEngine.Random.Range(min, max);
        if (cond % division == 0) {
            return true;
        }
        return false;
    }
}
}
