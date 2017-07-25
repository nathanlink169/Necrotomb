using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTransfer : LevelTransfer
{
    public override void TransferLevel()
    {
        if (isAvailable())
            base.TransferLevel();
    }

    private bool isAvailable()
    {
        switch (m_TransferType)
        {
            case eTransferType.Basic:
                return true;
            case eTransferType.LMR11ToL1R1:
                return GPlayerManager.Instance.PlayerData.ActivatedElevatorM;
            default:
                return false;
        }
    }

    [SerializeField] private eTransferType m_TransferType;
    private enum eTransferType
    {
        Basic,
        LMR11ToL1R1
    }
}