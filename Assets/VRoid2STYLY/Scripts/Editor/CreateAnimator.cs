using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Networking;
using System.Collections.Generic;

// Create a menu item that causes a new controller and statemachine to be created.

public class CreateAnimator : MonoBehaviour
{
    static string pathAnimationClip = "Assets/VRoid2STYLY/AnimationClips/Walking.anim";
    static string modelDirectoryPath = "Assets/VRoid2STYLY/Models/";
    static string modelPath = modelDirectoryPath + "MyModel";
    static string prefabPath = "Assets/VRoid2STYLY/Models/MyModel.prefab";
    static string controllerPath = "Assets/VRoid2STYLY/Models/animator.controller";
    static string prefabForUploadPath = "Assets/VRoid2STYLY/ForUpload/VRoid_with_Motion.prefab";
    static AnimatorController controller;
    static List<string> fileList = new List<string>();
    static GameObject goFromPrefab;

    public static List<string> FileList
    {
        get
        {
            return fileList;
        }

        set
        {
            fileList = value;
        }
    }

    public static void FindAnimationClips()
    {
        FileList.Clear();
        string[] foldersToSearch = new string[] { "Assets/VRoid2STYLY/AnimationClips" };

        string[] guids = AssetDatabase.FindAssets("t:animationclip", foldersToSearch);
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(path);
            FileList.Add(path);
        }
        //Debug.Log(fileList);
    }

    public static void SetAnimationClipPath(int index)
    {
        if (null != FileList[index])
        {
            pathAnimationClip = FileList[index];
            Debug.Log("Set AnimationClip to:" + pathAnimationClip);
        }
    }


    public static void CreateAnimatorController()
    {
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(pathAnimationClip);
        Debug.Log("clip:"+clip);
        // Creates the controller
        controller = AnimatorController.CreateAnimatorControllerAtPathWithClip(controllerPath, clip);
    }

    //[MenuItem("MyMenu/Import file")]
    public static void Import()
    {
        string path = EditorUtility.OpenFilePanel("Select file to import", "", "*");
        if(path == "" || path == null)
        {
            return;
        }
        string extension = System.IO.Path.GetExtension(path);

        FileUtil.DeleteFileOrDirectory(modelDirectoryPath);
        string dirPath = System.IO.Directory.GetParent(Application.dataPath) + "/" + modelDirectoryPath;

        System.IO.Directory.CreateDirectory(dirPath);
        FileUtil.CopyFileOrDirectory(path, modelPath + extension);

        AssetDatabase.Refresh();
    }

    //[MenuItem("MyMenu/Load prefab to Scene")]
    public static void Load()
    {
        RemoveInstance();

        Debug.Log("in Load() prefabPath: " + prefabPath);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        Debug.Log("in Load() prefab: " + prefab);
        goFromPrefab = GameObject.Instantiate(prefab);
    }

    //[MenuItem("MyMenu/Set Animator Controller")]
    public static void SetAnimatorController()
    {
        Animator animator = goFromPrefab.GetComponent<Animator>();
        controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        Debug.Log("controller name:" + controller.name);
        animator.runtimeAnimatorController = controller as RuntimeAnimatorController;

        var prefabForUpload = PrefabUtility.CreatePrefab(prefabForUploadPath, goFromPrefab);
        PrefabUtility.ConnectGameObjectToPrefab(goFromPrefab, prefabForUpload);
    }

    static void RemoveInstance()
    {
        GameObject go = GameObject.Find("VRoid_with_Motion");
        DestroyImmediate(go);
    }
}