using DG.Tweening;
using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.Saving;
using Scripts.Logic;
using Scripts.Sound;
using Scripts.UI.Infrastructure;
using Scripts.UI.Infrastructure.Services;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IInitializableState, IWarmupProgress
    {
        private const float MinimalDelay = 3f;
        
        private GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IUIFactory _uiFactory;
        private readonly ISavingService _savingService;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IAssetBundleProvider _assetBundleProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly ISoundService _soundService;
        private bool _initialized;

        public LoadLevelState(SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IUIFactory uiFactory,
            ISavingService savingService,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService,
            IAssetBundleProvider assetBundleProvider,
            IAssetProvider assetProvider,
            ISoundService soundService)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _uiFactory = uiFactory;
            _savingService = savingService;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _assetBundleProvider = assetBundleProvider;
            _assetProvider = assetProvider;
            _soundService = soundService;
        }

        public void Init(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _soundService.StopAll();
            _loadingScreen.Show();
            _loadingScreen.SetWarmupProgress(this);
            Cleanup();
            StartMinimalDelay();
            _assetBundleProvider.Warmup(WarmupAssetProvider);
        }

        public void Exit()
        {
            _initialized = false;
            _loadingScreen.Hide();
            _assetProvider.Unload();
            
        }

        public float GetWarmupProgress() => 
            (_assetBundleProvider.GetWarmUpProgress() + _assetProvider.GetWarmUpProgress()) / 2;

        private void StartMinimalDelay()
        {
            DOVirtual.DelayedCall(MinimalDelay, OnDelayEnded);
        }

        private void OnDelayEnded()
        {
            if (_initialized)
                EnterGameLoopState();
            else
                _initialized = true;
        }

        private void WarmupAssetProvider() =>
            _assetProvider
                .Warmup(() => 
                    _sceneLoader.Load(SceneNames.Main, OnSceneLoaded));

        private void Cleanup()
        {
            _assetProvider.CleanUp();
            _assetBundleProvider.CleanUp();
            _savingService.CleanUp();
        }

        private void OnSceneLoaded()
        {
            InitUI();
            InitGameplayObjects();
            LoadOrCreateNewProgress();
            _soundService.Init();
            if (_initialized)
                EnterGameLoopState();
            else
                _initialized = true;
        }

        private void EnterGameLoopState()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void LoadOrCreateNewProgress()
        {
            PlayerProgress progress = _savingService.GetSavedProgress() ?? CreateNewProgress();

            _persistentProgressService.Progress = progress;
            _savingService.LoadProgress();
        }

        private PlayerProgress CreateNewProgress() =>
            new PlayerProgress
            {
                HorizontalBlock = new Data.HorizontalBlock
                {
                    ScrollPosition = 0
                },
                Audio = new Audio
                {
                    Music = true,
                    FX = true
                },
                VerticalBlock = new Data.VerticalBlock()
            };

        private void InitGameplayObjects()
        {
            _gameFactory.CreateHorizontalBlock();
            _gameFactory.CreateVerticalBlock();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();
        }
    }
}