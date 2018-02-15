using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShield : MonoBehaviour
{
    [SerializeField]
    ShieldSkeleton skeleton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (skeleton.armature.armature.animation.lastAnimationName == "hit")
            {

            }
        }

    }
}
