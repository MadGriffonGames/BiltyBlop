using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	private Vector3 posA;
    private Vector3 nextPos;

    private Vector3 posB;

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
	}

	void Update ()
    {
        Move();
	}

    private void Move()
    {
        platformTransform.localPosition = Vector3.MoveTowards(platformTransform.localPosition, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(platformTransform.localPosition, nextPos) <= 0)
        {
            ChangePoint();
        }
    }

    private void ChangePoint()
    {
        nextPos = nextPos != posA ? posA : posB;//nextPos = posA or posB
    }
}
