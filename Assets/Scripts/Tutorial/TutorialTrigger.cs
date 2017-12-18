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
    [SerializeField]
    bool reset;
	[SerializeField]
	int disablingOpenedLevel;

    bool active = true;
    bool hide = false;
    bool show = false;
    bool isLocalized;

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        if (arrow != null)
        {
            arrow.SetActive(false);
        }

        isLocalized = false;
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
            if (arrow != null)
            {
                arrow.SetActive(true);
            }
            hide = false;
            show = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!reset)
            {
                active = false;
            }
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
            show = false;
            hide = true;
        }
    }

    void FadeIn()
    {
        TutorialUI.Instance.oldmanFace.gameObject.SetActive(true);
        TutorialUI.Instance.textBar.gameObject.SetActive(true);
        TutorialUI.Instance.oldmanFace.color += new Color(0, 0, 0, 0.12f);
        TutorialUI.Instance.textBar.color += new Color(0, 0, 0, 0.12f);
        if (TutorialUI.Instance.textBar.color.a >= 0.2f)
        {
            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = text;
        }

        if (!isLocalized)
        {
            LocalizationManager.Instance.UpdateLocaliztion(TutorialUI.Instance.txt);
            isLocalized = true;
        }
        
        if (TutorialUI.Instance.textBar.color.a >= 1)
        {
            show = false;
        }
    }

    void FadeOut()
    {
        TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, 0.12f);
        TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, 0.12f);
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
