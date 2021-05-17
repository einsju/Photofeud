namespace Photofeud.Profile
{
    public class Player
    {
        public string UserId { get; }
        public string Avatar { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; }
        public bool IsAnonymous { get; }

        public string ScreenDisplayName => !IsAnonymous ? !string.IsNullOrEmpty(DisplayName) ? DisplayName : Email : $"Guest#{UserId.Substring(0, 10)}";

        public Player(string userId, string displayName, string email, bool isAnonymous = false)
        {
            UserId = userId;
            DisplayName = displayName;
            Email = email;
            IsAnonymous = isAnonymous;
            
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