//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections;
using UnityEngine;

namespace Frontend.Component.Asset.Sound
{
    public sealed class SoundEffectAsset : BaseSoundAsset
    {
        public SoundEffectAsset(string assetName, bool onlyOncePlay = false)
        {
            audioClip = Resources.Load(assetName) as AudioClip;
            this.onlyOncePlay = onlyOncePlay;
        }

        private AudioClip audioClip { get; }

        public override void Play()
        {
            if (false == enablePlay || null == audioClip) return;
            if (onlyOncePlay && enablePlay) enablePlay = false;
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, 1.0f);
        }

        public IEnumerator Delay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            Play();
        }
    }
}