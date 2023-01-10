namespace Scripts.Infrastructure.States
{
    public interface IInitializableState : IGameState
    {
        void Init(GameStateMachine gameStateMachine);
    }
}