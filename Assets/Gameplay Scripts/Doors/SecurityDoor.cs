using GameFramework;

public class SecurityDoor : Door
{
    public int SecurityLevelRequired = 0;
    public override bool GetCanOpen()
    {
        bool bToReturn = false;

        int securityClearance = GPlayerManager.Instance.PlayerData.SecurityClearance;
        if (securityClearance >= SecurityLevelRequired)
            bToReturn = true;

        return bToReturn;
    }
}