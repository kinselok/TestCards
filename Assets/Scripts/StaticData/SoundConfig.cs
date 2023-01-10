using Scripts.Data;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/SoundConfig", fileName = "SoundConfig", order = 0)]
    public class SoundConfig : ScriptableObject
    {
        public SoundId SoundId;
        public AudioClip AudioClip;
    }
}