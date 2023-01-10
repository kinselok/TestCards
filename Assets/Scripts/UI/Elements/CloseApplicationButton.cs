using Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Elements
{
    public class CloseApplicationButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            button.onClick.AddListener(Close);
        }

        private void Close()
        {
            _gameStateMachine.Enter<GameExitState>();
        }
    }
}