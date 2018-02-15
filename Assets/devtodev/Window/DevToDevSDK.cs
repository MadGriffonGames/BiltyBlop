using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevToDev;
using System;
using System.Linq;
#if UNITY_METRO && !UNITY_EDITOR
using System.Reflection;
#endif

namespace DTDEditor {
    public class DevToDevSDK : MonoBehaviour {
        public bool IsAnaluticsEnabled;
        public bool IsPushMessagesEnabled;
        public bool IsLogEnabled;
        public DTDCredentials[] Credentials;
        public MonoBehaviour PushListeners;
        public string OnTokenReceived;
        public string OnTokenFailed;
        public string OnPushReceived;
        public string OnPushOpened;

        void Awake() {
            DontDestroyOnLoad(this);
        }
        void Start() {
            if (IsLogEnabled) {
                Analytics.SetActiveLog(true);
            }

#if UNITY_ANDROID
			InitializeAnalytics(DTDPlatform.Android);
#elif UNITY_IOS
            InitializeAnalytics(DTDPlatform.iOS);
#elif UNITY_METRO
			InitializeAnalytics(DTDPlatform.WinStore);
#elif UNITY_WEBPLAYER || UNITY_WEBGL
			InitializeAnalytics(DTDPlatform.WebGL);
#elif UNITY_STANDALONE_OSX
			InitializeAnalytics(DTDPlatform.MacOS);
#elif UNITY_STANDALONE_WIN
			InitializeAnalytics(DTDPlatform.Windows);
#else
			return;
#endif

            if (IsPushMessagesEnabled) {
                PushManager.PushTokenReceived = (token) => ExecuteDeveloperDelegate(OnTokenReceived, token);
                PushManager.PushTokenFailed = (error) => ExecuteDeveloperDelegate(OnTokenFailed, error);
                PushManager.PushReceived = (pushData) => ExecuteDeveloperDelegate(OnPushReceived, pushData);
                PushManager.PushOpened = (pushMessage, actionButton) => ExecuteDeveloperDelegate(OnPushOpened, pushMessage, actionButton);
                PushManager.PushNotificationsEnabled = true;
            }
        }

        private void InitializeAnalytics(DTDPlatform platform) {
            var targetCredential = Credentials.FirstOrDefault(item => item.Platform == platform);
            if (targetCredential != null) {
                Analytics.Initialize(targetCredential.Key, targetCredential.Secret);
            }
        }

        private void ExecuteDeveloperDelegate(string methodName, params object[] parameters) {
            if (PushListeners != null && !string.IsNullOrEmpty(methodName)) {
                var targetMethod = PushListeners.GetType().GetMethod(methodName);
                if (targetMethod != null) {
                    targetMethod.Invoke(PushListeners, parameters);
                }
            }
        }
    }
}