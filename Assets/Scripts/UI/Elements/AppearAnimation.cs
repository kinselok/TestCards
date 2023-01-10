using DG.Tweening;
using UnityEngine;

namespace Scripts.UI.Elements
{
    public class AppearAnimation : MonoBehaviour
    {
        private const float Duration = 0.7f;
        
        [SerializeField] private CanvasGroup canvasGroup;

        public void Appear() => 
            canvasGroup.DOFade(1, Duration);

        public void Disappear(TweenCallback onEnd) => 
            canvasGroup.DOFade(0, Duration).OnComplete(onEnd);
    }
}