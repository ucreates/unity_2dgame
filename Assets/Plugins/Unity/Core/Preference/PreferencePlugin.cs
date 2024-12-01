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

namespace UnityPlugin.Core.Preference
{
    public sealed class PreferencePlugin : BasePlugin
    {
        public PreferencePlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.preference.PreferencePlugin");
        }

        [DllImport("__Internal")]
        private static extern bool getSwitchPreferencePlugin(string keyName);

        public bool GetSwitchPreference(string keyName)
        {
            var ret = false;
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                ret = getSwitchPreferencePlugin(keyName);
            else if (RuntimePlatform.Android == Application.platform)
                ret = androidPlugin.CallStatic<bool>("getSwitchPreference", keyName);
            return ret;
        }
    }
}