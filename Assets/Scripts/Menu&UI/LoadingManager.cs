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
	Text levelText;
    private AsyncOperation async;

    IEnumerator Start()
    {
		switch (GameManager.nextLevelName) 
		{
		case "MainMenu":
			levelText.text = "MAIN MENU";
			break;

		case "Shop":
			levelText.text = "SHOP";
			break;
		case "AchievementMenu":
			levelText.text = "ACHIEVEMENTS";
			break;
		default:
			if (GameManager.nextLevelName.Contains ("Level")) 
			{
				if (GameManager.nextLevelName.Length == 6) 
				{
					levelText.text = "LEVEL " + GameManager.nextLevelName [5].ToString (); 
				}
				if (GameManager.nextLevelName.Length == 7) 
				{
					levelText.text = "LEVEL " + GameManager.nextLevelName [5].ToString () + GameManager.nextLevelName [6].ToString (); 
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
