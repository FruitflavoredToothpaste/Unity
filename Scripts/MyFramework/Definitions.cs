using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definitions
{
    public static class AssetBundle
    {
        /// <summary>
        /// AB包最上级文件夹名称（仅名称，无后缀前缀）
        /// </summary>
        public static readonly string AssetBundlesDirectory = "AssetBundles";

        /// <summary>
        /// AB包后缀
        /// </summary>
        public static readonly string AssetBundlesSuffix = ".unity3d";

        /// <summary>
        /// AB包最上级文件夹绝对路径
        /// </summary>
        public static readonly string AssetBundlesPath = Application.streamingAssetsPath + "/" + AssetBundlesDirectory;
        
        /// <summary>
        /// AB包manifest字符串
        /// </summary>
        public static readonly string AssetBundleManifest = "AssetBundleManifest";
    }

    public static class Path
    {
    }

    public static class FileName
    {
    }
}