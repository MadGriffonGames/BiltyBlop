using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEdge : MonoBehaviour
{

    /*  private LineRenderer lineR;
      private int i;
      private bool isDrawing;
      // Use this for initialization
      void Start () {
          i = 0;
          lineR = gameObject.GetComponent<LineRenderer>();
          lineR.material = new Material(Shader.Find("Mobile/Particles/Additive"));
          lineR.startColor = new Color(255, 255, 255);
          lineR.startWidth = 0f;
          lineR.widthMultiplier = 1.2f;
          lineR.endWidth = 0f;
          lineR.numPositions = i;
          isDrawing = false;
      }

      void Update () {

         if (isDrawing)
         {

              lineR.numPositions = i+1; 
              lineR.SetPosition(i, this.gameObject.transform.position);
              i++;

          }
      }
     */
    private void Start()
    {

    }
    public void StartDraw(float drawDur, float freezeDur)
    {
        GameObject child = new GameObject("SwordSlash");
        child.transform.parent = this.gameObject.transform;
        child.transform.localPosition = new Vector3(0f, 0f, -5f);
        SwordSlash ssl = child.AddComponent<SwordSlash>();

        StartCoroutine( ssl.StartDraw(drawDur, freezeDur));
    }

}
