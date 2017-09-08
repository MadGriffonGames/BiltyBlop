using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    Vector3 target;

    void FixedUpdate ()
    {
        target = new Vector3(Player.Instance.transform.position.x - 1.24f * Mathf.Sign(Player.Instance.transform.localScale.x),
                             Player.Instance.transform.position.y + 1.57f, -5);
  
        if (Mathf.Sign(Player.Instance.transform.localScale.x) != Mathf.Sign(transform.localScale.x))
        {
            Vector2 tmp = transform.localScale;
            tmp.x *= -1;
            transform.localScale = tmp;
        }

        transform.position = Vector3.Slerp(transform.position, target, 0.087f);

    }
}
