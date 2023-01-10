using Scripts.UI.StaticData;
using Scripts.UI.Windows;

namespace Scripts.UI.Infrastructure.Services
{
    public interface IUIStaticDataService
    {
        void Load();
        WindowConfig GetWindowConfig(WindowId forId);
    }
}