using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustJumpAbility : BaseBehaviour
{
    #region Monobehaviour
    private void Start()
    {
        StartCoroutine(waitUntilUnlock());
    }
    #endregion
    #region Private
    private IEnumerator waitUntilUnlock()
    {
        yield return new WaitUntil(() => GPlayerManager.Instance.PlayerData.UnlockedThrustJump == true);
        m_bIsUnlocked = true;
        StartCoroutine(runAbility());
    }

    private IEnumerator runAbility()
    {
        Rigidbody playersRigidbody = GPlayerManager.Instance.PlayerObject.Rigidbody();
        while (m_bIsUnlocked)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift) && playersRigidbody.IsGrounded() == false && !IsPaused);

            if (m_bIsUnlocked)
            {
                playersRigidbody.AddForce(playersRigidbody.transform.forward * THRUST_JUMP_POWER + Vector3.up, ForceMode.Impulse);
            }

            yield return YieldFactory.GetWaitForSeconds(COOLDOWN);
        }
        StartCoroutine(waitUntilUnlock());
    }

    private bool m_bIsUnlocked = false;
    private const float THRUST_JUMP_POWER = 40.0f;
    private const float COOLDOWN = 0.25f;
    #endregion
}