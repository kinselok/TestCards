using System;
using System.Collections.Generic;
using System.IO;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public class AssetBundleProvider : IAssetBundleProvider
    {
        private List<AssetBundleCreateRequest> _creationRequests = new List<AssetBundleCreateRequest>();
        private Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle>();
        private Action _onWarmUpComplete;
        
        public void Warmup(Action onComplete)
        {
            _onWarmUpComplete = onComplete;
            RequestAssetBundle(AssetBundleName.cards);
            RequestAssetBundle(AssetBundleName.blocks);
            RequestAssetBundle(AssetBundleName.ui);
        }

        public float GetWarmUpProgress()
        {
            float progress = 0;
            _creationRequests.ForEach(r => progress += r.progress);
            return progress / _creationRequests.Count;
        }

        public AssetBundle GetBundle(AssetBundleName bundleName) => 
            _assetBundles.TryGetValue(bundleName.ToString(), out AssetBundle bundle) 
                ? bundle 
                : null;

        public void CleanUp()
        {
            _creationRequests = new List<AssetBundleCreateRequest>();
            _assetBundles = new Dictionary<string, AssetBundle>();
        }

        private void RequestAssetBundle(AssetBundleName bundleName)
        {
            var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName.ToString()));
            _creationRequests.Add(assetBundleCreateRequest);
            if (!assetBundleCreateRequest.isDone)
                assetBundleCreateRequest.completed += OnLoadCompleted;
            else
                OnLoadCompleted(assetBundleCreateRequest);
        }

        private void OnLoadCompleted(AsyncOperation operation)
        {
            AssetBundle bundle = ((AssetBundleCreateRequest)operation).assetBundle;
            _assetBundles.Add(bundle.name, bundle);
            if(IsWarmupEnded())
                _onWarmUpComplete?.Invoke();
        }

        private bool IsWarmupEnded() => 
            _creationRequests.Count == _assetBundles.Count;
    }
}