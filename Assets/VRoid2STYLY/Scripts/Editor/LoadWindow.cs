using UnityEditor;
using UnityEngine;

public class LoadWindow : EditorWindow
{
    [MenuItem("VRoid2STYLY/Place VRoid to Scene")]
    public static void Open()
    {
        var exampleWindow = CreateInstance<LoadWindow>();
        exampleWindow.ShowAuxWindow();
    }

    int selected;

    void OnGUI()
    {
        selected = GUILayout.SelectionGrid(selected,
            new string[] { "Model Loaded" }, 1, "PreferencesKeysElement");

        if (GUILayout.Button("Create"))
        {
            CreateAnimator.Load();
            CreateAnimator.SetAnimatorController();
        }
    }
}