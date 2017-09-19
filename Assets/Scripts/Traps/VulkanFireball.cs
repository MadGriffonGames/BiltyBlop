using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulkanFireball : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    float timeToLife;

    [SerializeField]
    public GameObject blow;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    float timer;

    SpriteRenderer mySpriteRenderer;

    void Start()
    {
        timer = 0;

        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        mySpriteRenderer.color = new Color(255, 255, 255, 0);
    }

    private void FixedUpdate()
    {
        mySpriteRenderer.color += new Color(0, 0, 0, 2);

        myRigidbody.velocity = direction * speed;

        timer += Time.fixedDeltaTime;

        if (timer >= timeToLife)
        {
            Instantiate(blow, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void Initialize(Vector2 dir)
    {
        this.direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(blow, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(blow, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
