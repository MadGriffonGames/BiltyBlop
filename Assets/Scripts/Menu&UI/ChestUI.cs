using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    Image lightCircle;
    [SerializeField]
    GameObject chest;
    [SerializeField]
    Sprite chestOpen;
    [SerializeField]
    Sprite chestClose;
    [SerializeField]
    GameObject activateButton;
    [SerializeField]
    Image loot;
    [SerializeField]
    GameObject chestFade;
    [SerializeField]
    Sprite[] lootArray;

    Image chestImage;
    bool isSpined;
    Quaternion rotationVector;
    Animator lootAnimator;

    private void Start()
    {
        chestImage = chest.GetComponent<Image>();
        lootAnimator = loot.gameObject.GetComponent<Animator>();

        chestImage.sprite = chestClose; //change sprite based on playerprefs info

        if (Player.Instance.stars >= 3 || PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_collects") >= 3)
        {
            lightCircle.gameObject.SetActive(true);
            isSpined = true;
            activateButton.SetActive(true);
        }
        else
        {
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            chestImage.color = new Color(chestImage.color.r, chestImage.color.g, chestImage.color.b, 0.55f);
            activateButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (isSpined)
        {
            rotationVector = lightCircle.rectTransform.rotation;
            rotationVector.z -= 0.0005f;
            lightCircle.rectTransform.rotation = rotationVector;
        }
    }

    public void OpenChest()
    {
        //Write to player prefs here
        chestImage.sprite = chestOpen;
        chestFade.SetActive(true);
        loot.gameObject.SetActive(true);
        loot.sprite = lootArray[0];
    }

    public void CollectLoot()
    {
        chestFade.SetActive(false);
        loot.gameObject.SetActive(false);
    }
}
