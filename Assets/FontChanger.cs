//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEditor.SceneManagement;
//using UnityEngine;

//public class FontChanger : MonoBehaviour
//{
//    Text[] tmp;
//    Font font;

//    [ContextMenu("ChangeFonts")]
//    private void ChangeFonts()
//    {
//        font = (Font)Resources.Load("Gilroy-Black");

//        tmp = FindObjectsOfType<Text>();
//        for (int i = 0; i < tmp.Length; i++)
//        {
//            tmp[i].font = font;
//        }
//    }
//}
