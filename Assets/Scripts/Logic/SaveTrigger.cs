using Scripts.Infrastructure.Services.Saving;
using UnityEngine;
using Zenject;

namespace Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISavingService _savingService;

        [Inject]
        private void Construct(ISavingService savingService)
        {
            _savingService = savingService;
        }

        public void Trigger()
        {
            _savingService.SaveProgress();
        }
    }
}