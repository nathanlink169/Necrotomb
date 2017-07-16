using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSubState : BaseSubState
{
    public const string STATE_NAME = "PauseMenuSubState";

    protected override void Start()
    {
        GCore.Instance.SetCursorLockmode(false);
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
    }

    #region Buttons
    public void ResumeButton()
    {
        GCore.Instance.SetCursorLockmode(true);
        GStateManager.Instance.PopCurrentSubState();
    }

    public void LoadGameButton()
    {

    }

    public void OptionsButton()
    {
        GStateManager.Instance.PushSubState(OptionsSubState.STATE_NAME, true);
    }

    public void ExitToMainMenuButton()
    {
        callExit(true);
    }

    public void ExitToDesktopButton()
    {
        callExit(false);
    }
    #endregion

    #region Private Functions
    private void callExit(bool in_bToMainMenu)
    {
        if (in_bToMainMenu)
            GStateManager.Instance.OnSubStateChange += exitToMainMenu;
        else
            GStateManager.Instance.OnSubStateChange += exitToDesktop;

        GStateManager.Instance.PushSubState(AreYouSureQuitSubState.STATE_NAME);
    }

    private void exitToMainMenu(StateInfo in_stateInfo)
    {
        GStateManager.Instance.OnSubStateChange -= exitToMainMenu;
        ((AreYouSureQuitSubState)GStateManager.Instance.CurrentSubState).LateInit(onExitToMainMenu);
    }

    private void exitToDesktop(StateInfo in_stateInfo)
    {
        GStateManager.Instance.OnSubStateChange -= exitToMainMenu;
        ((AreYouSureQuitSubState)GStateManager.Instance.CurrentSubState).LateInit(onExitToDesktop);
    }

    private void onExitToMainMenu()
    {
        GStateManager.Instance.ChangeState(MainMenuState.STATE_NAME, LoadingScreen.eEffect.BlackFade);
    }

    private void onExitToDesktop()
    {
        GCore.Instance.Quit();
    }
    #endregion
}