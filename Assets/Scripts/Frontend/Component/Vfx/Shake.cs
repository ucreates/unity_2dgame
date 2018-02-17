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
namespace Frontend.Component.Vfx {
public sealed class Shake {
    private const float FRICTION = 0.88f;
    private const float POWER = 0.4f;
    public float baseValue {
        get;
        set;
    }
    public float rate {
        get;
        set;
    }
    public Shake() : this(0f) {}
    public Shake(float baseValue) {
        this.baseValue = baseValue;
        this.rate = Shake.POWER;
    }
    public float Update() {
        int cond = Random.Range(0, 10);
        float sh = this.rate;
        cond = Random.Range(0, 10);
        if (0 == cond % 2) {
            sh *= -1;
        }
        if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) &&
                false != SystemInfo.supportsVibration) {
            Handheld.Vibrate();
        }
        this.rate *= Shake.FRICTION;
        return this.baseValue + sh;
    }
}
}
