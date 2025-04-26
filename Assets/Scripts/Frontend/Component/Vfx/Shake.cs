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
    public sealed class Shake
    {
        private const float FRICTION = 0.88f;
        private const float POWER = 0.4f;

        public Shake() : this(0f)
        {
        }

        public Shake(float baseValue)
        {
            this.baseValue = baseValue;
            rate = POWER;
        }

        public float baseValue { get; set; }

        public float rate { get; set; }

        public float Update()
        {
            var condition = Random.Range(0, 10);
            var currentRate = rate;
            condition = Random.Range(0, 10);
            if (0 == condition % 2) currentRate *= -1;
            if ((Application.platform == RuntimePlatform.IPhonePlayer ||
                 Application.platform == RuntimePlatform.Android) &&
                SystemInfo.supportsVibration)
                Handheld.Vibrate();
            rate *= FRICTION;
            return baseValue + currentRate;
        }
    }
}