using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private static TutorialUI instance;
    public static TutorialUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialUI>();
            return instance;
        }
    }

    [SerializeField]
    public Text txt;
    [SerializeField]
    public Image oldmanFace;
    [SerializeField]
    public Image textBar;
    [SerializeField]
    public GameObject okButton;
    [SerializeField]
    public GameObject nextButton;

    public void OKButton()
    {
        Time.timeScale = 1;
        GameObject arrow = GameObject.FindGameObjectWithTag("Arrow");
        if (arrow != null)
        {
            Destroy(arrow.gameObject);
        }
        oldmanFace.gameObject.SetActive(false);
        textBar.gameObject.SetActive(false);
        okButton.gameObject.SetActive(false);
    }
}
