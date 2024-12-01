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
    public sealed class WebViewPlugin : BasePlugin
    {
        public WebViewPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.frontend.view.WebViewPlugin");
        }

        [DllImport("__Internal")]
        private static extern void showWebViewPlugin(string url, float left, float top, float right, float bottom);

        [DllImport("__Internal")]
        private static extern void hideWebViewPlugin();

        public void Show(string requestUrl, float left, float top, float right, float bottom)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                showWebViewPlugin(requestUrl, left, top, right, bottom);
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                {
                    androidPlugin.Call("create", requestUrl, (int)left, (int)top, (int)right, (int)bottom);
                    androidPlugin.Call("show");
                }
        }

        public void Hide()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                hideWebViewPlugin();
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                {
                    androidPlugin.Call("hide");
                    androidPlugin.Call("destroy");
                }
        }
    }
}