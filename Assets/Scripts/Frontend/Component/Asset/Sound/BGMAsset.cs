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

namespace Frontend.Component.Asset.Sound
{
    public sealed class BGMAsset : BaseSoundAsset
    {
        public BGMAsset(string assetName, bool onlyOncePlay = false)
        {
            audioClip = Resources.Load(assetName) as AudioClip;
            audioSource = null;
            this.onlyOncePlay = onlyOncePlay;
        }

        private AudioSource audioSource { get; set; }

        private AudioClip audioClip { get; }

        public void Play(GameObject player, bool loop)
        {
            if (false == enablePlay) return;
            if (onlyOncePlay && enablePlay) enablePlay = false;
            audioSource = player.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = 1.0f;
            audioSource.loop = loop;
            audioSource.Play();
        }

        public void Play(string playerName, bool loop)
        {
            var player = GameObject.Find(playerName);
            if (null == player)
            {
                Debug.LogError("not exist audio player::" + playerName);
                return;
            }

            Play(player, loop);
        }

        public void Play(GameObject player)
        {
            Play(player, false);
        }

        public void Play(string playerName)
        {
            Play(playerName, false);
        }

        public override void Pause()
        {
            if (null == audioSource) return;
            audioSource.Pause();
        }

        public override void Stop()
        {
            if (null == audioSource) return;
            audioSource.Stop();
        }
    }
}