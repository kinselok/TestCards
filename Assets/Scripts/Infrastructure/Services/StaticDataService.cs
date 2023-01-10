using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string BlockConfigPath = "StaticData/BlockConfig";
        private const string SoundConfigPath = "StaticData/Sound";
        
        private Dictionary<BlockType, BlockConfig> _blockConfigs;
        private Dictionary<SoundId, SoundConfig> _soundConfigs;

        public void Load()
        {
            _blockConfigs = Resources.LoadAll<BlockConfig>(BlockConfigPath).ToDictionary(b => b.Type);
            _soundConfigs = Resources.LoadAll<SoundConfig>(SoundConfigPath).ToDictionary(b => b.SoundId);
        }

        public BlockConfig GetConfigForBlock(BlockType blockType) =>
            _blockConfigs.TryGetValue(blockType, out BlockConfig config)
                ? config
                : null;

        public SoundConfig GetSoundFor(SoundId soundId) =>
            _soundConfigs.TryGetValue(soundId, out SoundConfig config)
                ? config
                : null;
    }
}