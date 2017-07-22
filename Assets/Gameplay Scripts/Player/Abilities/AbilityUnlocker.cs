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
                    if (IsUpgrade) saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_THRUST_JUMP_FLAG);
                    else saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_THRUST_JUMP_FLAG);
                    break;
                case eAbilities.GrappleHook:
                    if (IsUpgrade) saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_GRAPPLE_HOOK_FLAG);
                    else saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_GRAPPLE_HOOK_FLAG);
                    break;
            }
        }
        else
        {
            switch (WeaponType)
            {
                case eWeaponTypes.REI:
                    if (IsUpgrade) saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_REI_FLAG);
                    else saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_REI_FLAG);
                    break;
                case eWeaponTypes.GuidedRocket:
                    if (IsUpgrade) saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UPGRADED_GUIDED_ROCKET_FLAG);
                    else saveData.SetItem(GSaveManager.eDataPoolID.UNIQUE_ITEMS, GSaveManager.UNLOCKED_GUIDED_ROCKET_FLAG);
                    break;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AbilityUnlocker))]
public class AbilityUnlockerInspector : Editor
{
    SerializedProperty AbilityType;
    SerializedProperty WeaponType;
    SerializedProperty IsAbility;
    SerializedProperty IsUpgrade;

    private void OnEnable()
    {
        AbilityType = serializedObject.FindProperty("AbilityType");
        WeaponType = serializedObject.FindProperty("WeaponType");
        IsAbility = serializedObject.FindProperty("IsAbility");
        IsUpgrade = serializedObject.FindProperty("IsUpgrade");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        string[] options = new string[] { "Ability", "Weapon" };
        IsAbility.boolValue = EditorGUILayout.Popup("Type", IsAbility.boolValue ? 0 : 1, options) == 0 ? true : false;

        if (IsAbility.boolValue)
            EditorGUILayout.PropertyField(AbilityType);
        else
            EditorGUILayout.PropertyField(WeaponType);

        EditorGUILayout.PropertyField(IsUpgrade);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif