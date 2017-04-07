using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    int coinsCollected;
    [SerializeField]
    Text monstersText;
    [SerializeField]
    Text coinsText;
    [SerializeField]
    GameObject[] starsFull;


    void Start ()
    {
        Time.timeScale = 0;
        coinsCollected = GameManager.lvlCollectedCoins;
        coinsText.text = ("Coins collected x" + coinsCollected);
        monstersText.text = ("Monsters killed x" + Player.Instance.monstersKilled);
        ShowStars(Player.Instance.collectables);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
    }

    public void ShowStars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            starsFull[i].SetActive(true);
        }
    }
}
