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
    public sealed class AlertViewPlugin : BasePlugin
    {
        [DllImport("__Internal")]
        private static extern void showAlertViewPlugin(string message);

        public void Show(string message)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                showAlertViewPlugin(message);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                androidPlugin = new AndroidJavaObject("com.frontend.view.AlertViewPlugin");
                androidPlugin.CallStatic("show", message);
            }
        }
    }
}