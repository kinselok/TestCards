using DG.Tweening;
using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.UI.Infrastructure.Services;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IInitializableState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IUIStaticDataService _uiStaticDataService;
        private readonly IStaticDataService _staticDataService;
        private GameStateMachine _gameStateMachine;

        public BootstrapState(SceneLoader sceneLoader,
            IUIStaticDataService uiStaticDataService,
            IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _uiStaticDataService = uiStaticDataService;
            _staticDataService = staticDataService;
        }

        public void Init(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InitStaticDataServices();
            _sceneLoader.Load(SceneNames.Bootstrap, EnterLoadLevelState);
        }

        public void Exit()
        {
            
        }

        private void InitStaticDataServices()
        {
            _uiStaticDataService.Load();
            _staticDataService.Load();
        }

        private void EnterLoadLevelState()
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}