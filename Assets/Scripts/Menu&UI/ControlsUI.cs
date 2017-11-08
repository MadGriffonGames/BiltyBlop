using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    [SerializeField]
    public GameObject throwButton;
    public Image throwButtonImage;

    private void Start()
    {
        if (GameManager.currentLvl == "Level1")
        {
            throwButtonImage = throwButton.GetComponent<Image>();
            throwButton.SetActive(false);
        }
    }

    public void Jump()
    {
        Player.Instance.ButtonJump();
    }

    public void Attack()
    {
        Player.Instance.ButtonAttack();
    }

    public void Move(int value)
    {
        Player.Instance.ButtonMove(value);
    }

    public void Throw()
    {
        Player.Instance.ButtonThrow();
    }
}
