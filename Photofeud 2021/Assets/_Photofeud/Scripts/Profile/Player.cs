namespace Photofeud.Profile
{
    public class Player
    {
        public string UserId { get; }
        public string DisplayName { get; }
        public string Email { get; }

        public string ScreenDisplayName => !string.IsNullOrEmpty(DisplayName) ? DisplayName : Email;

        public Player(string userId, string displayName, string email)
        {
            UserId = userId;
            DisplayName = displayName;
            Email = email;
        }
    }
}