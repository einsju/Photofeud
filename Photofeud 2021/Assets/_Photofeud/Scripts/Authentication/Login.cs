using Photofeud.Abstractions;
using Photofeud.State;
using Photofeud.Utility;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Authentication
{
    public class Login : MonoBehaviour
    {
        [SerializeField] TMP_InputField email;
        [SerializeField] TMP_InputField password;
        [SerializeField] Button login;

        LoginProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        CanvasGroup _loginButtonCanvasGroup;
        float _loginCanvasGroupAlpha;

        void Awake()
        {
            _processor = new LoginProcessor(InterfaceFinder.Find<IAuthenticationService>());
            _errorHandler = InterfaceFinder.Find<IErrorHandler>();
            _loader = InterfaceFinder.Find<ILoader>();
            _loginButtonCanvasGroup = login.GetComponent<CanvasGroup>();
            _loginCanvasGroupAlpha = _loginButtonCanvasGroup.alpha;
        }

        void OnEnable()
        {
            _processor.PlayerAuthenticated += PlayerAuthenticated;
            _processor.PlayerAuthenticationFailed += PlayerAuthenticationFailed;
            email.text = password.text = "";
        }

        void OnDisable()
        {
            _processor.PlayerAuthenticated -= PlayerAuthenticated;
            _processor.PlayerAuthenticationFailed -= PlayerAuthenticationFailed;
        }

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);
            var hasPassword = !string.IsNullOrEmpty(password.text);

            login.interactable = hasEmail && hasPassword;
            _loginButtonCanvasGroup.alpha = login.interactable ? 1f : _loginCanvasGroupAlpha;
        }

        public void SignIn()
        {
            _loader.Load();
            _processor.LoginPlayer(email.text, password.text);
        }

        void PlayerAuthenticated(object sender, EventArgs e)
        {
            _loader.Stop();
            StateManager.OnPlayerLoggedIn();
        }

        void PlayerAuthenticationFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
