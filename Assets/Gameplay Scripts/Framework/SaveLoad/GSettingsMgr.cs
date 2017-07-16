using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static class GSettingsManager
    {
        #region Public Functions
        #endregion

        #region Options Properties
        public static float GlobalVolume
        {
            get
            {
                float fToReturn = GetFloatValue(GLOBAL_VOLUME);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    GlobalVolume = 1f;
                    return 1f;
                }
            }

            set
            {
                SetFloatValue(GLOBAL_VOLUME, value);
                GEventManager.TriggerEvent(GEventManager.ON_VOLUME_CHANGED);
            }
        }

        public static float MusicVolume
        {
            get
            {
                float fToReturn = GetFloatValue(MUSIC_VOLUME);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    MusicVolume = 1f;
                    return 1f;
                }
            }

            set
            {
                SetFloatValue(MUSIC_VOLUME, value);
                GEventManager.TriggerEvent(GEventManager.ON_VOLUME_CHANGED);
            }
        }

        public static float SFXVolume
        {
            get
            {
                float fToReturn = GetFloatValue(SFX_VOLUME);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    SFXVolume = 1f;
                    return 1f;
                }
            }

            set
            {
                SetFloatValue(SFX_VOLUME, value);
                GEventManager.TriggerEvent(GEventManager.ON_VOLUME_CHANGED);
            }
        }

        public static float VoiceVolume
        {
            get
            {
                float fToReturn = GetFloatValue(VOICE_VOLUME);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    VoiceVolume = 1f;
                    return 1f;
                }
            }

            set
            {
                SetFloatValue(VOICE_VOLUME, value);
                GEventManager.TriggerEvent(GEventManager.ON_VOLUME_CHANGED);
            }
        }

        public static int Resolution
        {
            get
            {
                int sToReturn = GetIntValue(RESOLUTION);
                if (sToReturn != -1)
                    return sToReturn;
                else
                {
                    // TODO: Set this properly
                    Resolution = 0;
                    return sToReturn;
                }
            }
            set { SetIntValue(RESOLUTION, value); }
        }

        public static int TextureQuality
        {
            get
            {
                int sToReturn = GetIntValue(TEXTURE_QUALITY);
                if (sToReturn != -1)
                    return sToReturn;
                else
                {
                    // TODO: Set this properly
                    TextureQuality = 0;
                    return sToReturn;
                }
            }
            set { SetIntValue(TEXTURE_QUALITY, value); }
        }

        public static int Shadows
        {
            get
            {
                int sToReturn = GetIntValue(SHADOWS);
                if (sToReturn != -1)
                    return sToReturn;
                else
                {
                    // TODO: Set this properly
                    Shadows = 0;
                    return sToReturn;
                }
            }
            set { SetIntValue(SHADOWS, value); }
        }

        public static float Brightness
        {
            get
            {
                float fToReturn = GetFloatValue(BRIGHTNESS);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    Brightness = 1f;
                    return 1f;
                }
            }

            set { SetFloatValue(BRIGHTNESS, value); }
        }

        public static float FieldOfView
        {
            get
            {
                float fToReturn = GetFloatValue(FIELD_OF_VIEW);
                if (fToReturn >= 0f)
                    return fToReturn;
                else
                {
                    FieldOfView = 60f;
                    return 60f;
                }
            }

            set { SetFloatValue(FIELD_OF_VIEW, value); }
        }

        public static bool Subtitles
        {
            get { return GetBoolValue(SUBTITLES); }
            set { SetBoolValue(SUBTITLES, value); }
        }

        public static bool Fullscreen
        {
            get { return GetBoolValue(FULLSCREEN); }
            set { SetBoolValue(FULLSCREEN, value); }
        }

        public static bool Tooltips
        {
            get { return GetBoolValue(TOOLTIPS); }
            set { SetBoolValue(TOOLTIPS, value); }
        }
        #endregion

        #region Private Consts
        private const string GLOBAL_VOLUME = "GLOBAL_VOLUME";
        private const string MUSIC_VOLUME = "MUSIC_VOLUME";
        private const string SFX_VOLUME = "SFX_VOLUME";
        private const string VOICE_VOLUME = "VOICE_VOLUME";

        private const string RESOLUTION = "RESOLUTION";
        private const string TEXTURE_QUALITY = "TEXTURE_QUALITY";
        private const string SHADOWS = "SHADOWS";
        private const string BRIGHTNESS = "BRIGHTNESS";

        private const string FIELD_OF_VIEW = "FIELD_OF_VIEW";
        private const string SUBTITLES = "SUBTITLES";
        private const string FULLSCREEN = "FULLSCREEN";
        private const string TOOLTIPS = "TOOLTIPS";
        #endregion

        #region PlayerPrefs Saving
        private static float GetFloatValue(string in_key)
        {
            return PlayerPrefs.GetFloat(in_key, -1f);
        }
        private static void SetFloatValue(string in_key, float in_value)
        {
            PlayerPrefs.SetFloat(in_key, in_value);
        }

        private static int GetIntValue(string in_key)
        {
            return PlayerPrefs.GetInt(in_key, -1);
        }
        private static void SetIntValue(string in_key, int in_value)
        {
            PlayerPrefs.SetInt(in_key, in_value);
        }

        private static string GetStringValue(string in_key)
        {
            return PlayerPrefs.GetString(in_key, "");
        }
        private static void SetStringValue(string in_key, string in_value)
        {
            PlayerPrefs.SetString(in_key, in_value);
        }

        private static bool GetBoolValue(string in_key)
        {
            return (PlayerPrefs.GetInt(in_key, 0) == 1) ? true : false;
        }
        private static void SetBoolValue(string in_key, bool in_value)
        {
            PlayerPrefs.SetInt(in_key, in_value ? 1 : 0);
        }
        #endregion
    }
}