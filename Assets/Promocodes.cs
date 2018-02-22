using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class Promocodes : MonoBehaviour
{
    [SerializeField]
    PromocodeWindow promoceodesUi;

    public const int CODE_LENGTH = 10;
    public const int CODES_COUNT = 10;

    public const string BETA_TESTERS_CODE = "YJHXOXVRXV";
    public const string YOUTUBERS_CODE = "UVCTQSMHLI";
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

    public bool IsActivateCode(string data)
    {
        switch (data)
        {
            case BETA_TESTERS_CODE:
                PlayerPrefs.SetInt(BETA_TESTERS_CODE, 0);
                if (PlayerPrefs.GetInt(BETA_TESTERS_CODE) == 0)
                {
                    PlayerPrefs.SetInt(BETA_TESTERS_CODE, 1);

                    for (int i = 1; i <= 21; i++)
                    {
                        PlayerPrefs.SetInt("Level" + i.ToString(), 1);
                    }
                    GameManager.AddCoins(1000);
                    promoceodesUi.giftDescription.text = "thank you for your participation in the beta test! You unlocked episodes 1 and 2 and get 1000 gold";
                    return true;
                }
                else
                    return false;

            case YOUTUBERS_CODE:
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
                return false;
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

