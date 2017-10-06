#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using DevToDev;

[System.Serializable]
public class IntegrateWindow : EditorWindow  {

	private bool needToAttach = false; 
	private float waitForCompile = 1;
	private GameObject devtodevGameObject;
    private List<MemberInfo> membersString = new List<MemberInfo>();
    private List<MemberInfo> membersReceived = new List<MemberInfo>();
    private List<MemberInfo> membersOpened = new List<MemberInfo>();
    private List<MonoBehaviour> mbScripts = new List<MonoBehaviour>();
    private string firstSceneName = null;
    private bool firstOnGUI = false;

    #region Data
    [SerializeField]
	private string iosKey;
	[SerializeField]
	private string iosSecret;
	[SerializeField]
	private string androidKey;
	[SerializeField]
	private string androidSecret;
	[SerializeField]
	private string macKey;
	[SerializeField]
	private string macSecret;
	[SerializeField]
	private string winKey;
	[SerializeField]
	private string winSecret;
	[SerializeField]
	private string wsKey;
	[SerializeField]
	private string wsSecret;
	[SerializeField]
	private string webKey;
	[SerializeField]
	private string webSecret;
	[SerializeField]
	private bool pushGroupEnabled = false;
	[SerializeField]
	private bool logEnabled = false;
    [SerializeField]
    private int selected = 0;
    [SerializeField]
    private int pushListeners = -1;
    [SerializeField]
    private GameObject selectedGO;
    [SerializeField]
    private string currentSelectedPushGameObjectName;
    [SerializeField]
    private int pushReceived;
    [SerializeField]
    private int pushOpened;
    [SerializeField]
    private int tokenReceived;
    [SerializeField]
    private int tokenFailed;
    [SerializeField]
    private int platformSelected = 0;
    [SerializeField]
    private bool analyticsEnabled;
    [SerializeField]
    private string injectedSceneName;
    [SerializeField]
    private string savedGoName;

    private void LoadData() {
        if (!File.Exists(Application.dataPath + "/devtodev/Window/.devtodev")) {
            return;
        }
        using (FileStream saveFile = File.Open(Application.dataPath + "/devtodev/Window/.devtodev", FileMode.Open)) {
            using (StreamReader ms = new StreamReader(saveFile)) {
                injectedSceneName = ms.ReadLine();
                iosKey = ms.ReadLine();
                iosSecret = ms.ReadLine();
                androidKey = ms.ReadLine();
                androidSecret = ms.ReadLine();
                macKey = ms.ReadLine();
		        macSecret = ms.ReadLine();
		        winKey = ms.ReadLine();
		        winSecret = ms.ReadLine();
		        webKey = ms.ReadLine();
		        webSecret = ms.ReadLine();;
		        selected = int.Parse(ms.ReadLine());
		        platformSelected = int.Parse(ms.ReadLine());
		        pushGroupEnabled = bool.Parse(ms.ReadLine());
		        logEnabled = bool.Parse(ms.ReadLine());
		        analyticsEnabled = bool.Parse(ms.ReadLine());
                savedGoName = ms.ReadLine();
                this.ReloadPushListenersData();
                pushReceived = int.Parse(ms.ReadLine());
                pushOpened = int.Parse(ms.ReadLine());
                tokenReceived = int.Parse(ms.ReadLine());
                tokenFailed = int.Parse(ms.ReadLine());
            }
        }
    }

    private void SaveData() {
        if (string.IsNullOrEmpty(this.injectedSceneName) || !this.injectedSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
            return;
        }
        if (File.Exists(Application.dataPath + "/devtodev/Window/.devtodev")) {
            File.Delete(Application.dataPath + "/devtodev/Window/.devtodev");
        }
        using (FileStream saveFile = File.Open(Application.dataPath + "/devtodev/Window/.devtodev", FileMode.CreateNew)) {
            using (StreamWriter ms = new StreamWriter(saveFile)) {
                ms.WriteLine(injectedSceneName);
                ms.WriteLine(iosKey);
                ms.WriteLine(iosSecret);
                ms.WriteLine(androidKey);
                ms.WriteLine(androidSecret);
                ms.WriteLine(macKey);
                ms.WriteLine(macSecret);
                ms.WriteLine(winKey);
                ms.WriteLine(winSecret);
                ms.WriteLine(webKey);
                ms.WriteLine(webSecret);
                ms.WriteLine(selected);
                ms.WriteLine(platformSelected);
                ms.WriteLine(pushGroupEnabled);
                ms.WriteLine(logEnabled);
                ms.WriteLine(analyticsEnabled);
                ms.WriteLine(selectedGO != null ? selectedGO.name : savedGoName);
                ms.WriteLine(pushReceived);
                ms.WriteLine(pushOpened);
                ms.WriteLine(tokenReceived);
                ms.WriteLine(tokenFailed);
            }
        }
    }

