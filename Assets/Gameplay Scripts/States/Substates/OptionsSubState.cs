using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSubState : BaseSubState
{
    public const string STATE_NAME = "OptionsSubState";

    #region Public Properties
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXSlider;
    public Slider VoiceSlider;

    public Dropdown Resolution;
    public Dropdown TextureQuality;
    public Dropdown Shadows;
    public Slider Brightness;

    public Slider FieldOfView;
    public Toggle Subtitles;
    public Toggle Fullscreen;
    public Toggle Tooltips;
    #endregion

    #region Monobehaviour
    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();

        MasterVolumeSlider.value = GSettingsManager.GlobalVolume * 100f;
        MusicVolumeSlider.value = GSettingsManager.MusicVolume * 100f;
        SFXSlider.value = GSettingsManager.SFXVolume * 100f;
        VoiceSlider.value = GSettingsManager.VoiceVolume * 100f;

        Resolution.value = GSettingsManager.Resolution;
        TextureQuality.value = GSettingsManager.TextureQuality;
        Shadows.value = GSettingsManager.Shadows;
        Brightness.value = GSettingsManager.Brightness * 100f;

        FieldOfView.value = GSettingsManager.FieldOfView;
        Subtitles.isOn = GSettingsManager.Subtitles;
        Fullscreen.isOn = GSettingsManager.Fullscreen;
        Tooltips.isOn = GSettingsManager.Tooltips;
    }
    #endregion

    #region Buttons
    public void OnExitButton()
    {
        GSettingsManager.GlobalVolume = (float)(MasterVolumeSlider.value) / 100f;
        GSettingsManager.MusicVolume = (float)(MusicVolumeSlider.value) / 100f;
        GSettingsManager.SFXVolume = (float)(SFXSlider.value) / 100f;
        GSettingsManager.VoiceVolume = (float)(VoiceSlider.value) / 100f;

        GSettingsManager.Resolution = Resolution.value;
        GSettingsManager.TextureQuality = TextureQuality.value;
        GSettingsManager.Shadows = Shadows.value;
        GSettingsManager.Brightness = (float)(Brightness.value) / 100f;

        GSettingsManager.FieldOfView = (float)(FieldOfView.value);
        GSettingsManager.Subtitles = Subtitles.isOn;
        GSettingsManager.Fullscreen = Fullscreen.isOn;
        GSettingsManager.Tooltips = Tooltips.isOn;

        GStateManager.Instance.PopSubState(_stateInfo);
    }
    #endregion
}