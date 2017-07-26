using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PistolWeapon : BaseBehaviour, IWeapon
{
    #region Public Variables
    public GameObject PistolProjectile;
    public Transform StartFirePoint = null;
    #endregion

    #region Public Constants
    public const float SINGLE_SHOT_DAMAGE = 5.0f;
    public const float AUTO_SHOT_DAMAGE = 1.0f;
    public const float SPEED = 250.0f;
    #endregion

    #region IWeapon
    public void OnMainFireBegin()
    {
        GameObject gObject = GPoolManager.Instance.Get(PistolProjectile, StartFirePoint.position, Quaternion.identity, true);
        PistolProjectile pProj = gObject.GetComponent<PistolProjectile>();

        // Aim the bullet towards the camera's raycast hit point.
        // Just using the forward vector would cause inaccuracies at far range targets.
        FirstPersonCamera playerCamera = GPlayerManager.Instance.PlayerMainCamera.GetComponent<FirstPersonCamera>();

        Vector3 direction = playerCamera.RaycastPoint - StartFirePoint.position;

        pProj.Fire(direction, SPEED);
        pProj.Damage = SINGLE_SHOT_DAMAGE;
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
        return true;
    }

    public void OnMainFireContinue() { }
    public void OnMainFireEnd() { }
    #endregion

    #region Private Coroutines

    #endregion
}