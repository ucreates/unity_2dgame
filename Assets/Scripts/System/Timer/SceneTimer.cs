//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.Vfx;

namespace Core.Timer
{
    public sealed class SceneTimer
    {
        public SceneTimer()
        {
            timeLine = new TimeLine();
        }

        private TimeLine timeLine { get; }

        public float elapsedTime { get; set; }

        public void Update()
        {
            if (1.0f <= timeLine.currentTime)
            {
                elapsedTime += 1.0f;
                timeLine.Restore();
            }

            timeLine.Next();
        }

        public void Reset()
        {
            elapsedTime = 0.0f;
            timeLine.Restore();
        }
    }
}