using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

public enum eConsoleJobs
{
    ElevatorMUnlock,
}

public class ConsoleInteractable : MonoBehaviour, IInteractable
{
    public eConsoleJobs Job;
    public bool IsInteractable()
    {
        switch (Job)
        {
            case eConsoleJobs.ElevatorMUnlock:
                {
                    return !GPlayerManager.Instance.PlayerData.ActivatedElevatorM;
                }
        }
        return false;
    }

    public void OnInteract()
    {
        switch (Job)
        {
            case eConsoleJobs.ElevatorMUnlock:
                {
                    // TODO: Show elevator unlocking
                    GPlayerManager.Instance.PlayerData.SetItem(GSaveManager.eDataPoolID.STORY_EVENTS, GSaveManager.ACTIVATED_ELEVATOR_M);
                }
                break;
        }
    }
}
