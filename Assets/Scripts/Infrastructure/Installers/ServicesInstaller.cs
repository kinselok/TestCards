using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.Saving;
using Scripts.Logic;
using Scripts.Sound;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        [SerializeField] private SoundPlayer soundPlayerPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<LoadingScreen>().FromComponentInNewPrefab(loadingScreenPrefab).AsSingle();
            Container.Bind<ISavingService>().To<SavingService>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IAssetBundleProvider>().To<AssetBundleProvider>().AsSingle();
            Container.Bind<ISoundService>().FromComponentInNewPrefab(soundPlayerPrefab).AsSingle();
        }
    }
}