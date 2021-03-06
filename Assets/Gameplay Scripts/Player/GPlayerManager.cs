﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

public class GPlayerManager : SingletonBehaviour<GPlayerManager>
{
    #region Public
    public GameObject PlayerObject
    {
        get
        {
            if (m_PlayerObject == null)
                m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
            return m_PlayerObject;
        }
    }

    public Camera PlayerMainCamera
    {
        get
        {
            if (m_PlayerMainCamera == null)
                m_PlayerMainCamera = PlayerObject.GetComponentInChildren<FirstPersonCamera>().GetComponent<Camera>();
            return m_PlayerMainCamera;
        }
    }
    public GPlayerHealth Health { get { return GPlayerHealth.Instance; } }
    public SaveData PlayerData { get; set; }

    public WeaponManager PlayerWeaponManager
    {
        get
        {
            if (m_PlayerWeaponManager == null)
                m_PlayerWeaponManager = PlayerObject.GetComponentInChildren<WeaponManager>();
            return m_PlayerWeaponManager;
        }
    }
    #endregion

    #region Private
    GameObject m_PlayerObject = null;
    Camera m_PlayerMainCamera = null;
    WeaponManager m_PlayerWeaponManager = null;
    #endregion
}