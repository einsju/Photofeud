using System;

namespace Photofeud.Authentication
{
    public abstract class PlayerAuthenticationProcessor
    {
        public event EventHandler PlayerAuthenticated;
        public event EventHandler<string> PlayerAuthenticationFailed;

        protected virtual void OnPlayerAuthenticated()
        {
            PlayerAuthenticated?.Invoke(this, null);
        }

        protected virtual void OnPlayerAuthenticationFailed(string errorMessage)
        {
            PlayerAuthenticationFailed?.Invoke(this, errorMessage);
        }
    }
}
