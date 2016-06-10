using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class GameSettingsManager {
    private static IDictionary<string, object> instances = new Dictionary<string, object>();

    private static T GetInstance<T>(string relativePath) where T : ScriptableObject {
        object instance;
        if (!instances.TryGetValue(relativePath, out instance) || instance == null) {
            instance = Resources.Load(Constants.GameSettingsRelativePath + relativePath) as T;
            if (instance == null) {
                instance = CreateNewDefault<T>(relativePath);
                instances.Add(relativePath, instance);
            }
            else {
                instances.Add(relativePath, instance);
            }
        }
        return instance as T;
    }

    private static T CreateNewDefault<T>(string relativePath) where T : ScriptableObject {
        T instance = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
        string resourcesPath = Path.Combine(Constants.ResourcesPath, Constants.GameSettingsRelativePath);
        string assetPath = string.Concat(relativePath, Constants.AssetsExtension);
        string fullPath = Path.Combine(resourcesPath, assetPath);
        AssetDatabase.CreateAsset(instance as ScriptableObject, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
        return instance as T;
    }

    public static GameConstants GetGameConstants() {
        return GetInstance<GameConstants>(Constants.GameConstantsAssetName);
    }

#if UNITY_EDITOR
    [MenuItem(Constants.GameSettingsMenuName + "/" + Constants.GameConstantsAssetName)]
    private static void DisplayGameSettingsData() {
        Selection.activeObject = GetGameConstants();
    }
#endif

}