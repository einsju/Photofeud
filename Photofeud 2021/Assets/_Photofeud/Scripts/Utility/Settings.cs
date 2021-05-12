using UnityEngine;

namespace Photofeud.Utility
{
    public static class Settings
    {
        const string AudioKey = "_AUDIO_";
        const string VibrationKey = "_VIBRATION_";

        static bool HasValue(string key) => PlayerPrefs.GetInt(key, 1) == 1;
        static void Enable(string key) => PlayerPrefs.SetInt(key, 1);
        static void Disable(string key) => PlayerPrefs.SetInt(key, 0);

        public static bool HasAudio()
        {
            return HasValue(AudioKey);
        }

        public static void SetAudio(bool audio)
        {
            if (!audio)
            {
                Disable(AudioKey);
                return;
            }

            Enable(AudioKey);
        }

        public static bool HasVibration()
        {
            return HasValue(VibrationKey);
        }

        public static void SetVibration(bool vibrate)
        {
            if (!vibrate)
            {
                Disable(VibrationKey);
                return;
            }

            Enable(VibrationKey);
        }
    }
}
