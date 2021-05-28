using Photofeud.Abstractions.Authentication;
using Photofeud.Abstractions.Error;
using Photofeud.Abstractions.Loading;
using Photofeud.Utility;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Authentication
{
    public class PlayerLogin : MonoBehaviour
    {
        [SerializeField] TMP_InputField email;
        [SerializeField] TMP_InputField password;
        [SerializeField] Button login;

        PlayerLoginProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        CanvasGroup _loginButtonCanvasGroup;
        float _loginCanvasGroupAlpha;

        void Awake()
        {
            _processor = new PlayerLoginProcessor(GetComponent<IPlayerLoginService>());
            _errorHandler = GetComponent<IErrorHandler>();
            _loader = GetComponent<ILoader>();

            _loginButtonCanvasGroup = login.GetComponent<CanvasGroup>();
            _loginCanvasGroupAlpha = _loginButtonCanvasGroup.alpha;            
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

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);
            var hasPassword = !string.IsNullOrEmpty(password.text);

            login.interactable = hasEmail && hasPassword;
            _loginButtonCanvasGroup.alpha = login.interactable ? 1f : _loginCanvasGroupAlpha;
        }

        public void Login()
        {
            _loader.Load();
            _processor.LoginPlayer(email.text, password.text);
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
