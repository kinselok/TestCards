using System.Collections.Generic;
using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Saving
{
    public class SavingService : ISavingService
    {
        private const string ProgressKey = "Progress";
    
        private readonly IPersistentProgressService _progressService;
        private readonly List<ISavedProgress> subsribers = new List<ISavedProgress>();

        public SavingService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in subsribers)
                progressWriter.UpdateProgress(_progressService.Progress);
      
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public void LoadProgress()
        {
            foreach (ISavedProgress progressReaders in subsribers)
                progressReaders.LoadProgress(_progressService.Progress);
        }

        public void Subscribe(ISavedProgress savedProgress)
        {
            subsribers.Add(savedProgress);
        }

        public void CleanUp()
        {
            subsribers.Clear();
        }

        public PlayerProgress GetSavedProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }
    }
}