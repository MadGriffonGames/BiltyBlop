using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowballTest : MonoBehaviour
{

    public Vector3 startPosition;

   // [SerializeField]
   // public GameObject particle;

    private Vector2 direction;
    float xPower;
    float yPower;
    float increment;
    bool xPressed;
    bool yPressed;
    bool threw;
    //----------------------------------------

    private static SnowballTest instance;
    public static SnowballTest Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SnowballTest>();
            return instance;
        }
    }




    private void Start()
    {
        //Throw(transform.position, new Vector2(60, 15));
        xPower = 0;
        yPower = 0;
        increment = 0.5f;
        xPressed = false;
        yPressed = false;
        threw = false;
    }

    private void FixedUpdate()
    {
        if (!xPressed)
        {
            Debug.Log("x incremented");
            xPower += increment;
            if (xPower >= 40)
                increment = -0.5f;
            if (xPower <= 0)
                increment = 0.5f;
        }

        if (xPressed && !yPressed)
        {
            Debug.Log("y incremented");
            yPower += increment;
            if (yPower >= 25)
                increment = -0.5f;
            if (yPower <= 0)
                increment = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            xPressed = true;
            Debug.Log(xPower);
        }

        if (Input.GetKeyDown(KeyCode.G) && xPressed)
        {
            yPressed = true;
            Debug.Log(yPower);
        }

        if (xPressed && yPressed && !threw)
        {
            Throw(this.transform.position, new Vector2(xPower, yPower));
            Debug.Log("threw");
            threw = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            this.GetComponent<Rigidbody2D>().velocity += new Vector2(2, 8);
        }
    }
    //----------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GroundYeti"))
        {
            //Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("stone_crash");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("stone_crash");
           // Instantiate(particle, this.gameObject.transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void Throw(Vector3 startPos, Vector2 power)
    {
        this.transform.position = startPos;
        gameObject.SetActive(true);
        this.GetComponent<Rigidbody2D>().velocity += power;
    }
}
