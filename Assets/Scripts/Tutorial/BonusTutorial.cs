using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BonusTutorial : InAppTutorial
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
		if (isCollected && (PlayerPrefs.GetInt("Level3") == 0 || PlayerPrefs.GetInt("Level4") == 0) )
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

        string currentLevel = SceneManager.GetActiveScene().name;

        bonusLight.GetComponent<RectTransform>().transform.localPosition = lightPos;

		if (other.gameObject.CompareTag("Player") && currentLevel == "Level2" && PlayerPrefs.GetInt("Level3") == 0)
        {           

            isActive = true;

            GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;

			backpackLight.SetActive(true);

			TutorialUI.Instance.txt.fontSize = fontSize;
			TutorialUI.Instance.txt.text = textInventory;
			ActivateTutorial();
            if (type == "Immortal")
            {
                int currentCount = Inventory.Instance.GetItemCount(Inventory.IMMORTAL_BONUS);
                currentCount = currentCount > 3 ? 3 : currentCount;
                Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 3 - currentCount);
                DevToDev.Analytics.Tutorial(4);
            }        
        }
		else if (other.gameObject.CompareTag("Player") && currentLevel == "Level2" && PlayerPrefs.GetInt("Level3") == 1)
		{
			isActive = true;

			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;			

			if (type == "Immortal")
			{
				Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.IMMORTAL_BONUS);
                backpackLight.SetActive(false);
            }
		}

        if (isCollected && currentLevel == "Level3" && PlayerPrefs.GetInt("Level4") == 0)
        {
            Player.Instance.mobileInput = 0;

            isActive = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            backpackLight.SetActive(true);

            TutorialUI.Instance.txt.fontSize = fontSize;
            TutorialUI.Instance.txt.text = textInventory;
            ActivateTutorial();

            if (type == "Speed")
            {
                int currentCount = Inventory.Instance.GetItemCount(Inventory.SPEED_BONUS);
                currentCount = currentCount > 3 ? 3 : currentCount;
                Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 3 - currentCount);
                DevToDev.Analytics.Tutorial(6);
            }
        }
        else if (other.gameObject.CompareTag("Player") && currentLevel == "Level3" && PlayerPrefs.GetInt("Level4") == 1)
        {
            isActive = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            if (type == "Speed")
            {
                Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 1);
                Inventory.Instance.UseBonus(Inventory.SPEED_BONUS);
                backpackLight.SetActive(false);
            }
        }
    }

    
}
