using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreYouSureQuitSubState : BaseSubState
{
    public const string STATE_NAME = "AreYouSureQuitSubState";
    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
    }

    public delegate void OnAffirmativeButtonPress();
    public void LateInit(OnAffirmativeButtonPress in_dAffirmativeButtonPressDelegate)
    {
        m_dAffirmativeButtonPressDelegate = in_dAffirmativeButtonPressDelegate;
    }

    public void OnYesButtonPress()
    {
        m_dAffirmativeButtonPressDelegate.Invoke();
    }

    public void OnNoButtonPress()
    {
        GStateManager.Instance.PopCurrentSubState();
    }

    private OnAffirmativeButtonPress m_dAffirmativeButtonPressDelegate;
}
