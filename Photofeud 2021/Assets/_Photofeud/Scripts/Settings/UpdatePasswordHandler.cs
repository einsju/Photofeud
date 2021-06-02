using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Settings
{
    public class UpdatePasswordHandler : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_InputField password;
        [SerializeField] TMPro.TMP_InputField repeatPassword;
        [SerializeField] Button updatePassword;

        ProfileUpdateProcessor _processor;
        IErrorHandler _errorHandler;
        ILoader _loader;

        CanvasGroup _canvasGroup;
        float _alpha;

        void Awake()
        {
            _processor = new ProfileUpdateProcessor(InterfaceFinder.Find<IProfileUpdateService>());
            _errorHandler = InterfaceFinder.Find<IErrorHandler>();
            _loader = InterfaceFinder.Find<ILoader>();
            _canvasGroup = updatePassword.GetComponent<CanvasGroup>();
            _alpha = _canvasGroup.alpha;
        }

        void OnEnable()
        {
            _processor.ProfileUpdated += ProfileUpdated;
            _processor.ProfileUpdateFailed += ProfileUpdateFailed;
            password.text = repeatPassword.text = "";
        }

        void OnDisable()
        {
            _processor.ProfileUpdated -= ProfileUpdated;
            _processor.ProfileUpdateFailed -= ProfileUpdateFailed;
        }

        public void OnInputValueChanged()
        {
            var hasPassword = !string.IsNullOrEmpty(password.text);
            var hasMatchingPasswords = password.text == repeatPassword.text;

            updatePassword.interactable = hasPassword && hasMatchingPasswords;
            _canvasGroup.alpha = updatePassword.interactable ? 1f : _alpha;
        }

        public void UpdatePassword()
        {
            _loader.Load();
            _processor.UpdatePassword(password.text);
        }

        void ProfileUpdated(object sender, EventArgs e)
        {
            _loader.Stop();
            ScreenManager.Instance.CloseScreen();
        }

        void ProfileUpdateFailed(object sender, string error)
        {
            _loader.Stop();
            _errorHandler.HandleError(error);
        }
    }
}
