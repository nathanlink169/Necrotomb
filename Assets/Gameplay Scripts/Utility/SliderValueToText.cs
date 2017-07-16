using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;

public class SliderValueToText : BaseBehaviour
{
    public Text Text;
    public string Prefix;
    public string Suffix;

    private Slider m_Slider;
    
    void Start()
    {
        m_Slider = GetComponent<Slider>();
    }

    void Update()
    {
        Text.text = Prefix + m_Slider.value.ToInt().ToString() + Suffix;
    }
}