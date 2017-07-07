using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    const int IMMORTAL_DURATION = 5;
    const int DAMAGE_DURATION = 10;
    const int SPEED_DURATION = 3;
    const int TIME_DURATION = 7;

    [SerializeField]
    Image backpack;
    [SerializeField]
    Sprite backpackOpen;
    [SerializeField]
    Sprite backpackClose;
    [SerializeField]
    GameObject inventoryBar;
    [SerializeField]
    GameObject bonusBar;
    [SerializeField]
    GameObject inventoryFade;

    bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    public void OpenInventory()
    {
        if (!isOpen)
        {
            ActivateInventory();
        }
        else
        {
            DisactivateInventory();
        }
    }

    void ActivateInventory()
    {
        Time.timeScale = 0;
        backpack.sprite = backpackOpen;
        isOpen = !isOpen;
        inventoryBar.SetActive(true);
        inventoryFade.SetActive(true);
    }

    void DisactivateInventory()
    {
        Time.timeScale = 1;
        backpack.sprite = backpackClose;
        isOpen = !isOpen;
        inventoryBar.SetActive(false);
        inventoryFade.SetActive(false);
    }

    public void HPbutton()
    {
        if (Player.Instance.Health != Player.Instance.maxHealth)
        {
            Player.Instance.Health++;
            HealthUI.Instance.SetHealthbar();
            MakeFX.Instance.MakeHeal();
            DisactivateInventory();
        }
    }

    public void BonusButton()
    {
        if (!bonusBar.activeInHierarchy)
        {
            bonusBar.SetActive(true);
        }
        else
        {
            bonusBar.SetActive(false);
        }
    }

    public void AmmoButton()
    {
        if (Player.Instance.throwingIterator != Player.Instance.clipSize - 1)
        {
            Player.Instance.throwingIterator = Player.Instance.clipSize - 1;
            Player.Instance.ResetThrowing();
            DisactivateInventory();
        }
    }

    public void ImmortalButton()
    {
        if (Player.Instance.immortalBonusNum == 0)
        {
            Player.Instance.ExecBonusImmortal(IMMORTAL_DURATION);

            DisactivateInventory();
        }
        
    }

    public void DamageButton()
    {
        if (Player.Instance.damageBonusNum == 0)
        {
            Player.Instance.ExecBonusDamage(DAMAGE_DURATION);

            DisactivateInventory();
        }
    }

    public void SpeedButton()
    {
        if (Player.Instance.speedBonusNum == 0)
        {
            Player.Instance.ExecBonusSpeed(SPEED_DURATION);

            DisactivateInventory();
        }
    }

    public void TimeButton()
    {
        if (Player.Instance.timeBonusNum == 0)
        {
            Player.Instance.ExecBonusTime(TIME_DURATION);

            DisactivateInventory();
        }
    }
}
