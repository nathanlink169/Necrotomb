﻿using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Levels
{
    LevelM,
    Level1,
    Level2,
    Level3,
    Level4,
    LevelS,
}
public class GameState : BaseState
{
    public const string STATE_NAME = "GameState";

    public GameObject Player;
    public StateMachine<Levels> StateMachine;
    public AudioSource BGM;
    public AudioClip LevelMBGM;
    public AudioClip Level1BGM;
    public AudioClip Level2BGM;

    #region MonoBehaviour
    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();

        StateMachine = StateMachine<Levels>.Initialize(this);

        LoadPoint loadPoint = GPlayerManager.Instance.PlayerData.CurrentSavePoint;
        switch (loadPoint)
        {
            case LoadPoint.AreaMRoom1:
                CurrentSpawnPoint = AreaMRoom1_SpawnPoint;
                activateLevel(0);
                break;
            case LoadPoint.AreaMRoom4:
                CurrentSpawnPoint = AreaMRoom4_SpawnPoint;
                activateLevel(0);
                break;
            case LoadPoint.Area1Room2:
                CurrentSpawnPoint = Area1Room2_SpawnPoint;
                activateLevel(1);
                break;
            case LoadPoint.Area1Room8:
                CurrentSpawnPoint = Area1Room8_SpawnPoint;
                activateLevel(1);
                break;
            case LoadPoint.Area1Room15:
                CurrentSpawnPoint = Area1Room15_SpawnPoint;
                activateLevel(1);
                break;
            default:
                break;
        }

        SetCurrentSpawnPoint(loadPoint);
        Player.transform.position = CurrentSpawnPoint.transform.position;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
#else
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
#endif
        {
            GStateManager.Instance.PushSubState(PauseSubState.STATE_NAME, true);
        }
    }
    #endregion

    #region StateMachine
    private void LevelM_Enter()
    {
        AreaM.SetActive(true);
        Area1.SetActive(false);
        Area2.SetActive(false);

        BGM.clip = LevelMBGM;
        BGM.Play();
    }
    private void Level1_Enter()
    {
        AreaM.SetActive(false);
        Area1.SetActive(true);
        Area2.SetActive(false);

        BGM.clip = Level1BGM;
        BGM.Play();
    }
    private void Level2_Enter()
    {
        AreaM.SetActive(false);
        Area1.SetActive(false);
        Area2.SetActive(true);

        BGM.clip = Level2BGM;
        BGM.Play();
    }
    private void Level3_Enter() { }
    private void Level4_Enter() { }
    private void LevelS_Enter() { }
    #endregion

    #region Levels
    public GameObject AreaM;
    public GameObject Area1;
    public GameObject Area2;
    #endregion

    #region SavePoints
    [HideInInspector]
    public GameObject CurrentSpawnPoint;
    public GameObject AreaMRoom1_SpawnPoint;
    public GameObject AreaMRoom4_SpawnPoint;
    public GameObject Area1Room2_SpawnPoint;
    public GameObject Area1Room8_SpawnPoint;
    public GameObject Area1Room15_SpawnPoint;
    #endregion

    #region Private Functions
    private void activateLevel(int in_iLevelID)
    {
        switch (in_iLevelID)
        {
            case 0:
                StateMachine.ChangeState(Levels.LevelM);
                break;
            case 1:
                StateMachine.ChangeState(Levels.Level1);
                break;
            case 2:
                StateMachine.ChangeState(Levels.Level2);
                break;
        }
    }

    private void SetCurrentSpawnPoint(LoadPoint aLoadPoint)
    {
        switch (aLoadPoint)
        {
            case LoadPoint.AreaMRoom1:
                CurrentSpawnPoint = AreaMRoom1_SpawnPoint;
                break;
            case LoadPoint.AreaMRoom4:
                CurrentSpawnPoint = AreaMRoom4_SpawnPoint;
                break;
            case LoadPoint.Area1Room2:
                CurrentSpawnPoint = Area1Room2_SpawnPoint;
                break;
            case LoadPoint.Area1Room8:
                CurrentSpawnPoint = Area1Room2_SpawnPoint;
                break;
            case LoadPoint.Area1Room15:
                CurrentSpawnPoint = Area1Room15_SpawnPoint;
                break;
            default:
                break;
        }
    }
    #endregion
}