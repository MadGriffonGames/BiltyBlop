//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GooglePlayGames;

//public class GooglePlayGamesManager : MonoBehaviour
//{
//    private static GooglePlayGamesManager instance;
//    public static GooglePlayGamesManager Instance
//    {
//        get
//        {
//            return instance;
//        }
//    }

//    bool isConnected;

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(this.gameObject);
//        }

//        instance = GetComponent<GooglePlayGamesManager>();
//        DontDestroyOnLoad(this);
//    }

//    private void Start()
//    {
//        PlayGamesPlatform.Activate();
//        Social.localUser.Authenticate((bool success) =>
//        {
//            if (success)
//            {
//                // if we signed in successfully, load data from cloud
//                Debug.LogError("Login successful!");
//            }
//            else
//            {
//                // no need to show error message (error messages are shown automatically
//                // by plugin)
//                Debug.LogError("Failed to sign in with Google Play Games.");
//            }
//        });
//    }
//}
