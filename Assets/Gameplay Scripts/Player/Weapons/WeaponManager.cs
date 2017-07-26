using System.Collections.Generic;
using UnityEngine;
using GameFramework;

//TODO: Refactor
public class WeaponManager : BaseBehaviour
{
    #region Public Variables
    [HideInInspector] public List<IWeapon> Weapons = new List<IWeapon>();
    public int CurrentSelectedWeapon
    {
        get { return m_iCurrentSelectedWeapon; }
        set
        {
            m_iPreviousSelectedWeapon = m_iCurrentSelectedWeapon;
            m_iCurrentSelectedWeapon = value;

            for (int i = 0; i < Weapons.Count; i++)
            {
                m_WeaponObjects[i].SetActive(i == value);
            }
        }
    }
    public IWeapon CurrentWeapon { get { return Weapons[CurrentSelectedWeapon]; } }

    public GameObject GetCurrentWeaponGameObject()
    {
        GameObject weaponObject = null;

        if (m_WeaponObjects != null && m_WeaponObjects.Count > 0)
        {
            if (CurrentSelectedWeapon >= 0 && CurrentSelectedWeapon < m_WeaponObjects.Count)
            {
                weaponObject = m_WeaponObjects[CurrentSelectedWeapon];
            }
        }

        return weaponObject;
    }
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        for (int i = 0; i < m_WeaponObjects.Count; i++)
        {
            Weapons.Add(m_WeaponObjects[i].GetComponent<IWeapon>());
        }

        CurrentSelectedWeapon = 0;
    }

    void Update()
    {
        if (IsPaused)
            return;

        if (Input.anyKeyDown || Input.GetAxis("Mouse ScrollWheel") != 0.0f)
        {
            int iCurrentWeapon = CurrentSelectedWeapon;
            checkNumberKeys(ref iCurrentWeapon);
            //checkMouseWheel(ref iCurrentWeapon);

            if (iCurrentWeapon != CurrentSelectedWeapon)
            {
                CurrentSelectedWeapon = iCurrentWeapon;
            }
        }

        updateCurrentWeapon();
    }
    #endregion

    #region Private Functions
    private void checkNumberKeys(ref int out_iWeaponValue)
    {
        checkForKey(KeyCode.Alpha1, ref out_iWeaponValue, 0);
        checkForKey(KeyCode.Alpha2, ref out_iWeaponValue, 1);
        checkForKey(KeyCode.Alpha3, ref out_iWeaponValue, 2);
        checkForKey(KeyCode.Alpha4, ref out_iWeaponValue, 3);
        checkForKey(KeyCode.Alpha5, ref out_iWeaponValue, 4);
    }

    // TODO: Make this work
    private void checkMouseWheel(ref int out_iWeaponValue)
    {
        float delta = Mathf.Clamp01(Input.GetAxis("Mouse ScrollWheel"));
        out_iWeaponValue += Mathf.RoundToInt(delta);
    }

    private void checkForKey(KeyCode in_keyToCheck, ref int io_intToChange, int in_weaponValue)
    {
        if (Weapons.Count <= in_weaponValue)
            return;

        if (Weapons[in_weaponValue].GetIsUnlocked() == false)
            return;

        if (Input.GetKeyDown(in_keyToCheck))
        {
            io_intToChange = in_weaponValue;
        }
    }

    // This function is just here to remove the warning. TODO: Remove
    private void REMOVE_THIS_FUNCTION()
    {
        if (m_iPreviousSelectedWeapon == 0) { }
    }

    private void updateCurrentWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            CurrentWeapon.OnMainFireContinue();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CurrentWeapon.OnMainFireEnd();
        }
        if (Input.GetMouseButtonDown(0))
        {
            CurrentWeapon.OnMainFireBegin();
        }
        if (Input.GetMouseButton(1))
        {
            CurrentWeapon.OnAltFireContinue();
        }
        if (Input.GetMouseButtonUp(1))
        {
            CurrentWeapon.OnAltFireEnd();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CurrentWeapon.OnAltFireBegin();
        }
    }
    #endregion

    #region Private Variables
    [SerializeField] private List<GameObject> m_WeaponObjects = new List<GameObject>();
    private int m_iCurrentSelectedWeapon = 0;
    private int m_iPreviousSelectedWeapon = 0;
    #endregion
}