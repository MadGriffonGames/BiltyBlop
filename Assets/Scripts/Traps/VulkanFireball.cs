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
            Blow();
        }
    }

    public void Initialize(Vector2 dir, float lifeTime, float _speed)
    {
        direction = dir;
        timeToLife = lifeTime;
        speed = _speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("troll_bomb");
            Blow();
        }
    }

    void Blow()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        if (transform.rotation.eulerAngles.z <= 270)
        {
            offset = new Vector3(0.4f, 0, 0);
        }

        Instantiate(blow, this.gameObject.transform.position + offset, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
