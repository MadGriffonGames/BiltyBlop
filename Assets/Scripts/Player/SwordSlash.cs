using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SwordSlash : MonoBehaviour
{
    private LineRenderer lineR;
    private int i;
    private bool isDrawing;
    private void Start()
    {
        i = 0;
        lineR = this.gameObject.AddComponent<LineRenderer>();
        lineR.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineR.startColor = new Color(255, 255, 255);
        lineR.receiveShadows = false;
        lineR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Keyframe a = new Keyframe(0f, 0f, 0f, 0f);
        Keyframe b = new Keyframe(0.5f, 0.5f, 0f, 0f);
        Keyframe c = new Keyframe(1f, 0f, 0f, 0f);
        lineR.widthCurve = new AnimationCurve(new Keyframe[] { a, b, c });
       // lineR.widthMultiplier = 1.2f;
        lineR.numPositions = i;
        isDrawing = true;
    }


    void Update()
    {

        if (isDrawing)
        {

            lineR.numPositions = i + 1;
            lineR.SetPosition(i, this.gameObject.transform.position);
            i++;

        }
    }
    public IEnumerator StartDraw(float drawDur, float freezeDur)
    {
        isDrawing = true;
        yield return new WaitForSeconds(drawDur);
        isDrawing = false;
        yield return new WaitForSeconds(freezeDur);
        Destroy();
    }
    private void Destroy()
    {
        DestroyObject(lineR);
        DestroyObject(this.gameObject);
    }

}
