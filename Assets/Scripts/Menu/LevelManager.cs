using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public int unlocked;
        public bool isInteractable;
    }

    public List<Level> levelList;
    public GameObject levelButton;
    public Transform spacer;

	void Start ()
    {
        SetButtons();
	}

    void SetButtons()
    {
        foreach (var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.levelText.text = level.levelText;
            if (PlayerPrefs.GetInt("Level" + button.levelText.text) == 1)
            {
                level.unlocked = 1;
                level.isInteractable = true;
            }
            button.unlocked = level.unlocked;
            button.GetComponent<Button>().interactable = level.isInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.levelText.text));

            newButton.transform.SetParent(spacer);
        }
        SaveAll();
    }

    void LoadLevel(string levelName)
    {
        GameManager.levelName = levelName;
        SceneManager.LoadScene("Loading");
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level" + button.levelText.text, button.unlocked);
        }
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}
