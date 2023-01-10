using DanielLochner.Assets.SimpleScrollSnap;
using Scripts.Data;
using Scripts.Infrastructure.Services.Saving;
using Scripts.Logic;
using Scripts.StaticData;
using Scripts.UI.Infrastructure;
using UnityEngine;
using Zenject;
using HorizontalBlock = Scripts.Logic.HorizontalBlock;
using VerticalBlock = Scripts.Logic.VerticalBlock;

namespace Scripts.Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly DiContainer _diContainer;
        private readonly ISavingService _savingService;
        private readonly IStaticDataService _staticDataService;


        public GameFactory(IAssetProvider assetProvider,
            IUIFactory uiFactory,
            DiContainer diContainer,
            ISavingService savingService,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _diContainer = diContainer;
            _savingService = savingService;
            _staticDataService = staticDataService;
        }

        public GameObject CreateHorizontalBlock()
        {
            HorizontalBlock horizontalBlock = SpawnOnUIRoot(_assetProvider.GetHorizontalBlock());
            SpawnElements(contentHolder: horizontalBlock, config: _staticDataService.GetConfigForBlock(BlockType.Horizontal));
            
            horizontalBlock.GetComponentInChildren<SimpleScrollSnap>().Setup();
            
            SubscribeToSavingService(horizontalBlock.gameObject);
            return horizontalBlock.gameObject;
        }

        public GameObject CreateVerticalBlock()
        {
            VerticalBlock verticalBlock = SpawnOnUIRoot(_assetProvider.GetVerticalBlock());
            SpawnElements(contentHolder: verticalBlock, config: _staticDataService.GetConfigForBlock(BlockType.Vertical));
            SubscribeToSavingService(verticalBlock.gameObject);
            return verticalBlock.gameObject;
        }

        private T SpawnOnUIRoot<T>(T prefab) where T: Object => 
            _diContainer.InstantiatePrefabForComponent<T>(prefab, _uiFactory.UIRoot);

        private void SubscribeToSavingService(GameObject horizontalBlockGameObject)
        {
            foreach (ISavedProgress savedProgress in
                     horizontalBlockGameObject.GetComponentsInChildren<ISavedProgress>())
            {
                _savingService.Subscribe(savedProgress);
            }
        }

        private void SpawnElements(IContentHolder contentHolder, BlockConfig config)
        {
            foreach (BlockElementsConfig elementConfig in config.ElementConfigs)
                for (int i = 0; i < elementConfig.Amount; i++)
                    _diContainer.InstantiatePrefab(_assetProvider.GetCard(elementConfig.CardName), contentHolder.ContentHolder);
        }
    }
}