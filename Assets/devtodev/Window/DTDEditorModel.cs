using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DTDEditor {
    [Serializable]
    [XmlRoot("DTDEditorSettings")]
    public class DTDEditorModel {
        #region EditorProperties
        [XmlAttribute("activeWindow")]
        public DTDEditorWindow ActiveWindow { get; set; }
        #endregion

        #region AnalyticsProperties
        [XmlAttribute("isLogEnabled")]
        public bool IsLogEnabled { get; set; }

        [XmlAttribute("isAnalyticsEnabled")]
        public bool IsAnalyticsEnabled { get; set; }

        [XmlAttribute("isPushMessagesEnabled")]
        public bool IsPushMessagesEnabled { get; set; }

        [XmlAttribute("activePlatform")]
        public DTDPlatform ActivePlatform { get; set; }

        [XmlArray("Credentials")]
        [XmlArrayItem("Credential")]
        public List<DTDCredentials> Credentials { get; set; }
        #endregion

        #region NotificationProperties
        [XmlAttribute("pushGameObjectName")]
        public string PushGameObjectName { get; set; }

        [XmlAttribute("pushGameObjectScriptIndex")]
        public int PushGameObjectScriptIndex { get; set; }

        [XmlAttribute("pushTokenFunctionIndex")]
        public int PushTokenFunctionIndex { get; set; }

        [XmlAttribute("pushTokenErrorFunctionIndex")]
        public int PushTokenErrorFunctionIndex { get; set; }

        [XmlAttribute("pushReceivedFunctionIndex")]
        public int PushReceivedFunctionIndex { get; set; }

        [XmlAttribute("pushOpenedFunctionIndex")]
        public int PushOpenedFunctionIndex { get; set; }
        #endregion

        public DTDEditorModel() {
            this.ActiveWindow = DTDEditorWindow.Choise;
            this.IsLogEnabled = false;
            this.IsAnalyticsEnabled = false;
            this.IsPushMessagesEnabled = false;
            this.ActivePlatform = DTDPlatform.Android;
            this.Credentials = new List<DTDCredentials>();
            this.PushGameObjectName = string.Empty;
            this.PushGameObjectScriptIndex = 0;
            this.PushTokenFunctionIndex = 0;
            this.PushTokenErrorFunctionIndex = 0;
            this.PushReceivedFunctionIndex = 0;
            this.PushOpenedFunctionIndex = 0;
        }

        public override string ToString() {
            var output = new StringBuilder(string.Format("ActivePlatform: {0} Platforms count: {1}", ActivePlatform, Credentials.Count));
            foreach (DTDCredentials info in Credentials) {
                output.AppendLine(string.Format("Platform: {0}, Key: {1} Secret: {2}", info.Platform, info.Key, info.Secret));
            }
            return output.ToString();
        }
    }

    [Serializable]
    public class DTDCredentials {

        [XmlAttribute("platform")]
        public DTDPlatform Platform;

        [XmlAttribute("key")]
        public string Key;

        [XmlAttribute("secret")]
        public string Secret;

        public DTDCredentials() {
            this.Platform = DTDPlatform.Android;
            this.Key = string.Empty;
            this.Secret = string.Empty;

        }

        public DTDCredentials(DTDPlatform platform, string key, string secret) {
            this.Platform = platform;
            this.Key = key;
            this.Secret = secret;
        }
    }

    [Serializable]
    public enum DTDPlatform {
        Android,
        iOS,
        MacOS,
        Windows,
        WebGL,
        WinStore
    }

    [Serializable]
    public enum DTDEditorWindow {
        Choise,
        Analytics,
        Notifications
    }
}