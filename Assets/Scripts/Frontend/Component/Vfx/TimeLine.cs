//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System;
using System.Collections;
using UnityEngine;
namespace Frontend.Component.Vfx {
public sealed class TimeLine {
    public const int PLAY = 0x01;
    public const int PAUSE = 0x02;
    public const int STOP = 0x04;
    public float currentFrame {
        get;
        set;
    }
    public float currentTime {
        get;
        set;
    }
    public float rate {
        get;
        set;
    }
    public int status {
        get;
        set;
    }
    public bool isPlay {
        get {
            return 0x00 != (this.status & TimeLine.PLAY) ? true : false;
        }
    }
    public bool isPause {
        get {
            return 0x00 != (this.status & TimeLine.PAUSE) ? true : false;
        }
    }
    public bool isStop {
        get {
            return 0x00 != (this.status & TimeLine.STOP) ? true : false;
        }
    }
    public TimeLine() : this(0f, 0f) {
    }
    public TimeLine(float currentFrame, float currentTime, float rate = 1f) {
        this.currentFrame = currentFrame;
        this.currentTime = currentTime;
        this.rate = rate;
        this.status = TimeLine.PLAY;
    }
    public void Play() {
        this.status = TimeLine.PLAY;
    }
    public void Pause() {
        this.status = TimeLine.PAUSE;
    }
    public void Stop() {
        this.status = TimeLine.STOP;
        this.currentFrame = 0f;
        this.currentTime = 0f;
    }
    public void Next(float multipleRate = 1.0f) {
        if (this.isPlay) {
            this.currentFrame += this.rate;
            this.currentTime += Time.deltaTime * multipleRate;
        }
    }
    public void Preview() {
        this.currentFrame -= this.rate;
    }
    public void Goto(float frame) {
        this.currentFrame = frame;
    }
    public float Difference(float difference) {
        return this.currentFrame - difference;
    }
    public void Restore() {
        this.Goto(0f);
        this.currentTime = 0.0f;
    }
}
}
