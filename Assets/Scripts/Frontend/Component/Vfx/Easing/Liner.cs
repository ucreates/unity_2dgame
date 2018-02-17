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
public sealed class Liner {
    public static float EaseIn(float currentTime, float start, float end, float totalTime) {
        float rate = currentTime / totalTime;
        float diff = end - start;
        if (1.0f <= rate) {
            rate = 1.0f;
        }
        return diff * rate + start;
    }
    public static float EaseOut(float currentTime, float start, float end, float totalTime) {
        float rate = 1.0f - (currentTime / totalTime);
        float diff = end - start;
        if (0.0f >= rate) {
            rate = 0.0f;
        }
        return diff * rate + start;
    }
    public static float EaseInOut(float currentTime, float start, float end, float totalTime) {
        bool switchType = currentTime / totalTime >= 0.5f;
        if (false == switchType) {
            return Liner.EaseIn(currentTime, start, end, totalTime);
        } else {
            return Liner.EaseOut(currentTime, start, end, totalTime);
        }
    }
}
}
