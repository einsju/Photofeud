using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] List<Sprite> avatars;
        [SerializeField] Button previous;
        [SerializeField] Button next;
        [SerializeField] Color enabledColor;
        [SerializeField] Color disabledColor;

        int _index = 0;

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
            avatar.sprite = avatars[_index];
        }

        void UpdateButtons()
        {
            previous.interactable = _index > 0;
            previous.image.color = previous.interactable ? enabledColor : disabledColor;
            next.interactable = _index < avatars.Count - 1;
            next.image.color = next.interactable ? enabledColor : disabledColor;
        }
    }
}
