using Firebase;
using Firebase.Auth;
using Photofeud.Abstractions;
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
            return user != null ? new Player(user.UserId, user.DisplayName, user.Email, user.IsAnonymous) : null;
        }
    }
}
