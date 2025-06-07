//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections;
using Core.Object;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scene
{
    public sealed class Director
    {
        private Director()
        {
            previousSceneName = string.Empty;
        }

        private static Director instance { get; set; }

        public string previousSceneName { get; set; }

        public static Director GetInstance()
        {
            instance ??= new Director();
            return instance;
        }

        public void Translate(int level)
        {
            var scene = SceneManager.GetActiveScene();
            previousSceneName = scene.name;
            SceneManager.LoadScene(level);
            Resource.Unload();
        }

        public void Translate(string sceneName)
        {
            var scene = SceneManager.GetActiveScene();
            previousSceneName = scene.name;
            SceneManager.LoadScene(sceneName);
            Resource.Unload();
        }

        public IEnumerator Translate(int level, float waitForSeconds)
        {
            var scene = SceneManager.GetActiveScene();
            previousSceneName = scene.name;
            yield return new WaitForSeconds(waitForSeconds);
            SceneManager.LoadScene(level);
            Resource.Unload();
            yield return null;
        }

        public IEnumerator Translate(string sceneName, float waitForSeconds)
        {
            var scene = SceneManager.GetActiveScene();
            previousSceneName = scene.name;
            yield return new WaitForSeconds(waitForSeconds);
            SceneManager.LoadScene(sceneName);
            Resource.Unload();
            yield return null;
        }

        public bool HasPreviewScene()
        {
            return false == string.IsNullOrEmpty(previousSceneName);
        }
    }
}