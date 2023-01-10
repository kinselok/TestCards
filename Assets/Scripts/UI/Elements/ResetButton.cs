using Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Elements
{
    public class ResetButton : MonoBehaviour
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
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            _gameStateMachine.Enter<ResetProgressState>();
        }
    }
}