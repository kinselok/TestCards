using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Saving;
using Scripts.UI.Infrastructure.Services;
using Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Infrastructure
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IUIStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly ISavingService _savingService;

        private Transform _uiRoot;
        public Transform UIRoot => _uiRoot;
        
        public UIFactory(DiContainer container,
            IUIStaticDataService staticDataService,
            IAssetProvider assetProvider,
            ISavingService savingService)
        {
            _container = container;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _savingService = savingService;
        }
        
        public void CreateInfo() => 
            SpawnOnUIRoot(_assetProvider.GetWindow(GetWindowPrefabName(WindowId.Info)));

        public void CreateSettings() => 
            SpawnOnUIRoot(_assetProvider.GetWindow(GetWindowPrefabName(WindowId.Settings)));

        public void CreateHUD() => 
            SpawnOnUIRoot(_assetProvider.GetHUD());


        public void CreateUIRoot()
        {
            _uiRoot = _container.InstantiatePrefab(_assetProvider.GetUIRoot(), Camera.main.transform.root).transform;
            _uiRoot.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        private void SpawnOnUIRoot<T>(T prefab) where T: Object
        {
            GameObject instance = _container.InstantiatePrefab(prefab, parentTransform: _uiRoot);
            SubscribeToSavingService(instance);
        }

        private void SubscribeToSavingService(GameObject instance)
        {
            foreach (ISavedProgress saveable in instance.GetComponentsInChildren<ISavedProgress>())
                _savingService.Subscribe(saveable);
        }

        private string GetWindowPrefabName(WindowId windowId) => 
            _staticDataService.GetWindowConfig(windowId).PrefabName;
    }
}