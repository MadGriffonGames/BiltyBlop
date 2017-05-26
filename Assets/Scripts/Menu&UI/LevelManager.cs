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
    public Transform spacer;
    public LevelButton[] levelButtonsArray;
    Vector2 screenSize;

	void Start ()
    {
        groupIndex = 0;
		for (int i = 1; i < 10; i++) 
		{
			PlayerPrefs.SetInt ("Level" + i, 1);
		}
        screenSize.x = 1920;
        screenSize.y = 1080;
        SetButtons();
        levelButtonsArray = new LevelButton[levelList.Count];
        levelButtonsArray = FindObjectsOfType<LevelButton>();
    }

    void SetButtons()
    {
        int lvlNum = 1;
        foreach (var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.levelText.text = (lvlNum++ + levelList.Count * groupIndex).ToString();
            if (PlayerPrefs.GetInt("Level" + button.levelText.text) == 1)
            {
                level.unlocked = 1;
                level.isInteractable = true;
            }
            button.unlocked = level.unlocked;
            button.GetComponent<Button>().interactable = level.isInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.levelText.text));
            button.GetComponent<Button>().gameObject.transform.localScale = new Vector3(Screen.width / screenSize.x, Screen.height / screenSize.y, 1);
            newButton.transform.SetParent(spacer, true);
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

    public void Back()
    {
        if (spacer.gameObject.activeInHierarchy) 
		{
            spacer.gameObject.SetActive (false);
			actsSpacer.SetActive (true);
		}
        else 
		{
            lvlSelectCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
		}
    }
}