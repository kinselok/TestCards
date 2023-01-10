using System.Collections.Generic;
using System.Linq;
using Scripts.UI.StaticData;
using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.UI.Infrastructure.Services
{
    public class UIStaticDataService : IUIStaticDataService
    {
        private const string WindowConfigsPath = "StaticData/UI/Windows";
        private Dictionary<WindowId, WindowConfig> _windows;
        
        public void Load()
        {
            _windows = Resources
                .LoadAll<WindowConfig>(WindowConfigsPath)
                .ToDictionary(w => w.WindowId);
        }

        public WindowConfig GetWindowConfig(WindowId forId) =>
            _windows.TryGetValue(forId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
        
    }
}