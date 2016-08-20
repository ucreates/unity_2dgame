using UnityEngine;
using UnityEditor;
using System;
public class PrefsEditorCommand : EditorWindow {
    [MenuItem("EditorMenu/PrefsEditor/Clear")]
    private static void Clear() {
        PlayerPrefs.DeleteAll();
    }
}
