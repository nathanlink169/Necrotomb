using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutsceneState : BaseState
{
    // TODO: Make this a proper scene
    public const string STATE_NAME = "StartingCutsceneState";

    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
        StartCoroutine(transferScene());
    }

    IEnumerator transferScene()
    {
        yield return YieldFactory.GetWaitForFixedUpdate();
        GStateManager stateManager = GStateManager.Instance;
        stateManager.ChangeState(GameState.STATE_NAME);
        stateManager.PopSubState(_stateInfo);
    }
}