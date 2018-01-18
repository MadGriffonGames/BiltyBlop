using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DragonFlyAway : MonoBehaviour {

    public UnityArmatureComponent armature;
    bool isUp = false;

    private void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    // Use this for initialization
    void Start () {
        armature.animation.FadeIn("WEAKNESS_END", -1, 1);
    }
	
	// Update is called once per frame
	void Update () {
		if (armature.animation.lastAnimationName == "WEAKNESS_END" && armature.animation.isCompleted)
        {
            armature.animation.FadeIn("RISE", -1, 1);
        }

        if (armature.animation.lastAnimationName == "RISE" && armature.animation.isCompleted)
        {
            isUp = true;
            armature.animation.FadeIn("FLY", -1, -1);
        }

        if (isUp)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-13 * transform.localPosition.x / 1.6f, 1.3f * transform.localPosition.y);
        }
	}

    public void PlayAnimation(string name)
    {
        armature.animation.Play(name);
    }

    private void OnBecameInvisible()
    {
        Destroy(this);
        this.gameObject.SetActive(false);
    }
}
