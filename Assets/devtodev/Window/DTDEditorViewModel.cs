#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevToDev;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#endif

namespace DTDEditor {
    public class DTDEditorViewModel {
        private static readonly string OldFileStoragePath = Path.Combine(Path.Combine(Path.Combine(Application.dataPath, "devtodev"), "Window"), ".devtodev");
        private static readonly string NewFileStoragePath = Path.Combine(Path.Combine(Path.Combine(Application.dataPath, "devtodev"), "Window"), ".devtodev_services.xml");

        public static readonly Type[] PushTokenMethodSignature = { typeof(string) };
        public static readonly Type[] PushReceivedMethodSignature = { typeof(IDictionary<string, string>) };
        public static readonly Type[] PushOpenedMethodSignature = { typeof(PushMessage), typeof(ActionButton) };
        private const string DTDGameObjectOldName = "[devtodev_initialize]";
        private const string DTDGameObjectName = "[devtodev]";
        private DTDEditorModel Model;
        private GameObject DTDGameObject;
        private DevToDevSDK DTDScriptObject;

        internal DTDEditorWindow ActiveWindow {
            get {
                return Model.ActiveWindow;
            }
            set {
                if (value == DTDEditorWindow.Notifications && !Model.IsAnalyticsEnabled) {
                    ShowDialog("Please enable analytics first");
                    return;
                }
                Model.ActiveWindow = value;
            }
        }
        internal DTDPlatform ActivePlatform {
            get {
                return Model.ActivePlatform;
            }
            set {
                Model.ActivePlatform = value;
            }
        }
        internal bool IsAnalyticsEnabled {
            get {
                return Model.IsAnalyticsEnabled;
            }
            set {
                if (CanModifySettings()) {
                    Model.IsAnalyticsEnabled = value;
                    if (value) {
                        AddDTDGameObject();
                    } else {
                        IsPushMessagesEnabled = value;
                        RemoveDTDGameObject();
                    }
                    MakeSceneDirty();
                }
            }
        }
        internal bool IsPushMessagesEnabled {
            get {
                return Model.IsPushMessagesEnabled;
            }
            set {
                if (Model.IsPushMessagesEnabled != value && CanModifySettings()) {
                    Model.IsPushMessagesEnabled = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.IsPushMessagesEnabled = value;
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal bool IsLogEnabled {
            get {
                return Model.IsLogEnabled;
            }
            set {
                if (Model.IsLogEnabled != value && CanModifySettings()) {
                    Model.IsLogEnabled = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.IsLogEnabled = value;
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal string[] PushScriptsNames;
        internal string[] PushTokenMethods;
        internal string[] PushReceivedMethods;
        internal string[] PushOpenedMethods;
        private MonoBehaviour[] PushScripts;
        private GameObject pushGameObject;
        internal GameObject PushGameObject {
            get {
                return pushGameObject;
            }
            set {
                if (pushGameObject != value && CanModifySettings()) {
                    pushGameObject = value;
                    if (value != null) {
                        PushScripts = pushGameObject.GetComponents<MonoBehaviour>();
                        PushScriptsNames = PushScripts.Select(x => x.GetType().Name).ToArray();
                        PushTokenMethods = GetMethodNames(PushTokenMethodSignature);
                        PushReceivedMethods = GetMethodNames(PushReceivedMethodSignature);
                        PushOpenedMethods = GetMethodNames(PushOpenedMethodSignature);
                    } else {
                        PushScripts = new MonoBehaviour[] { };
                        PushScriptsNames = new string[] { };
                        PushTokenMethods = new string[] { };
                        PushReceivedMethods = new string[] { };
                        PushOpenedMethods = new string[] { };
                    }
                    Model.PushGameObjectName = value ? value.name : null;
                    UpdateGameObject();
                }
            }
        }
        internal int PushGameObjectScriptIndex {
            get {
                return Model.PushGameObjectScriptIndex;
            }
            set {
                if (CanModifySettings()) {
                    Model.PushGameObjectScriptIndex = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.PushListeners = GetSafeFromArray(PushScripts, value, null);
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal int PushTokenFunctionIndex {
            get {
                return Model.PushTokenFunctionIndex;
            }
            set {
                if (CanModifySettings()) {
                    Model.PushTokenFunctionIndex = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.OnTokenReceived = GetSafeFromArray(PushTokenMethods, value, string.Empty);
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal int PushTokenErrorFunctionIndex {
            get {
                return Model.PushTokenErrorFunctionIndex;
            }
            set {
                if (CanModifySettings()) {
                    Model.PushTokenErrorFunctionIndex = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.OnTokenFailed = GetSafeFromArray(PushTokenMethods, value, string.Empty);
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal int PushReceivedFunctionIndex {
            get {
                return Model.PushReceivedFunctionIndex;
            }
            set {
                if (CanModifySettings()) {
                    Model.PushReceivedFunctionIndex = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.OnPushReceived = GetSafeFromArray(PushReceivedMethods, value, string.Empty);
                        MakeSceneDirty();
                    }
                }
            }
        }
        internal int PushOpenedFunctionIndex {
            get {
                return Model.PushOpenedFunctionIndex;
            }
            set {
                if (CanModifySettings()) {
                    Model.PushOpenedFunctionIndex = value;
                    if (DTDScriptObject != null) {
                        DTDScriptObject.OnPushOpened = GetSafeFromArray(PushOpenedMethods, value, string.Empty);
                        MakeSceneDirty();
                    }
                }
            }
        }

        private string CurrentSceneName {
            get {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                return EditorSceneManager.GetActiveScene().path;
#else
        		return EditorApplication.currentScene;
#endif
            }
        }

        public DTDEditorViewModel() {
            Model = LoadModel();

            PushGameObject = GameObject.Find(Model.PushGameObjectName);
            Debug.Log("PushGameObject: " + PushGameObject + " From Name: " + Model.PushGameObjectName);
            if (PushGameObject != null) {
                DTDGameObject = FindCurrentGameObjectIfExist();
                if (DTDGameObject != null) {
                    DTDScriptObject = DTDGameObject.GetComponent<DevToDevSDK>();
                    UpdateGameObject();
                }
            }
        }
        
        internal void UpdateGameObject() {
            if (Model.IsAnalyticsEnabled) {
                AddDTDGameObject();
            } else {
                RemoveDTDGameObject();
            }
            MakeSceneDirty();
        }

        internal void UpdateActivePlatformCredentials(string key, string secret) {
            DTDCredentials credentials = new DTDCredentials(Model.ActivePlatform, key, secret);
            int index = -1;
            for (int i = 0; i < Model.Credentials.Count; i++) {
                if (Model.Credentials[i].Platform == Model.ActivePlatform) {
                    index = i;
                    break;
                }
            }
            if (index >= 0) {
                Model.Credentials.RemoveAt(index);
                Model.Credentials.Insert(index, credentials);
            } else {
                Model.Credentials.Add(credentials);
            }

            if (DTDScriptObject != null) {
                DTDScriptObject.Credentials = Model.Credentials.ToArray();
            }
            MakeSceneDirty();
        }

        internal DTDCredentials GetPlatformInfo(DTDPlatform platform) {
            foreach (DTDCredentials credential in Model.Credentials) {
                if (credential.Platform == platform) {
                    return credential;
                }
            }
            DTDCredentials credentials = new DTDCredentials(Model.ActivePlatform, "", "");
            Model.Credentials.Add(credentials);
            return credentials;
        }

        private void AddDTDGameObject() {
            DTDGameObject = FindCurrentGameObjectIfExist();
            if (DTDGameObject != null) {
                UnityEngine.Object.DestroyImmediate(DTDGameObject);
            }
            DTDGameObject = new GameObject();
            DTDGameObject.name = DTDGameObjectName;

            DTDScriptObject = DTDGameObject.AddComponent(typeof(DevToDevSDK)) as DevToDevSDK;
            DTDScriptObject.IsAnaluticsEnabled = Model.IsAnalyticsEnabled;
            DTDScriptObject.IsPushMessagesEnabled = Model.IsPushMessagesEnabled;
            DTDScriptObject.IsLogEnabled = Model.IsLogEnabled;
            DTDScriptObject.Credentials = Model.Credentials.ToArray();
            if (PushGameObject != null) {
                DTDScriptObject.PushListeners = GetSafeFromArray(PushScripts, PushGameObjectScriptIndex, null);
                DTDScriptObject.OnTokenReceived = GetSafeFromArray(PushTokenMethods, PushTokenFunctionIndex, string.Empty);
                DTDScriptObject.OnTokenFailed = GetSafeFromArray(PushTokenMethods, PushTokenErrorFunctionIndex, string.Empty);
                DTDScriptObject.OnPushReceived = GetSafeFromArray(PushReceivedMethods, PushReceivedFunctionIndex, string.Empty);
                DTDScriptObject.OnPushOpened = GetSafeFromArray(PushOpenedMethods, PushOpenedFunctionIndex, string.Empty);
            }
        }

        private void RemoveDTDGameObject() {
            DTDGameObject = FindCurrentGameObjectIfExist();
            if (DTDGameObject != null) {
                UnityEngine.Object.DestroyImmediate(DTDGameObject);
                DTDGameObject = null;
            }
        }

        private GameObject FindCurrentGameObjectIfExist() {
            if (DTDGameObject != null) {
                return DTDGameObject;
            }
            DTDGameObject = GameObject.Find(DTDGameObjectOldName);
            if (DTDGameObject != null) {
                return DTDGameObject;
            }
            DTDGameObject = GameObject.Find(DTDGameObjectName);
            if (DTDGameObject != null) {
                return DTDGameObject;
            }
            return null;
        }

        private string[] GetMethodNames(Type[] parameters) {
            var script = GetSafeFromArray(PushScripts, PushGameObjectScriptIndex, null);
            if (script == null) {
                return new string[0];
            }

            Type type = script.GetType();
            MethodInfo[] methods = type.GetMethods();
            List<MemberInfo> membersString = new List<MemberInfo>();
            foreach (MethodInfo info in methods) {
                if (info.ReturnType != typeof(void) || info.GetParameters().Length != parameters.Length || !IsSignatureValid(info, parameters) || info.DeclaringType != type) {
                    continue;
                }
                membersString.Add(info);
            }

            return membersString.Select(x => x.Name).ToArray();
        }

        private bool IsSignatureValid(MethodInfo info, Type[] parameters) {
            for (int i = 0; i < parameters.Length; i++) {
                if (info.GetParameters()[i].ParameterType != parameters[i]) {
                    return false;
                }
            }
            return true;
        }

        private bool CanModifySettings() {
            var firstScene = EditorBuildSettings.scenes.FirstOrDefault();
            if (firstScene == null) {
                EditorUtility.DisplayDialog("devtodev", "Scenes in Scenes in Build not found (File -> Build Settings...)", "Ok");
                return false;
            }

            if (!firstScene.path.ToLower().Equals(CurrentSceneName.ToLower())) {
                // ShowDialog("Any changes and integration can be made only on the first scene of the application. Open " + firstSceneName + " to make changes.");
                Debug.Log("Make changes locked");
                return false;
            }
            return true;
        }

        private void MakeSceneDirty() {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        private void ShowDialog(string title) {
            EditorUtility.DisplayDialog("devtodev", title, "OK");
        }

        private DTDEditorModel LoadModel() {
            if (File.Exists(OldFileStoragePath)) {
                var model = LoadOldFormat();
                File.Delete(OldFileStoragePath);
                return model;
            }

            if (File.Exists(NewFileStoragePath)) {
                return LoadNewFormat();
            }

            return new DTDEditorModel();
        }

        private DTDEditorModel LoadOldFormat() {
            try {
                var resultModel = new DTDEditorModel();
                using (FileStream saveFile = File.Open(OldFileStoragePath, FileMode.Open)) {
                    using (StreamReader streamReader = new StreamReader(saveFile)) {
                        streamReader.ReadLine();
                        resultModel.Credentials.Add(new DTDCredentials(DTDPlatform.iOS, streamReader.ReadLine(), streamReader.ReadLine()));
                        resultModel.Credentials.Add(new DTDCredentials(DTDPlatform.Android, streamReader.ReadLine(), streamReader.ReadLine()));
                        resultModel.Credentials.Add(new DTDCredentials(DTDPlatform.MacOS, streamReader.ReadLine(), streamReader.ReadLine()));
                        resultModel.Credentials.Add(new DTDCredentials(DTDPlatform.Windows, streamReader.ReadLine(), streamReader.ReadLine()));
                        resultModel.Credentials.Add(new DTDCredentials(DTDPlatform.WebGL, streamReader.ReadLine(), streamReader.ReadLine()));
                        resultModel.ActiveWindow = (DTDEditorWindow)Enum.Parse(typeof(DTDEditorWindow), streamReader.ReadLine());
                        resultModel.ActivePlatform = (DTDPlatform)Enum.Parse(typeof(DTDPlatform), streamReader.ReadLine());
                        resultModel.IsPushMessagesEnabled = bool.Parse(streamReader.ReadLine());
                        resultModel.IsLogEnabled = bool.Parse(streamReader.ReadLine());
                        resultModel.IsAnalyticsEnabled = bool.Parse(streamReader.ReadLine());
                        resultModel.PushGameObjectName = streamReader.ReadLine();
                        resultModel.PushReceivedFunctionIndex = int.Parse(streamReader.ReadLine());
                        resultModel.PushOpenedFunctionIndex = int.Parse(streamReader.ReadLine());
                        resultModel.PushTokenFunctionIndex = int.Parse(streamReader.ReadLine());
                        resultModel.PushTokenErrorFunctionIndex = int.Parse(streamReader.ReadLine());
                    }
                }

                return resultModel;
            } catch (Exception) {
                return new DTDEditorModel();
            }
        }

        private DTDEditorModel LoadNewFormat() {
            try {
                var serializer = new XmlSerializer(typeof(DTDEditorModel));
                using (var reader = new FileStream(NewFileStoragePath, FileMode.Open)) {
                    return (DTDEditorModel)serializer.Deserialize(reader);
                }
            } catch (Exception) {
                return new DTDEditorModel();
            }
        }

        public void SaveData() {
            if (File.Exists(NewFileStoragePath)) {
                File.Delete(NewFileStoragePath);
            }

            var serializer = new XmlSerializer(typeof(DTDEditorModel));
            using (var writer = new FileStream(NewFileStoragePath, FileMode.Create)) {
                serializer.Serialize(writer, Model);
            }
        }

        private T GetSafeFromArray<T>(T[] array, int index, T defaultValue) {
            if (array == null || index < 0 || index >= array.Length) {
                return defaultValue;
            }

            return array[index];
        }
    }
}
#endif