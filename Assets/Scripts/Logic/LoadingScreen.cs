using DG.Tweening;
using Scripts.UI.Elements;
using UnityEngine;

namespace Scripts.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        private const float FadeDuration = 1f;
        
        [SerializeField] private ImageProgressor progressor;
        [SerializeField] private CanvasGroup screen;

        private IWarmupProgress _warmupProgress;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if(_warmupProgress != null)
                UpdateLoadProgress(_warmupProgress.GetWarmupProgress());
        }

        public void SetWarmupProgress(IWarmupProgress warmupProgress)
        {
            _warmupProgress = warmupProgress;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            screen.alpha = 1;
        }

        public void Hide()
        {
            _warmupProgress = null;
            screen
                .DOFade(endValue: 0, FadeDuration)
                .OnComplete(DisableGameObject);
        }

        public void UpdateLoadProgress(float value01)
        {
            progressor.SetValue(value01, max: 1);
        }

        private void DisableGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}