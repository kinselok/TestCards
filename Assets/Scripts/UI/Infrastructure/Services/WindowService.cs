using Scripts.UI.Windows;

namespace Scripts.UI.Infrastructure.Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.None:
                    break;
                case WindowId.Info:
                    _uiFactory.CreateInfo();
                    break;
                case WindowId.Settings:
                    _uiFactory.CreateSettings();
                    break;
            }
        }
    }
}