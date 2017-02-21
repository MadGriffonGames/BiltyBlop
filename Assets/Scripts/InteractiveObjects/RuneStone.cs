using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{

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
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "SwordCollider")
        {
            animator.SetTrigger("shine");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
