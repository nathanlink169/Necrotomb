using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

public class QuickTurn : BaseBehaviour
{
    #region Public Variables
    public FirstPersonCamera CameraToDisable;
    #endregion

    void Update () 
	{
		if(Input.GetMouseButtonDown(2))
        {
            StartCoroutine(quickTurn());
        }
	}

    private IEnumerator quickTurn()
    {
        CameraToDisable.enabled = false;
        float amountToTurnPerFrame = TURN_SPEED * Time.fixedDeltaTime;
        for(int i = 0; i < 180f / amountToTurnPerFrame; i++)
        {
            transform.Rotate(0f, amountToTurnPerFrame, 0f);
            yield return YieldFactory.GetWaitForFixedUpdate();
        }
        CameraToDisable.enabled = true;
    }

    private const float TURN_SPEED = 1000f;
}
