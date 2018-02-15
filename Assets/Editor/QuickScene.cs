using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuickSceneManager : MonoBehaviour
{
    [MenuItem("QuickSceneManager/Main Menu")]
    static void MainMenu()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/Scenes/Menu/MainMenu.unity");
    }

    [MenuItem("QuickSceneManager/Shop")]
    static void Shop()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/Scenes/Menu/Shop.unity");
    }

    [MenuItem("QuickSceneManager/Map")]
    static void Map()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/Scenes/Menu/Map.unity");
    }
}
