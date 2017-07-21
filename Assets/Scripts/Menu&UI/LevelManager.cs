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

    [SerializeField]
    Button nextButton;
    [SerializeField]
    Button backButton;
	[SerializeField]
	GameObject actsSpacer;
    [SerializeField]
    public GameObject mainMenuCanvas;
    [SerializeField]
    public GameObject lvlSelectCanvas;

    public int groupCount;
    private int groupIndex;
    public List<Level> levelList;
    public GameObject levelButton;
    public Transform map;
    public LevelButton[] levelButtonsArray;
    Vector2 screenSize;

	void Start ()
    {
        PlayerPrefs.SetInt("Level1", 1);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Back();
            }
        }
    }

    void SetButtons()
    {
        foreach (var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            if (PlayerPrefs.GetInt("Level" + button.levelText) == 1)
            {
                level.unlocked = 1;
                level.isInteractable = true;
            }
            button.unlocked = level.unlocked;
            button.GetComponent<Button>().interactable = level.isInteractable;
            newButton.transform.SetParent(map, true);
        }
        SaveAll();
    }

    public void LoadLevel(string levelName)
    {
        GameManager.nextLevelName = "Level" + levelName;
        SceneManager.LoadScene("Loading");
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level" + button.levelText, button.unlocked);
        }
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        if (map.gameObject.activeInHierarchy) 
		{
            map.gameObject.SetActive (false);
			actsSpacer.SetActive (true);
		}
        else 
		{
            lvlSelectCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
		}
    }
}