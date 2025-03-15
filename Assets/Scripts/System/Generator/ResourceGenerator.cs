//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Threading.Tasks;
using UnityEngine;

namespace Core.Generator
{
    public sealed class ResourceGenerator
    {
        public static async Task Generate(string prefabName, Vector3 position, Quaternion rotation)
        {
            var request = Resources.LoadAsync(prefabName);
            await request;
            var prefab = request.asset as GameObject;
            GameObject.Instantiate(prefab, position, rotation);
        }
    }
}