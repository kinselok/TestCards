using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public interface IAssetBundleProvider
    {
        void Warmup(Action onComplete);
        float GetWarmUpProgress();
        AssetBundle GetBundle(AssetBundleName bundleName);
        void CleanUp();
    }
}