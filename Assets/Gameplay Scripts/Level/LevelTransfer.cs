using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransfer : BaseBehaviour
{
    public LoadPoint ToTransferTo;

    public virtual void TransferLevel()
    {
        GStateManager.Instance.EnableLoadingSpinner(true);
        (CurrentState as GameState).TransferPlayer(ToTransferTo);
    }
}