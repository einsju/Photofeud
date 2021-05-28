using Photofeud.Abstractions.Profile;
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
            State.Profile.SetPlayer(_profileLoader.LoadProfile());
        }

        void Start() => ShowPlayerInfo();

        void ShowPlayerInfo()
        {
            avatar.sprite = Resources.Load<Sprite>($"Avatars/{State.Profile.Player.Avatar}");
            displayName.text = State.Profile.Player.ScreenDisplayName;
        }
    }
}
