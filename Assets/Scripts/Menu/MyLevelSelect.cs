using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class MyLevelSelect : MonoBehaviour
{
    private string iconPath = "SceneIcons";
    [SerializeField]
    private string fileName = "Levels.data";
    [SerializeField]
    private string scenePrefix;
    [SerializeField]
    private GameObject levelMenu;
    [SerializeField]
    private RectTransform levelGroup;
    [SerializeField]
    private int groupCount;//count of "pages" with levels
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Sprite lockIcon;
    [SerializeField]
    private Sprite unlockIcon;
    [SerializeField]
    private bool dontDestroyOnLoad;

    private static bool _active;
    private static MyLevelSelect _internal;
    private int groupIndex;
    private LevelSelectButton[] buttonsArray;
    private LevelData[] data;
    private Sprite[] sceneIcon;

    void Awake()
    {
        _internal = this;
        if (dontDestroyOnLoad) DontDestroyOnLoad(transform.gameObject);
        sceneIcon = Resources.LoadAll<Sprite>(iconPath);
        backButton.onClick.AddListener(() => { Back(); });
        nextButton.onClick.AddListener(() => { Next(); });
        buttonsArray = levelGroup.GetComponentsInChildren<LevelSelectButton>();
        data = new LevelData[buttonsArray.Length * groupCount];
        Load();

    }

    struct LevelData // параметры для сохранения
    {
        public bool isActive, canUse; // обязательные

        // пользовательские (можно изменять)
        public int coins;
        public string time;
    }

    public void SaveScene(int coins, string time)
    {
        int j = 0;
        foreach (LevelData element in data)
        {
            if (!element.isActive) break; else j++;
            Debug.Log(element.isActive);
        }

        if (j <= data.Length - 1)
        {
            data[j].isActive = true;
            data[j].coins = coins;
            data[j].time = time;
        }
        else return;
        StreamWriter writer = new StreamWriter(GetPath());
        foreach (LevelData element in data)
        {
            if (element.isActive)
            {
                writer.WriteLine(element.coins + "|" + element.time);
            }
        }
        writer.Close();
        Debug.Log("[LevelSelect] сохранение в файл: " + GetPath());
        if ((j + 1) <= data.Length - 1 && !data[(j + 1)].isActive) // после сохранения, открываем следующую сцену, если таковая есть
        {
            data[(j + 1)].canUse = true;
            ButtonUpdate();
        }
    }

    void SetLevel(string text, int index)
    {
        string[] t = text.Split(new char[] { '|' });

        // загрузка в таком же порядке, что и запись
        int score = Parse(t[0]);
        string time = t[1];

        data[index].isActive = true;
        data[index].coins = score;
        data[index].time = time;
    }

    void Load()
    {
        backButton.interactable = false;

        if (!File.Exists(GetPath())) // если файла сохранения еще нет, то будет открыта первая сцена
        {
            if (data.Length > 0)
            {
                data[0].canUse = true;
                ButtonUpdate();
            }
            return;
        }

        StreamReader reader = new StreamReader(GetPath());

        int j = 0;
        while (!reader.EndOfStream)
        {
            SetLevel(reader.ReadLine(), j);
            j++;
        }

        if (j <= data.Length - 1)
        {
            data[j].canUse = true;
        }

        reader.Close();

        ButtonUpdate();
    }

    public void LoadScene(int id)
    {
        string level = scenePrefix + id;
        if (!Application.CanStreamedLevelBeLoaded(level))
        {
            Debug.Log("[LevelSelect] сцены не существует или она не добавлена в Build Setting: " + level);
            return;
        }
        Hide();
        GameManager.levelName = level;
        SceneManager.LoadScene("Loading");
    }

    public static bool isActive
    {
        get { return _active; }
    }

    public static MyLevelSelect use
    {
        get { return _internal; }
    }

    static int Parse(string text)
    {
        int value;
        if (int.TryParse(text, out value)) return value;
        return 0;
    }

    string GetPath()
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    void Show()
    {
        _active = true;
        levelMenu.SetActive(true);
    }

    void Hide()
    {
        _active = false;
        levelMenu.SetActive(false);
    }

    void Back()
    {
        if (groupIndex > 1)
        {
            nextButton.interactable = true;
            groupIndex--;
            ButtonUpdate();
        }

        if (groupIndex == 1) backButton.interactable = false;
    }

    void Next()
    {
        if (groupIndex < groupCount)
        {
            backButton.interactable = true;
            groupIndex++;
            ButtonUpdate();
        }

        if (groupIndex == groupCount) nextButton.interactable = false;
    }

    Sprite GetSprite(bool isLock, string iconName)
    {
        if (isLock) return lockIcon;

        if (sceneIcon.Length > 0)
        {
            foreach (Sprite element in sceneIcon)
            {
                if (string.Compare(element.name, iconName) == 0)
                {
                    return element;
                }
            }
        }

        return unlockIcon;
    }

    void ButtonUpdate()
    {
        int j = (buttonsArray.Length * groupIndex) - buttonsArray.Length;
        foreach (LevelSelectButton element in buttonsArray)
        {
            if (data[j].isActive || data[j].canUse)
            {
                element.button.interactable = true;
                element.button.image.sprite = GetSprite(false, scenePrefix + (j + 1));
            }
            else
            {
                element.button.interactable = false;
                element.button.image.sprite = GetSprite(true, scenePrefix + (j + 1));
            }

            j++;
            element.id = j;
            element.buttonText.text = j.ToString();
        }
    }
}