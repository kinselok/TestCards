using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.UI.StaticData
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "StaticData/WindowConfig")]
    public class WindowConfig : ScriptableObject
    {
        public WindowId WindowId;
        public string PrefabName;
    }
}