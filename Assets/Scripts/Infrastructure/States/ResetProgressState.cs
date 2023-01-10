using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.Saving;

namespace Scripts.Infrastructure.States
{
    public class ResetProgressState : IInitializableState
    {
        private readonly ISavingService _savingService;
        private readonly IPersistentProgressService _persistentProgressService;
        private GameStateMachine _gameStateMachine;

        public ResetProgressState(ISavingService savingService, IPersistentProgressService persistentProgressService)
        {
            _savingService = savingService;
            _persistentProgressService = persistentProgressService;
        }

        public void Init(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            PlayerProgress playerProgress = _persistentProgressService.Progress;
            playerProgress.HorizontalBlock = new HorizontalBlock();
            playerProgress.VerticalBlock = new VerticalBlock();
            
            _savingService.LoadProgress();
        }

        public void Exit()
        {
            
        }
    }
}