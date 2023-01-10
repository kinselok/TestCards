using DanielLochner.Assets.SimpleScrollSnap;
using Scripts.Data;
using Scripts.Infrastructure.Services.Saving;
using UnityEngine;

namespace Scripts.Logic
{
    public class HorizontalBlock : MonoBehaviour, ISavedProgress, IContentHolder
    {
        [SerializeField] private SimpleScrollSnap scroll;
        [SerializeField] private Transform contentHolder;

        public Transform ContentHolder => contentHolder;


        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HorizontalBlock.ScrollPosition = scroll.CenteredPanel;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            scroll.StartingPanel = (progress.HorizontalBlock.ScrollPosition);
            scroll.Setup();
        }
    }
}