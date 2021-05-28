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
    public class PlayerRegistration : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] TMP_InputField displayName;
        [SerializeField] TMP_InputField email;
        [SerializeField] TMP_InputField password;
        [SerializeField] TMP_InputField repeatPassword;
        [SerializeField] Button register;

        PlayerRegistrationProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        CanvasGroup _registerButtonCanvasGroup;
        float _registerCanvasGroupAlpha;

        void Awake()
        {
            _processor = new PlayerRegistrationProcessor(GetComponent<IPlayerRegistrationService>());
            _errorHandler = GetComponent<IErrorHandler>();
            _loader = GetComponent<ILoader>();

            _registerButtonCanvasGroup = register.GetComponent<CanvasGroup>();
            _registerCanvasGroupAlpha = _registerButtonCanvasGroup.alpha;
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
            var hasDisplayName = !string.IsNullOrEmpty(displayName.text);
            var hasEmail = !string.IsNullOrEmpty(email.text);
            var hasPassword = !string.IsNullOrEmpty(password.text);
            var hasMatchingPasswords = password.text == repeatPassword.text;

            register.interactable = hasDisplayName && hasEmail && hasPassword && hasMatchingPasswords;
            _registerButtonCanvasGroup.alpha = register.interactable ? 1f : _registerCanvasGroupAlpha;
        }

        public void Register()
        {
            _loader.Load();
            var displayNameAndAvatar = $"{displayName.text};{avatar.sprite.name}";
            _processor.RegisterPlayer(displayNameAndAvatar, email.text, password.text);
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
