#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DTDEditor {
    internal class DTDVisualEditorStyles {
        private GUIStyle onStateStyle;
        public GUIStyle OnStateStyle {
            get {
                if (onStateStyle == null) {
                    onStateStyle = CreateStateStyle(new Color(0.098f, 0.8f, 0.97f));
                }
                return onStateStyle;
            }
        }
        private GUIStyle offStateStyle;
        public GUIStyle OffStateStyle {
            get {
                if (offStateStyle == null) {
                    offStateStyle = CreateStateStyle(Color.black);
                }
                return offStateStyle;
            }
        }
        private GUIStyle backButtonStyle;
        public GUIStyle ButtonBackStyle {
            get {
                if (backButtonStyle == null) {
                    backButtonStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
                    backButtonStyle.normal.textColor = Color.white;
                    backButtonStyle.stretchWidth = false;
                    backButtonStyle.stretchHeight = false;
                    backButtonStyle.fontSize = 10;
                    backButtonStyle.alignment = TextAnchor.MiddleLeft;
                    backButtonStyle.fixedWidth = 130;
                }
                return backButtonStyle;
            }
        }
        private GUIStyle splitterStyle;
        public GUIStyle SplitterStyle {
            get {
                if (splitterStyle == null) {
                    splitterStyle = new GUIStyle();
                    splitterStyle.normal.background = MakeTex(1, 1, new Color(197 / 255.0f, 197 / 255.0f, 197 / 255.0f));
                    splitterStyle.stretchWidth = true;
                    splitterStyle.fixedHeight = 2;
                    splitterStyle.fixedWidth = 0;
                }
                return splitterStyle;
            }
        }
        private GUIStyle dashboardBackStyle;
        public GUIStyle DashboardBackStyle {
            get {
                if (dashboardBackStyle == null) {
                    dashboardBackStyle = new GUIStyle("LockedHeaderBackground");
                    dashboardBackStyle.normal.background = MakeTex(1, 1, new Color(0.35f, 0.37f, 0.38f));
                    dashboardBackStyle.padding = new RectOffset(0, 0, -1, -1);
                }
                return dashboardBackStyle;
            }
        }
        private GUIStyle blockBackStyle;
        public GUIStyle BlockBackStyle {
            get {
                if (blockBackStyle == null) {
                    blockBackStyle = new GUIStyle("LockedHeaderBackground");
                    blockBackStyle.normal.background = MakeTex(1, 1, new Color(0.95f, 0.95f, 0.95f));
                }
                return blockBackStyle;
            }
        }
        private GUIStyle blockLogoStyle;
        public GUIStyle BlockLogoStyle {
            get {
                if (blockLogoStyle == null) {
                    blockLogoStyle = TextStyle(20, new int[]{20,2,0,0}, new Color(0, 0.75f, 0.96f));
                }
                return blockLogoStyle;
            }
        }
        private GUIStyle mainTextStyle;
        public GUIStyle MainTextStyle {
            get {
                if (mainTextStyle == null) {
                    mainTextStyle = TextStyle(12, new int[]{20,0,50,25}, Color.black);
                }
                return mainTextStyle;
            }
        }
        private GUIStyle blockTextStyle;
        public GUIStyle BlockTextStyle {
            get {
                if (blockTextStyle == null) {
                    blockTextStyle = TextStyle(12, new int[]{20,-15,0,0}, Color.black);
                }
                return blockTextStyle;
            }
        }
        private GUIStyle logoTextStyle;
        public GUIStyle LogoTextStyle {
            get {
                if (logoTextStyle == null) {
                    logoTextStyle = TextStyle(26, new int[]{20,0,0,0}, Color.black);
                }
                return logoTextStyle;
            }
        }
        private GUIStyle servicesTextStyle;
        public GUIStyle ServicesStyle {
            get {
                if (servicesTextStyle == null) {
                    servicesTextStyle = TextStyle(18, new int[]{20,-5,0,0}, Color.black);
                }
                return servicesTextStyle;
            }
        }
        private GUIStyle sideLogoStyle;
        internal GUIStyle SideLogoStyle {
            get {
                if (sideLogoStyle == null) {
                    sideLogoStyle = TextStyle(22, new int[]{20,20,0,0}, Color.black);
                }
                return sideLogoStyle;
            }
        }
        private GUIStyle dashboardTextStyle;
        public GUIStyle DashboardTextStyle {
            get {
                if (dashboardTextStyle == null) {
                    dashboardTextStyle = new GUIStyle(GUI.skin.label);
                    dashboardTextStyle.wordWrap = true;
                    dashboardTextStyle.fontStyle = FontStyle.Normal;
                    dashboardTextStyle.fontSize = 10;
                    dashboardTextStyle.imagePosition = ImagePosition.ImageLeft;
                    dashboardTextStyle.normal.textColor = Color.white;
                    dashboardTextStyle.alignment = TextAnchor.MiddleRight;
                    dashboardTextStyle.fixedWidth = 130;
                }
                return dashboardTextStyle;
            }
        }
        private GUIStyle platformInactiveStyle;
        private GUIStyle PlatformInactiveStyle {
            get {
                if (platformInactiveStyle == null) {
                    platformInactiveStyle = BigButtonStyle(false, new int[]{0,0,5,2}, -1, 22);
                }
                return platformInactiveStyle;
            }
        }
        private GUIStyle platformActiveStyle;
        private GUIStyle PlatformActiveStyle {
            get {
                if (platformActiveStyle == null) {
                    platformActiveStyle = BigButtonStyle(true, new int[]{0,0,5,2}, -1, 22);
                }
                return platformActiveStyle;
            }
        }
        private GUIStyle toDashboardButtonStyle;
        public GUIStyle ToDashboardButtonStyle {
            get {
                if (toDashboardButtonStyle == null) {
                    toDashboardButtonStyle = BigButtonStyle(true, new int[]{27,0,0,30}, 140, 35);
                }
                return toDashboardButtonStyle;
            }
        }
        private GUIStyle textFieldStyle;
        public GUIStyle TextFieldStyle {
            get {
                if (textFieldStyle == null) {
                    Texture2D texture = MakeTex(1, 1, new Color(250 / 255.0f, 251 / 255.0f, 253 / 255.0f));
                    textFieldStyle = new GUIStyle("LargeTextField");
                    textFieldStyle.normal.background = texture;
                    textFieldStyle.active.background = texture;
                    textFieldStyle.onActive.background = texture;
                    textFieldStyle.onNormal.background = texture;
                    textFieldStyle.onHover.background = texture;
                    textFieldStyle.hover.background = texture;
                    textFieldStyle.normal.textColor = Color.black;
                    textFieldStyle.active.textColor = Color.black;
                    textFieldStyle.onActive.textColor = Color.black;
                    textFieldStyle.onNormal.textColor = Color.black;
                    textFieldStyle.onHover.textColor = Color.black;
                    textFieldStyle.hover.textColor = Color.black;
                    textFieldStyle.alignment = TextAnchor.MiddleLeft;
                    textFieldStyle.fixedHeight = 30;
                    textFieldStyle.fixedWidth = 0;
                }
                return textFieldStyle;
            }
        }
        private GUIStyle topStyle;
        public GUIStyle TopStyle {
            get {
                if (topStyle == null) {
                    topStyle = new GUIStyle("LockedHeaderBackground");
                    topStyle.normal.background = MakeTex(1, 1, new Color(0.89f, 0.898f, 0.89f));
                }
                return topStyle;
            }
        }

        private GUIStyle blockPlatformStyle;
        public GUIStyle BlockPlatformStyle {
            get {
                if (blockPlatformStyle == null) {
                    blockPlatformStyle = new GUIStyle(GUIStyle.none);
                    blockPlatformStyle.padding = new RectOffset(20, 20, 0, 0);
                    blockPlatformStyle.fixedHeight = 38;
                    blockPlatformStyle.normal.background = MakeTex(1, 1, new Color(0.89f, 0.898f, 0.89f));
                }
                return blockPlatformStyle;
            }
        }

        //==================================================================================
            
        internal Rect MakeButton(bool isOn, Action onClickAction) {
            GUIStyle button = new GUIStyle(GUIStyle.none);
            button.alignment = TextAnchor.LowerRight;
            button.padding.right = 20;

            Rect buttonPositions = EditorGUILayout.BeginHorizontal(button, GUILayout.Width(70), GUILayout.ExpandWidth(false));
            EditorGUILayout.LabelField("On", isOn ? OnStateStyle : OffStateStyle, GUILayout.ExpandWidth(false));
            EditorGUILayout.LabelField("|", OffStateStyle, GUILayout.ExpandWidth(false));
            EditorGUILayout.LabelField("Off", !isOn ? OnStateStyle : OffStateStyle, GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && buttonPositions.Contains(Event.current.mousePosition)) {
                if (onClickAction != null) {
                    onClickAction.Invoke();
                }
            }
            return buttonPositions;
        }

        internal GUIStyle PlatformButtonStyle(bool isActivePlatform) {
            if (isActivePlatform) {
                return PlatformActiveStyle;
            } else {
                return PlatformInactiveStyle;
            }
        }

        private GUIStyle CreateStateStyle(Color color) {
            GUIStyle style = new GUIStyle(EditorStyles.whiteLargeLabel);
            style.wordWrap = true;
            style.fontStyle = FontStyle.Normal;
            style.fontSize = 12;
            style.alignment = TextAnchor.UpperLeft;
            style.normal.textColor = color;
            return style;
        }

        private GUIStyle BigButtonStyle(bool isActive, int [] paddings, int width, int height) {
            GUIStyle buttonStyle = new GUIStyle("Button");

            Texture2D backgroundTexture;
            Color textColor;
            if (isActive) {
                backgroundTexture = MakeTex(1, 1, new Color(0.0f, 0.75f, 0.96f));
                textColor = Color.white;
            } else {
                backgroundTexture = MakeTex(1, 1, Color.white);
                textColor = Color.black;
            }

            buttonStyle.normal.background = backgroundTexture;
            buttonStyle.active.background = backgroundTexture;
            buttonStyle.onActive.background = backgroundTexture;
            buttonStyle.onNormal.background = backgroundTexture;
            buttonStyle.onHover.background = backgroundTexture;
            buttonStyle.hover.background = backgroundTexture;
            buttonStyle.normal.textColor = textColor;
            buttonStyle.active.textColor = textColor;
            buttonStyle.onActive.textColor = textColor;
            buttonStyle.onNormal.textColor = textColor;
            buttonStyle.onHover.textColor = textColor;
            buttonStyle.hover.textColor = textColor;
            buttonStyle.margin.left = paddings[0];
            buttonStyle.margin.top = paddings[1];
            buttonStyle.margin.right = paddings[2];
            buttonStyle.margin.bottom = paddings[3];
            if (width >= 0) {
                buttonStyle.fixedWidth = width;
            }
            if (height >= 0) {
                buttonStyle.fixedHeight = height;
            }

            return buttonStyle;
        }

        private GUIStyle TextStyle(int fontSize, int[] paddings, Color color) {
            GUIStyle style = new GUIStyle(EditorStyles.whiteLargeLabel);
            style.wordWrap = true;
            style.fontStyle = FontStyle.Normal;
            style.fontSize = fontSize;
            style.padding.left = paddings[0];
            style.padding.top = paddings[1];
            style.padding.right = paddings[2];
            style.padding.bottom = paddings[3];
            style.normal.textColor = color;
            style.alignment = TextAnchor.UpperLeft;
            return style;
        }

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
    }
}
#endif