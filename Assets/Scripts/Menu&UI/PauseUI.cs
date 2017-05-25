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
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    GameObject continueButton;

    public void Pause()
    {
        Time.timeScale = 0;
        buttonsSetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        buttonsSetActive(false);
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

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    void buttonsSetActive(bool pause) // переключатель кнопок
    {
        pauseMenu.gameObject.SetActive(pause);
        fade.SetActive(pause);
        controls.SetActive(!pause);
        pauseButton.SetActive(!pause);
        continueButton.SetActive(pause);
    }

}
