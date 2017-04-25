using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]
    GameObject musicButton;
    [SerializeField]
    GameObject fxButton;
    [SerializeField]
    GameObject settingsBar;
    Animator MyAnimator;
    
    bool hide = true;

    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
        musicButton.gameObject.SetActive(false);
        fxButton.gameObject.SetActive(false);
        settingsBar.gameObject.SetActive(false);
    }

    public void ShowButtons()
    {
        hide = !hide;
        if (!hide)
        {
            musicButton.gameObject.SetActive(true);
            fxButton.gameObject.SetActive(true);
            settingsBar.gameObject.SetActive(true);
        }
        else
        {
            musicButton.gameObject.SetActive(false);
            fxButton.gameObject.SetActive(false);
            settingsBar.gameObject.SetActive(false);
        }
    }
}
