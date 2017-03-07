using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    Image pauseMenu;
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject fade;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        fade.SetActive(true);
        controls.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        fade.SetActive(false);
        controls.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.collectedCoins = Player.Instance.startCoinCount;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
