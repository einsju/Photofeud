using UnityEngine;
using UnityEngine.UI;

namespace Photofeud
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] Button previous;
        [SerializeField] Button next;
        [SerializeField] Color enabledColor;
        [SerializeField] Color disabledColor;

        Sprite[] _avatars;
        int _index = 0;

        void Awake()
        {
            _avatars = Resources.LoadAll<Sprite>("Avatars");
            SetAvatar();
        }

        void OnEnable()
        {
            _index = 0;
            SetAvatar();
            UpdateButtons();
        }

        public void OnPrevious()
        {
            _index--;
            SetAvatar();
            UpdateButtons();

        }

        public void OnNext()
        {
            _index++;
            SetAvatar();
            UpdateButtons();
        }

        void SetAvatar()
        {
            avatar.sprite = _avatars[_index];
        }

        void UpdateButtons()
        {
            previous.interactable = _index > 0;
            previous.image.color = previous.interactable ? enabledColor : disabledColor;
            next.interactable = _index < _avatars.Length - 1;
            next.image.color = next.interactable ? enabledColor : disabledColor;
        }
    }
}
