using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Settings
{
    public class ResetPasswordHandler : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_InputField email;
        [SerializeField] Button resetPassword;

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
            _canvasGroup = resetPassword.GetComponent<CanvasGroup>();
            _alpha = _canvasGroup.alpha;
        }
        void OnEnable()
        {
            _processor.ProfileUpdated += ProfileUpdated;
            _processor.ProfileUpdateFailed += ProfileUpdateFailed;
            email.text = "";
        }

        void OnDisable()
        {
            _processor.ProfileUpdated -= ProfileUpdated;
            _processor.ProfileUpdateFailed -= ProfileUpdateFailed;
        }

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);

            resetPassword.interactable = hasEmail;
            _canvasGroup.alpha = resetPassword.interactable ? 1f : _alpha;
        }

        public void ResetPassword()
        {
            _loader.Load();
            _processor.ResetPassword(email.text);
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
