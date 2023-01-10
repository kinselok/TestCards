using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.UI.Windows;
using UnityEngine;
using HorizontalBlock = Scripts.Logic.HorizontalBlock;
using VerticalBlock = Scripts.Logic.VerticalBlock;

namespace Scripts.Infrastructure.Services
{
    public class AssetProvider : IAssetProvider
    {
        private const string HorizontalBlockName = "HorizontalBlock";
        private const string VerticalBlockName = "VerticalBlock";
        private const string UIRoot = "UIRoot";
        private const string HUD = "HUD";
        
        private readonly IAssetBundleProvider _assetBundleProvider;
        private Dictionary<AssetBundleName, AssetBundleWrapper> _loadedBundles = new Dictionary<AssetBundleName, AssetBundleWrapper>();
        private Action _onWarmupCompleted;

        public AssetProvider(IAssetBundleProvider assetBundleProvider)
        {
            _assetBundleProvider = assetBundleProvider;
        }

        public void Warmup(Action onComplete)
        {
            _onWarmupCompleted = onComplete;
            WarmUpBundle(AssetBundleName.cards);
            WarmUpBundle(AssetBundleName.blocks);
            WarmUpBundle(AssetBundleName.ui);
        }

        public float GetWarmUpProgress()
        {
            float progress = 0f;
            _loadedBundles.Values.ToList().ForEach(w => progress += w.GetWarmUpProgress());
            return progress / _loadedBundles.Count;
        }

        public HorizontalBlock GetHorizontalBlock() => 
            (_loadedBundles[AssetBundleName.blocks].GetAsset(HorizontalBlockName) as GameObject)
            .GetComponent<HorizontalBlock>();

        public VerticalBlock GetVerticalBlock() => 
            (_loadedBundles[AssetBundleName.blocks].GetAsset(VerticalBlockName) as GameObject)
            .GetComponent<VerticalBlock>();

        public GameObject GetCard(string name) => 
            (_loadedBundles[AssetBundleName.cards].GetAsset(name) as GameObject);

        public GameObject GetHUD() => 
            (_loadedBundles[AssetBundleName.ui].GetAsset(HUD) as GameObject);
        
        public GameObject GetUIRoot() => 
            (_loadedBundles[AssetBundleName.ui].GetAsset(UIRoot) as GameObject);

        public BaseWindow GetWindow(string prefabName) =>
            (_loadedBundles[AssetBundleName.ui].GetAsset(prefabName) as GameObject)
            .GetComponent<BaseWindow>();

        public void Unload(bool unloadAllLoadedObjects = false) => 
            _loadedBundles
                .Values.ToList()
                .ForEach(w =>
                    w.Unload(unloadAllLoadedObjects));

        public void CleanUp()
        {
            Unload(true);
            _loadedBundles = new Dictionary<AssetBundleName, AssetBundleWrapper>();
        }

        private void WarmUpBundle(AssetBundleName assetBundleName)
        {
            var assetBundleWrapper = new AssetBundleWrapper(_assetBundleProvider.GetBundle(assetBundleName));
            assetBundleWrapper.Warmup(OnBundleLoaded);
            _loadedBundles.Add(assetBundleName, assetBundleWrapper);
        }

        private void OnBundleLoaded()
        {
            if(AllBundlesLoaded())
                _onWarmupCompleted?.Invoke();
        }

        private bool AllBundlesLoaded() => _loadedBundles
            .Values.
            ToList()
            .TrueForAll(w => 
                w.IsWarmupCompleted);
    }
}