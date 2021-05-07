using Firebase;
using Firebase.Auth;
using Photofeud.Profile;
using UnityEngine;

namespace Photofeud.Firebase
{
    public class ProfileLoader : MonoBehaviour, IProfileLoader
    {
        void Awake()
        {
            FirebaseApp.CheckAndFixDependenciesAsync();
        }

        public Player LoadProfile()
        {
            var user = FirebaseAuth.DefaultInstance.CurrentUser;
            if (user is null) return null;

            return new Player(user.UserId, user.DisplayName, user.Email);
        }
    }
}
