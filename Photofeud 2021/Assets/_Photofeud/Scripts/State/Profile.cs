using Photofeud.Profile;

namespace Photofeud.State
{
    public class Profile
    {
        public static Player Player { get; private set; }

        public static bool IsAuthenticated => Player != null;

        public static void SetPlayer(Player player) => Player = player;
    }
}
