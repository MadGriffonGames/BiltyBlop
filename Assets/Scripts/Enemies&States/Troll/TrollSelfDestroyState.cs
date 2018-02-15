using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSelfDestroyState : MonoBehaviour, ITrollState
{
    private Troll enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit = true;
    bool preattack = false;
    float timer;
    float delay = 0.1f;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        enemy.armature.armature.animation.timeScale = 1.5f;
        
        enemy.armature.animation.FadeIn("blow", -1, 2);
    }

    public void Execute()
    {
        if (enemy.armature.animation.isCompleted)
        {
            GameObject tmp = Instantiate(enemy.blow, enemy.transform.position, Quaternion.identity);
            tmp.transform.parent = null;

            enemy.OnBlow();
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    { }

}
