using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum eDatapadAction
{
    ChangeSecurityClearance,
}

public class DatapadInteractable : BaseBehaviour, IInteractable
{
    public eDatapadAction DatapadAction = eDatapadAction.ChangeSecurityClearance;
    public int SecurityClearance = 0;

    public void OnInteract()
    {
        // TODO: Play Sound
        // TODO: Show Player On UI
        GPlayerManager playerManager = GPlayerManager.Instance;
        playerManager.PlayerData.SecurityClearance = Math.Max(playerManager.PlayerData.SecurityClearance, SecurityClearance);

        GameObject.Destroy(this.gameObject);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DatapadInteractable))]
public class DatapadInteractableInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DatapadInteractable datapadInteractable = (DatapadInteractable)target;

        datapadInteractable.DatapadAction = (eDatapadAction)EditorGUILayout.EnumPopup("Datapad Action", datapadInteractable.DatapadAction);

        switch (datapadInteractable.DatapadAction)
        {
            case eDatapadAction.ChangeSecurityClearance:
                datapadInteractable.SecurityClearance = EditorGUILayout.IntField("Security Clearance To Unlock", datapadInteractable.SecurityClearance);
                break;
            default:
                break;
        }
    }
}

#endif