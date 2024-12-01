//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityPlugin.Frontend.View
{
    public sealed class IndicatorViewPlugin : BasePlugin
    {
        public IndicatorViewPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.frontend.view.IndicatorViewPlugin");
        }

        [DllImport("__Internal")]
        private static extern void showIndicatorViewPlugin();

        [DllImport("__Internal")]
        private static extern void hideIndicatorViewPlugin();

        public void Show()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                showIndicatorViewPlugin();
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                    androidPlugin.Call("show");
        }

        public void Hide()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                hideIndicatorViewPlugin();
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                    androidPlugin.Call("hide");
        }
    }
}