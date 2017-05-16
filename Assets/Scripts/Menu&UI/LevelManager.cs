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
        screenSize.x = 1920;
        screenSize.y = 1080;
        SetButtons();
        levelButtonsArray = new LevelButton[levelList.Count];
        levelButtonsArray = FindObjectsOfType<LevelButton>();
        ButtonsUpdate();
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


    public void NextButton()
    {
        if (groupIndex < groupCount)
        {
            groupIndex++;
            int lvlNum = 1;
            int i = levelButtonsArray.Length-1;//cuz lvls goes in revert order
            foreach (var level in levelList)
            {
                LevelButton button = levelButtonsArray[i--];//cuz lvls goes in revert order
                button.levelText.text = (lvlNum++ + levelList.Count * groupIndex).ToString();
                if (PlayerPrefs.GetInt("Level" + button.levelText.text) == 1)
                {
                    level.unlocked = 1;
                    level.isInteractable = true;
                }
                else
                {
                    level.unlocked = 0;
                    level.isInteractable = false;
                }
                button.unlocked = level.unlocked;
                button.GetComponent<Button>().interactable = level.isInteractable;
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.levelText.text));
                if (PlayerPrefs.HasKey("Level" + button.levelText.text + "_collects"))
                {
                    button.ShowStars(PlayerPrefs.GetInt("Level" + button.levelText.text + "_collects"));
                }
                else button.HideStars();
            }
            ButtonsUpdate();
        }
    }

    public void BackButton()
    {
        if (groupIndex != 0)
        {
            groupIndex--;
            int lvlNum = 1;
            int i = levelButtonsArray.Length - 1;//cuz lvls goes in revert order
            foreach (var level in levelList)
            {
                LevelButton button = levelButtonsArray[i--];//cuz lvls goes in revert order
                button.levelText.text = (lvlNum++ + levelList.Count * groupIndex).ToString();
                //2nd statement needs bad, but i dont know why, without it, doesn't works
                if (PlayerPrefs.GetInt("Level" + button.levelText.text) == 1 || button.levelText.text == "1")
                {
                    level.unlocked = 1;
                    level.isInteractable = true;
                }
                else
                {
                    level.unlocked = 0;
                    level.isInteractable = false;
                }
                button.unlocked = level.unlocked;
                button.GetComponent<Button>().interactable = level.isInteractable;
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.levelText.text));
                if (PlayerPrefs.HasKey("Level" + button.levelText.text + "_collects"))
                {
                    button.ShowStars(PlayerPrefs.GetInt("Level" + button.levelText.text + "_collects"));
                }
                else button.HideStars();
            }
            ButtonsUpdate();
        }
    }

    void ButtonsUpdate()
    {
        if (groupIndex == 0)
            backButton.interactable = false;
        else backButton.interactable = true;
        if (groupIndex == groupCount)
            nextButton.interactable = false;
        else nextButton.interactable = true;
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
		} else 
		{
			SceneManager.LoadScene ("MainMenu");
		}
			
    }
}