using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InteractiveObject
{
    [SerializeField]
    private HP hp;

    [SerializeField]
    private Coin coin;

    [SerializeField]
    public GameObject chips;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword") || other.transform.CompareTag("Throwing"))
        {
            SpawnObject();
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(chips, this.gameObject.transform.position + new Vector3(0, 0.5f , -3), Quaternion.identity);
			SoundManager.PlaySound ("wooden_box1");
            Destroy(this.gameObject);
        }
    }

    public void SpawnObject()
    {
        if (UnityEngine.Random.Range(1, 100) <= 80)//spawn or not
        {
            if (Player.Instance.Health == 1)
            {
                if (UnityEngine.Random.Range(1, 100) <= 50)
                    Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                else
                    Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
            }
            if (Player.Instance.Health == 2)
            {
                if (UnityEngine.Random.Range(1, 100) <= 70)
                    Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                else
                    Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
            }
            if (Player.Instance.Health >= 3)
            {
                if (UnityEngine.Random.Range(1, 100) <= 75)
                    Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                else
                    Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
