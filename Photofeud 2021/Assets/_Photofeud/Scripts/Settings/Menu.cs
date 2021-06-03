using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.State;
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
            StateManager.OnPlayerLoggedOut();
        }
    }
}
