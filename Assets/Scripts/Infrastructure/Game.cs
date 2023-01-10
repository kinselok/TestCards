using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.States;

namespace Scripts.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;

        public Game(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
        }

        public void Init()
        {
            _sceneLoader.Init(_coroutineRunner);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}