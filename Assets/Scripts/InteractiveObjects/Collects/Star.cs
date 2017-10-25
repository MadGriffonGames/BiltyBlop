using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : InteractiveObject
{
	[SerializeField]
	private GameObject crystalParticle;
    bool collect = false;

	public override void Start ()
	{
		base.Start();
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword") && !collect)
		{
            collect = true;
            MyAnimator.SetTrigger("collected");
            Player.Instance.stars++;
        }   
	}

	public void MakeFX()
	{
		Instantiate(crystalParticle, this.gameObject.transform.position + new Vector3(0, 0.2f, 1), Quaternion.Euler(new Vector3 (0, 0 , 0)));
	}

    public void ShowStar()
    {
        StarsUI.Instance.ShowStar(Player.Instance.stars);
    }

    public void PlaySound()
    {
        SoundManager.PlaySound("star_collect");
    }

	public void DestroyObject()
	{
        ShowStar();
		Destroy(this.gameObject);
	}
}
