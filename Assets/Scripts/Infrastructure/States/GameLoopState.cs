using Scripts.Sound;
using Scripts.UI.Infrastructure;

namespace Scripts.Infrastructure.States
{
    public class GameLoopState : IGameState
    {
        private readonly IUIFactory _uiFactory;
        private ISoundService _soundService;

        public GameLoopState(IUIFactory uiFactory, ISoundService soundService)
        {
            _soundService = soundService;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            _uiFactory.CreateInfo();
            _soundService.PlayMusic();
        }

        public void Exit()
        {
        
        }
    }
}