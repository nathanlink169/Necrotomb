public enum eWeaponTypes
{
    Pistol,
    REI,
    GuidedRocket,
}

public interface IWeapon
{
    void OnMainFireBegin();
    void OnMainFireContinue();
    void OnMainFireEnd();

    void OnAltFireBegin();
    void OnAltFireContinue();
    void OnAltFireEnd();

    bool GetIsUnlocked();
}