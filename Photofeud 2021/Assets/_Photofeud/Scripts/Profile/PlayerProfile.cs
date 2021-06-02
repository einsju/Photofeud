using Photofeud.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Profile
{
    public class PlayerProfile : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] TMP_Text displayName;

        IProfileLoader _profileLoader;

        void Awake()
        {
            _profileLoader = GetComponent<IProfileLoader>();
        }

        void OnEnable()
        {
            if (_profileLoader is null) return;
            State.Profile.SetPlayer(_profileLoader.LoadProfile());
            if (State.Profile.IsAuthenticated) ShowPlayerInfo();
        }

        void ShowPlayerInfo()
        {
            displayName.text = State.Profile.Player.ScreenDisplayName;
            if (!string.IsNullOrEmpty(State.Profile.Player.Avatar)) avatar.sprite = Resources.Load<Sprite>($"Avatars/{State.Profile.Player.Avatar}");
        }
    }
}
