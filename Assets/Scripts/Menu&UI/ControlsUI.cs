using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
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
}
