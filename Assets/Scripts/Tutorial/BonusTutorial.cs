using System.Collections;
using System.Collections.Generic;
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
		if (isCollected && PlayerPrefs.GetInt("Level3") == 0)
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
		if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level3") == 0)
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
                Player.Instance.damageBonusNum = 0;
                Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 3);
            }
            if (type == "Damage")
            {
                Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 3);

            }
            if (type == "Speed")
            {
                Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 3);
            }
            if (type == "Time")
            {
                Inventory.Instance.AddItem(Inventory.TIME_BONUS, 3);
            }
        }
		else if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level3") == 1)
		{
			isActive = true;

			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;

			backpackLight.SetActive(false);

			if (type == "Damage")
			{
				tutorialManeken.GetComponent<Maneken> ().isNeedDoubleDamage = false;
				Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.DAMAGE_BONUS);
			}
			if (type == "Immortal")
			{
				Player.Instance.damageBonusNum = 0;
				Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.IMMORTAL_BONUS);	
			}
			if (type == "Speed")
			{
				Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.SPEED_BONUS);
			}
			if (type == "Time")
			{
				Inventory.Instance.AddItem(Inventory.TIME_BONUS, 1);
				Inventory.Instance.UseBonus (Inventory.TIME_BONUS);
			}
		}
    }
}
