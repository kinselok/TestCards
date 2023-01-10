using Scripts.Data;
using Scripts.StaticData;

namespace Scripts.Infrastructure.Services
{
    public interface IStaticDataService
    {
        BlockConfig GetConfigForBlock(BlockType blockType);
        SoundConfig GetSoundFor(SoundId soundId);
        void Load();
    }
}