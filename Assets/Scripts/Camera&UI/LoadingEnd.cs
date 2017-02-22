using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingEnd : MonoBehaviour
{
    [SerializeField]
    public GameObject loadingInfo, loadingIcon;
    private AsyncOperation async;

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(GameManager.levelName);
        loadingIcon.SetActive(true);
        loadingInfo.SetActive(false);
        yield return true;
        async.allowSceneActivation = false;
        loadingIcon.SetActive(false);
        loadingInfo.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKey)
            async.allowSceneActivation = true;
    }
}
