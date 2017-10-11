﻿using UnityEngine;
using System.Collections;
using System.IO;

using UnityEditor;

public class AppodealAssetsPostProcess : AssetPostprocessor {

	#if UNITY_5_5 || UNITY_5_4
	private static string[] Plugins = new string[] {
		"adcolony",
		"appodeal",
		"inmobi",
		"startapp",
		"ogury",
		"applovin",
		"yandex-metrica"
	};
	#endif

	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {

		#if UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_5
		foreach(string importedAsset in importedAssets) {
			if(importedAsset.Equals(importedAssets[importedAssets.Length - 1])) {
				if(Directory.Exists("Assets/Plugins/Android/appodeal/assets") && !Directory.Exists("Assets/Plugins/Android/assets")) {
					if (EditorUtility.DisplayDialog("Appodeal Unity", "We have detected that you're using Unity" + Application.unityVersion + " and it can't compile android assets from library folder so Assets/Plugins/Android/appodeal/assets folder should be moved into Android folder.", "Move it for me", "Leave it there"))
					{
						Directory.Move("Assets/Plugins/Android/appodeal/assets", "Assets/Plugins/Android/assets");
						Directory.Delete("Assets/Plugins/Android/appodeal/assets");
					}
				}
			}
		}
		#endif

		#if UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
		if(File.Exists("Assets/Appodeal/SampleAndroidManifest.xml") && !File.Exists("Assets/Plugins/Android/AndroidManifest.xml")) {
			FileUtil.CopyFileOrDirectory("Assets/Appodeal/SampleAndroidManifest.xml", "Assets/Plugins/Android/AndroidManifest.xml");
		}
		#endif

		#if UNITY_5_5 || UNITY_5_4
		foreach(string plugin in Plugins) {
			string fullpath = "Assets/Plugins/Android/" + plugin;
			if(File.Exists(fullpath)) {
				PluginImporter pluginImporter = AssetImporter.GetAtPath(fullpath) as PluginImporter;
				if(!pluginImporter.GetCompatibleWithPlatform(BuildTarget.Android)) {
					pluginImporter.SetCompatibleWithPlatform(BuildTarget.Android, true);
				}
			}
		}

		string path = "Assets/Plugins/Ionic.Zip.Unity.dll";
		PluginImporter ionicPluginImporter = AssetImporter.GetAtPath(path) as PluginImporter;
		if(!ionicPluginImporter.GetCompatibleWithAnyPlatform()) {
			ionicPluginImporter.SetCompatibleWithAnyPlatform(true);
		}
		#endif

	}
}