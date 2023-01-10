using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure
{
    public class AssetBundleWrapper
    {
        private readonly AssetBundle _assetBundle;
        private List<AssetBundleRequest> _loadRequests = new List<AssetBundleRequest>();
        private Dictionary<string, Object> _loadedAssets = new Dictionary<string, Object>();
        private Action _onWarmUpComplete;

        public bool IsWarmupCompleted { get; private set; }

        public AssetBundleWrapper(AssetBundle assetBundle)
        {
            _assetBundle = assetBundle;
        }
        
        public void Warmup(Action onCompleted)
        {
            _onWarmUpComplete = onCompleted;
            var paths = GetAllAssetNames();
            if (paths.Length < 1)
            {
                IsWarmupCompleted = true;
                _onWarmUpComplete?.Invoke();
                return;
            }

            foreach (string path in paths)
            {
                var assetBundleRequest = _assetBundle.LoadAssetAsync(path);
                _loadRequests.Add(assetBundleRequest);
                if (!assetBundleRequest.isDone)
                    assetBundleRequest.completed += OnLoadCompleted;
                else
                    OnLoadCompleted(assetBundleRequest);
            }
        }
        
        public float GetWarmUpProgress()
        {
            float progress = 0;
            _loadRequests.ForEach(r => progress += r.progress);
            return progress / _loadRequests.Count;
        }

        public Object GetAsset(string name) => 
            _loadedAssets.TryGetValue(name, out Object asset) 
                ? asset
                : null;

        public void Unload(bool unloadAllLoadedObjects = false) => 
            _assetBundle.Unload(unloadAllLoadedObjects);

        private void OnLoadCompleted(AsyncOperation operation)
        {
            Object asset = ((AssetBundleRequest)operation).asset;
            _loadedAssets.Add(asset.name, asset);
            if(IsWarmupEnded())
            {
                IsWarmupCompleted = true;
                _onWarmUpComplete?.Invoke();
            }
        }
        
        private bool IsWarmupEnded() => 
            _loadRequests.Count == _loadedAssets.Count;
        
        private string[] GetAllAssetNames() => 
            _assetBundle.GetAllAssetNames();
    }
}