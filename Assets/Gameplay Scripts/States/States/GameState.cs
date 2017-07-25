using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLevels
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
    public StateMachine<eLevels> StateMachine;
    public AudioClip LevelMBGM;
    public AudioClip Level1BGM;
    public AudioClip Level2BGM;

    #region MonoBehaviour
    protected override void Start()
    {
        _stateInfo = new StateInfo(STATE_NAME, this);
        base.Start();

        StateMachine = StateMachine<eLevels>.Initialize(this);

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

    #region Public
    public void TransferPlayer(LoadPoint in_LoadPoint)
    {
        switch (in_LoadPoint)
        {
            case LoadPoint.AreaMRoom1:
                activateLevel(0);
                Player.transform.position = AreaMRoom1_SpawnPoint.transform.position;
                break;
            case LoadPoint.AreaMRoom4:
                activateLevel(0);
                Player.transform.position = AreaMRoom4_SpawnPoint.transform.position;
                break;
            case LoadPoint.AreaMRoom11:
                activateLevel(0);
                Player.transform.position = AreaMRoom11_SpawnPoint.transform.position;
                break;
            case LoadPoint.Area1Room1:
                activateLevel(1);
                Player.transform.position = Area1Room1_SpawnPoint.transform.position;
                break;
            case LoadPoint.Area1Room2:
                activateLevel(1);
                Player.transform.position = Area1Room2_SpawnPoint.transform.position;
                break;
            case LoadPoint.Area1Room8:
                activateLevel(1);
                Player.transform.position = Area1Room8_SpawnPoint.transform.position;
                break;
            case LoadPoint.Area1Room15:
                activateLevel(1);
                Player.transform.position = Area1Room15_SpawnPoint.transform.position;
                break;
            default:
                break;
        }
    }
    #endregion

    #region StateMachine
    private void LevelM_Enter()
    {
        AreaM.SetActive(true);
        Area1.SetActive(false);
        Area2.SetActive(false);

        GAudioManager.Instance.PlayBGM(LevelMBGM);
        GStateManager.Instance.EnableLoadingSpinner(false);
    }
    private void Level1_Enter()
    {
        AreaM.SetActive(false);
        Area1.SetActive(true);
        Area2.SetActive(false);

        GAudioManager.Instance.PlayBGM(Level1BGM);
        GStateManager.Instance.EnableLoadingSpinner(false);
    }
    private void Level2_Enter()
    {
        AreaM.SetActive(false);
        Area1.SetActive(false);
        Area2.SetActive(true);

        GAudioManager.Instance.PlayBGM(Level2BGM);
        GStateManager.Instance.EnableLoadingSpinner(false);
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
    public GameObject AreaMRoom11_SpawnPoint;
    public GameObject Area1Room1_SpawnPoint;
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
                StateMachine.ChangeState(eLevels.LevelM);
                break;
            case 1:
                StateMachine.ChangeState(eLevels.Level1);
                break;
            case 2:
                StateMachine.ChangeState(eLevels.Level2);
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