using Scripts.Data;
using Scripts.Infrastructure.Services.Saving;
using Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic
{
    public class VerticalBlockCard : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Button button;
        [SerializeField] private SaveTrigger saveTrigger;
        [SerializeField] private GameObject disableFX;
        
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }

        private void Awake()
        {
            button.onClick.AddListener(OnClicked);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if(!gameObject.activeSelf)
                progress.VerticalBlock.AddRemovedCard(GetId());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            gameObject.SetActive(!progress.VerticalBlock.IsRemoved(GetId()));
        }

        private void OnClicked()
        {
            gameObject.SetActive(false);
            Instantiate(disableFX, transform.position, Quaternion.identity);
            saveTrigger.Trigger();
            _soundService.PlayDisappear();
        }

        private int GetId() => 
            transform.GetSiblingIndex();
    }
}