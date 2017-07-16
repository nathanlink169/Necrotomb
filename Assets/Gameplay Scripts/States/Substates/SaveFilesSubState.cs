using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameFramework;
using UnityEngine.Events;
using UnityEngine;

public class SaveFilesSubState : BaseSubState
{
    #region PushSubState
    public const string STATE_NAME = "SaveFilesSubState";

    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();
    }

    public delegate void OnSaveFilePressedDelegate(SaveData in_saveData);
    public static void PushSaveFilesSubState(OnSaveFilePressedDelegate in_delegate)
    {
        GStateManager stateManager = GStateManager.Instance;
        stateManager.OnSubStateChange += onLoadGenericDialog;
        s_delegate = in_delegate;
        stateManager.PushSubState(STATE_NAME);
    }

    private static OnSaveFilePressedDelegate s_delegate;

    private static void onLoadGenericDialog(StateInfo in_stateInfo)
    {
        SaveFilesSubState subState = in_stateInfo.State as SaveFilesSubState;

        if (subState != null)
        {
            subState.SetDelegate(s_delegate);
        }
        s_delegate = null;
    }
    #endregion

    #region Public
    public void SetDelegate(OnSaveFilePressedDelegate in_delegate) { m_delegate = in_delegate; }

    public void OnFileSelect(int in_iFileIndex)
    {
        m_delegate.Invoke(GSaveManager.Load(in_iFileIndex));
    }

    public void OnCloseSubState()
    {
        GStateManager.Instance.PopSubState(StateInfo);
    }
    #endregion

    #region private
    private void closeScene()
    {
        GStateManager.Instance.PopSubState(_stateInfo);
    }

    private OnSaveFilePressedDelegate m_delegate;
    #endregion
}
