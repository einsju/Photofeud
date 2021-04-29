namespace Photofeud
{
    public class Player
    {
        public string UserId { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public Player(string userId, string displayName, string email)
        {
            UserId = userId;
            DisplayName = displayName;
            Email = email;
        }
    }
}