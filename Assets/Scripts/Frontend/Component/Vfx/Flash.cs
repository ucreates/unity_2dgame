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

namespace Frontend.Component.Vfx
{
    public sealed class Flash
    {
        public static float Update(float time, float times = 1.0f, float maxRate = 1.0f)
        {
            var sin = Mathf.Sin(time * times) * maxRate;
            return Mathf.Abs(sin);
        }
    }
}