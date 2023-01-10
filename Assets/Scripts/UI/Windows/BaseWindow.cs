using Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows
{
    public class BaseWindow : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private AppearAnimation appearAnimation;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        private void Start()
        {
            OnStart();
            appearAnimation.Appear();
        }

        protected virtual void OnStart()
        {
            
        }

        private void Close() => 
            appearAnimation.Disappear(()=> 
                Destroy(gameObject));
    }
}