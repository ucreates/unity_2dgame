﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using UnityEngine;

namespace Frontend.Component.Vfx.Easing
{
    public sealed class Quartic
    {
        public static float EaseIn(float currentTime, float start, float end, float totalTime)
        {
            var rate = currentTime / totalTime;
            if (1.0f <= rate) rate = 1.0f;
            var diff = end - start;
            return diff * Mathf.Pow(rate, 4) + start;
        }

        public static float EaseOut(float currentTime, float start, float end, float totalTime)
        {
            var rate = currentTime / totalTime;
            if (1.0f <= rate) rate = 1.0f;
            rate -= 1.0f;
            var diff = end - start;
            return -1.0f * diff * (Mathf.Pow(rate, 4) - 1) + start;
        }

        public static float EaseInOut(float currentTime, float start, float end, float totalTime)
        {
            var switchType = currentTime / totalTime >= 0.5f;
            if (!switchType) return EaseIn(currentTime, start, end, totalTime);

            return EaseOut(currentTime, start, end, totalTime);
        }
    }
}