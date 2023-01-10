using System;
using Scripts.Logic;
using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public interface IAssetProvider
    {
        void Warmup(Action onComplete);
        float GetWarmUpProgress();
        HorizontalBlock GetHorizontalBlock();
        VerticalBlock GetVerticalBlock();
        GameObject GetCard(string name);
        void Unload(bool unloadAllLoadedObjects = false);
        void CleanUp();
        GameObject GetHUD();
        GameObject GetUIRoot();
        BaseWindow GetWindow(string prefabName);
    }
}