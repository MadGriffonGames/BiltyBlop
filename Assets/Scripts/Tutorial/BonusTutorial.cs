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

    bool isActivated;
    bool isCollected;

    private void Start()
    {
        isActivated = false;
        isCollected = false;
        PlayerPrefs.DeleteKey("Level3");
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
        base.OnTriggerEnter2D(other);
        isCollected = true;
        bonusLight.GetComponent<RectTransform>().transform.localPosition = lightPos;
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level3") == 0)
        {
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
    }
}
