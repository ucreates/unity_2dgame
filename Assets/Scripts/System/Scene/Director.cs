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
using UnityEngine.SceneManagement;
using System.Collections;
namespace Core.Scene {
public sealed class Director {
    public static IEnumerator Translate(int level, float waitForSeconds = 1.0f) {
        yield return new WaitForSeconds(waitForSeconds);
        SceneManager.LoadScene(level);
    }
    public static IEnumerator Translate(string sceneName, float waitForSeconds = 1.0f) {
        yield return new WaitForSeconds(waitForSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
}
