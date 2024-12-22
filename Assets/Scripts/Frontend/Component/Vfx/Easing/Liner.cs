//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Frontend.Component.Vfx.Easing
{
    public sealed class Liner
    {
        public static float EaseIn(float currentTime, float start, float end, float totalTime)
        {
            var rate = currentTime / totalTime;
            var diff = end - start;
            if (1.0f <= rate) rate = 1.0f;
            return diff * rate + start;
        }

        public static float EaseOut(float currentTime, float start, float end, float totalTime)
        {
            var rate = 1.0f - currentTime / totalTime;
            var diff = end - start;
            if (0.0f >= rate) rate = 0.0f;
            return diff * rate + start;
        }

        public static float EaseInOut(float currentTime, float start, float end, float totalTime)
        {
            var switchType = currentTime / totalTime >= 0.5f;
            if (!switchType) return EaseIn(currentTime, start, end, totalTime);

            return EaseOut(currentTime, start, end, totalTime);
        }
    }
}