using System.Collections.Generic;
using GameFramework;

public class SaveData
{
    public int SaveDataIndex;

    public int[] DataPools;

    public int SecurityClearance = -1;

    public LoadPoint CurrentSavePoint = LoadPoint.AreaMRoom1;

    public SaveData(int in_iSaveDataIndex)
    {
        ResetSaveData(in_iSaveDataIndex);
    }

    #region Helper methods for checking if various items are unlocked
    // This should be the only place you need to change if you want to switch what pools items are in.

    // Unique items
    public bool UnlockedREI
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_REI_FLAG); }
    }
    public bool UpgradedREI
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_REI_FLAG); }
    }
    public bool UnlockedGuidedRocket
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_GUIDED_ROCKET_FLAG); }
    }
    public bool UpgradedGuidedRocket
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_REI_FLAG); }
    }
    public bool UnlockedThrustJump
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_THRUST_JUMP_FLAG); }
    }
    public bool UpgradedThrustJump
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_THRUST_JUMP_FLAG); }
    }
    public bool UnlockedGrappleHook
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_GRAPPLE_HOOK_FLAG); }
    }
    public bool UpgradedGrappleHook
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_GRAPPLE_HOOK_FLAG); }
    }

    // Story event triggers
    public bool DeathHintActivated
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.STORY_EVENTS, GSaveManager.DEATH_HINT_ACTIVATED_FLAG); }
    }
    public bool DefeatedBoss
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.STORY_EVENTS, GSaveManager.DEFEATED_BOSS_FLAG); }
    }
    public bool ActivatedElevatorM
    {
        get { return UnlockedItem(GSaveManager.eDataPoolID.STORY_EVENTS, GSaveManager.ACTIVATED_ELEVATOR_M); }
    }

    // Health upgrades
    public bool UnlockedHealthUpgrade(int in_iDataFlag)
    {
        return (UnlockedItem(GSaveManager.eDataPoolID.HEALTH_UPGRADES, in_iDataFlag));
    }
    //public bool HealthUpgrade1 = false;
    //public bool HealthUpgrade2 = false;
    //public bool HealthUpgrade3 = false;
    //public bool HealthUpgrade4 = false;
    #endregion

    #region SaveData Manipulation

    /// <summary>
    /// Returns true if an item from one item pool has been collected, false otherwise.
    /// </summary>
    /// <param name="in_iDataPool"></param>
    /// <param name="in_iDataFlag"></param>
    /// <returns></returns>
    public bool UnlockedItem(GSaveManager.eDataPoolID in_iDataPool, int in_iDataFlag)
    {
        if (in_iDataPool < 0 || (int)in_iDataPool >= DataPools.Length)
            return false;

        int dataBitmask = DataPools[(int)in_iDataPool];

        return (dataBitmask & in_iDataFlag) != 0;
    }

    /// <summary>
    /// Sets a bit flag in the data pool bitmask.
    /// Use this to lock or unlock an item.
    /// </summary>
    /// <param name="in_iDataPool"></param>
    /// <param name="in_iDataFlag"></param>
    public void SetItem(GSaveManager.eDataPoolID in_iDataPool, int in_iDataFlag)
    {
        if (in_iDataPool < 0 || (int)in_iDataPool >= DataPools.Length)
            return;

        DataPools[(int)in_iDataPool] |= in_iDataFlag;
    }

    /// <summary>
    /// Sets a bit flag in the data pool bitmask to 0.
    /// </summary>
    /// <param name="in_iDataPool"></param>
    /// <param name="in_iDataFlag"></param>
    public void ClearItem(int in_iDataPool, int in_iDataFlag)
    {
        if (in_iDataPool < 0 || in_iDataPool >= DataPools.Length)
            return;

        DataPools[in_iDataPool] &= ~(1 << in_iDataFlag);
    }

    /// <summary>
    /// Toggles a bit flag in the data pool bitmask.
    /// </summary>
    /// <param name="in_iDataPool"></param>
    /// <param name="in_iDataFlag"></param>
    public void ToggleItem(int in_iDataPool, int in_iDataFlag)
    {
        if (in_iDataPool < 0 || in_iDataPool >= DataPools.Length)
            return;

        DataPools[in_iDataPool] ^= 1 << in_iDataFlag;
    }

    /// <summary>
    /// Clears all data in current SaveData.
    /// </summary>
    public void ResetSaveData(int in_iSaveDataIndex)
    {
        SaveDataIndex = in_iSaveDataIndex;

        if (DataPools == null)
        {
            DataPools = new int[(int)GSaveManager.eDataPoolID.MAX_POOL_IDS];
        }

        for (int i = 0; i < DataPools.Length; i++)
        {
            DataPools[i] = 0;
        }

        SecurityClearance = -1;

        CurrentSavePoint = LoadPoint.AreaMRoom1;
    }

    #endregion

    // Used for debugging
    public override string ToString()
    {
        string message = string.Format("SaveData\nFile Index:{0}\nSecurity Clearance:{1}\nCurrent Save Point:{2}\nDataPools:\n", SaveDataIndex, SecurityClearance, CurrentSavePoint);
        for (int i = 0; i < GSaveManager.MaxSaveFileCount; i++)
        {
            message += DataPools[i] + "\n";
        }

        return message;
    }
}
