using UnityEditor;
using UnityEngine;

public class PrefsEditorCommand : EditorWindow
{
    [MenuItem("EditorMenu/PrefsEditor/Clear")]
    private static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}