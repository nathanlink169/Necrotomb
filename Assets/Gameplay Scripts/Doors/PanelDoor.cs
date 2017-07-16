using GameFramework;

public class PanelDoor : Door
{
    public KeypadInteractable Keypad;
    public override bool GetCanOpen()
    {
        bool bToReturn = Keypad.Activated;
        return bToReturn;
    }
}