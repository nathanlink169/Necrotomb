using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eAudioType
{
    INVALID = -1,
    BGM = 0, // Background Music (Music overlaying the game)
    BGS,    // Background Sound (Background Sound Effects - ex. storm)
    ME,     // Music Effect (Pauses the music to play - ex. victory fanfare)
    SE,     // Sound Effect (Plays alongside other sounds - ex. footsteps)
}

public class GAudioManager : SingletonBehaviour<GAudioManager>
{
    #region BaseBehaviour
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GEventManager.StopListening(GEventManager.ON_VOLUME_CHANGED, updateVolume);
    }
    #endregion

    #region Public
    public AudioSource BGMAudioClip
    {
        get
        {
            if (m_BGM == null)
                createBGM();
            return m_BGM;
        }
    }

    public void PlayBGM()
    {
        PlayBGM(m_BGM.clip);
    }

    public void PlayBGM(AudioClip in_BGM)
    {
        if (in_BGM == BGMAudioClip.clip && m_bIsBGMPaused)
        {
            m_bIsBGMPaused = false;
        }
        else
        {
            StopAllCoroutines();
            m_bIsBGMPaused = false;
            BGMAudioClip.time = 0;
            BGMAudioClip.clip = in_BGM;
            BGMAudioClip.Play();
        }
    }

    public void PauseBGM()
    {
        StartCoroutine(pauseBGMUntilResumed());
    }

    public void StopBGM()
    {
        StopAllCoroutines();
        m_bIsBGMPaused = false;
        BGMAudioClip.time = 0;
        BGMAudioClip.Stop();
    }
    #endregion

    #region private
    private IEnumerator pauseBGMUntilResumed()
    {
        float fTime = m_BGM.time;
        m_bIsBGMPaused = true;
        yield return new WaitUntil(() => !m_bIsBGMPaused);
        m_BGM.time = fTime;
    }

    private void createBGM()
    {
        m_MusicMixer = Resources.Load("Audio/AudioMixer_Music") as AudioMixer;
        m_SFXMixer = Resources.Load("Audio/AudioMixer_SFX") as AudioMixer;
        m_VoiceMixer = Resources.Load("Audio/AudioMixer_Voice") as AudioMixer;
        updateVolume();
        GEventManager.StartListening(GEventManager.ON_VOLUME_CHANGED, updateVolume);

        GameObject obj = new GameObject("BGM");
        DontDestroyOnLoad(obj);

        m_BGM = obj.AddComponent<AudioSource>();
        m_BGM.playOnAwake = false;
        m_BGM.outputAudioMixerGroup = m_MusicMixer.FindMatchingGroups("Master")[0];
        m_BGM.loop = true;
        m_BGM.spatialBlend = 0.0f;
    }

    private void updateVolume()
    {
        m_MusicMixer.SetFloat(VOLUME, getOptionsValue(GSettingsManager.MusicVolume * GSettingsManager.GlobalVolume));
        m_SFXMixer.SetFloat(VOLUME, getOptionsValue(GSettingsManager.SFXVolume * GSettingsManager.GlobalVolume));
        m_VoiceMixer.SetFloat(VOLUME, getOptionsValue(GSettingsManager.VoiceVolume * GSettingsManager.GlobalVolume));
    }

    private float getOptionsValue(float in_fSetting)
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

    private const string VOLUME = "Volume";

    private AudioSource m_BGM = null;
    private AudioMixer m_MusicMixer = null;
    private AudioMixer m_VoiceMixer = null;
    private AudioMixer m_SFXMixer = null;
    private bool m_bIsBGMPaused = false;
    #endregion
}