using GameFramework;
using System.Collections;
using UnityEngine;

public abstract class Door : BaseBehaviour
{
    #region Public Const
    public const float CLOSE_TO_DOOR_DISTANCE = 10.0f;
    #endregion

    #region Monobehaviour
    private void Start()
    {
        StartCoroutine(checkPlayerDistance());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        m_Player = null;
    }
    #endregion

    #region Public
    public abstract bool GetCanOpen();

    // TODO: Change this to be proper
    public void SetOpen(bool in_bIsOpen)
    {
        if (in_bIsOpen != IsOpen)
        {
            GetComponent<Collider>().enabled = !in_bIsOpen;
            GetComponent<Renderer>().enabled = !in_bIsOpen;
            IsOpen = in_bIsOpen;
        }
    }

    public bool IsOpen { get; protected set; }
    #endregion

    #region Private
    private IEnumerator checkPlayerDistance()
    {
        float vDistance = 0.0f;
        while (m_bIsChecking)
        {
            if (m_Player == null)
            {
                m_Player = GameObject.FindObjectOfType<FirstPersonMovement>().gameObject;
                yield return YieldFactory.GetWaitForSeconds(CHECK_DISTANCE_DELAY);
                continue;
            }
            //Debug.Log(vDistance);
            vDistance = Vector3.Distance(vWorldPosition, m_Player.transform.position);
            if (vDistance < CLOSE_TO_DOOR_DISTANCE && GetCanOpen())
            {
                SetOpen(true);
            }
            else
            {
                SetOpen(false);
            }

            yield return YieldFactory.GetWaitForSeconds(CHECK_DISTANCE_DELAY);
        }
    }

    private static GameObject m_Player = null;
    private bool m_bIsChecking = true;
    private const float CHECK_DISTANCE_DELAY = 0.2f;
    #endregion
}