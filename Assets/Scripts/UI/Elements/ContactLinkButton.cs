using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Elements
{
    public class ContactLinkButton : MonoBehaviour
    {
        private const string Link = "https://t.me/kinseloc";

        [SerializeField] private Button button;

        private void Awake() =>
            button.onClick.AddListener(OpenLink);

        private void OpenLink() =>
            Application.OpenURL(Link);
    }
}