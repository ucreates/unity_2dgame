//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using UnityEngine;

namespace Core.Object
{
    public sealed class Resource
    {
        public static GameObject Instanciate(string prefabName, Vector3 position, Quaternion rotation)
        {
            var prefab = Resources.Load(prefabName) as GameObject;
            return GameObject.Instantiate(prefab, position, rotation);
        }

        public static void Unload()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}