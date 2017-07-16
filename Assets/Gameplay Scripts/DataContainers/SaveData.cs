using System.Collections.Generic;
using GameFramework;

public class SaveData
{
    public int SaveDataIndex;
    public int SecurityClearance = -1;

    public bool UnlockedREI = false;
    public bool UpgradedREI = false;
    public bool UnlockedGuidedRocket = false;
    public bool UpgradedGuidedRocket = false;

    public bool UnlockedThrustJump = false;
    public bool UpgradedThrustJump = false;
    public bool UnlockedGrappleHook = false;
    public bool UpgradedGrappleHook = false;

    public bool HealthUpgrade1 = false;
    public bool HealthUpgrade2 = false;
    public bool HealthUpgrade3 = false;
    public bool HealthUpgrade4 = false;

    public bool DeathHintActivated = false;
    public bool DefeatedBoss = false;

    public LoadPoint CurrentSavePoint = LoadPoint.AreaMRoom1;

    public SaveData(int in_iSaveDataIndex)
    {
        SaveDataIndex = in_iSaveDataIndex;
    }

    public SaveData(int in_iSaveDataIndex, Dictionary<string, object> in_data)
    {
        SaveDataIndex = in_iSaveDataIndex;

        SecurityClearance = (int)in_data[GSaveManager.SECURITY_CLEARANCE];

        UnlockedREI = (bool)in_data[GSaveManager.UNLOCKED_REI];
        UpgradedREI = (bool)in_data[GSaveManager.UPGRADED_REI];
        UnlockedGuidedRocket = (bool)in_data[GSaveManager.UNLOCKED_GUIDED_ROCKET];
        UpgradedGuidedRocket = (bool)in_data[GSaveManager.UPGRADED_GUIDED_ROCKET];

        UnlockedThrustJump = (bool)in_data[GSaveManager.UNLOCKED_THRUST_JUMP];
        UpgradedThrustJump = (bool)in_data[GSaveManager.UPGRADED_THRUST_JUMP];
        UnlockedGrappleHook = (bool)in_data[GSaveManager.UNLOCKED_GRAPPLE_HOOK];
        UpgradedGrappleHook = (bool)in_data[GSaveManager.UPGRADED_THRUST_JUMP];

        HealthUpgrade1 = (bool)in_data[GSaveManager.UNLOCKED_REI];
        HealthUpgrade2 = (bool)in_data[GSaveManager.UPGRADED_REI];
        HealthUpgrade3 = (bool)in_data[GSaveManager.UNLOCKED_REI];
        HealthUpgrade4 = (bool)in_data[GSaveManager.UPGRADED_REI];

        DeathHintActivated = (bool)in_data[GSaveManager.UPGRADED_REI];
        DefeatedBoss = (bool)in_data[GSaveManager.DEFEATED_BOSS];

        CurrentSavePoint = (LoadPoint)in_data[GSaveManager.SAVE_POINT];
    }
}