using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{
    public string nextLvl;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Sword"))
        {
            animator.SetTrigger("shine");
        }
    }

    public void ChangeScene()
    {
        GameManager.levelName = nextLvl;
        SceneManager.LoadScene("Loading");
    }
}
