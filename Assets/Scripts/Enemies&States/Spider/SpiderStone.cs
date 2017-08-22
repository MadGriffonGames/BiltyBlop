using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStone : MonoBehaviour {


    /*[SerializeField]
    public GameObject particle;*/
    string soundName;
    int rand;


    private Rigidbody2D myRigidbody;

    private Vector2 direction;
    [SerializeField]
    GameObject particle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("DestroyGround"))
        {
            rand = UnityEngine.Random.Range(0, 2);
            Debug.Log(rand);
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
            if (rand == 1)
                SoundManager.PlaySound("stoned3");
            else
                SoundManager.PlaySound("stoned4");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rand = UnityEngine.Random.Range(0, 2);
            if (rand == 1)
                SoundManager.PlaySound("stoned3");
            else
                SoundManager.PlaySound("stoned4");
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void Throw(Vector3 startPos)
    {
        this.transform.position = startPos;
        gameObject.SetActive(true);
    }
}
