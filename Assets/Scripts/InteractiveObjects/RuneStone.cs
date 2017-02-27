using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{
    public string nextLvl;

	private Transform position;

	[SerializeField]
	private GameObject lightParticle;

    public override void Start()
    {
        base.Start();
		position = GetComponent<Transform>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Sword"))
        {
            animator.SetTrigger("shine");
        }
    }
	public void MakeFX()
	{
		Instantiate(lightParticle, this.gameObject.transform.position + new Vector3(0, -0.5f, 1), Quaternion.Euler(new Vector3 (-90, 0 , 0)));
	}

    public void ChangeScene()
    {
        GameManager.levelName = nextLvl;
        LevelSelect.use.SaveScene(GameManager.CollectedCoins, "0");
        SceneManager.LoadScene("Loading");
    }
}
