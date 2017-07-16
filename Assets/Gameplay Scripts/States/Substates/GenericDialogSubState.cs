using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameFramework;
using UnityEngine.Events;
using UnityEngine;

public class GenericDialogSubState : BaseSubState
{
    #region PushSubState
    public const string STATE_NAME = "GenericDialogSubState";

    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
    }

    public static void PushGenericDialog(string in_sTitle,
                                         string in_sDescription,
                                         string in_sLeftButtonText = "Okay",
                                         UnityAction in_LeftButtonDelegate = null,
                                         string in_sRightButtonText = "Cancel",
                                         UnityAction in_RightButtonDelegate = null)
    {
        GStateManager stateManager = GStateManager.Instance;
        stateManager.OnSubStateChange += onLoadGenericDialog;
        s_sTitle = in_sTitle;
        s_sDescription = in_sDescription;
        s_sLeftButtonText = in_sLeftButtonText;
        s_sRightButtonText = in_sRightButtonText;
        s_LeftButtonDelegate = in_LeftButtonDelegate;
        s_RightButtonDelegate = in_RightButtonDelegate;
        stateManager.PushSubState(STATE_NAME);
    }

    private static string s_sTitle = "";
    private static string s_sDescription = "";
    private static string s_sLeftButtonText = "";
    private static string s_sRightButtonText = "";
    private static UnityAction s_LeftButtonDelegate = null;
    private static UnityAction s_RightButtonDelegate = null;

    private static void onLoadGenericDialog(StateInfo in_stateInfo)
    {
        GenericDialogSubState dialog = in_stateInfo.State as GenericDialogSubState;

        if (dialog != null)
        {
            dialog.Title.text = s_sTitle;
            dialog.Description.text = s_sDescription;
            dialog.LeftButtonText.text = s_sLeftButtonText;
            dialog.RightButtonText.text = s_sRightButtonText;
            dialog.LeftButtonDelegate = s_LeftButtonDelegate;
            dialog.RightButtonDelegate = s_RightButtonDelegate;
        }

        s_sTitle = "";
        s_sDescription = "";
        s_sLeftButtonText = "";
        s_sRightButtonText = "";
        s_LeftButtonDelegate = null;
        s_RightButtonDelegate = null;
    }
    #endregion

    #region Public
    public Text Title;
    public Text Description;
    public Text LeftButtonText;
    public Text RightButtonText;
    [HideInInspector] public UnityAction LeftButtonDelegate;
    [HideInInspector] public UnityAction RightButtonDelegate;

    public void OnLeftButtonPress()
    {
        if (LeftButtonDelegate != null)
            LeftButtonDelegate.Invoke();
        closeScene();
    }

    public void OnRightButtonPress()
    {
        if (RightButtonDelegate != null)
            RightButtonDelegate.Invoke();
        closeScene();
    }
    #endregion

    #region private
    private void closeScene()
    {
        GStateManager.Instance.PopSubState(_stateInfo);
    }
    #endregion
}
