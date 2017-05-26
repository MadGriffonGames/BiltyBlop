using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    public GameObject loadingUI;
    private AsyncOperation async;

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(GameManager.levelName);
        
        yield return true;
        async.allowSceneActivation = true;
        

    }
}
