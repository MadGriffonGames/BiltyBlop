using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTutorial : InAppTutorial
{
    [SerializeField]
    string type;
    [SerializeField]
    GameObject bonusLight;
    [SerializeField]
    Vector3 lightPos;
    [SerializeField]
    GameObject tutorialManeken;


    bool isActivated;
    bool isCollected;

    private void Start()
    {
        isActivated = false;
        isCollected = false;
    }

    private void Update()
    {
        if (isCollected)
        {
            base.Update();
            if (!InventoryUI.isBonusBarOpen)
            {
                bonusLight.SetActive(false);
            }

            if (InventoryUI.isBonusBarOpen && !isActivated)
            {
                isActivated = true;
                light.SetActive(false);
                bonusLight.SetActive(true);
            }
            if (!InventoryUI.isOpen && bonusLight.activeInHierarchy)
            {
                isActivated = false;
                isCollected = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollected = true;

        bonusLight.GetComponent<RectTransform>().transform.localPosition = lightPos;
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level5") == 0)
        {
            isActive = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            backpackLight.SetActive(true);

            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = textInventory;
            ActivateTutorial();

            int currentCount = Inventory.Instance.GetItemCount(Inventory.DAMAGE_BONUS);
            currentCount = currentCount > 3 ? 3 : currentCount;
            Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 3 - currentCount);
            DevToDev.Analytics.Tutorial(3);
        }
        else if (PlayerPrefs.GetInt("Level5") > 0)
        {
            //tutorialManeken.GetComponent<Maneken> ().isNeedDoubleDamage = false;
            Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 1);
            Inventory.Instance.UseBonus(Inventory.DAMAGE_BONUS);
            backpackLight.SetActive(false);
        }
    }


}
