//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityPlugin.Frontend.Notify
{
    public sealed class LocalNotifierPlugin : BasePlugin
    {
        public LocalNotifierPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.frontend.notify.LocalNotifierPlugin");
        }

        [DllImport("__Internal")]
        private static extern void registerLocalNotifier();

        [DllImport("__Internal")]
        private static extern void localNotify(string title, string body, double timeInterval);

        public void Register()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                registerLocalNotifier();
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                {
                    var javaObject = androidPlugin.CallStatic<AndroidJavaObject>("getInstance");
                    javaObject.Call("register");
                }
        }

        public void Notify(string title, string body, double timeInterval)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                localNotify(title, body, timeInterval);
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                {
                    var ltimeInterval = Convert.ToInt64(timeInterval);
                    var javaObject = androidPlugin.CallStatic<AndroidJavaObject>("getInstance");
                    javaObject.Call("notify", title, body, ltimeInterval);
                }
        }
    }
}