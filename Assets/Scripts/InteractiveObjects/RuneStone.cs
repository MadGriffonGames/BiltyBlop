using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{
	[SerializeField]
	public Transform position;

	[SerializeField]
	private GameObject light;

    public override void Start()
    {
        base.Start();
		position = GetComponent<Transform>();
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
	public void MakeFX()
	{
		Instantiate(light, position.localPosition + new Vector3(0, -0.5f, 1), Quaternion.Euler(new Vector3 (-90, 0 , 0)));
	}

    public void ChangeScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
