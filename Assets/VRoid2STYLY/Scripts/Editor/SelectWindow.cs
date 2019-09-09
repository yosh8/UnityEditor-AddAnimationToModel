using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectWindow : EditorWindow
{
    int selected;
    string[] array;
    static SelectWindow exampleWindow;

   [MenuItem("VRoid2STYLY/Select Animation")]
    static void Open()
    {
        if(null == exampleWindow)
        {
            exampleWindow = CreateInstance<SelectWindow>();
        }
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
        selected = GUILayout.SelectionGrid(selected,
            array, 1, "PreferencesKeysElement");

        if (GUILayout.Button("OK"))
        {
            CreateAnimator.SetAnimationClipPath(selected);
            CreateAnimator.CreateAnimatorController();
            CreateAnimator.Load();
            CreateAnimator.SetAnimatorController();
            exampleWindow.Close();
        }
    }
}