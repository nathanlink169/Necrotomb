using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : SingletonBehaviour<AudioMixerManager>
{
    public AudioMixer MusicMixer;
    public AudioMixer VoiceMixer;
    public AudioMixer SFXMixer;

    public const string VOLUME = "Volume";

    private void Start()
    {
        MusicMixer = Resources.Load("Audio/AudioMixer_Music") as AudioMixer;
        SFXMixer = Resources.Load("Audio/AudioMixer_SFX") as AudioMixer;
        VoiceMixer = Resources.Load("Audio/AudioMixer_Voice") as AudioMixer;
        UpdateVolume();
        GEventManager.StartListening(GEventManager.ON_VOLUME_CHANGED, UpdateVolume);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GEventManager.StopListening(GEventManager.ON_VOLUME_CHANGED, UpdateVolume);
    }

    private void UpdateVolume()
    {
        MusicMixer.SetFloat(VOLUME, GetOptionsValue(GSettingsManager.MusicVolume * GSettingsManager.GlobalVolume));
        SFXMixer.SetFloat(VOLUME, GetOptionsValue(GSettingsManager.SFXVolume * GSettingsManager.GlobalVolume));
        VoiceMixer.SetFloat(VOLUME, GetOptionsValue(GSettingsManager.VoiceVolume * GSettingsManager.GlobalVolume));
    }

    private float GetOptionsValue(float in_fSetting)
    {
        float fToReturn = 0.0f;
        if (in_fSetting >= 0.2f)
        {
            fToReturn = 50.0f * in_fSetting - 50.0f;
        }
        else
        {
            fToReturn = 200.0f * in_fSetting - 80.0f;
        }
        return fToReturn;
    }
}