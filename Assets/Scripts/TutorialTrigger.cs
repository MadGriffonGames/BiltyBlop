using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    string text;
    [SerializeField]
    int fontSize;
    [SerializeField]
    GameObject arrow;

    bool active = true;
    bool hide = false;
    bool show = false;

    private void Start()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
    }

    private void Update()
    {
        if (hide)
        {
            FadeOut();
        }
        if (show)
        {
            FadeIn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && active)
        {
            SoundManager.MakeSteps(false);
            if (arrow != null)
            {
                arrow.SetActive(true);
            }
            show = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = false;
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
            hide = true;
        }
    }

    void FadeIn()
    {
        
        TutorialUI.Instance.oldmanFace.gameObject.SetActive(true);
        TutorialUI.Instance.textBar.gameObject.SetActive(true);
        TutorialUI.Instance.oldmanFace.color += new Color(0, 0, 0, 0.05f);
        TutorialUI.Instance.textBar.color += new Color(0, 0, 0, 0.05f);
        if (TutorialUI.Instance.textBar.color.a >= 0.2f)
        {
            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = text;
        }
        if (TutorialUI.Instance.textBar.color.a >= 1)
        {
            show = false;
        }
    }

    void FadeOut()
    {
        TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, 0.05f);
        TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, 0.05f);
        if (TutorialUI.Instance.textBar.color.a <= 0.5f)
        {
            TutorialUI.Instance.txt.text = "";
        }
        if (TutorialUI.Instance.textBar.color.a <= 0)
        {
            hide = false;
        }
    }
}
