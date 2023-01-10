using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();

        private IGameState _currentState;

        public GameStateMachine(BootstrapState bootstrapState,
            LoadLevelState loadLevelState,
            GameLoopState gameLoopState,
            GameExitState gameExitState,
            ResetProgressState resetProgressState)
        {
            _states.Add(bootstrapState.GetType(), bootstrapState);
            _states.Add(loadLevelState.GetType(), loadLevelState);
            _states.Add(gameLoopState.GetType(), gameLoopState);
            _states.Add(gameExitState.GetType(), gameExitState);
            _states.Add(resetProgressState.GetType(), resetProgressState);
            
            InitStates();
        }

        public void Enter<TState>() where TState: class, IGameState
        {
            var state = ChangeCurrentStateTo<TState>();
            state.Enter();
        }

        private void InitStates()
        {
            IEnumerable<IInitializableState> initializableStates = _states
                .Values
                .Where(s => s is IInitializableState)
                .Cast<IInitializableState>();
            
            foreach (IInitializableState initializable in initializableStates) 
                initializable.Init(this);
        }

        private TState ChangeCurrentStateTo<TState>() where TState : class, IGameState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState: class, IGameState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}