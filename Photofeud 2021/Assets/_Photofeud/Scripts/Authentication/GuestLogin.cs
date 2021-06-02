using Photofeud.Abstractions;
using Photofeud.Utility;
using System;
using UnityEngine;

namespace Photofeud.Authentication
{
    public class GuestLogin : MonoBehaviour
    {
        GuestLoginProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        void Awake()
        {
            _processor = new GuestLoginProcessor(InterfaceFinder.Find<IAuthenticationService>());
            _errorHandler = InterfaceFinder.Find<IErrorHandler>();
            _loader = InterfaceFinder.Find<ILoader>();
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
            _loader.Stop();
            ScreenManager.Instance.CloseScreen();
        }

        void PlayerAuthenticationFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
