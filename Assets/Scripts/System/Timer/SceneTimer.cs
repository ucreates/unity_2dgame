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
using Frontend.Component.Vfx;
namespace Core.Timer {
public sealed class SceneTimer {
    private TimeLine timeLine {
        get;
        set;
    }
    public float elapsedTime {
        get;
        set;
    }
    public SceneTimer() {
        this.timeLine = new TimeLine();
    }
    public void Update() {
        if (1.0f <= this.timeLine.currentTime) {
            this.elapsedTime += 1.0f;
            this.timeLine.Restore();
        }
        this.timeLine.Next();
    }
    public void Reset() {
        this.elapsedTime = 0.0f;
        this.timeLine.Restore();
    }
}
}
