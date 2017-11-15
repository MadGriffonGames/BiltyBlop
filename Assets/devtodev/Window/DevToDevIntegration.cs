using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DevToDev;
using System;
using System.Text;
#if UNITY_METRO && !UNITY_EDITOR
using System.Reflection;
#endif
#pragma warning disable 414

namespace com.devtodev {
	
	public class DevToDevIntegration : MonoBehaviour  {

		public MonoBehaviour pushListeners;
		public string onTokenReceived;
		public string onTokenFailed;
		public string onPushReceived;
		public string onPushOpened;

		private string iosKey = "";
		private string iosSecret = "";
		
		private string androidKey = "6d78756b-b818-0cdc-a65e-7bcd334318da";
		private string androidSecret = "cpNbYkLP6ZhJlEgBUCeARF45zTHSXVsr";
		
		private string macKey = "";
		private string macSecret = "";
		
		private string winKey = "";
		private string winSecret = "";
		
		private string wsKey = "";
		private string wsSecret = "";
		
		private string webKey = "";
		private string webSecret = "";
		
		private bool pushEnabled = true;
		private bool logEnabled = false;

		void Awake() {
			DontDestroyOnLoad(this);
		}

		void Start() {
			if (logEnabled) {
				Analytics.SetActiveLog(true);
			}
#if UNITY_ANDROID
			if (androidKey == null || androidSecret == null) return;
	        Analytics.Initialize(androidKey, androidSecret);
#elif UNITY_IOS
			if (iosKey == null || iosSecret == null) return;
	        Analytics.Initialize(iosKey, iosSecret);
#elif UNITY_METRO
			if (wsKey == null || wsSecret == null) return;
	        Analytics.Initialize(wsKey, wsSecret);
#elif UNITY_WEBPLAYER || UNITY_WEBGL
			if (webKey == null || webSecret == null) return;
	        Analytics.Initialize(webKey, webSecret);
#elif UNITY_STANDALONE_OSX
			if (macKey == null || macSecret == null) return;
	        Analytics.Initialize(macKey, macSecret);
#elif UNITY_STANDALONE_WIN
			if (winKey == null || winSecret == null) return;
		    Analytics.Initialize(winKey, winSecret);
#else 
			return;
#endif
			if (pushEnabled) {
		      PushManager.PushReceived = delegate(IDictionary<string, string> pushAdditionalData) {
		            if (pushListeners != null && onPushReceived != null) {
#if !UNITY_METRO || UNITY_EDITOR
                        pushListeners.GetType().GetMethod(onPushReceived).Invoke(pushListeners, new object[1] { pushAdditionalData });
#else
                        pushListeners.GetType().GetRuntimeMethod(onPushReceived, new Type[] { typeof(IDictionary<string, string>) }).Invoke(pushListeners, new object[1] { pushAdditionalData });
#endif

                    }
		        };
		      PushManager.PushOpened = delegate(PushMessage message, ActionButton button) {
		            if (pushListeners != null && onPushOpened != null) {
#if !UNITY_METRO || UNITY_EDITOR
                        pushListeners.GetType().GetMethod(onPushOpened).Invoke(pushListeners, new object[2] { message, button });
#else
                        pushListeners.GetType().GetRuntimeMethod(onPushOpened, new Type[] { typeof(PushMessage), typeof(ActionButton) }).Invoke(pushListeners, new object[2] { message, button });
#endif

                    }
		        };
		        PushManager.PushTokenFailed = delegate(string error) {
		            if (pushListeners != null && onTokenFailed != null) {
#if !UNITY_METRO || UNITY_EDITOR
                        pushListeners.GetType().GetMethod(onTokenFailed).Invoke(pushListeners, new object[1] { error });
#else
                        pushListeners.GetType().GetRuntimeMethod(onTokenFailed, new Type[] { typeof(string) }).Invoke(pushListeners, new object[1] { error });
#endif
                    }
		        };
		        PushManager.PushTokenReceived = delegate(string data) {
		            if (pushListeners != null && onTokenReceived != null) {
#if !UNITY_METRO || UNITY_EDITOR
                        pushListeners.GetType().GetMethod(onTokenReceived).Invoke(pushListeners, new object[1] { data });
#else
                        pushListeners.GetType().GetRuntimeMethod(onTokenReceived, new Type[] { typeof(string) }).Invoke(pushListeners, new object[1] { data });
#endif
                    }
		        };
		        PushManager.PushNotificationsEnabled = true;
	    	}
	    }
	}
}