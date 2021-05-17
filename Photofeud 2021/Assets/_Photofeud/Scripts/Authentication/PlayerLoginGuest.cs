using Photofeud.Error;
using Photofeud.Loading;
using Photofeud.Utility;
using System;
using UnityEngine;

namespace Photofeud.Authentication
{
    public class PlayerLoginGuest : MonoBehaviour
    {
        PlayerLoginGuestProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        void Awake()
        {
            _processor = new PlayerLoginGuestProcessor(GetComponent<IPlayerLoginGuestService>());
            _errorHandler = GetComponent<IErrorHandler>();
            _loader = GetComponent<ILoader>();
        }

        void OnEnable()
        {
            _processor.PlayerAuthenticated += PlayerAuthenticated;
            _processor.PlayerAuthenticationFailed += PlayerAuthenticationFailed;
        }

        void OnDisable()
        {
            _processor.PlayerAuthenticated -= PlayerAuthenticated;
            _processor.PlayerAuthenticationFailed -= PlayerAuthenticationFailed;
        }

        public void Login()
        {
            _loader.Load();
            _processor.LoginPlayerAsGuest();
        }

        void PlayerAuthenticated(object sender, EventArgs e)
        {
            SceneNavigator.LoadScene(Scenes.Main);
        }

        void PlayerAuthenticationFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
