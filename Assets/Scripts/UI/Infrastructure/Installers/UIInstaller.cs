using Scripts.UI.Infrastructure.Services;
using Scripts.UI.Windows;
using Zenject;

namespace Scripts.UI.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIStaticDataService>().To<UIStaticDataService>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
        }
    }
}