using UnityEngine;

namespace Photofeud.Utility
{
    public static class Settings
    {
        const string AudioKey = "_AUDIO_";
        const string VibrationKey = "_VIBRATION_";

        public static bool HasAudio => PlayerPrefs.GetInt(AudioKey, 1) == 1;
        public static void SetAudio(bool audio) => PlayerPrefs.SetInt(AudioKey, audio ? 1 : 0);

        public static bool HasVibration => PlayerPrefs.GetInt(VibrationKey, 1) == 1;
        public static void SetVibration(bool vibrate) => PlayerPrefs.SetInt(VibrationKey, vibrate ? 1 : 0);
    }
}
