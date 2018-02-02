using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    public GameObject loadingUI;
    [SerializeField]
    RectTransform progressBar;
	[SerializeField]
	Text levelText;
    private AsyncOperation async;
    Vector3 barScale;

    IEnumerator Start()
    {
		switch (GameManager.nextLevelName) 
		{
		default:
			if (GameManager.nextLevelName.Contains ("Level")) 
			{
				if (GameManager.nextLevelName.Length == 6) 
				{
					levelText.text = "level";
                    LocalizationManager.Instance.UpdateLocaliztion(levelText);
                    levelText.text += " " + GameManager.nextLevelName[5].ToString();


                }
				if (GameManager.nextLevelName.Length == 7) 
				{
                    levelText.text = "level";
                    LocalizationManager.Instance.UpdateLocaliztion(levelText);
                    levelText.text += " " + GameManager.nextLevelName[5].ToString() + GameManager.nextLevelName[6].ToString();
				}
			}
			else
				levelText.text = "";
			break;
		}

        async = SceneManager.LoadSceneAsync(GameManager.nextLevelName);

        yield return true;
        async.allowSceneActivation = true;
    }
}
