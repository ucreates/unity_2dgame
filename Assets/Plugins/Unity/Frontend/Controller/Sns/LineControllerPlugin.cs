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

namespace UnityPlugin.Frontend.Controller.Sns
{
    public sealed class LineControllerPlugin : BasePlugin
    {
        public LineControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        public override int id => 4;

        [DllImport("__Internal")]
        private static extern void transitionLineViewControllerPlugin(IntPtr imageData, int imageDataLength);

        public void Message(Texture2D texture)
        {
            var imageData = texture.EncodeToPNG();
            Message(imageData);
        }

        public void Message(byte[] imageData)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                var heapSize = Marshal.SizeOf(imageData[0]) * imageData.Length;
                var imageDataPtr = Marshal.AllocHGlobal(heapSize);
                Marshal.Copy(imageData, 0, imageDataPtr, imageData.Length);
                transitionLineViewControllerPlugin(imageDataPtr, imageData.Length);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                if (null == androidPlugin) return;
                var imagePath = Path.Combine(Application.temporaryCachePath, "image.png");
                File.WriteAllBytes(imagePath, imageData);
                androidPlugin.CallStatic("transitionLine", imagePath);
            }
        }
    }
}