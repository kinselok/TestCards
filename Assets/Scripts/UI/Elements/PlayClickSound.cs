using Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Elements
{
    public class PlayClickSound : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }
        
        private void Awake()
        {
            button.onClick.AddListener(_soundService.PlayButtonClick);
        }
    }
}