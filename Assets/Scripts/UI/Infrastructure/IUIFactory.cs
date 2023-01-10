using UnityEngine;

namespace Scripts.UI.Infrastructure
{
    public interface IUIFactory
    {
        void CreateInfo();
        void CreateSettings();
        void CreateUIRoot();
        void CreateHUD();
        Transform UIRoot { get; }
    }
}