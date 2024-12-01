//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Frontend.Component.Asset.Sound
{
    public abstract class BaseSoundAsset : BaseAsset
    {
        public BaseSoundAsset()
        {
            onlyOncePlay = false;
            enablePlay = true;
        }

        public override string type => "sound";

        protected bool enablePlay { get; set; }

        protected bool onlyOncePlay { get; set; }

        public virtual void Play(bool loop)
        {
        }

        public virtual void Play()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual void Stop()
        {
        }

        public void Reset()
        {
            enablePlay = true;
        }
    }
}