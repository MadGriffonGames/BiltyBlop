using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiderStonesState : MonoBehaviour, ISpiderState
{

    private Spider enemy;
    float timer;
    float pauseTime = 0.4f;
    bool lastStoneDestroyed;
    bool stoneDown;
    int i = 0;
    int delta = 5;
    int random;
    GameObject tmpParticle;
    GameObject tmpCloudParticle;

    public void Enter(Spider enemy)
    {
        enemy.enemyDamageStone.enabled = true;
        stoneDown = false;
        lastStoneDestroyed = false;
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.armature.animation.FadeIn("Stone_down_preatk", 1, 1);
    }

    public void Execute()
    {
        if (enemy.armature.animation.lastAnimationName == "Stone_down_preatk" && enemy.armature.animation.isCompleted)
        {
            tmpCloudParticle = (GameObject)Instantiate(enemy.cloudParticle, enemy.transform.position - new Vector3(3f, 1), Quaternion.identity);
            enemy.armature.sortingOrder = 1;
            stoneDown = true;
            timer = Time.time;
            enemy.armature.animation.FadeIn("stone_down", -1, -1);

        }

        if (!lastStoneDestroyed && stoneDown)
        {
            if (i == 19)
            {
                lastStoneDestroyed = true;
            }

            if (Time.time - timer > pauseTime)
            {
                SpawnStones(i);
                i++;
                timer = Time.time;
            }
        }

        if (lastStoneDestroyed)
            enemy.ChangeState(new SpiderUndergroundState());
    }

    public void Exit()
    {
        enemy.enemyDamageStone.enabled = false;
    }

    void SpawnStones(int i)
    {
        Vector3 tmp = new Vector3(Player.Instance.gameObject.transform.position.x, Player.Instance.gameObject.transform.position.y, Player.Instance.gameObject.transform.position.z);
        delta = UnityEngine.Random.Range(-6, 6);
        random = UnityEngine.Random.Range(0, 100);
        if (random < 70)
        {
            delta = 0;
        }
            enemy.spiderStones[i].transform.position = tmp + new Vector3(delta, 13);
            enemy.spiderStones[i].SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}