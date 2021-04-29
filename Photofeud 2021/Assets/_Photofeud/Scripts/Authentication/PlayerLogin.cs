using Photofeud.Abstractions;
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

        void Awake()
        {
            _processor = new PlayerLoginProcessor(GetComponent<IPlayerLoginService>());
            _errorHandler = GetComponent<IErrorHandler>();
            _loader = GetComponent<ILoader>();
        }

        void OnEnable()
        {
            _processor.PlayerLoginSucceeded += PlayerLoginSucceeded;
            _processor.PlayerLoginFailed += PlayerLoginFailed;
        }

        void OnDisable()
        {
            _processor.PlayerLoginSucceeded -= PlayerLoginSucceeded;
            _processor.PlayerLoginFailed -= PlayerLoginFailed;
        }

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);
            var hasPassword = !string.IsNullOrEmpty(password.text);

            login.interactable = hasEmail && hasPassword;
        }

        public void Login()
        {
            _loader.Load();
            _processor.LoginPlayer(email.text, password.text);
        }

        void PlayerLoginSucceeded(object sender, EventArgs e)
        {
            SceneNavigator.LoadScene(Scene.Main);
        }

        void PlayerLoginFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
