﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFireball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float cos;
    float acos;
    float angle;

    float scalar;
    float module;

    float timer = 0;
    public float lifeTime; 

    public bool visible = false;
    bool isScaled = false;
    bool faded = false;

    Renderer myRenderer;

    Vector3 scaling = new Vector3(0.01f, 0.01f, 0);

    [SerializeField]
    public Vector3 startPosition;

    Vector2 myVector = new Vector2(1, 0);
    Vector2 targetVector;

    Transform parent;

    [SerializeField]
    GameObject blow;

    public bool isBlowed;

    private void Awake()
    {
        parent = transform.parent;
        startPosition = transform.localPosition;
        isBlowed = false;
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.localScale = new Vector2(-0.8f, 0.8f);

        targetVector = Player.Instance.transform.position - transform.position;
        scalar = targetVector.x * myVector.x + targetVector.y * myVector.y;
        module = Mathf.Sqrt(Mathf.Pow(targetVector.x, 2) + Mathf.Pow(targetVector.y, 2)) * Mathf.Sqrt(Mathf.Pow(myVector.x, 2) + Mathf.Pow(myVector.y, 2));
        cos = scalar / module;
        acos = Mathf.Acos(cos);
        float z = acos * Mathf.Rad2Deg * Mathf.Sign(targetVector.y - myVector.y);
        transform.rotation = Quaternion.Euler(-transform.rotation.x, transform.rotation.y, z);
        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            FadeOut();
        }
    }

    private void Update()
    {
        if (this.gameObject.transform.localScale == new Vector3(0.3f, 0.3f))
            this.gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
        if (!visible)
        {
            FadeIn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Blow();

            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Blow();

            this.gameObject.SetActive(false);
        }
    }

    public void ThrowFireball()
    {
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        isBlowed = false;

        transform.parent = null;

        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        visible = false;
        isScaled = false;
    }

    private void OnDisable()
    {
        timer = 0;
    }

    void FadeIn()
    {
        if (!visible)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);
            if (!isScaled)
                this.gameObject.transform.localScale += scaling;
            if (this.gameObject.transform.localScale.x >= 0.5f)
            {
                isScaled = true;
                this.gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
            }
        }
        if (this.gameObject.GetComponent<SpriteRenderer>().color.a >= 1)
            visible = true;
    }

    public void FadeOut()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.2f);
        if (isScaled)
            this.gameObject.transform.localScale -= scaling;

        if (this.gameObject.transform.localScale.x <= 0.02f)
        {
            isScaled = false;
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("mage_fireball_destroy");
        }

        if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0.02f)
            visible = false;

        Blow();

        if (!visible)
        {
            this.gameObject.SetActive(false);

            SoundManager.PlaySound("mage_fireball_destroy");
        }
    }

    void Blow()
    {
        isBlowed = true;

        GameObject tmp = Instantiate(blow, this.gameObject.transform.position, Quaternion.identity);
        tmp.transform.localScale = new Vector2(0.95f, 0.95f);

        transform.parent = parent;
        transform.localPosition = startPosition;

        timer = 0;
    }
}
