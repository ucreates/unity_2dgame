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
    public sealed class TimeLine
    {
        public const int PLAY = 0x01;
        public const int PAUSE = 0x02;
        public const int STOP = 0x04;

        public TimeLine() : this(0f, 0f)
        {
        }

        public TimeLine(float currentFrame, float currentTime, float rate = 1f)
        {
            this.currentFrame = currentFrame;
            this.currentTime = currentTime;
            this.rate = rate;
            status = PLAY;
        }

        public float currentFrame { get; set; }

        public float currentTime { get; set; }

        public float rate { get; set; }

        public int status { get; set; }

        public bool isPlay => 0x00 != (status & PLAY) ? true : false;

        public bool isPause => 0x00 != (status & PAUSE) ? true : false;

        public bool isStop => 0x00 != (status & STOP) ? true : false;

        public void Play()
        {
            status = PLAY;
        }

        public void Pause()
        {
            status = PAUSE;
        }

        public void Stop()
        {
            status = STOP;
            currentFrame = 0f;
            currentTime = 0f;
        }

        public void Next(float multipleRate = 1.0f)
        {
            if (isPlay)
            {
                currentFrame += rate;
                currentTime += Time.deltaTime * multipleRate;
            }
        }

        public void Preview()
        {
            currentFrame -= rate;
        }

        public void Goto(float frame)
        {
            currentFrame = frame;
        }

        public float Difference(float difference)
        {
            return currentFrame - difference;
        }

        public void Restore()
        {
            Goto(0f);
            currentTime = 0.0f;
        }
    }
}