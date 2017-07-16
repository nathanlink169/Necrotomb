using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class REIWeapon : BaseBehaviour, IWeapon
{
    #region Public Variables
    public GameObject REIProjectile;
    #endregion

    #region Public Constants

    #endregion

    #region IWeapon
    public void OnMainFireBegin()
    {
        if (!checkFireAvailable())
            return;

        Camera playerCamera = GPlayerManager.Instance.PlayerMainCamera;

        RaycastHit hitInfo;
        if (MathUtils.RaycastFirstObjectHit(out hitInfo, playerCamera.transform.position, playerCamera.transform.forward, 250.0f))
        {
            GameObject gObject = GPoolManager.Instance.Get(REIProjectile, true);
            REIProjectile projectile = gObject.GetComponent<REIProjectile>();
            projectile.Init(hitInfo.transform, hitInfo.point);
            m_projectiles.Add(projectile);
        }
    }

    public void OnAltFireBegin()
    {

    }

    public void OnAltFireContinue()
    {

    }

    public void OnAltFireEnd()
    {

    }

    public bool GetIsUnlocked()
    {
        //return GPlayerManager.Instance.PlayerData.UnlockedREI;
        return true;
    }

    public void OnMainFireContinue() { }
    public void OnMainFireEnd() { }
    #endregion

    #region Private
    private bool checkFireAvailable()
    {
        SaveData saveData = GPlayerManager.Instance.PlayerData;

        bool bToReturn = (saveData.UpgradedREI || m_projectiles.Count == 0);
        return bToReturn;
    }

    private List<REIProjectile> m_projectiles = new List<REIProjectile>();
    #endregion
}