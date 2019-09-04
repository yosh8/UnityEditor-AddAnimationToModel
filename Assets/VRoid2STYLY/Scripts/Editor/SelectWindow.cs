using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectWindow : EditorWindow
{
    int selected;
    string[] array;

    [MenuItem("VRoid2STYLY/Select Animation")]
    static void Open()
    {
        var exampleWindow = CreateInstance<SelectWindow>();
        exampleWindow.ShowAuxWindow();
    }

    private void Awake()
    {
        CreateAnimator.FindAnimationClips();

        List<string> list = CreateAnimator.FileList;
        List<string> listToShow = new List<string>();
        foreach (var item in list)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(item);
            listToShow.Add(filename);
        }
        array = listToShow.ToArray();
        Debug.LogFormat("array: {0}", array);
    }

    void OnGUI()
    {
        //selected = GUILayout.SelectionGrid(selected,
        //    new string[] { "1", "2", "3" }, 1, "PreferencesKeysElement");

        selected = GUILayout.SelectionGrid(selected,
            array, 1, "PreferencesKeysElement");

        if (GUILayout.Button("Create"))
        {
            Debug.Log("Clicked Button");
            CreateAnimator.SetAnimationClipPath(selected);
            CreateAnimator.Execute();
            //LoadWindow.Open();
        }
        if (GUILayout.Button("Change Animation"))
        {
            CreateAnimator.SetAnimationClipPath(selected);
            CreateAnimator.CreateAnimatorController();
            CreateAnimator.Load();
            CreateAnimator.SetAnimatorController();
        }
    }
}