using DG.Tweening;
using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Scripts.Sound
{
    public class SoundPlayer : MonoBehaviour, ISoundService
    {
        private const float FadeDuration = 1f;
        
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioSource fxPlayer;
        
        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService persistentProgressService)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            _persistentProgressService.Progress.Audio.OnChanged += AudioSettingsOnChanged;
            InitPlayers();
        }

        public void PlayMusic()
        {
            if (musicPlayer.isPlaying || !_persistentProgressService.Progress.Audio.Music)
                return;

            musicPlayer.Stop();
            musicPlayer.clip = _staticDataService.GetSoundFor(SoundId.Music).AudioClip;
            musicPlayer.volume = 0;
            musicPlayer.Play();
            musicPlayer.DOFade(1, FadeDuration);
        }


        public void StopAll()
        {
            musicPlayer.Stop();
            fxPlayer.Stop();
        }

        public void PlayDisappear() => 
            PlayFX(SoundId.CardDisappear);

        public void PlayButtonClick() => 
            PlayFX(SoundId.ButtonClick);

        private void AudioSettingsOnChanged()
        {
            if(_persistentProgressService.Progress.Audio.Music)
                musicPlayer.DOFade(1, FadeDuration);
            else
                musicPlayer.DOFade(0, FadeDuration);

            if (_persistentProgressService.Progress.Audio.FX)
                fxPlayer.DOFade(1, FadeDuration);
            else
                fxPlayer.DOFade(0, FadeDuration);

        }

        private void InitPlayers()
        {
            musicPlayer.volume = _persistentProgressService.Progress.Audio.Music ? 1f : 0f;
            fxPlayer.volume = _persistentProgressService.Progress.Audio.FX ? 1f : 0f;
        }

        private void PlayFX(SoundId soundId)
        {
            if (!_persistentProgressService.Progress.Audio.FX)
                return;
            fxPlayer.clip = _staticDataService.GetSoundFor(soundId).AudioClip;
            fxPlayer.Play();
        }
    }
}