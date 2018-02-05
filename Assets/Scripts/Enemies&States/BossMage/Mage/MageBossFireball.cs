using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossFireball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float cos;
    float acos;
    float angle;

    float scalar;
    float module;

    float timer = 0;
    float lifeTime = 5f;

    public bool visible = false;
    bool isScaled = false;
    bool faded = false;

    Renderer myRenderer;

    Vector3 scaling = new Vector3(0.01f, 0.01f, 0);
    Vector3 scalingReverse = new Vector3(-0.01f, 0.01f, 0);

    [SerializeField]
    public GameObject startPosition;

    Transform startTransform;

    Vector2 myVector = new Vector2(1, 0);
    Vector2 targetVector;

    Transform parent;
    int dir = 1;

    private void Awake()
    {
        parent = transform.parent;
    }

    private void FixedUpdate()
    {
        targetVector = Player.Instance.transform.position - transform.position;
        scalar = targetVector.x * myVector.x + targetVector.y * myVector.y;
        module = Mathf.Sqrt(Mathf.Pow(targetVector.x, 2) + Mathf.Pow(targetVector.y, 2)) * Mathf.Sqrt(Mathf.Pow(myVector.x, 2) + Mathf.Pow(myVector.y, 2));
        cos = scalar / module;
        acos = Mathf.Acos(cos);
        float z = acos * Mathf.Rad2Deg * Mathf.Sign(targetVector.y - myVector.y);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            FadeOut();
        }
    }

    private void Update()
    {
        this.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
        if (!visible)
        {
            FadeIn();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            startTransform = startPosition.GetComponent<Transform>();
            this.transform.position = startTransform.transform.position;
            transform.parent = parent;
            Player.Instance.InvertControls();

            SoundManager.PlaySound("mage_fireball_destroy");
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            startTransform = startPosition.GetComponent<Transform>();
            this.transform.position = startTransform.transform.position;
            transform.parent = parent;
            Player.Instance.InvertControls();
            SoundManager.PlaySound("mage_fireball_destroy");
        }
    }

    public void ThrowFireball()
    {
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        dir = parent.transform.localScale.x > 0 ? 1 : -1;
        transform.parent = null;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.transform.localScale = new Vector2(0.1f, 0.1f);
        visible = false;
        isScaled = false;
    }

    void FadeIn()
    {
        if (!visible)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);
            if (!isScaled)
            {
                Vector3 scl = dir > 0 ? scaling : scalingReverse;
                this.gameObject.transform.localScale += scaling;
            }
            if (this.gameObject.transform.localScale.x >= 0.5f)
            {
                isScaled = true;
            }
        }
        if (this.gameObject.GetComponent<SpriteRenderer>().color.a >= 1)
            visible = true;
    }

    public void FadeOut()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.2f);
        if (isScaled)
        {
            Vector3 scl = parent.localScale.x > 0 ? scaling : scalingReverse;
            this.gameObject.transform.localScale -= scaling;
        }
        if (Mathf.Abs(this.gameObject.transform.localScale.x) <= 0.02f)
        {
            isScaled = false;
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("mage_fireball_destroy");
        }

        if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0.02f)
            visible = false;

        if (!visible)
        {
            timer = 0;
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("mage_fireball_destroy");
            startTransform = startPosition.GetComponent<Transform>();
            this.transform.position = startTransform.transform.position;
            this.gameObject.transform.localScale = new Vector2(0.5f * dir, 0.5f);
            transform.parent = parent;
        }
    }

    void CorrectScale()
    {
        Vector3 tmp = transform.localScale;
        tmp.x *= dir;
        transform.localScale = tmp;
    }
}
