using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSelect : MonoBehaviour
{

    [SerializeField]
    private string iconPath = "SceneIcons"; // папка в Resources, где лежат уникальные иконки сцен, их имена должны быть как и у сцен (необязательно)
    [SerializeField]
    private string fileName = "Levels.data"; // файл для сохранения
    [SerializeField]
    private string scenePrefix; // приставка имени сцены, например, может быть так: Scene_1, Scene_2, Scene_3 и т.п.
    [SerializeField]
    private GameObject levelMenu; // родительский объект менюшки
    [SerializeField]
    private RectTransform levelGroup; // группа иконок
    [SerializeField]
    private int groupCount = 5; // сколько страниц, формула: например, группа иконок содержит 10 объектов, то умножаем на количество страниц = число сцен
    [SerializeField]
    private Button backButton; // предощущая страница
    [SerializeField]
    private Button nextButton; // следующая страница
    [SerializeField]
    private Sprite lockIcon; // иконка, если сцена закрыта
    [SerializeField]
    private Sprite unlockIcon; // иконка, если сцена открыта
    [SerializeField]
    private bool dontDestroyOnLoad; // если 'true' - менюшка будет переходить из сцены в сцену

    private static bool _active;
    private static LevelSelect _internal;
    private int groupIndex;
    private LevelSelectButton[] comp;
    private LevelData[] data;
    private Sprite[] sceneIcon;

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
        if ((j + 1) <= data.Length - 1) // после сохранения, открываем следующую сцену, если таковая есть
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

        SceneManager.LoadScene(level);
    }

    public static bool isActive
    {
        get { return _active; }
    }

    public static LevelSelect use
    {
        get { return _internal; }
    }

    static int Parse(string text)
    {
        int value;
        if (int.TryParse(text, out value)) return value;
        return 0;
    }

    void Awake()
    {
        _internal = this;
        if (dontDestroyOnLoad) DontDestroyOnLoad(transform.gameObject);
        sceneIcon = Resources.LoadAll<Sprite>(iconPath);
        groupIndex = 1;
        backButton.onClick.AddListener(() => { Back(); });
        nextButton.onClick.AddListener(() => { Next(); });
        comp = levelGroup.GetComponentsInChildren<LevelSelectButton>();
        data = new LevelData[comp.Length * groupCount];
        Load();
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
        int j = (comp.Length * groupIndex) - comp.Length;
        foreach (LevelSelectButton element in comp)
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