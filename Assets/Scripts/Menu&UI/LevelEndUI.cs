using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    int coinsCollected;
    [SerializeField]
    Text coinsText;
    [SerializeField]
    GameObject[] starsFull;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject controlsUI;
    [SerializeField]
    GameObject pauseUI;

    void Start ()
    {
        controlsUI.SetActive(false);
        pauseUI.SetActive(false);

        SoundManager.PlayMusic("victory sound", false);
        fade.SetActive(true);

        coinsCollected = GameManager.lvlCollectedCoins;
        coinsText.text = "" + coinsCollected;
        StartCoroutine(ShowStars(Player.Instance.stars));
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

    public void DoubleCoins()
    {

    }

    public IEnumerator ShowStars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            starsFull[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
