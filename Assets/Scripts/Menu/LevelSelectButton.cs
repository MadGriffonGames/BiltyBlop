using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private Text _buttonText;

    public int id { get; set; }

    public Text buttonText
    {
        get { return _buttonText; }
    }

    public Button button
    {
        get { return _button; }
    }

    public void ButtonAction() // событие кнопки
    {
        LevelSelect.use.LoadScene(id);
    }
}
