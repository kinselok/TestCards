using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.Saving;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Windows
{
    public class SettingsWindow : BaseWindow, ISavedProgress
    {
        [SerializeField] private Toggle music;
        [SerializeField] private Toggle fx;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        protected override void OnStart()
        {
            LoadProgress(_persistentProgressService.Progress);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Audio.Music = music.isOn;
            progress.Audio.FX = fx.isOn;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            music.SetIsOnWithoutNotify(progress.Audio.Music);
            fx.SetIsOnWithoutNotify(progress.Audio.FX);
        }
    }
}