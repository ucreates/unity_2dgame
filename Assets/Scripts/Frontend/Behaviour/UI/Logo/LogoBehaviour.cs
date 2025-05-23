﻿//======================================================================
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
using Core.Scene;
using Frontend.Component.Asset.Render;
using Service;
using TMPro;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public sealed class LogoBehaviour : BaseBehaviour
{
    public TextMeshProUGUI progressText;

    public IEnumerator Start()
    {
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.enabled = false;
        var downloadCallback = new Action<float>(delegate(float progress) { progressText.text = $"Downloading...{Convert.ToInt32(progress * 100f)}%"; });
        var strategy = ServiceGateway.GetInstance()?.Request("service://master/init");
        yield return strategy?.Request(downloadCallback);
        strategy?.Update();
        progressText.enabled = false;
        renderer.enabled = true;
        AnimationState.TrackEntryDelegate callback = state => { StartCoroutine(Director.Translate("game", 0.0f)); };
        var animator = new AnimatorAsset(this);
        animator?.Play("show", callback, false);
    }
}