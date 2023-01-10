using Scripts.Infrastructure.States;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class StatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<GameExitState>().AsSingle();
            Container.Bind<ResetProgressState>().AsSingle();
            
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}