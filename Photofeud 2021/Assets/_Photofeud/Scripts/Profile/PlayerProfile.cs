using Photofeud.Abstractions;
using Photofeud.Utility;
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
        bool _canShow = true;

        void Awake()
        {
            _profileLoader = GetComponent<IProfileLoader>();
            State.Profile.SetPlayer(_profileLoader.LoadProfile());
            _canShow = State.Profile.Player != null;
        }

        void Start()
        {
            if (!_canShow)
            {
                SceneNavigator.LoadScene(Scenes.Login);
                return;
            }

            ShowPlayerInfo();
        }

        void ShowPlayerInfo()
        {
            displayName.text = State.Profile.Player.ScreenDisplayName;
            if (!string.IsNullOrEmpty(State.Profile.Player.Avatar)) avatar.sprite = Resources.Load<Sprite>($"Avatars/{State.Profile.Player.Avatar}");
        }
    }
}
