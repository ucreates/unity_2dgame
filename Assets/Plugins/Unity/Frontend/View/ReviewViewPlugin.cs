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
    public sealed class ReviewViewPlugin : BasePlugin
    {
        [DllImport("__Internal")]
        private static extern void showReviewViewPlugin(string storeUrl);

        public void Show(string storeUrl)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                showReviewViewPlugin(storeUrl);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                androidPlugin = new AndroidJavaObject("com.frontend.view.ReviewViewPlugin");
                androidPlugin.CallStatic("show", storeUrl);
            }
        }
    }
}