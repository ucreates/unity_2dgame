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
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityPlugin.Frontend.Component.Asset
{
    public class NativeTextureAssetPlugin : BasePlugin
    {
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern void LoadTextureByNativeTextureAssetPlugin(int instanceId, string textureAssetPath,
            int width = 0, int height = 0, bool useAlphaChannel = false);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern void DestroyTextureByNativeTextureAssetPlugin(int instanceId);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern int GetTextureWidthByNativeTextureAssetPlugin(int instanceId);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern int GetTextureHeightByNativeTextureAssetPlugin(int instanceId);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern bool EnableAlphaChannelByNativeTextureAssetPlugin(int instanceId);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern void SetTextureIdToNativeTextureAssetPlugin(int instanceId, IntPtr unityNativeTexturePtr);
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern IntPtr GetRenderTextureCallbackByNativeRendererPlugin();
#if UNITY_IPHONE
    [DllImport("__Internal")]
#else
        [DllImport("UnityNativePlugin")]
#endif
        private static extern bool EnableTextureByNativeTextureAssetPlugin(int instanceId);

        public int width { get; set; }
        public int height { get; set; }
        private int instanceId { get; set; }

        public NativeTextureAssetPlugin()
        {
            instanceId = 0;
        }

        public void Load(string textureAssetPath, int width, int height, bool useAlphaChannel,
            GameObject renderGameObject, Shader shader = null)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform || RuntimePlatform.Android == Application.platform)
            {
                if (null == renderGameObject) return;
                var renderer = renderGameObject.GetComponent<UnityEngine.Renderer>();
                if (null == renderer) return;
                instanceId = renderGameObject.GetInstanceID();
                LoadTextureByNativeTextureAssetPlugin(instanceId, textureAssetPath, width, height, useAlphaChannel);
                this.width = GetTextureWidthByNativeTextureAssetPlugin(instanceId);
                this.height = GetTextureHeightByNativeTextureAssetPlugin(instanceId);
                var enableAlphaChannel = EnableAlphaChannelByNativeTextureAssetPlugin(instanceId);
                var format = TextureFormat.RGB24;
                if (enableAlphaChannel) format = TextureFormat.ARGB32;
                var extension = Path.GetExtension(textureAssetPath);
                var texture = new Texture2D(this.width, this.height, format, false);
                texture.filterMode = FilterMode.Point;
                texture.Apply();
                if (extension.Equals(".png"))
                {
                    if (null == shader)
                    {
                        Destroy();
                        return;
                    }

                    var material = new Material(shader);
                    material.mainTexture = texture;
                    renderer.material = material;
                }
                else
                {
                    renderer.material.mainTexture = texture;
                }

                renderer.material.mainTextureScale = new Vector2(-1f, 1f);
                var texturePtr = texture.GetNativeTexturePtr();
                SetTextureIdToNativeTextureAssetPlugin(instanceId, texturePtr);
            }
        }

        public void Destroy()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform || RuntimePlatform.Android == Application.platform)
                DestroyTextureByNativeTextureAssetPlugin(instanceId);
        }

        public bool Enable()
        {
            var ret = true;
            if (RuntimePlatform.IPhonePlayer == Application.platform || RuntimePlatform.Android == Application.platform)
                ret = EnableTextureByNativeTextureAssetPlugin(instanceId);
            return ret;
        }

        public IntPtr GetNativeRenderCallback()
        {
            var ret = IntPtr.Zero;
            if (RuntimePlatform.IPhonePlayer == Application.platform || RuntimePlatform.Android == Application.platform)
                ret = GetRenderTextureCallbackByNativeRendererPlugin();
            return ret;
        }
    }
}