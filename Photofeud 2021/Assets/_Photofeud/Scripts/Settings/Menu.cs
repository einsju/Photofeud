using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Utility;
using UnityEngine;

namespace Photofeud.Settings
{
    [RequireComponent(typeof(Animator))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] GameObject[] controlsThatNeedAuthentication;

        Animator _transition;
        LogoutProcessor _logoutProcessor;

        bool IsAuthenticated => State.Profile.PlayerIsSignedIn();

        void Awake()
        {
            _transition = GetComponent<Animator>();
            _logoutProcessor = new LogoutProcessor(GetComponent<IAuthenticationService>());
        }

        void Start()
        {
            ActivateControlsIfAuthenticated();
        }

        void ActivateControlsIfAuthenticated()
        {
            if (!IsAuthenticated) return;

            foreach (var control in controlsThatNeedAuthentication)
                control.SetActive(true);
        }

        public void OpenMenu()
        {
            _transition.SetTrigger(Triggers.MenuOpen);
        }

        public void CloseMenu()
        {
            _transition.SetTrigger(Triggers.MenuClose);
        }

        public void SignOut()
        {
            _logoutProcessor.LogoutPlayer();
            SceneNavigator.LoadScene(Scenes.Login);
        }

        public void ResetPassword()
        {
        }
    }
}
