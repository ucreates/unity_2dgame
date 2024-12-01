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
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityPlugin.Core.Configure.Sns;

namespace UnityPlugin.Frontend.Controller.Sns
{
    public sealed class TwitterControllerPlugin : BasePlugin
    {
        public TwitterControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        public override int id => 3;

        [DllImport("__Internal")]
        private static extern void transitionTwitterViewControllerPlugin(string message, IntPtr imageData,
            int imageDataLength, bool enableTwitterCard);

        public void Tweet(string message, Texture2D texture, bool enableTwitterCard = false)
        {
            var imageData = texture.EncodeToPNG();
            Tweet(message, imageData, enableTwitterCard);
        }

        public void Tweet(string message, byte[] imageData, bool enableTwitterCard = false)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                var heapSize = Marshal.SizeOf(imageData[0]) * imageData.Length;
                var imageDataPtr = Marshal.AllocHGlobal(heapSize);
                Marshal.Copy(imageData, 0, imageDataPtr, imageData.Length);
                transitionTwitterViewControllerPlugin(message, imageDataPtr, imageData.Length, enableTwitterCard);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                if (null == androidPlugin) return;
                var imagePath = Path.Combine(Application.temporaryCachePath, "image.png");
                File.WriteAllBytes(imagePath, imageData);
                androidPlugin.CallStatic("transitionTwitter", message, imagePath, TwitterConfigurePlugin.CONSUMER_KEY,
                    TwitterConfigurePlugin.CONSUMER_SEACRET, enableTwitterCard);
            }
        }
    }
}