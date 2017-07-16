using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeypadInteractable : BaseBehaviour, IInteractable
{
    #region Public
    [Header("Materials")]
    public Material ActivatedMaterial = null;
    public Material DeactivatedMaterial = null;
    public Renderer ScreenRenderer = null;
    [Header("Lights")]
    public Light Light = null;
    public Color ActivatedColour = Color.green;
    public Color DeactivatedColour = Color.red;
    #endregion

    #region Interface Implementation
    public bool Activated { get { return m_bIsActivated; } private set { m_bIsActivated = value; } }
    public void OnInteract()
    {
        m_bIsActivated = !m_bIsActivated;
        setColours();
    }
    #endregion

    #region Private
    private void setColours()
    {
        if (ScreenRenderer != null)
        {
            ScreenRenderer.material = m_bIsActivated ? ActivatedMaterial : DeactivatedMaterial;
            Light.color = m_bIsActivated ? ActivatedColour : DeactivatedColour;
        }
    }

    private bool m_bIsActivated = false;
    #endregion
}
