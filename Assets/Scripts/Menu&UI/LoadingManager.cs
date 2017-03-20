using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    public GameObject loadingCompleteTxt, loadingTxt;
    private AsyncOperation async;

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(GameManager.levelName);
        loadingTxt.SetActive(true);
        loadingCompleteTxt.SetActive(false); 
        yield return true;
        async.allowSceneActivation = false;
        

    }

    void Update()
    {
        if (async.progress == 0.9f)
        {
            loadingTxt.SetActive(false);
            loadingCompleteTxt.SetActive(true);
        }
        if (Input.anyKey)
            async.allowSceneActivation = true;
    }
}
