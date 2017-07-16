using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static class GSaveManager
    {
        #region Public Consts
        public const string UNLOCKED_REI = "UNLOCKED_REI";
        public const string UPGRADED_REI = "UPGRADED_REI";
        public const string UNLOCKED_GUIDED_ROCKET = "UNLOCKED_GUIDED_ROCKET";
        public const string UPGRADED_GUIDED_ROCKET = "UPGRADED_GUIDED_ROCKET";

        public const string UNLOCKED_THRUST_JUMP = "UNLOCKED_THRUST_JUMP";
        public const string UPGRADED_THRUST_JUMP = "UPGRADED_THRUST_JUMP";
        public const string UNLOCKED_GRAPPLE_HOOK = "UNLOCKED_GRAPPLE_HOOK";
        public const string UPGRADED_GRAPPLE_HOOK = "UPGRADED_GRAPPLE_HOOK";

        public const string UNLOCKED_HEALTH_UPGRADE_1 = "UNLOCKED_HEALTH_UPGRADE_1";
        public const string UNLOCKED_HEALTH_UPGRADE_2 = "UNLOCKED_HEALTH_UPGRADE_2";
        public const string UNLOCKED_HEALTH_UPGRADE_3 = "UNLOCKED_HEALTH_UPGRADE_3";
        public const string UNLOCKED_HEALTH_UPGRADE_4 = "UNLOCKED_HEALTH_UPGRADE_4";

        public const string DEATH_HINT_ACTIVATED = "DEATH_HINT_ACTIVATED";
        public const string DEFEATED_BOSS = "DEFEATED_BOSS";
        public const string SAVE_POINT = "SAFE_POINT";
        public const string SECURITY_CLEARANCE = "SECURITY_CLEARANCE";

        private const string SAVE_DATA_EXISTS = "EXISTS";
        #endregion

        #region Public
        public static bool FileExist(int in_iSaveData)
        {
            return GetBool(SAVE_DATA_EXISTS + in_iSaveData.ToString());
        }

        public static void Save(SaveData in_saveData)
        {
            SetBool(SAVE_DATA_EXISTS + in_saveData.SaveDataIndex.ToString(), true);

            SetInt(SECURITY_CLEARANCE, in_saveData.SecurityClearance);
            SetInt(SAVE_POINT, (int)in_saveData.CurrentSavePoint);

            SetBool(UNLOCKED_REI + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedREI);
            SetBool(UPGRADED_REI + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedREI);
            SetBool(UNLOCKED_GUIDED_ROCKET + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedGuidedRocket);
            SetBool(UPGRADED_GUIDED_ROCKET + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedGuidedRocket);

            SetBool(UNLOCKED_THRUST_JUMP + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedThrustJump);
            SetBool(UPGRADED_THRUST_JUMP + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedThrustJump);
            SetBool(UNLOCKED_GRAPPLE_HOOK + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedGrappleHook);
            SetBool(UPGRADED_GRAPPLE_HOOK + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedGrappleHook);

            SetBool(UNLOCKED_HEALTH_UPGRADE_1 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade1);
            SetBool(UNLOCKED_HEALTH_UPGRADE_2 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade2);
            SetBool(UNLOCKED_HEALTH_UPGRADE_3 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade3);
            SetBool(UNLOCKED_HEALTH_UPGRADE_4 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade4);

            SetBool(DEATH_HINT_ACTIVATED + in_saveData.SaveDataIndex.ToString(), in_saveData.DeathHintActivated);
            SetBool(DEFEATED_BOSS + in_saveData.SaveDataIndex.ToString(), in_saveData.DefeatedBoss);
        }

        public static SaveData Load(int in_iSaveDataIndex)
        {
            if (!FileExist(in_iSaveDataIndex))
                return new SaveData(in_iSaveDataIndex);

            Dictionary<string, object> objects = new Dictionary<string, object>();
            objects.Add(UNLOCKED_REI, GetBool(UNLOCKED_REI + in_iSaveDataIndex.ToString()));
            objects.Add(UPGRADED_REI, GetBool(UPGRADED_REI + in_iSaveDataIndex.ToString()));
            objects.Add(UNLOCKED_GUIDED_ROCKET, GetBool(UNLOCKED_GUIDED_ROCKET + in_iSaveDataIndex.ToString()));
            objects.Add(UPGRADED_GUIDED_ROCKET, GetBool(UPGRADED_GUIDED_ROCKET + in_iSaveDataIndex.ToString()));

            objects.Add(UNLOCKED_THRUST_JUMP, GetBool(UNLOCKED_THRUST_JUMP + in_iSaveDataIndex.ToString()));
            objects.Add(UPGRADED_THRUST_JUMP, GetBool(UPGRADED_THRUST_JUMP + in_iSaveDataIndex.ToString()));
            objects.Add(UNLOCKED_GRAPPLE_HOOK, GetBool(UNLOCKED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString()));
            objects.Add(UPGRADED_GRAPPLE_HOOK, GetBool(UPGRADED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString()));

            objects.Add(UNLOCKED_HEALTH_UPGRADE_1, GetBool(UNLOCKED_HEALTH_UPGRADE_1 + in_iSaveDataIndex.ToString()));
            objects.Add(UNLOCKED_HEALTH_UPGRADE_2, GetBool(UNLOCKED_HEALTH_UPGRADE_2 + in_iSaveDataIndex.ToString()));
            objects.Add(UNLOCKED_HEALTH_UPGRADE_3, GetBool(UNLOCKED_HEALTH_UPGRADE_3 + in_iSaveDataIndex.ToString()));
            objects.Add(UNLOCKED_HEALTH_UPGRADE_4, GetBool(UNLOCKED_HEALTH_UPGRADE_4 + in_iSaveDataIndex.ToString()));

            objects.Add(DEATH_HINT_ACTIVATED, GetBool(DEATH_HINT_ACTIVATED + in_iSaveDataIndex.ToString()));
            objects.Add(DEFEATED_BOSS, GetBool(DEFEATED_BOSS + in_iSaveDataIndex.ToString()));

            objects.Add(SAVE_POINT, GetInt(SAVE_POINT));
            objects.Add(SECURITY_CLEARANCE, GetInt(SECURITY_CLEARANCE));

            SaveData toReturn = new SaveData(in_iSaveDataIndex, objects);
            return toReturn;
        }

        public static void Delete(int in_iSaveDataIndex)
        {
            PlayerPrefs.DeleteKey(UNLOCKED_REI + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UPGRADED_REI + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UNLOCKED_GUIDED_ROCKET + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UPGRADED_GUIDED_ROCKET + in_iSaveDataIndex.ToString());

            PlayerPrefs.DeleteKey(UNLOCKED_THRUST_JUMP + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UPGRADED_THRUST_JUMP + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UNLOCKED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UPGRADED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString());

            PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_1 + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_2 + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_3 + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_4 + in_iSaveDataIndex.ToString());

            PlayerPrefs.DeleteKey(DEATH_HINT_ACTIVATED + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(DEFEATED_BOSS + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(SAVE_POINT + in_iSaveDataIndex.ToString());
            PlayerPrefs.DeleteKey(SECURITY_CLEARANCE + in_iSaveDataIndex.ToString());

            PlayerPrefs.DeleteKey(SAVE_DATA_EXISTS + in_iSaveDataIndex.ToString());
        }
        #endregion

        #region PlayerPrefs Saving
        private static float GetFloat(string in_key)
        {
            return PlayerPrefs.GetFloat(in_key, -1f);
        }
        private static void SetFloat(string in_key, float in_value)
        {
            PlayerPrefs.SetFloat(in_key, in_value);
        }

        private static int GetInt(string in_key)
        {
            return PlayerPrefs.GetInt(in_key, -1);
        }
        private static void SetInt(string in_key, int in_value)
        {
            PlayerPrefs.SetInt(in_key, in_value);
        }

        private static string GetString(string in_key)
        {
            return PlayerPrefs.GetString(in_key, "");
        }
        private static void SetString(string in_key, string in_value)
        {
            PlayerPrefs.SetString(in_key, in_value);
        }

        private static bool GetBool(string in_key)
        {
            return (PlayerPrefs.GetInt(in_key, 0) == 1) ? true : false;
        }
        private static void SetBool(string in_key, bool in_value)
        {
            PlayerPrefs.SetInt(in_key, in_value ? 1 : 0);
        }
        #endregion
    }
}