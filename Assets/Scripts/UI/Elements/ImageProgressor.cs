using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Elements
{
    public class ImageProgressor : MonoBehaviour
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private TextMeshProUGUI progressText;

        public void SetValue(float current, float max)
        {
            float progressValue = current / max;
            progressImage.fillAmount = progressValue;
            progressText.text = (progressValue * 100).ToString("N1");
        }
    }
}