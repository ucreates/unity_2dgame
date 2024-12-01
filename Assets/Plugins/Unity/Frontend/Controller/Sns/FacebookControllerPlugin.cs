//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityPlugin.Core.Configure.Sns;

namespace UnityPlugin.Frontend.Controller.Sns
{
    public sealed class FacebookControllerPlugin : BasePlugin
    {
        public FacebookControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        public override int id => 4;

        [DllImport("__Internal")]
        private static extern void transitionFacebookViewControllerPlugin(IntPtr imageData, int imageDataLength);

        public void Post(Texture2D texture)
        {
            var imageData = texture.EncodeToPNG();
            Post(imageData);
        }

        public void Post(byte[] imageData)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                var heapSize = Marshal.SizeOf(imageData[0]) * imageData.Length;
                var imageDataPtr = Marshal.AllocHGlobal(heapSize);
                Marshal.Copy(imageData, 0, imageDataPtr, imageData.Length);
                transitionFacebookViewControllerPlugin(imageDataPtr, imageData.Length);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                if (null == androidPlugin) return;
                androidPlugin.CallStatic("transitionFacebook", FacebookConfigurePlugin.APP_ID, imageData);
            }
        }
    }
}