using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{
<<<<<<< HEAD
    public string nextLvl;
=======
	[SerializeField]
	public Transform position;

	[SerializeField]
	private GameObject light;
>>>>>>> origin/DevG

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
        if (other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Sword"))
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
        GameManager.levelName = nextLvl;
        SceneManager.LoadScene("Loading");
    }
}
