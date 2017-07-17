using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InAppTutorial : MonoBehaviour
{
    [SerializeField]
    public string textInventory;
    [SerializeField]
    public string textInApp;
    [SerializeField]
    public int fontSize;
    [SerializeField]
    public GameObject light;
    [SerializeField]
    public GameObject backpackLight;

    public bool isActive = false;
    public bool isTextChanged = false;

    public void Update()
    {
        if (!isTextChanged && InventoryUI.isOpen && isActive)
        {
            isTextChanged = true;

            light.SetActive(true);
            backpackLight.SetActive(false);
            TutorialUI.Instance.txt.text = textInApp;
        }
        if (isTextChanged && !InventoryUI.isOpen && isActive)
        {
            DisactivateTutorial();
            isActive = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            backpackLight.SetActive(true);

            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = textInventory;
            ActivateTutorial();
        }
    }

    public void ActivateTutorial()
    {
        TutorialUI.Instance.oldmanFace.gameObject.SetActive(true);
        TutorialUI.Instance.textBar.gameObject.SetActive(true);
        TutorialUI.Instance.oldmanFace.color += new Color(0, 0, 0, 1);
        TutorialUI.Instance.textBar.color += new Color(0, 0, 0, 1);
    }

    public void DisactivateTutorial()
    {
        TutorialUI.Instance.txt.text = "";
        light.SetActive(false);

        TutorialUI.Instance.oldmanFace.gameObject.SetActive(false);
        TutorialUI.Instance.textBar.gameObject.SetActive(false);
        TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, 1);
        TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, 1);
    }
}
