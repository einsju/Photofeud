using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Utility;
using System.Linq;
using UnityEngine;

namespace Photofeud.Settings
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] Transform settingsCanvas;
        [SerializeField] GameObject[] controlsThatNeedAuthentication;

        LogoutProcessor _processor;

        void Awake()
        {
            _processor = new LogoutProcessor(InterfaceFinder.Find<IAuthenticationService>());
        }

        void OnEnable()
        {
            ActivateControlsIfAuthenticated();
        }

        void ActivateControlsIfAuthenticated()
        {
            controlsThatNeedAuthentication.ToList().ForEach(c => c.SetActive(State.Profile.IsAuthenticated));
        }

        public void SignOut()
        {
            _processor.LogoutPlayer();
            State.Profile.SetPlayer(null);
            ScreenManager.Instance.OpenLoginScreen();
        }
    }
}
