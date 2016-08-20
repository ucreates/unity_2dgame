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
namespace Frontend.Component.Vfx.Easing {
public sealed class Quartic {
    public static float EaseIn(float currentTime, float start, float end, float totalTime) {
        float rate = currentTime / totalTime;
        if (1.0f <= rate) {
            rate = 1.0f;
        }
        float diff = end - start;
        return diff * Mathf.Pow(rate, 4) + start;
    }
    public static float EaseOut(float currentTime, float start, float end, float totalTime) {
        float rate = currentTime / totalTime;
        if (1.0f <= rate) {
            rate = 1.0f;
        }
        rate -= 1.0f;
        float diff = end - start;
        return -1.0f * diff * (Mathf.Pow(rate, 4) - 1) + start;
    }
    public static float EaseInOut(float currentTime, float start, float end, float totalTime) {
        bool switchType = currentTime / totalTime >= 0.5f;
        if (false == switchType) {
            return Quartic.EaseIn(currentTime, start, end, totalTime);
        } else {
            return Quartic.EaseOut(currentTime, start, end, totalTime);
        }
    }
}
}
