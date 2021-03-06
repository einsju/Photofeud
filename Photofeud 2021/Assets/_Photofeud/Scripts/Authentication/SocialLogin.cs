using Photofeud.Abstractions;
using Photofeud.Utility;
using System;
using UnityEngine;

namespace Photofeud.Authentication
{
    public class SocialLogin : MonoBehaviour
    {
        SocialLoginProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        void Awake()
        {
            _processor = new SocialLoginProcessor(InterfaceFinder.Find<IAuthenticationService>());
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

        public void LoginGoogle()
        {
            Login(SocialLoginProvider.Google);
        }

        public void LoginFacebook()
        {
            Login(SocialLoginProvider.Facebook);
        }

        void Login(SocialLoginProvider provider)
        {
            _loader.Load();
            _processor.LoginPlayer(provider);
        }

        void PlayerAuthenticated(object sender, EventArgs e)
        {
            //SceneNavigator.LoadScene(Scenes.Main);
        }

        void PlayerAuthenticationFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
