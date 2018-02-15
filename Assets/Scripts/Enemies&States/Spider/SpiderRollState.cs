using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpiderRollState : MonoBehaviour, ISpiderState
{
    private Spider enemy;
    float timer;
    float changingTime = 1.92f;
    float timer2;
    bool isUkused = false;
    float colliderTimer;
    float colliderEnable = 1f;
    bool isColliderEnabled;
    GameObject tmpParticle;


    public void Enter(Spider enemy)
    {
        SoundManager.PlaySound("earthroll");
        enemy.shadow.gameObject.SetActive(false);
        enemy.armature.sortingOrder = 0;
        enemy.enemyDamageRoll.enabled = true;
        timer = Time.time;
        timer2 = timer + timer;
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.rightEdge.gameObject.SetActive(false);
        isColliderEnabled = false;
        colliderTimer = Time.time;
        enemy.transform.position += new Vector3(14f, 0);
        enemy.armature.animation.FadeIn("roll", -1, 1);
        tmpParticle= (GameObject)Instantiate(enemy.rollGroundParticles, enemy.transform.position + new Vector3(-5.5f, 0), Quaternion.identity, enemy.gameObject.transform);
    }

    public void Execute()
    {
        if (Time.time - colliderTimer > colliderEnable && !isColliderEnabled)
        {
            enemy.rightEdge.gameObject.SetActive(true);
            isColliderEnabled = true;
        }
        if (enemy.armature.animation.lastAnimationName == "roll" && enemy.armature.animation.isCompleted)
            enemy.armature.animation.FadeIn("roll", -1, 1);
       enemy.Move();
    }

    public void Exit()
    {
        enemy.enemyDamageRoll.enabled = false;
        Destroy(tmpParticle);
    }



    public void OnCollisionEnter2D (Collision2D other)
    {
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
