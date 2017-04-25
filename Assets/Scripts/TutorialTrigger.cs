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

    private void Start()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && active)
        {
            active = false;
            Time.timeScale = 0;
            if (arrow != null)
            {
                arrow.SetActive(true);
            }
            TutorialUI.Instance.oldmanFace.gameObject.SetActive(true);
            TutorialUI.Instance.textBar.gameObject.SetActive(true);
            TutorialUI.Instance.okButton.gameObject.SetActive(true);
            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = text;
        }
    }
}
