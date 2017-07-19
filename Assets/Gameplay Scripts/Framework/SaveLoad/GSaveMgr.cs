using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        // The following flags represent bits that can be set in the save data.
        // For one-time collectibles in-game, give them an appropriate pool and a unique flag.
        // Using bitmasks, you can store the values of 31 unique flags in a single bitmask int.
        #region Optimization Notice
        // Optimization note: If you require an expansive number of unique flags to be stored,
        // (over 31 unique flags in a pool), you may consider switching to longs, or use multiple pools
        // at the cost of increase memory usage. Likewise, if you find that you never have more than 15
        // flags in each pool, switch to shorts to decrease memory usage. You could potentially put all flags
        // into a single pool if you have < 32 total for an int or < 64 total for a long.
        // If more space is needed, create more pools.
        #endregion

        // Pool IDs
        public enum DataPoolID
        {
            UNIQUE_ITEMS,
            HEALTH_UPGRADES,
            STORY_EVENTS,
            MAX_POOL_IDS
        }

        // Unique item pool
        public const int UNLOCKED_REI_FLAG =            1 << 0;
        public const int UPGRADED_REI_FLAG =            1 << 1;
        public const int UNLOCKED_GUIDED_ROCKET_FLAG =  1 << 2;
        public const int UPGRADED_GUIDED_ROCKET_FLAG =  1 << 3;
        public const int UNLOCKED_THRUST_JUMP_FLAG =    1 << 4;
        public const int UPGRADED_THRUST_JUMP_FLAG =    1 << 5;
        public const int UNLOCKED_GRAPPLE_HOOK_FLAG =   1 << 6;
        public const int UPGRADED_GRAPPLE_HOOK_FLAG =   1 << 7;

        // Health upgrade pool
        public const int HEALTH_UPGRADE_1_FLAG =        1 << 0;
        public const int HEALTH_UPGRADE_2_FLAG =        1 << 1;
        public const int HEALTH_UPGRADE_3_FLAG =        1 << 2;
        public const int HEALTH_UPGRADE_4_FLAG =        1 << 3;

        // Story event pool
        public const int DEATH_HINT_ACTIVATED_FLAG =    1 << 0;
        public const int DEFEATED_BOSS_FLAG =           1 << 1;

        #endregion

        #region Public

        /// <summary>
        /// Returns the file path of the save data at index, or null if the index is invalid.
        /// </summary>
        /// <param name="in_iSaveDataIndex">Save data index</param>
        /// <returns>Save data file path</returns>
        public static string GetSaveFilePath(int in_iSaveDataIndex)
        {
            // Error checking
            if (in_iSaveDataIndex < 0 || in_iSaveDataIndex > MaxSaveFileCount)
            {
                GDebug.LogError("GetSaveFilePath: Invalid SaveData index - " + in_iSaveDataIndex);
                return null;
            }

            return SaveDataPaths[in_iSaveDataIndex];
        }

        public static bool FileExist(int in_iSaveData)
        {
            string SaveFilePath = GetSaveFilePath(in_iSaveData);
            if (SaveFilePath == null)
            {
                return false;
            }

            return (File.Exists(SaveFilePath));
            //return GetBool(SAVE_DATA_EXISTS + in_iSaveData.ToString());
        }

        public static void Save(SaveData in_saveData)
        {
            if (in_saveData == null || in_saveData.SaveDataIndex < 0 || in_saveData.SaveDataIndex >= MaxSaveFileCount)
                return;

            try
            {
                string json = JsonUtility.ToJson(in_saveData, true);

                string filePath = GetSaveFilePath(in_saveData.SaveDataIndex);

                GDebug.Log("Saving game data to file: " + filePath);
                
                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(json);
                file.Close();
            }
            catch(Exception ex)
            {
                GDebug.LogError("Error writing file - " + SaveDataPaths[in_saveData.SaveDataIndex] + ":\n" + ex.Message);
            }

            #region Save (Old PlayerPrefs code)
            //SetBool(SAVE_DATA_EXISTS + in_saveData.SaveDataIndex.ToString(), true);

            //SetInt(SECURITY_CLEARANCE, in_saveData.SecurityClearance);
            //SetInt(SAVE_POINT, (int)in_saveData.CurrentSavePoint);

            //SetBool(UNLOCKED_REI + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedREI);
            //SetBool(UPGRADED_REI + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedREI);
            //SetBool(UNLOCKED_GUIDED_ROCKET + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedGuidedRocket);
            //SetBool(UPGRADED_GUIDED_ROCKET + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedGuidedRocket);

            //SetBool(UNLOCKED_THRUST_JUMP + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedThrustJump);
            //SetBool(UPGRADED_THRUST_JUMP + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedThrustJump);
            //SetBool(UNLOCKED_GRAPPLE_HOOK + in_saveData.SaveDataIndex.ToString(), in_saveData.UnlockedGrappleHook);
            //SetBool(UPGRADED_GRAPPLE_HOOK + in_saveData.SaveDataIndex.ToString(), in_saveData.UpgradedGrappleHook);

            //SetBool(UNLOCKED_HEALTH_UPGRADE_1 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade1);
            //SetBool(UNLOCKED_HEALTH_UPGRADE_2 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade2);
            //SetBool(UNLOCKED_HEALTH_UPGRADE_3 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade3);
            //SetBool(UNLOCKED_HEALTH_UPGRADE_4 + in_saveData.SaveDataIndex.ToString(), in_saveData.HealthUpgrade4);

            //SetBool(DEATH_HINT_ACTIVATED + in_saveData.SaveDataIndex.ToString(), in_saveData.DeathHintActivated);
            //SetBool(DEFEATED_BOSS + in_saveData.SaveDataIndex.ToString(), in_saveData.DefeatedBoss);
            #endregion
        }

        public static SaveData Load(int in_iSaveDataIndex)
        {
            if (!FileExist(in_iSaveDataIndex))
                return new SaveData(in_iSaveDataIndex);

            SaveData toReturn = null;

            try
            {
                GDebug.Log("Reading game data from file: " + SaveDataPaths[in_iSaveDataIndex]);

                StreamReader file = new StreamReader(SaveDataPaths[in_iSaveDataIndex]);
                string json = file.ReadToEnd();
                file.Close();

                toReturn = JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception ex)
            {
                GDebug.LogError("Error reading file - " + SaveDataPaths[in_iSaveDataIndex] + ":\n" + ex.Message);
            }

            // toReturn could possibly be null after file read
            if (toReturn == null)
            {
                toReturn = new SaveData(in_iSaveDataIndex);
            }
            // If we do have a valid save file, perform any validations needed in here.
            else
            {
                // Make sure DataPools are valid
                //if (toReturn.DataPools == null || toReturn.DataPools.Length != (int)DataPoolID.MAX_POOL_IDS)
                //{
                //    // TODO If the data is invalid, should we cancel loading the file? Reset the save data?
                //}
            }

            // Test incoming SaveData
            //CDebug.Log(toReturn.ToString());

            #region Load (Old PlayerPrefs code)
            //Dictionary<string, object> objects = new Dictionary<string, object>();
            //objects.Add(UNLOCKED_REI, GetBool(UNLOCKED_REI + in_iSaveDataIndex.ToString()));
            //objects.Add(UPGRADED_REI, GetBool(UPGRADED_REI + in_iSaveDataIndex.ToString()));
            //objects.Add(UNLOCKED_GUIDED_ROCKET, GetBool(UNLOCKED_GUIDED_ROCKET + in_iSaveDataIndex.ToString()));
            //objects.Add(UPGRADED_GUIDED_ROCKET, GetBool(UPGRADED_GUIDED_ROCKET + in_iSaveDataIndex.ToString()));

            //objects.Add(UNLOCKED_THRUST_JUMP, GetBool(UNLOCKED_THRUST_JUMP + in_iSaveDataIndex.ToString()));
            //objects.Add(UPGRADED_THRUST_JUMP, GetBool(UPGRADED_THRUST_JUMP + in_iSaveDataIndex.ToString()));
            //objects.Add(UNLOCKED_GRAPPLE_HOOK, GetBool(UNLOCKED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString()));
            //objects.Add(UPGRADED_GRAPPLE_HOOK, GetBool(UPGRADED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString()));

            //objects.Add(UNLOCKED_HEALTH_UPGRADE_1, GetBool(UNLOCKED_HEALTH_UPGRADE_1 + in_iSaveDataIndex.ToString()));
            //objects.Add(UNLOCKED_HEALTH_UPGRADE_2, GetBool(UNLOCKED_HEALTH_UPGRADE_2 + in_iSaveDataIndex.ToString()));
            //objects.Add(UNLOCKED_HEALTH_UPGRADE_3, GetBool(UNLOCKED_HEALTH_UPGRADE_3 + in_iSaveDataIndex.ToString()));
            //objects.Add(UNLOCKED_HEALTH_UPGRADE_4, GetBool(UNLOCKED_HEALTH_UPGRADE_4 + in_iSaveDataIndex.ToString()));

            //objects.Add(DEATH_HINT_ACTIVATED, GetBool(DEATH_HINT_ACTIVATED + in_iSaveDataIndex.ToString()));
            //objects.Add(DEFEATED_BOSS, GetBool(DEFEATED_BOSS + in_iSaveDataIndex.ToString()));

            //objects.Add(SAVE_POINT, GetInt(SAVE_POINT));
            //objects.Add(SECURITY_CLEARANCE, GetInt(SECURITY_CLEARANCE));

            //SaveData toReturn = new SaveData(in_iSaveDataIndex, objects);
            #endregion

            return toReturn;
        }

        public static void Delete(int in_iSaveDataIndex)
        {
            try
            {
                if (!FileExist(in_iSaveDataIndex))
                    return;

                string filePath = GetSaveFilePath(in_iSaveDataIndex);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                GDebug.LogError("Error while deleting save file - " + SaveDataPaths[in_iSaveDataIndex] + ":\n" + ex.Message);
            }

            #region Delete (Old PlayerPrefs code)
            //PlayerPrefs.DeleteKey(UNLOCKED_REI + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UPGRADED_REI + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UNLOCKED_GUIDED_ROCKET + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UPGRADED_GUIDED_ROCKET + in_iSaveDataIndex.ToString());

            //PlayerPrefs.DeleteKey(UNLOCKED_THRUST_JUMP + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UPGRADED_THRUST_JUMP + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UNLOCKED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UPGRADED_GRAPPLE_HOOK + in_iSaveDataIndex.ToString());

            //PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_1 + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_2 + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_3 + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(UNLOCKED_HEALTH_UPGRADE_4 + in_iSaveDataIndex.ToString());

            //PlayerPrefs.DeleteKey(DEATH_HINT_ACTIVATED + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(DEFEATED_BOSS + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(SAVE_POINT + in_iSaveDataIndex.ToString());
            //PlayerPrefs.DeleteKey(SECURITY_CLEARANCE + in_iSaveDataIndex.ToString());

            //PlayerPrefs.DeleteKey(SAVE_DATA_EXISTS + in_iSaveDataIndex.ToString());
            #endregion
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

        #region Generated Save File Paths

        // Gets the save data file paths.
        // First time this is called, the paths get generated.
        public static string[] SaveDataPaths
        {
            get
            {
                // Generate file paths once
                if (_SaveDataPaths == null)
                {
                    _SaveDataPaths = new string[MaxSaveFileCount];

                    // Application.persistentDataPath is not known at compile time and must be called after program starts.
                    // That is why we cannot just assign the strings as consts.

                    for (int i = 0; i < MaxSaveFileCount; i++)
                    {
                        _SaveDataPaths[i] = Application.persistentDataPath + "/SaveData" + (i + 1) + ".json";
                    }

                    // Make sure directories exists
                    if (_SaveDataPaths.Length > 0)
                        // Does nothing if directory already exists, creates it if it doesn't.
                        new FileInfo(_SaveDataPaths[0]).Directory.Create();
                }

                return _SaveDataPaths;
            }
        }
        private static string[] _SaveDataPaths = null;

        // Max number of files allowed
        public static int MaxSaveFileCount = 3;
        #endregion
    }
}
