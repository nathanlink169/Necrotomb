using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
    public float SpinSpeed = 5.0f;
    public Image LargeSpinner;
    public Image MediumSpinner;
    public Image SmallSpinner;

    void FixedUpdate()
    {
        LargeSpinner.rectTransform.Rotate(0.0f, 0.0f, Time.fixedDeltaTime * SpinSpeed * (1 + TIME_MODIFIER));
        MediumSpinner.rectTransform.Rotate(0.0f, 0.0f, Time.fixedDeltaTime * SpinSpeed * 1);
        SmallSpinner.rectTransform.Rotate(0.0f, 0.0f, Time.fixedDeltaTime * SpinSpeed * (1 - TIME_MODIFIER));
    }

    private const float TIME_MODIFIER = 0.25f;
    private float m_LargeTime;
    private float m_MediumTime;
    private float m_SmallTime;
}