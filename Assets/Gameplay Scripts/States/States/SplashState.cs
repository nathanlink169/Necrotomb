using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSplashStates
{
    Black,
    StartUpManagers,
    Unity,
    Somniplex,
    Complete
}

public class SplashState : BaseState
{
    #region BaseState
    public const string STATE_NAME = "SplashState";

    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();

        m_StateMachine = StateMachine<eSplashStates>.Initialize(this);
        m_StateMachine.ChangeState(eSplashStates.Black);
    }
    #endregion

    #region Public
    public Image UnityLogo;
    public Image SomniplexLogo;
    #endregion

    #region StateMachine
    private IEnumerator Black_Enter()
    {
        yield return YieldFactory.GetWaitForFixedUpdate();
        m_StateMachine.ChangeState(eSplashStates.StartUpManagers);
    }

    private IEnumerator StartUpManagers_Enter()
    {
        yield return YieldFactory.GetWaitForFixedUpdate();
        GStateManager.Instance.ForceStateInfo(_stateInfo);

        yield return YieldFactory.GetWaitForFixedUpdate();
        GCore.Instance.EnsureMgrsAreSetup();

        yield return YieldFactory.GetWaitForFixedUpdate();
        GStateManager.Instance.EnableLoadingSpinner(false);

        yield return YieldFactory.GetWaitForFixedUpdate();
        m_StateMachine.ChangeState(eSplashStates.Unity);
    }

    private IEnumerator Unity_Enter()
    {
        yield return YieldFactory.GetWaitForFixedUpdate();

        Color colour = UnityLogo.color;
        while (colour.a <= 1.0f)
        {
            colour.a += Time.fixedDeltaTime * 0.5f;
            UnityLogo.color = colour;
            yield return YieldFactory.GetWaitForFixedUpdate();
        }

        yield return YieldFactory.GetWaitForSeconds(1.0f);
        m_StateMachine.ChangeState(eSplashStates.Somniplex);
    }

    private IEnumerator Somniplex_Enter()
    {
        yield return YieldFactory.GetWaitForFixedUpdate();

        Color colour = SomniplexLogo.color;
        while (colour.a <= 1.0f)
        {
            colour.a += Time.fixedDeltaTime * 0.5f;
            SomniplexLogo.color = colour;
            yield return YieldFactory.GetWaitForFixedUpdate();
        }

        yield return YieldFactory.GetWaitForSeconds(1.0f);
        m_StateMachine.ChangeState(eSplashStates.Complete);
    }

    private void Complete_Enter()
    {
        GStateManager.Instance.ChangeState(MainMenuState.STATE_NAME);
    }
    #endregion

    #region Private
    private StateMachine<eSplashStates> m_StateMachine;
    #endregion
}
