using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    public const string STATE_NAME = "MainMenuState";

    protected override void Start()
    {
        GCore.Instance.SetCursorLockmode(false);
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
    }

    #region Buttons
    public void LoadGameButton()
    {
        if (IsPaused)
        {
            return;
        }

        SaveFilesSubState.PushSaveFilesSubState(onLoadGame);
    }

    public void NewGameButton()
    {
        if (IsPaused)
        {
            return;
        }

        SaveFilesSubState.PushSaveFilesSubState(onNewGame);
    }

    public void OptionsButton()
    {
        if (IsPaused)
        {
            return;
        }

        GStateManager.Instance.PushSubState(OptionsSubState.STATE_NAME, true);
    }

    public void ExitGameButton()
    {
        if (IsPaused)
        {
            return;
        }

        GCore.Instance.Quit();
    }
    #endregion

    #region Private
    private void onLoadGame(SaveData in_saveData)
    {
        GPlayerManager.Instance.PlayerData = in_saveData;
        GStateManager.Instance.ChangeState(StartingCutsceneState.STATE_NAME);
    }

    private void onNewGame(SaveData in_saveData)
    {
        GSaveManager.Delete(in_saveData.SaveDataIndex);
        GPlayerManager.Instance.PlayerData = new SaveData(in_saveData.SaveDataIndex);
        GStateManager.Instance.ChangeState(StartingCutsceneState.STATE_NAME);
    }
    #endregion
}