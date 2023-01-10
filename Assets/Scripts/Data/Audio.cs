using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class Audio
    {
        [SerializeField] private bool _music;
        [SerializeField] private bool _fx;

        public event Action OnChanged;
        
        public bool Music
        {
            get => _music;
            set
            {
                _music = value;
                OnChanged?.Invoke();
            }
        }

        public bool FX
        {
            get => _fx;
            set
            {
                _fx = value;
                OnChanged?.Invoke();
            }
        }
    }
}