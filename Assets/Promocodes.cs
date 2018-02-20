using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

    public class Promocodes : MonoBehaviour
{
    public const int CODE_LENGTH = 10;
    public const int CODES_COUNT = 10;

    public const string skin1 = "YJHXOXVRXV";
    public const string skin2 = "UVCTQSMHLI";
    public const string skin3 = "MCKGKVAZUG";
    public const string skin4 = "QIRWZSOOES";
    public const string skin5 = "TOYMOPBEOF";
    public const string skin6 = "MVGAISQVXC";
    public const string skin7 = "CFWJXUKRMX";
    public const string skin8 = "UMEWSXZJVU";
    public const string skin9 = "YSLMHUNYFG";
    public const string skin10 = "QZTABYBQOE";
    public string promocodeGift;
    

    private string[] promocodes;


    private static Promocodes instance;
    public static Promocodes Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Promocodes>();
            return instance;
        }
    }

    public Promocodes(int count)
    {
        promocodes = new string[10];
    }

    //public bool IsCorrectCode(string data)
    //{
    //    bool result = false;
    //    for (int i = 0; i < promocodes.Length; i++)
    //    {
    //        if (promocodes[i] == data)
    //        {
    //            ActivateCode(data);
    //        }

    //        result = true;
    //        return result;
    //    }
    //    return result;
    //}

    public bool IsActivateCode(string data)
    {
        switch (data)
        {
            case skin1:
                promocodeGift = "skin1";
                return true;

            case skin2:
                promocodeGift = "skin2";
                return true;

            case skin3:
                promocodeGift = "skin3";
                return true;

            case skin4:
                promocodeGift = "skin4";
                return true;

            case skin5:
                promocodeGift = "skin5";
                return true;

            case skin6:
                promocodeGift = "skin6";
                return true;

            case skin7:
                promocodeGift = "skin1";
                return true;

            case skin8:
                promocodeGift = "skin1";
                return true;

            case skin9:
                promocodeGift = "skin1";
                return true;

            case skin10:
                promocodeGift = "skin1";
                return true;
            case "W":
                promocodeGift = "skin1";
                return true;
            default:
                {
                    return false;
                }
        }
    }

    public string RandomString(int length)
    {
        StringBuilder builder = new StringBuilder();
        System.Random random = new System.Random();
        char ch;
        for (int i = 0; i < length; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        return builder.ToString();
    }

    public void CreateCodes(int CODES_COUNT)
    {
        for (int i = 0; i < CODES_COUNT; i++)
        {
            promocodes[i] = RandomString(CODE_LENGTH);
            using (StreamWriter sw = File.AppendText(@"C:\Users\Code\Desktop\Codes.txt"))
                sw.WriteLine(promocodes[i]);
        }
    }
}

