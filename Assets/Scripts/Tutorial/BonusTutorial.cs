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
            EnableControls(false);
            Player.Instance.mobileInput = 0;

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
            }
            if (type == "Damage")
            {
                int currentCount = Inventory.Instance.GetItemCount(Inventory.DAMAGE_BONUS);
                currentCount = currentCount > 3 ? 3 : currentCount;
                Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 3 - currentCount);
            }            
        }
		else if (other.gameObject.CompareTag("Player") && currentLevel == "Level2" && PlayerPrefs.GetInt("Level3") == 1)
		{
			isActive = true;

			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;			

			if (type == "Damage")
			{
				tutorialManeken.GetComponent<Maneken> ().isNeedDoubleDamage = false;
				Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.DAMAGE_BONUS);
                backpackLight.SetActive(false);
            }
			if (type == "Immortal")
			{
				Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.IMMORTAL_BONUS);
                backpackLight.SetActive(false);
            }
			
		}

        if (isCollected && currentLevel == "Level3" && PlayerPrefs.GetInt("Level4") == 0)
        {
            Debug.Log(1);
            EnableControls(false);
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
            }
            if (type == "Time")
            {
                int currentCount = Inventory.Instance.GetItemCount(Inventory.TIME_BONUS);
                currentCount = currentCount > 3 ? 3 : currentCount;
                Inventory.Instance.AddItem(Inventory.TIME_BONUS, 3 - currentCount);
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
            if (type == "Time")
            {
                Inventory.Instance.AddItem(Inventory.TIME_BONUS, 1);
                Inventory.Instance.UseBonus(Inventory.TIME_BONUS);
                backpackLight.SetActive(false);
            }
        }
    }

    
}
