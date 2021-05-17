namespace Photofeud.Profile
{
    public class Player
    {
        public string UserId { get; }
        public string Avatar { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; }

        public string ScreenDisplayName => !string.IsNullOrEmpty(DisplayName) ? DisplayName : Email;

        public Player(string userId, string displayName, string email)
        {
            UserId = userId;
            DisplayName = displayName;
            Email = email;
            
            EnforceAvatarRules();
        }

        void EnforceAvatarRules()
        {
            var split = DisplayName.Split(';');
            if (split.Length != 2) return;
            DisplayName = split[0];
            Avatar = split[1];
        }
    }
}