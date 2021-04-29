using Photofeud.Abstractions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Authentication
{
    public class PlayerRegistration : MonoBehaviour
    {
        [SerializeField] TMP_InputField email;
        [SerializeField] TMP_InputField password;
        [SerializeField] TMP_InputField repeatPassword;
        [SerializeField] Button register;

        PlayerRegistrationProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        void Awake()
        {
            _processor = new PlayerRegistrationProcessor(GetComponent<IPlayerRegistrationService>());
            _errorHandler = GetComponent<IErrorHandler>();
            _loader = GetComponent<ILoader>();
        }

        void OnEnable()
        {
            _processor.PlayerRegistrationSucceeded += PlayerRegistrationSucceeded;
            _processor.PlayerRegistrationFailed += PlayerRegistrationFailed;
        }

        void OnDisable()
        {
            _processor.PlayerRegistrationSucceeded -= PlayerRegistrationSucceeded;
            _processor.PlayerRegistrationFailed -= PlayerRegistrationFailed;
        }

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);
            var hasPassword = !string.IsNullOrEmpty(password.text);
            var hasMatchingPasswords = password.text == repeatPassword.text;

            register.interactable = hasEmail && hasPassword && hasMatchingPasswords;
        }

        public void Register()
        {
            _loader.Load();
            _processor.RegisterPlayer(email.text, password.text);
        }

        void PlayerRegistrationSucceeded(object sender, EventArgs e)
        {
            SceneNavigator.LoadScene(Scene.Main);
        }

        void PlayerRegistrationFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
