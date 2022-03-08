using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyFramework.Modules
{
    public class ResourcesManager : ModuleBase
    {
        private Dictionary<string, AssetBundle> assetBundlesCache; //ab包缓存
        private Dictionary<string, float> assetBundlesRemainTime; //ab包剩余存活时间

        public override void Init()
        {
            base.Init();
            assetBundlesCache = new Dictionary<string, AssetBundle>();
            assetBundlesRemainTime = new Dictionary<string, float>();

            rootAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Definitions.AssetBundle.AssetBundlesPath,
                Definitions.AssetBundle.AssetBundlesDirectory));
            manifest =
                rootAssetBundle.LoadAsset<AssetBundleManifest>(Definitions.AssetBundle.AssetBundleManifest);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Destroy()
        {
            base.Destroy();

            string[] keys = assetBundlesCache.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                TinyDeleteAssetBundle(keys[i]);
            }

            manifest = null;
            rootAssetBundle.Unload(true);
        }


        private float gcRecoveryTime = 30; //GC回收时间间隔
        private float lastGCTime; //上次GC回收的时间
        private float assetBundleSurvivalTime = 30; //ab包存活时间
        private string[] assetBundleKeys;

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Time.frameCount % 2 == 0)
            {
                assetBundleKeys = assetBundlesRemainTime.Keys.ToArray();
                for (int i = 0; i < assetBundleKeys.Length; i++)
                {
                    if (assetBundlesRemainTime[assetBundleKeys[i]] < Time.realtimeSinceStartup)
                    {
                        //超时
                        TinyDeleteAssetBundle(assetBundleKeys[i]);
                    }
                }
            }


            if (Time.realtimeSinceStartup - lastGCTime > gcRecoveryTime)
            {
                lastGCTime = Time.realtimeSinceStartup;
                Resources.UnloadUnusedAssets();
            }
        }

        /// <summary>
        /// 加载指定AB包内指定资源
        /// </summary>
        /// <param name="relativePath">相对AB包总文件夹的路径，不包括文件后缀</param>
        /// <param name="assetName">资源名称</param>
        public T GetFromAssetBundle<T>(string relativePath, string assetName) where T : Object
        {
            relativePath = relativePath.ToLower();
            if (!assetBundlesCache.ContainsKey(relativePath))
            {
                //没有缓存，补齐后缀加载AB包
                LoadAssetBundle(relativePath + Definitions.AssetBundle.AssetBundlesSuffix);
            }
            
            assetBundlesRemainTime[relativePath] = Time.realtimeSinceStartup + assetBundleSurvivalTime;
            return assetBundlesCache[relativePath].LoadAsset<T>(assetName);
        }

        private AssetBundle rootAssetBundle;
        private AssetBundleManifest manifest;

        /// <summary>
        ///加载AB包
        /// </summary>
        /// <param name="relativePath">相对AB包总文件夹的路径，包括文件后缀</param>
        private void LoadAssetBundle(string relativePath)
        {
            string[] dependencies = manifest.GetAllDependencies(relativePath);
            for (int i = 0; i < dependencies.Length; i++)
            {
                LoadAssetBundle(dependencies[i]);
            }

            AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Definitions.AssetBundle.AssetBundlesPath,
                relativePath));

            assetBundlesCache.Add(
                relativePath.Substring(0, relativePath.IndexOf(Definitions.AssetBundle.AssetBundlesSuffix, StringComparison.Ordinal)),
                assetBundle);
            
            assetBundlesRemainTime.Add(
                relativePath.Substring(0, relativePath.IndexOf(Definitions.AssetBundle.AssetBundlesSuffix, StringComparison.Ordinal)),
                Time.realtimeSinceStartup + assetBundleSurvivalTime);
        }

        /// <summary>
        /// 不完全清除AB包
        /// </summary>
        /// <param name="key"></param>
        private void TinyDeleteAssetBundle(string key)
        {
            assetBundlesCache[key].Unload(false);

            assetBundlesCache.Remove(key);
            assetBundlesRemainTime.Remove(key);
        }
    }
}