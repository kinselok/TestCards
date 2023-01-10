using Scripts.Data;
using Scripts.Infrastructure.Services.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic
{
    public class VerticalBlock : MonoBehaviour, IContentHolder, ISavedProgress
    {
        private const float ThresholdValueForTrigger = 0.1f;
        [SerializeField] private Transform contentHolder;
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private SaveTrigger saveTrigger;

        private float _lastTriggeredOnValue; 
    
        public Transform ContentHolder => contentHolder;

        private void Awake()
        {
            scroll.onValueChanged.AddListener(OnScrollMoved);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.VerticalBlock.ScrollPosition = scroll.verticalNormalizedPosition;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            scroll.verticalNormalizedPosition = progress.VerticalBlock.ScrollPosition;
            _lastTriggeredOnValue = progress.VerticalBlock.ScrollPosition;
        }

        private void OnScrollMoved(Vector2 arg0)
        {
            if(CanTrigger()) 
                TriggerSave();
        }

        private void TriggerSave()
        {
            saveTrigger.Trigger();
            _lastTriggeredOnValue = scroll.verticalNormalizedPosition;
        }

        private bool CanTrigger()
        {
            return Mathf.Abs(_lastTriggeredOnValue - scroll.verticalNormalizedPosition) > ThresholdValueForTrigger;
        }
    }
}