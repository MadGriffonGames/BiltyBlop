using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	private Vector3 posA;
    private Vector3 nextPos;
    private Vector3 posB;
    Rigidbody2D MyRigidbody;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform platformTransform;

    [SerializeField]
    private Transform transformPosB;

	void Start ()
    {
        posA = platformTransform.localPosition;
        posB = transformPosB.localPosition;
        nextPos = posB;
        MyRigidbody = GetComponent<Rigidbody2D>();
	}

	void Update ()
    {
        Move();
	}

    private void Move()
    {
        platformTransform.localPosition = Vector3.MoveTowards(platformTransform.localPosition, nextPos, speed*Time.deltaTime);
        if (Vector3.Distance(platformTransform.localPosition, nextPos) <= 0)
        {
            ChangePoint();
        }
    }

    private void ChangePoint()
    {
        nextPos = nextPos != posA ? posA : posB;//nextPos = posA or posB
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 9;//9 - is layer called "Platform"
            other.transform.SetParent(platformTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
