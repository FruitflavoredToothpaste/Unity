using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetsBundle : Editor
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        if (!Directory.Exists(Definitions.AssetBundle.AssetBundlesPath))
        {
            Directory.CreateDirectory(Definitions.AssetBundle.AssetBundlesPath);
        }

        BuildPipeline.BuildAssetBundles(Definitions.AssetBundle.AssetBundlesPath, BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
        Debug.Log("Build AssetBundles Succeeded!");
    }
}