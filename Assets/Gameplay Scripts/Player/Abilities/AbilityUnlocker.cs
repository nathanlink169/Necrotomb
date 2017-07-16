using GameFramework;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public enum eAbilities
{
    ThrustJump,
    GrappleHook,
}

public class AbilityUnlocker : BaseBehaviour
{
    public eAbilities AbilityType = eAbilities.ThrustJump;
    public eWeaponTypes WeaponType = eWeaponTypes.Pistol;
    public bool IsAbility = true;
    public bool IsUpgrade = false;

    public void Unlock()
    {
        SaveData saveData = GPlayerManager.Instance.PlayerData;
        if (IsAbility)
        {
            switch (AbilityType)
            {
                case eAbilities.ThrustJump:
                    if (IsUpgrade) saveData.UpgradedThrustJump = true;
                    else saveData.UnlockedThrustJump = true;
                    break;
                case eAbilities.GrappleHook:
                    if (IsUpgrade) saveData.UpgradedGrappleHook = true;
                    else saveData.UnlockedGrappleHook = true;
                    break;
            }
        }
        else
        {
            switch (WeaponType)
            {
                case eWeaponTypes.REI:
                    if (IsUpgrade) saveData.UpgradedREI = true;
                    else saveData.UnlockedREI = true;
                    break;
                case eWeaponTypes.GuidedRocket:
                    if (IsUpgrade) saveData.UpgradedGuidedRocket = true;
                    else saveData.UnlockedGuidedRocket = true;
                    break;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AbilityUnlocker))]
public class AbilityUnlockerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        AbilityUnlocker abilityUnlocker = (AbilityUnlocker)target;
        string[] options = new string[] { "Ability", "Weapon" };
        abilityUnlocker.IsAbility = EditorGUILayout.Popup("Type", abilityUnlocker.IsAbility ? 0 : 1, options) == 0 ? true : false;
        if (abilityUnlocker.IsAbility)
        {
            abilityUnlocker.AbilityType = (eAbilities)EditorGUILayout.EnumPopup("Ability", abilityUnlocker.AbilityType);
        }
        else
        {
            abilityUnlocker.WeaponType = (eWeaponTypes)EditorGUILayout.EnumPopup("Weapon", abilityUnlocker.WeaponType);
        }
        abilityUnlocker.IsUpgrade = EditorGUILayout.Toggle("Is Upgrade", abilityUnlocker.IsUpgrade);
    }
}
#endif