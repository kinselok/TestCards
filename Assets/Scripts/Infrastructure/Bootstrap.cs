using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            _game = new Game(_gameStateMachine, this, _sceneLoader);
            _game.Init();
            DontDestroyOnLoad(this);
        }
    }
}