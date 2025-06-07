//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using UnityEngine;

namespace Core.Scene
{
    public class Runtime
    {
        private const string LAUNCH_SCENE_NAME = "Scenes/logo";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad()
        {
            Director.GetInstance().Translate(LAUNCH_SCENE_NAME);
        }
    }
}