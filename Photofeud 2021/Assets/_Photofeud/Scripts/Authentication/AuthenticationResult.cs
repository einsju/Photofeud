namespace Photofeud.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResultCode Code { get; set; }
        public string ErrorMessage { get; set; }
        public Player Player { get; set; }
    }
}