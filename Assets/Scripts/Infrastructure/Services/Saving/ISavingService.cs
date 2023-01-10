using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;

namespace Scripts.Infrastructure.Services.Saving
{
    public interface ISavingService
    {
        void SaveProgress();
        void LoadProgress();
        void Subscribe(ISavedProgress savedProgress);
        void CleanUp();
        PlayerProgress GetSavedProgress();
    }
}