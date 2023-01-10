using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;

namespace Scripts.Infrastructure.Services.Saving
{
    public interface ISavedProgress
    {
        void UpdateProgress(PlayerProgress progress);
        void LoadProgress(PlayerProgress progress);
    }
}