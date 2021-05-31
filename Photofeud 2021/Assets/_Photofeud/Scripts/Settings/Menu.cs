using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Utility;
using UnityEngine;

namespace Photofeud.Settings
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] Transform settingsCanvas;
        [SerializeField] GameObject[] controlsThatNeedAuthentication;        

        LogoutProcessor _processor;

        bool IsAuthenticated => State.Profile.PlayerIsSignedIn();

        void Awake()
        {
            _processor = new LogoutProcessor(GetComponent<IAuthenticationService>());
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

        public void OpenMenu(Transform fromCanvas)
        {
            CanvasHandler.ChangeCanvas(fromCanvas, transform);
        }

        public void CloseMenu(Transform toCanvas)
        {
            CanvasHandler.ChangeCanvas(transform, toCanvas);
        }

        public void OpenSubMenu(Transform toCanvas)
        {
            CanvasHandler.ChangeCanvas(settingsCanvas, toCanvas);
        }

        public void CloseSubMenu(Transform fromCanvas)
        {
            CanvasHandler.ChangeCanvas(fromCanvas, settingsCanvas);
        }

        public void SignOut()
        {
            _processor.LogoutPlayer();
            State.Profile.SetPlayer(null);
            SceneNavigator.LoadScene(Scenes.Login);
        }
    }
}
