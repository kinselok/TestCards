using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class GameExitState : IGameState
    {
        public void Enter()
        {
            Application.Quit();
        }

        public void Exit()
        {
            
        }
    }
}