    private void ReloadPushListenersData() {
        if (selectedGO == null) {
            selectedGO = GameObject.Find(savedGoName);
            if (selectedGO != null) {
                currentSelectedPushGameObjectName = selectedGO.name;
                savedGoName = null;
            }
        }
    }

    private bool IsInjectedScene() {
        if (string.IsNullOrEmpty(this.injectedSceneName)) {
            return true;
        }
        if (File.Exists(this.injectedSceneName)) {
            if (!this.injectedSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
                this.PushSimpleInformationDialog("Please open " + this.injectedSceneName + " scene to modify devtodev integration settings!");
                return false;
            }
        } else {
            ClearDataIfSceneNotExistsAnymore();
            return true;
        }
        return true;
    }

    private void ClearDataIfSceneNotExistsAnymore() {
        this.PushSimpleInformationDialog("The scene with integrated devtodev SDK was removed. SDK settings will be wiped. Please configure them again.");
            if (File.Exists(Application.dataPath + "/devtodev/Window/.devtodev")) {
                File.Delete(Application.dataPath + "/devtodev/Window/.devtodev");
            }
            if (File.Exists(Application.dataPath + "/devtodev/Window/DevToDevIntegration.cs")) {
                File.Delete(Application.dataPath + "/devtodev/Window/DevToDevIntegration.cs");
            }
            needToAttach = false;
            waitForCompile = 1;
            devtodevGameObject = null;
            membersString = new List<MemberInfo>();
            membersReceived = new List<MemberInfo>();
            membersOpened = new List<MemberInfo>();
            mbScripts = new List<MonoBehaviour>();
            firstSceneName = null;
            firstOnGUI = false;
            iosKey = null;
            iosSecret = null;
            androidKey = null;
            androidSecret = null;
            macKey = null;
            macSecret = null;
            winKey = null;
            winSecret = null;
            wsKey = null;
            wsSecret = null;
            webKey = null;
            webSecret = null;
            pushGroupEnabled = false;
            logEnabled = false;
            selected = 0;
            pushListeners = -1;
            selectedGO = null;
            currentSelectedPushGameObjectName = null;
            pushReceived = 0;
            pushOpened = 0;
            tokenReceived = 0;
            tokenFailed = 0;
            platformSelected = 0;
            analyticsEnabled = false;
            injectedSceneName = null;
            savedGoName = null;

            foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes) {
                this.firstSceneName = scene.path;
                break;
            }
    }

    #endregion

	[MenuItem ("Window/devtodev")]
	public static void  ShowWindow () {
		IntegrateWindow window = EditorWindow.GetWindow(typeof(IntegrateWindow), false, "devtodev", false) as IntegrateWindow;
        window.LoadData();
    }

    private void EditorHierarchyWindowChanged() {
        selectedGO = GameObject.Find(currentSelectedPushGameObjectName);
    }

    private string GetCurrentSceneName() {
#if UNITY_5_3 || UNITY_5_4 || UNITY_5_5 || UNITY_5_6 || UNITY_5_7 || UNITY_5_8 || UNITY_5_9 || UNITY_6_0
        return UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
#else 
        return EditorApplication.currentScene;
#endif
    }

    void reloadFirstScene() { 
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes) {
            this.firstSceneName = scene.path;
            break;
        }
    }

    void OnEnable() {
        this.analyticsImg = (Texture)Resources.Load("devtodev/analytics");
        this.pushImg = (Texture)Resources.Load("devtodev/push");
        this.dashboardImg = (Texture)Resources.Load("devtodev/dashboard");
        this.minSize = new Vector2(300, 300);
        this.reloadFirstScene();
        this.LoadData();
        EditorApplication.hierarchyWindowChanged -= this.EditorHierarchyWindowChanged;
        EditorApplication.hierarchyWindowChanged += this.EditorHierarchyWindowChanged;
        this.IsInjectedScene();
    }

	void OnDisable() {
        if (selectedGO == null && savedGoName != null) {
            this.ReloadPushListenersData();
        }
        this.SaveData();
	}

    #region Styles
    private Texture analyticsImg, pushImg, dashboardImg;
    private GUIStyle logoStyle, servicesStyle, mainTextStyle, blockLogoStyle, blockTextStyle,
        buttonFurther, buttonBack, toggleTextStyle, textFieldStyle, toggle, blockHorizontalStyle,
        topStyle, splitter, blockBackStyle, dashboardLabel, dashboardBack, dashboardText, dashboardImgStyle, sideLogoStyle, buttonToDashBoard,
        smallPlatformButtonActive, smallPlatformButtonInActive;
    private Vector2 scrollPos;

    private Texture2D MakeTex(int width, int height, Color col) {
        var pix = new Color[width * height];
   
        for (var i = 0; i < pix.Length; i++) {
            pix[i] = col;
        } 
        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    void OnSelectionChange() {
        this.firstOnGUI = false;
    }

    void OnInspectorUpdate() {
        this.firstOnGUI = false;
    }

    private void CreateStyles() {
        splitter = new GUIStyle();
        splitter.normal.background = this.MakeTex(1, 1, new Color(197 / 255.0f, 197 / 255.0f, 197 / 255.0f));
        splitter.stretchWidth = true;
        splitter.fixedHeight = 2;
        splitter.fixedWidth = 0;

        topStyle = new GUIStyle("LockedHeaderBackground");
        topStyle.normal.background = this.MakeTex(1, 1, new Color(228 / 255.0f, 229 / 255.0f, 228 / 255.0f));

        blockBackStyle = new GUIStyle("LockedHeaderBackground");
        blockBackStyle.normal.background = this.MakeTex(1, 1, new Color(243 / 255.0f, 244 / 255.0f, 245 / 255.0f));

        dashboardBack = new GUIStyle("LockedHeaderBackground");
        dashboardBack.normal.background = this.MakeTex(1, 1, new Color(89 / 255.0f, 94 / 255.0f, 98 / 255.0f));
        dashboardBack.padding = new RectOffset(0, 0, -1, -1);

        buttonToDashBoard = new GUIStyle("Button");
        buttonToDashBoard.normal.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.active.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.onActive.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.onNormal.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.onHover.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.hover.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        buttonToDashBoard.normal.textColor = Color.white;
        buttonToDashBoard.active.textColor = Color.white;
        buttonToDashBoard.onActive.textColor = Color.white;
        buttonToDashBoard.onNormal.textColor = Color.white;
        buttonToDashBoard.onHover.textColor = Color.white;
        buttonToDashBoard.hover.textColor = Color.white;
        buttonToDashBoard.margin.left = 27;
        buttonToDashBoard.margin.bottom = 30;
        buttonToDashBoard.fixedWidth = 140;
        buttonToDashBoard.fixedHeight = 35;

        smallPlatformButtonActive = new GUIStyle("Button");
        smallPlatformButtonActive.normal.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.active.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.onActive.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.onNormal.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.onHover.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.hover.background = this.MakeTex(1, 1, new Color(0 / 255.0f, 191 / 255.0f, 245 / 255.0f));
        smallPlatformButtonActive.normal.textColor = Color.white;
        smallPlatformButtonActive.active.textColor = Color.white;
        smallPlatformButtonActive.onActive.textColor = Color.white;
        smallPlatformButtonActive.onNormal.textColor = Color.white;
        smallPlatformButtonActive.onHover.textColor = Color.white;
        smallPlatformButtonActive.hover.textColor = Color.white;
        smallPlatformButtonActive.margin.right = 5;
        smallPlatformButtonActive.margin.bottom = 2;
        smallPlatformButtonActive.fixedHeight = 22;

        smallPlatformButtonInActive = new GUIStyle("Button");
        smallPlatformButtonInActive.normal.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.active.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.onActive.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.onNormal.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.onHover.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.hover.background = this.MakeTex(1, 1, Color.white);
        smallPlatformButtonInActive.normal.textColor = Color.black;
        smallPlatformButtonInActive.active.textColor = Color.black;
        smallPlatformButtonInActive.onActive.textColor = Color.black;
        smallPlatformButtonInActive.onNormal.textColor = Color.black;
        smallPlatformButtonInActive.onHover.textColor = Color.black;
        smallPlatformButtonInActive.hover.textColor = Color.black;
        smallPlatformButtonInActive.margin.right = 5;
        smallPlatformButtonInActive.margin.bottom = 2;
        smallPlatformButtonInActive.fixedHeight = 22;

        dashboardLabel = new GUIStyle(GUIStyle.none);
        dashboardLabel.alignment = TextAnchor.MiddleRight;

        dashboardImgStyle = new GUIStyle(GUIStyle.none);
        dashboardImgStyle.padding.right = 20;
        dashboardImgStyle.alignment = TextAnchor.MiddleRight;

        dashboardText = new GUIStyle(GUI.skin.label);
        dashboardText.wordWrap = true;
        dashboardText.fontStyle = FontStyle.Normal;
        dashboardText.fontSize = 10;
        dashboardText.imagePosition = ImagePosition.ImageLeft;
        dashboardText.normal.textColor = Color.white;
        dashboardText.alignment = TextAnchor.MiddleRight;
        dashboardText.fixedWidth = 130;

        logoStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        logoStyle.wordWrap = true;
        logoStyle.fontStyle = FontStyle.Normal;
        logoStyle.fontSize = 26;
        logoStyle.padding.left = 20;
        logoStyle.normal.textColor = Color.black;
        logoStyle.alignment = TextAnchor.UpperLeft;

        sideLogoStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        sideLogoStyle.wordWrap = true;
        sideLogoStyle.fontStyle = FontStyle.Normal;
        sideLogoStyle.fontSize = 22;
        sideLogoStyle.padding.top = 20; 
        sideLogoStyle.padding.left = 20;
        sideLogoStyle.normal.textColor = Color.black;
        sideLogoStyle.alignment = TextAnchor.UpperLeft;

        servicesStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        servicesStyle.wordWrap = true;
        servicesStyle.fontSize = 18;
        servicesStyle.padding.top = -5;
        servicesStyle.padding.left = 20;
        servicesStyle.fontStyle = FontStyle.Bold;
        servicesStyle.normal.textColor = Color.black;
        servicesStyle.alignment = TextAnchor.UpperLeft;

        mainTextStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        mainTextStyle.wordWrap = true;
        mainTextStyle.fontStyle = FontStyle.Normal;
        mainTextStyle.fontSize = 12;
        mainTextStyle.padding.left = 20;
        mainTextStyle.padding.right = 50;
        mainTextStyle.padding.bottom = 25;
        mainTextStyle.normal.textColor = Color.black;
        mainTextStyle.alignment = TextAnchor.UpperLeft;

        blockHorizontalStyle = new GUIStyle(GUIStyle.none);
        blockHorizontalStyle.padding.left = 17;
        blockHorizontalStyle.padding.right = 50;
        blockHorizontalStyle.stretchWidth = false;
        blockHorizontalStyle.alignment = TextAnchor.UpperLeft;
        
        blockLogoStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        blockLogoStyle.wordWrap = true;
        blockLogoStyle.fontStyle = FontStyle.Normal;
        blockLogoStyle.padding.left = 20;
        blockLogoStyle.padding.top = 2;
        blockLogoStyle.fontSize = 20;
        blockLogoStyle.normal.textColor = new Color(0, 191 / 255.0f, 245 / 255.0f);
        blockLogoStyle.alignment = TextAnchor.UpperLeft;

        blockTextStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        blockTextStyle.wordWrap = true;
        blockTextStyle.fontStyle = FontStyle.Normal;
        blockTextStyle.fontSize = 12;
        blockTextStyle.padding.left = 20;
        blockTextStyle.padding.top -= 15;
        blockTextStyle.alignment = TextAnchor.UpperLeft;
        blockTextStyle.normal.textColor = Color.black;

        buttonFurther = new GUIStyle(EditorStyles.whiteLargeLabel);
        buttonFurther.normal.textColor = Color.grey;
        buttonFurther.stretchWidth = true;
        buttonFurther.stretchHeight = false;
        buttonFurther.fontSize = 12;
        buttonFurther.alignment = TextAnchor.LowerRight;

        buttonBack = new GUIStyle(EditorStyles.whiteLargeLabel);
        buttonBack.normal.textColor = Color.white;
        buttonBack.stretchWidth = true;
        buttonBack.stretchHeight = false;
        buttonBack.fontSize = 10;
        buttonBack.alignment = TextAnchor.MiddleLeft;
        buttonBack.fixedWidth = 130;

        toggleTextStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        toggleTextStyle.wordWrap = true;
        toggleTextStyle.fontStyle = FontStyle.Bold;
        toggleTextStyle.fontSize = 14;
        toggleTextStyle.padding.left = 10;
        toggleTextStyle.padding.right = 10;
        toggleTextStyle.alignment = TextAnchor.MiddleLeft;
        toggleTextStyle.normal.textColor = Color.black;

        textFieldStyle = new GUIStyle("LargeTextField");
        textFieldStyle.normal.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.active.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.onActive.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.onNormal.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.onHover.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.hover.background = this.MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
        textFieldStyle.normal.textColor = Color.black;
        textFieldStyle.active.textColor = Color.black;
        textFieldStyle.onActive.textColor = Color.black;
        textFieldStyle.onNormal.textColor = Color.black;
        textFieldStyle.onHover.textColor = Color.black;
        textFieldStyle.hover.textColor = Color.black;
        textFieldStyle.alignment = TextAnchor.MiddleLeft;
        textFieldStyle.fixedHeight = 30;
        textFieldStyle.fixedWidth = 0;

        toggle = new GUIStyle(GUIStyle.none);
        toggle.padding.right = 10;
        toggle.padding.bottom = 5;
        toggle.padding.top = 5;
    }

    #endregion

    #region InterfaceHelpers

    private Rect MakeButton(bool variable, Action onCLick) {
        GUIStyle button = new GUIStyle(GUIStyle.none);
        button.alignment = TextAnchor.LowerRight;
        button.padding.right = 20;
        
        GUIStyle onStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        onStyle.wordWrap = true;
        onStyle.fontStyle = FontStyle.Normal;
        onStyle.fontSize = 12;
        onStyle.alignment = TextAnchor.UpperLeft;
        onStyle.normal.textColor = new Color(25 / 255.0f, 205 / 255.0f, 247 / 255.0f);

        GUIStyle offStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
        offStyle.wordWrap = true;
        offStyle.fontStyle = FontStyle.Normal;
        offStyle.fontSize = 12;
        offStyle.alignment = TextAnchor.UpperLeft;
        offStyle.normal.textColor = Color.black;

        Rect buttonPositions = EditorGUILayout.BeginHorizontal(button, GUILayout.Width(70), GUILayout.ExpandWidth(false));
        EditorGUILayout.LabelField("On", variable ? onStyle : offStyle, GUILayout.ExpandWidth(false));
        EditorGUILayout.LabelField("|", offStyle, GUILayout.ExpandWidth(false));
        EditorGUILayout.LabelField("Off", variable ? offStyle : onStyle, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && buttonPositions.Contains(Event.current.mousePosition)) {
            if (onCLick != null) {
                onCLick.Invoke();
            }
        }
        return buttonPositions;
    }

    private void CreateTopSubmenu() {
        EditorGUILayout.BeginVertical(dashboardBack);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        if (GUILayout.Button("← Back to services", buttonBack, GUILayout.ExpandWidth(false))) {
            this.selected = 0;
            if (this.analyticsEnabled) {
                this.AddScript();
                this.SaveData();
            }
        }
        EditorGUILayout.LabelField("", dashboardText, GUILayout.Width(10));
        GUIContent content = new GUIContent();
        content.text = "Go to Site";
        content.image = dashboardImg;
        if (GUILayout.Button(content, dashboardText, GUILayout.ExpandWidth(false))) {
            Application.OpenURL("https://www.devtodev.com/myapps/");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void CreateTopMain() {
        EditorGUILayout.BeginVertical(dashboardBack);
        GUIContent content = new GUIContent();
        content.text = "Go to Site";
        content.image = dashboardImg;
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("", dashboardText);
        if (GUILayout.Button(content, dashboardText, GUILayout.ExpandWidth(false))) {
            Application.OpenURL("https://www.devtodev.com/myapps/");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    #endregion

    #region MainWindow

    private bool SubCreateTop() {
        EditorGUILayout.BeginVertical(topStyle);
        EditorGUILayout.LabelField(PlayerSettings.productName, logoStyle);
        EditorGUILayout.LabelField("devtodev", servicesStyle);
        EditorGUILayout.LabelField("devtodev is a powerful analytical and marketing platform for mobile and web applications. Gather all the data of your application in one simple interface and analyze every bite of it. With devtodev, it is easy to find the weak points, to improve traffic source efficiency and to build strong communications with the customers.",
            mainTextStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndVertical();
        GUILayout.Label(new GUIContent(), splitter);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        Rect blockPosition = EditorGUILayout.BeginVertical(blockBackStyle);

        GUIContent content = new GUIContent();
        content.text = "Analytics";
        content.image = analyticsImg;
        EditorGUILayout.LabelField(content, blockLogoStyle);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("\nKey solution to rule your apps.\n", blockTextStyle);
        Rect buttonAnalyticsRect = this.MakeButton(this.analyticsEnabled, delegate {
            if (!this.IsInjectedScene()) {
                return;
            }
            if (this.EnableAnalytics()) {
                this.AddScript();
            }
            this.SaveData();
        });
        bool analyticsStroken = blockPosition.Contains(Event.current.mousePosition) && !buttonAnalyticsRect.Contains(Event.current.mousePosition);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        GUILayout.Label(new GUIContent(), splitter);
        return analyticsStroken;
    }

    private void SelectMain() {
        this.CreateTopMain();
        bool analyticsStroken = this.SubCreateTop();

        Rect blockPosition = EditorGUILayout.BeginVertical(blockBackStyle);

        GUIContent content = new GUIContent();
        content.text = "Push-notifications";
        content.image = pushImg;
        EditorGUILayout.LabelField(content, blockLogoStyle);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("\nSimple and powerful tool to re-engage your users\n", blockTextStyle);
        Rect buttonPushRect = this.MakeButton(this.pushGroupEnabled, delegate {
            if (!this.IsInjectedScene()) {
                return;
            }
            this.EnablePush();
            this.SaveData();
        });
        bool pushesStroken = blockPosition.Contains(Event.current.mousePosition) && !buttonPushRect.Contains(Event.current.mousePosition);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && analyticsStroken) {
            this.reloadFirstScene();
            if (string.IsNullOrEmpty(this.injectedSceneName) && !this.firstSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
                this.PushSimpleInformationDialog("Please open scene " + this.firstSceneName + " to integrate SDK properly!");
                return;
            }
            if (!this.IsInjectedScene()) {
                return;
            }
            this.selected = 1;
        }

        if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && pushesStroken) {
            this.reloadFirstScene();
            if (string.IsNullOrEmpty(this.injectedSceneName) && !this.firstSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
                this.PushSimpleInformationDialog("Please open scene " + this.firstSceneName + " to integrate SDK properly!");
                return;
            }
            if (!this.IsInjectedScene()) {
                return;
            }
            this.selected = 2;
        }
    }

    #endregion

#region Analytics

    private void SubCreatePlatformsBlock() {
        EditorGUILayout.BeginVertical(topStyle);
        EditorGUILayout.LabelField("Supported Platforms", sideLogoStyle);
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 50, 10, 0) });
        if (GUILayout.Button("Android", platformSelected == 0 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 0;
            GUI.FocusControl("");
        }
        if (GUILayout.Button("iOS", platformSelected == 1 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 1;
            GUI.FocusControl("");
        }
        if (GUILayout.Button("Mac", platformSelected == 2 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 2;
            GUI.FocusControl("");
        }
        if (GUILayout.Button("PC", platformSelected == 3 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 3;
            GUI.FocusControl("");
        }
        if (GUILayout.Button("Web", platformSelected == 4 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 4;
            GUI.FocusControl("");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 170 , 0, 20) });
        if (GUILayout.Button("Windows Store", platformSelected == 5 ? smallPlatformButtonActive : smallPlatformButtonInActive)) {
            platformSelected = 5;
            GUI.FocusControl("");
        }
        EditorGUILayout.EndHorizontal();
    }

    private void CreatePlatformsBlock() { 
        this.SubCreatePlatformsBlock();
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("App key", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, 4, 0) }, GUILayout.Width(70));
        switch (platformSelected) { 
            case 0:
                androidKey = EditorGUILayout.TextField(androidKey, textFieldStyle);
                break;
            case 1:
                iosKey = EditorGUILayout.TextField(iosKey, textFieldStyle);
                break;
            case 2:
                macKey = EditorGUILayout.TextField(macKey, textFieldStyle);
                break;
            case 3:
                winKey = EditorGUILayout.TextField(winKey, textFieldStyle);
                break;
            case 4:
                webKey = EditorGUILayout.TextField(webKey, textFieldStyle);
                break;
            case 5:
                wsKey = EditorGUILayout.TextField(wsKey, textFieldStyle);
                break;
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 5, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("Secret key", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, 5, 0) }, GUILayout.Width(70));
        switch (platformSelected) { 
            case 0:
                androidSecret = EditorGUILayout.TextField(androidSecret, textFieldStyle);
                break;
            case 1:
                iosSecret = EditorGUILayout.TextField(iosSecret, textFieldStyle);
                break;
            case 2:
                macSecret = EditorGUILayout.TextField(macSecret, textFieldStyle);
                break;
            case 3:
                winSecret = EditorGUILayout.TextField(winSecret, textFieldStyle);
                break;
            case 4:
                webSecret = EditorGUILayout.TextField(webSecret, textFieldStyle);
                break;
            case 5:
                wsSecret = EditorGUILayout.TextField(wsSecret, textFieldStyle);
                break;
        }        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void CreateLogBlock() {
        EditorGUILayout.BeginVertical(blockBackStyle);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        GUIStyle logStyle = new GUIStyle(sideLogoStyle);
        logStyle.padding.top -= 25;
        EditorGUILayout.LabelField("Log enabled", logStyle);
        this.MakeButton(this.logEnabled, delegate {
            this.logEnabled = !this.logEnabled;
        });
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    private void SelectAnalytics() {
        this.CreateTopSubmenu();

        EditorGUILayout.BeginVertical(blockBackStyle);
        EditorGUILayout.LabelField("");
        GUIContent content = new GUIContent();
        content.text = "ANALYTICS";
        content.image = analyticsImg;
        EditorGUILayout.LabelField(content, blockLogoStyle);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("\nKey solution to rule your apps.\n", blockTextStyle);
        this.MakeButton(this.analyticsEnabled, delegate {
            if (this.EnableAnalytics()) {
                this.AddScript();
            }
        });
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        GUILayout.Label(new GUIContent(), splitter);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginVertical(topStyle);
        EditorGUILayout.LabelField("Analytics", sideLogoStyle);
        EditorGUILayout.LabelField("devtodev is a powerful all-in-one analytical tool for mobile and web applications. Explore your app metrics in one simple interface that includes teamwork features, game metrics, LTV forecast, and many other cool things.",
            mainTextStyle, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Go to Site", buttonToDashBoard)) {
            Application.OpenURL("https://www.devtodev.com/myapps/");
        }
        EditorGUILayout.EndVertical();

        GUILayout.Label(new GUIContent(), splitter);

        this.CreatePlatformsBlock();

        GUILayout.Label(new GUIContent(), splitter);

        this.CreateLogBlock();
    }

#endregion
    
#region Push-Notifications

    private void SubCreateListenersBlockReceived() { 
        membersReceived.Clear();
        System.Type type = mbScripts[pushListeners].GetType();
        var allMembers = type.GetMethods();
        foreach (var info in allMembers) {
            if (info.ReturnType == typeof(void) && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == typeof(IDictionary<string, string>) && info.DeclaringType == type) {
                membersReceived.Add(info);
            }
        }
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("Push received", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, -3, 0) }, GUILayout.Width(95));
        pushReceived = EditorGUILayout.Popup(pushReceived, membersReceived.Select(x => x.Name).ToArray(), EditorStyles.popup);
        EditorGUILayout.EndHorizontal();
    }

    private void SubCreateListenersBlockOpened() { 
    	membersOpened.Clear();     
        System.Type type = mbScripts[pushListeners].GetType();
        var allMembers = type.GetMethods();   
            foreach (var info in allMembers) {
                if (info.ReturnType == typeof(void) && info.GetParameters().Length == 2 && info.GetParameters()[0].ParameterType == typeof(PushMessage) && info.GetParameters()[1].ParameterType == typeof(ActionButton) && info.DeclaringType == type) {
                    membersOpened.Add(info);
                }
            }
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
            EditorGUILayout.LabelField("Push opened", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, -3, 0) }, GUILayout.Width(95));
            pushOpened = EditorGUILayout.Popup(pushOpened, membersOpened.Select(x => x.Name).ToArray(), EditorStyles.popup);
            EditorGUILayout.EndHorizontal();
    }

    private void SubCreateListenersBlockPushToken() { 
        membersString.Clear();
        System.Type type = mbScripts[pushListeners].GetType();
        var allMembers = type.GetMethods();
        foreach (var info in allMembers) {
            if (info.ReturnType == typeof(void) && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == typeof(string) && info.DeclaringType == type) {
                membersString.Add(info);
            }
        }
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("Push token received", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, -10, 0) }, GUILayout.Width(95));
        tokenReceived = EditorGUILayout.Popup(tokenReceived, membersString.Select(x => x.Name).ToArray(), EditorStyles.popup);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("Push token failed", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, -8, 0) }, GUILayout.Width(95));
        tokenFailed = EditorGUILayout.Popup(tokenFailed, membersString.Select(x => x.Name).ToArray(), EditorStyles.popup);
        EditorGUILayout.EndHorizontal();
    }

    private void CreateListenersBlock() { 
            if (mbScripts == null || mbScripts.Count <= pushListeners) {
                return;
            }
            this.SubCreateListenersBlockPushToken();
            this.SubCreateListenersBlockReceived();
            this.SubCreateListenersBlockOpened();
    }

    private void SelectPush() {
        this.CreateTopSubmenu();

        EditorGUILayout.BeginVertical(blockBackStyle);
        EditorGUILayout.LabelField("");
        GUIContent content = new GUIContent();
        content.text = "PUSH-NOTIFICATIONS";
        content.image = pushImg;
        EditorGUILayout.LabelField(content, blockLogoStyle);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("\nSimple and powerful tool to re-engage your users\n", blockTextStyle);
        this.MakeButton(this.pushGroupEnabled, delegate {
            this.EnablePush();
        });
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        GUILayout.Label(new GUIContent(), splitter);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginVertical(topStyle);
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
        EditorGUILayout.LabelField("Target", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, 0, 0) }, GUILayout.Width(95));
        selectedGO = (GameObject)EditorGUILayout.ObjectField(selectedGO, typeof(GameObject), true, GUILayout.ExpandWidth(true), GUILayout.Height(18));
        EditorGUILayout.EndHorizontal();
        if (selectedGO != null) {
            currentSelectedPushGameObjectName = selectedGO.name;
            this.mbScripts.Clear();
            selectedGO.GetComponents<MonoBehaviour>(mbScripts);
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUIStyle.none) { padding = new RectOffset(20, 20, 0, 0), fixedHeight = 38 });
            EditorGUILayout.LabelField("Script", new GUIStyle(mainTextStyle) { padding = new RectOffset(0, 0, -2, 0) }, GUILayout.Width(95));
            pushListeners = EditorGUILayout.Popup(pushListeners == -1 ? 0 : pushListeners, mbScripts.Select(x => x.GetType().Name).ToArray(), EditorStyles.popup);
            EditorGUILayout.EndHorizontal();
        } else {
            pushListeners = -1;
        }

        if (pushListeners != -1) {
            this.CreateListenersBlock();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

#endregion

#region UpdateScript

    private List<string[]> dialogs = new List<string[]>();

    private bool EnableAnalytics() {
        this.reloadFirstScene();
        if (string.IsNullOrEmpty(this.injectedSceneName) && this.firstSceneName != null && !this.firstSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
            return false;
        }
        this.analyticsEnabled = !this.analyticsEnabled;
        if (!this.analyticsEnabled) { 
            if (devtodevGameObject == null) {
			    devtodevGameObject = GameObject.Find("[devtodev_initialize]");
		    }
		    if (devtodevGameObject != null) {
			    DestroyImmediate(devtodevGameObject);
			    devtodevGameObject = null;
		    }
            this.injectedSceneName = null;
            this.pushGroupEnabled = false;
        }
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        return this.analyticsEnabled;
    }

    private void EnablePush() {
        if (!this.analyticsEnabled) {
            this.PushSimpleInformationDialog("Please eneble analytics first!");
            return;
        }
        this.pushGroupEnabled = !this.pushGroupEnabled;
    }

    void OnGUI () {
        if (!firstOnGUI) {
            this.CreateStyles();
            this.firstOnGUI = true;
            if (selectedGO == null && savedGoName != null) {
                this.ReloadPushListenersData();
                savedGoName = null;
            }
        }
        EditorGUILayout.BeginVertical();
        if (selected == 0) {
            this.SelectMain();
        } else if (selected == 1) {
            this.SelectAnalytics();
        } else if (selected == 2) {
            this.SelectPush();
        }
        EditorGUILayout.EndVertical(); 
		this.Repaint ();
        this.DisplayDialogs();
    }

    public void PushSimpleInformationDialog(string dialogText) {
        lock (dialogs) {
            dialogs.Add(new string[] { "devtodev", dialogText, "OK" });
        }
    }

    private void DisplayDialogs() {
        lock (dialogs) {
            foreach (var dialog in dialogs) {
                EditorUtility.DisplayDialog(dialog[0], dialog[1], dialog[2]);
            }
            dialogs.Clear();
        }
    }

    private void OnUpdate() {
        MonoBehaviour devtodevInit = (MonoBehaviour)devtodevGameObject.GetComponent(System.Type.GetType("com.devtodev.DevToDevIntegration"));
		if (devtodevInit != null) {
            if (pushListeners != -1 && this.mbScripts.Count > pushListeners) {
				devtodevInit.GetType ().GetField ("pushListeners").SetValue (devtodevInit, this.mbScripts [pushListeners]);
			}
			if (membersString != null && membersString.Count > 0) {
				devtodevInit.GetType ().GetField ("onTokenReceived").SetValue (devtodevInit, membersString [tokenReceived].Name);
				devtodevInit.GetType ().GetField ("onTokenFailed").SetValue (devtodevInit, membersString [tokenFailed].Name);
			}
			if (membersReceived != null && membersReceived.Count > 0) {
				devtodevInit.GetType ().GetField ("onPushReceived").SetValue (devtodevInit, membersReceived [pushReceived].Name);
			}
			if (membersOpened != null && membersOpened.Count > 0) {
				devtodevInit.GetType().GetField("onPushOpened").SetValue(devtodevInit, membersOpened[pushOpened].Name);
			}
		}
    }

    void Update () {
		if(needToAttach) {
			waitForCompile -= 0.01f;
			if(waitForCompile <= 0) {
				if(!EditorApplication.isCompiling) {
                    var component = System.Type.GetType("com.devtodev.DevToDevIntegration");
					if (component == null) {
						return;
					}
                    if (devtodevGameObject == null) { 
                        this.AddScript();
                    }
					devtodevGameObject.AddComponent(component);
                    this.OnUpdate();
					needToAttach = false;
					waitForCompile = 1;
				}
			} 
		}
	} 

	public void AddScript(){
        if (string.IsNullOrEmpty(this.injectedSceneName) || this.injectedSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
            if (devtodevGameObject == null) {
			    devtodevGameObject = GameObject.Find("[devtodev_initialize]");
		    }
		    if (devtodevGameObject != null) {
			    DestroyImmediate(devtodevGameObject);
			    devtodevGameObject = null;
		    }
		    devtodevGameObject = new GameObject();
		    devtodevGameObject.name = "[devtodev_initialize]";
        }
		
        TextAsset templateTextFile = AssetDatabase.LoadAssetAtPath("Assets/devtodev/Window/DevToDevIntegration.txt", 
			typeof(TextAsset)) as TextAsset;
		string contents = "";

		if(templateTextFile != null) {
			contents = templateTextFile.text;
			contents = contents.Replace("%@iosKey", iosKey);
			contents = contents.Replace("%@iosSecret", iosSecret);
			contents = contents.Replace("%@androidKey", androidKey);
			contents = contents.Replace("%@androidSecret", androidSecret);
			contents = contents.Replace("%@macKey", macKey);
			contents = contents.Replace("%@macSecret", macSecret);
			contents = contents.Replace("%@winKey", winKey); 
			contents = contents.Replace("%@winSecret", winSecret);
			contents = contents.Replace("%@wsKey", wsKey);
			contents = contents.Replace("%@wsSecret", wsSecret);
			contents = contents.Replace("%@webKey", webKey);
			contents = contents.Replace("%@webSecret", webSecret);
			contents = contents.Replace("%@pushEnabled", pushGroupEnabled.ToString().ToLower());
			contents = contents.Replace("%@logEnabled", logEnabled.ToString().ToLower());
		} else {
            Debug.LogError("Can't find the Plugins/DevToDev/DevToDevIntegration.txt.");
		} 

		using(StreamWriter sw = new StreamWriter(Application.dataPath + "/devtodev/Window/DevToDevIntegration.cs")) {
			sw.Write(contents);
		}
		AssetDatabase.Refresh();
        if (string.IsNullOrEmpty(this.injectedSceneName) || this.injectedSceneName.ToLower().Equals(this.GetCurrentSceneName().ToLower())) {
            needToAttach = true;
            this.injectedSceneName = this.GetCurrentSceneName();
        }
    }

#endregion
}
#endif