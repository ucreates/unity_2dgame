//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityPlugin.Frontend.Notify
{
    public sealed class FCMNotifierPlugin : BasePlugin
    {
        public FCMNotifierPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.frontend.notify.FCMNotifierPlugin");
        }

        [DllImport("__Internal")]
        private static extern void registerFCMNotifier();

        public void Register()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                registerFCMNotifier();
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                {
                    var javaObject = androidPlugin.CallStatic<AndroidJavaObject>("getInstance");
                    javaObject.Call("register");
                }
        }
    }
}