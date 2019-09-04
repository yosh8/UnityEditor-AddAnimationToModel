using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImportWindow : EditorWindow
{
    int selected;
    string[] array;

    static void Open()
    {
        var exampleWindow = CreateInstance<ImportWindow>();
        exampleWindow.ShowAuxWindow();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Create"))
        {
            CreateAnimator.Import();
        }
    }

    [MenuItem("VRoid2STYLY/Import")]
    static void Execute()
    {
        CreateAnimator.Import();
    }
}