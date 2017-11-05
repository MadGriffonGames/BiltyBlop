#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace DTDEditor {
    public class DTDVisualEditorWindow : EditorWindow {
        private const string DTDTitle = "devtodev";
        private const string DTDDescription = "devtodev is a powerful analytical and marketing platform for mobile and web applications. Gather all the data of your application in one simple interface and analyze every bite of it. With devtodev, it is easy to find the weak points, to improve traffic source efficiency and to build strong communications with the customers.";
        private const string DTDAnalyticsTitle = "Analytics";
        private const string DTDAnalyticsMessage = "\nKey solution to rule your apps.\n";
        private const string DTDAnalyticsDescription = "devtodev is a powerful all-in-one analytical tool for mobile and web applications. Explore your app metrics in one simple interface that includes teamwork features, game metrics, LTV forecast, and many other cool things.";
        private const string DTDNotificationTitle = "Push Notifications";
        private const string DTDNotificationMessage = "\nSimple and powerful tool to re-engage your users\n";
        private const string DTDUrl = "https://www.devtodev.com/myapps/";

        [MenuItem("Window/devtodev")]
        public static void ShowWindow() {
            DTDVisualEditorWindow window = (DTDVisualEditorWindow)EditorWindow.GetWindow(typeof(DTDVisualEditorWindow), false, "devtodev", true);
            window.ViewModel = new DTDEditorViewModel();
            window.Styles = new DTDVisualEditorStyles();
            window.Show();
        }

        private Vector2 ScrollPosition;
        private DTDVisualEditorStyles Styles;
        private DTDEditorViewModel ViewModel;

        void OnEnable() {
            ViewModel = new DTDEditorViewModel();
            Styles = new DTDVisualEditorStyles();
        }

        void OnDisable() {
            if (ViewModel != null) {
                ViewModel.SaveData();
            }
        }

        void OnGUI() {
            EditorGUILayout.BeginVertical();
            switch (ViewModel.ActiveWindow) {
                case DTDEditorWindow.Choise: DrawChoise(); break;
                case DTDEditorWindow.Analytics: DrawAnalytics(); break;
                case DTDEditorWindow.Notifications: DrawNotifications(); break;
            }
            EditorGUILayout.EndVertical();
            this.Repaint();
        }

        private void DrawChoise() {
            DrawHeader(false);
            DrawSubHeader();
            DrawSplitter();
            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
            DrawAnalyticsBlock();
            DrawNotificationsBlock();
            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader(bool isNeedBack) {
            EditorGUILayout.BeginVertical(Styles.DashboardBackStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            if (isNeedBack) {
                if (GUILayout.Button("← Back to services", Styles.ButtonBackStyle, GUILayout.ExpandWidth(false))) {
                    ViewModel.ActiveWindow = DTDEditorWindow.Choise;
                }
            }

            EditorGUILayout.LabelField("", Styles.DashboardTextStyle, GUILayout.Width(10));

            GUIContent content = new GUIContent();
            content.text = "Go to Site";
            content.image = (Texture)Resources.Load("devtodev/dashboard");

            if (GUILayout.Button(content, Styles.DashboardTextStyle, GUILayout.ExpandWidth(false))) {
                Application.OpenURL(DTDUrl);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        #region Choise
        private void DrawSubHeader() {
            EditorGUILayout.BeginVertical(Styles.TopStyle);
            EditorGUILayout.LabelField(PlayerSettings.productName, Styles.LogoTextStyle);
            EditorGUILayout.LabelField(DTDTitle, Styles.ServicesStyle);
            EditorGUILayout.LabelField(DTDDescription, Styles.MainTextStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndVertical();
        }

        private void DrawAnalyticsBlock() {
            Rect blockRect = EditorGUILayout.BeginVertical(Styles.BlockBackStyle);

            GUIContent content = new GUIContent();
            content.text = DTDAnalyticsTitle;
            content.image = (Texture)Resources.Load("devtodev/analytics");
            EditorGUILayout.LabelField(content, Styles.BlockLogoStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.LabelField(DTDAnalyticsMessage, Styles.BlockTextStyle);
            Rect switchRect = Styles.MakeButton(ViewModel.IsAnalyticsEnabled, delegate {
                ViewModel.IsAnalyticsEnabled = !ViewModel.IsAnalyticsEnabled;
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            if (blockRect.Contains(Event.current.mousePosition)
                && !switchRect.Contains(Event.current.mousePosition)
                && Event.current.type == EventType.MouseUp) {
                ViewModel.ActiveWindow = DTDEditorWindow.Analytics;
            }

            DrawSplitter();
        }

        private void DrawNotificationsBlock() {
            Rect blockRect = EditorGUILayout.BeginVertical(Styles.BlockBackStyle);

            GUIContent content = new GUIContent();
            content.text = DTDNotificationTitle;
            content.image = (Texture)Resources.Load("devtodev/push");
            EditorGUILayout.LabelField(content, Styles.BlockLogoStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.LabelField(DTDNotificationMessage, Styles.BlockTextStyle);
            Rect switchRect = Styles.MakeButton(ViewModel.IsPushMessagesEnabled, delegate {
                ViewModel.IsPushMessagesEnabled = !ViewModel.IsPushMessagesEnabled;
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            if (blockRect.Contains(Event.current.mousePosition)
                && !switchRect.Contains(Event.current.mousePosition)
                && Event.current.type == EventType.MouseUp) {
                ViewModel.ActiveWindow = DTDEditorWindow.Notifications;
            }
        }
        #endregion
        #region Analytics
        private void DrawAnalytics() {
            DrawHeader(true);
            EditorGUILayout.BeginVertical(Styles.BlockBackStyle);
            EditorGUILayout.LabelField("");
            GUIContent content = new GUIContent();
            content.text = "ANALYTICS";
            content.image = (Texture)Resources.Load("devtodev/analytics");
            EditorGUILayout.LabelField(content, Styles.BlockLogoStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.LabelField(DTDAnalyticsMessage, Styles.BlockTextStyle);
            Styles.MakeButton(ViewModel.IsAnalyticsEnabled, delegate {
                ViewModel.IsAnalyticsEnabled = !ViewModel.IsAnalyticsEnabled;
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            DrawSplitter();
            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);

            EditorGUILayout.BeginVertical(Styles.TopStyle);
            EditorGUILayout.LabelField(DTDAnalyticsTitle, Styles.SideLogoStyle);
            EditorGUILayout.LabelField(DTDAnalyticsDescription, Styles.MainTextStyle, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Go to Site", Styles.ToDashboardButtonStyle)) {
                Application.OpenURL(DTDUrl);
            }
            EditorGUILayout.EndVertical();

            DrawSplitter();
            DrawPlatformsBlock();
            DrawSplitter();
            DrawLogBlock();

            EditorGUILayout.EndScrollView();
        }

        private void DrawPlatformsBlock() {
            DrawPlatformsList();

            string Key;
            string Secret;
            DTDCredentials credentials = ViewModel.GetPlatformInfo(ViewModel.ActivePlatform);

            EditorGUILayout.BeginHorizontal(Styles.BlockPlatformStyle);
            EditorGUILayout.LabelField("App key", new GUIStyle(Styles.MainTextStyle) { padding = new RectOffset(10, 0, 4, 0) }, GUILayout.Width(70));
            Key = EditorGUILayout.TextField(credentials.Key, Styles.TextFieldStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(Styles.BlockPlatformStyle);
            EditorGUILayout.LabelField("Secret key", new GUIStyle(Styles.MainTextStyle) { padding = new RectOffset(10, 0, 5, 0) }, GUILayout.Width(70));
            Secret = EditorGUILayout.TextField(credentials.Secret, Styles.TextFieldStyle);
            EditorGUILayout.EndHorizontal();

            ViewModel.UpdateActivePlatformCredentials(Key, Secret);
        }

        private void DrawPlatformsList() {
            EditorGUILayout.BeginVertical(Styles.TopStyle);
            EditorGUILayout.LabelField("Supported Platforms", Styles.SideLogoStyle);
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 15, 10, 0) });
            Array platforms = Enum.GetValues(typeof(DTDPlatform));
            for (int i = 0; i < platforms.Length / 2; i++) {
                DTDPlatform platform = (DTDPlatform)platforms.GetValue(i);
                if (GUILayout.Button(platform.ToString(), Styles.PlatformButtonStyle(ViewModel.ActivePlatform == platform))) {
                    ViewModel.ActivePlatform = platform;
                    GUI.FocusControl("");
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 15, 5, 0) });
            for (int i = platforms.Length / 2; i < platforms.Length; i++) {
                DTDPlatform platform = (DTDPlatform)platforms.GetValue(i);
                if (GUILayout.Button(platform.ToString(), Styles.PlatformButtonStyle(ViewModel.ActivePlatform == platform))) {
                    ViewModel.ActivePlatform = platform;
                    GUI.FocusControl("");
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private void DrawLogBlock() {
            EditorGUILayout.BeginVertical(Styles.BlockBackStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            GUIStyle logStyle = new GUIStyle(Styles.SideLogoStyle);
            logStyle.padding.top -= 25;
            EditorGUILayout.LabelField("Log enabled", logStyle);
            Styles.MakeButton(ViewModel.IsLogEnabled, delegate {
                ViewModel.IsLogEnabled = !ViewModel.IsLogEnabled;
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        #endregion
        #region Notifications
        private void DrawNotifications() {
            DrawHeader(true);

            EditorGUILayout.BeginVertical(Styles.BlockBackStyle);
            EditorGUILayout.LabelField("");
            GUIContent content = new GUIContent();
            content.text = "PUSH-NOTIFICATIONS";
            content.image = (Texture)Resources.Load("devtodev/push");
            EditorGUILayout.LabelField(content, Styles.BlockLogoStyle);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.LabelField(DTDNotificationMessage, Styles.BlockTextStyle);
            Styles.MakeButton(ViewModel.IsPushMessagesEnabled, delegate {
                ViewModel.IsPushMessagesEnabled = !ViewModel.IsPushMessagesEnabled;
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            DrawSplitter();
            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);

            EditorGUILayout.BeginVertical(Styles.TopStyle);
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
            EditorGUILayout.LabelField("Target", new GUIStyle(Styles.MainTextStyle) { padding = new RectOffset(0, 0, 0, 0) }, GUILayout.Width(95));
            ViewModel.PushGameObject = EditorGUILayout.ObjectField(ViewModel.PushGameObject, typeof(GameObject), true, GUILayout.ExpandWidth(true), GUILayout.Height(18)) as GameObject;
            EditorGUILayout.EndHorizontal();
            if (ViewModel.PushGameObject != null) {
                EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
                EditorGUILayout.LabelField("Script", new GUIStyle(Styles.MainTextStyle) { padding = new RectOffset(0, 0, -2, 0) }, GUILayout.Width(95));
                ViewModel.PushGameObjectScriptIndex = EditorGUILayout.Popup(ViewModel.PushGameObjectScriptIndex, ViewModel.PushScriptsNames, EditorStyles.popup);
                EditorGUILayout.EndHorizontal();
                DrawListenersBlock();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        private void DrawListenersBlock() {
            ViewModel.PushTokenFunctionIndex = DrawPushBlock(ViewModel.PushTokenFunctionIndex, "Token received", ViewModel.PushTokenMethods);
            ViewModel.PushTokenErrorFunctionIndex = DrawPushBlock(ViewModel.PushTokenErrorFunctionIndex, "Token error", ViewModel.PushTokenMethods);
            ViewModel.PushReceivedFunctionIndex = DrawPushBlock(ViewModel.PushReceivedFunctionIndex, "Push received", ViewModel.PushReceivedMethods);
            ViewModel.PushOpenedFunctionIndex = DrawPushBlock(ViewModel.PushOpenedFunctionIndex, "Push opened", ViewModel.PushOpenedMethods);
        }

        private int DrawPushBlock(int index, string title, string[] methods) {
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
            EditorGUILayout.LabelField(title, new GUIStyle(Styles.MainTextStyle) { padding = new RectOffset(0, 0, 0, 0) }, GUILayout.Width(95));
            index = EditorGUILayout.Popup(index, methods, EditorStyles.popup);
            EditorGUILayout.EndHorizontal();
            return index;
        }
        #endregion

        private void DrawSplitter() {
            GUILayout.Label(new GUIContent(), Styles.SplitterStyle);
        }
    }
}
#endif