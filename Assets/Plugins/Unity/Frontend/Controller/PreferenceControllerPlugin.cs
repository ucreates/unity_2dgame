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

namespace UnityPlugin.Frontend.Controller
{
    public sealed class PreferenceControllerPlugin : BasePlugin
    {
        public PreferenceControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        public override int id => 2;

        [DllImport("__Internal")]
        private static extern void transitionViewControllerPlugin(int viewControllerId);

        public void Transition()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                transitionViewControllerPlugin(id);
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                    androidPlugin.CallStatic("transition", id);
        }
    }
}