using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : InteractiveObject
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
        if (other.transform.CompareTag("Sword"))
        {
            SpawnObject();
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(chips, this.gameObject.transform.position + new Vector3(0, 0.5f , 0), Quaternion.identity);
			SoundManager.PlaySound ("wooden_box");
            Destroy(this.gameObject);
        }
    }

    public void SpawnObject()
    {
        if (UnityEngine.Random.Range(1, 100) <= 65)//spawn or not
        {
            switch (Player.Instance.Health)
            {
                case 1:
                    if (UnityEngine.Random.Range(1, 100) <= 75)
                        Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                    else
                        Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
                    break;
                case 2:
                    if (UnityEngine.Random.Range(1, 100) <= 85)
                        Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                    else
                        Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
                    break;
                case 3:
                    if (UnityEngine.Random.Range(1, 100) <= 95)
                        Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                    else
                        Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}
