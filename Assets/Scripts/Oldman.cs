using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldman : MonoBehaviour
{
    [SerializeField]
    string[] text;
    int i = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            TutorialUI.Instance.oldmanFace.gameObject.SetActive(true);
            TutorialUI.Instance.textBar.gameObject.SetActive(true);
            TutorialUI.Instance.txt.text = text [i];
        }
    }

    public void ShowNextMsg()
    {
        TutorialUI.Instance.txt.text = text[++i];
    }

    public void NextButton()
    {

    }
}
