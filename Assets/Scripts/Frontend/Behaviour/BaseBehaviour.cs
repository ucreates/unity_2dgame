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
using Frontend.Component.Asset;
using Frontend.Component.Vfx;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    public BaseBehaviour()
    {
        assetCollection = new AssetCollection();
        timeLine = new TimeLine();
    }

    protected IDisposable rx { get; set; }

    public AssetCollection assetCollection { get; set; }

    public TimeLine timeLine { get; set; }

    protected void OnDestroy()
    {
        rx?.Dispose();
    }
}