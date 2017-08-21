using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

class SpiderGroundAttackState : MonoBehaviour, ISpiderState
{
    private Spider enemy;
    float timer;
    float platformTime = 4.6f;
    bool groundAttackedTwice;
    bool platformed;
    int i;
    GameObject tmpParticle;
    GameObject tmpCloudParticle;
    bool isAttacked1;
    bool isAttacked2;
    bool isAttacked3;
    bool isThrew1;
    bool isThrew2;
    bool isThrew3;

    public void Enter(Spider enemy)
    {
        enemy.armature.sortingOrder = 1;
        enemy.enemyDamageStone.enabled = true;
        i = 0;
        timer = Time.time;
        this.enemy = enemy;
        groundAttackedTwice = false;
        platformed = false;
        isAttacked1 = false;
        isAttacked2 = false;
        isAttacked3 = false;
        isThrew1 = false;
        isThrew2 = false;
        isThrew3 = false;
    }

    public void Execute()
    {
        if (!isAttacked1)
        {
            tmpCloudParticle = (GameObject)Instantiate(enemy.cloudParticle, enemy.transform.position - new Vector3(-5.5f, 0), Quaternion.identity);
            //tmpParticle = (GameObject)Instantiate(enemy.rollGroundParticles, enemy.transform.position + new Vector3(-5.5f, 0), Quaternion.identity, enemy.gameObject.transform);
            enemy.shadow.gameObject.SetActive(true);
            isAttacked1 = true;
            enemy.armature.animation.FadeIn("ground_attack", -1, 1);
            CameraEffect.Shake(0.2f, 0.2f);
            enemy.WallFall(i);
            i++;
        }

        if (isAttacked1 && enemy.armature.animation.lastAnimationName == "ground_attack" && enemy.armature.animation.isCompleted && !isThrew1 && !isThrew2 && !isThrew3 && !isAttacked2 && !isAttacked3)
        {
            enemy.armature.animation.FadeIn("wall_hit", -1, 1);
            isThrew1 = true;
        }

        if (isAttacked1 && enemy.armature.animation.lastAnimationName == "wall_hit" && enemy.armature.animation.isCompleted && !isAttacked2 && !isAttacked3 && isThrew1 && !isThrew2 && !isThrew3)
        {
            isAttacked2 = true;
            enemy.armature.animation.FadeIn("ground_attack", -1, 1);
            CameraEffect.Shake(0.2f, 0.2f);
            enemy.WallFall(i);
            i++;
        }

        if (isAttacked1 && isAttacked2 && enemy.armature.animation.lastAnimationName == "ground_attack" && enemy.armature.animation.isCompleted && isThrew1 && !isThrew2 && !isThrew3 &&!isAttacked3)
        {
            enemy.armature.animation.FadeIn("wall_hit", -1, 1);
            isThrew2 = true;
        }

        if (isAttacked1 && isAttacked2 && !isAttacked3 && enemy.armature.animation.lastAnimationName == "wall_hit" && enemy.armature.animation.isCompleted && isThrew1 && isThrew2 && !isThrew3)
        {
            isAttacked3 = true;
            enemy.armature.animation.FadeIn("ground_attack", -1, 1);
            CameraEffect.Shake(0.2f, 0.2f);
            enemy.WallFall(i);
        }

        if (isAttacked1 && isAttacked2 && enemy.armature.animation.lastAnimationName == "ground_attack" && enemy.armature.animation.isCompleted && isThrew1 && isThrew2 && !isThrew3 && isAttacked3)
        {
            enemy.armature.animation.FadeIn("wall_hit", -1, 1);
            isThrew3 = true;
        }

        if (Time.time - timer > platformTime && !groundAttackedTwice && enemy.armature.animation.lastAnimationName == "wall_hit" && enemy.armature.animation.isCompleted && isAttacked2 && isAttacked3 && isThrew3)
        {
            groundAttackedTwice = true;
            enemy.armature.animation.FadeIn("ground_attack", -1, 1);
        }

        if (enemy.armature.animation.lastAnimationName == "ground_attack" && isAttacked1 && enemy.armature.animation.isCompleted && !platformed && groundAttackedTwice)
        {
            platformed = true;
            enemy.SpawnPlatforms();
        }

        if (platformed)
        {
            enemy.ChangeState(new SpiderShopDaWoopState());
        }
    }

    public void Exit()
    {
        enemy.enemyDamageStone.enabled = false;
        //Destroy(tmpParticle);
        Destroy(tmpCloudParticle);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
} 

