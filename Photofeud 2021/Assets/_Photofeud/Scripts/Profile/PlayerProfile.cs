using TMPro;
using UnityEngine;

namespace Photofeud.Profile
{
    public class PlayerProfile : MonoBehaviour
    {
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
            displayName.text = State.Profile.Player.ScreenDisplayName;
        }
    }
}
