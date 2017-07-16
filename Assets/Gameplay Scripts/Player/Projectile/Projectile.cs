using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : BaseBehaviour
{
    public Vector3 Direction { get { return m_vDirection; } set { m_vDirection = value.normalized; } }
    public float Speed { get { return m_fSpeed; } set { m_fSpeed = value; } }

    public void Fire(Vector3 in_vDirection, float in_fSpeed)
    {
        Direction = in_vDirection;
        Speed = in_fSpeed;
        vForward = Direction;

    }

    private void FixedUpdate()
    {
        m_vNextPosition = vWorldPosition + m_vDirection * m_fSpeed * Time.fixedDeltaTime;

        RaycastHit rHit;
        if (Physics.Linecast(vWorldPosition, m_vNextPosition, out rHit, -1, QueryTriggerInteraction.Ignore))
        {
            vWorldPosition = rHit.point;
            OnObjectHit(rHit.transform.gameObject, rHit.point);

            gameObject.SetActive(false);
        }
        else
        {
            vWorldPosition = m_vNextPosition;
        }
    }

    protected abstract void OnObjectHit(GameObject in_gObjectHit, Vector3 in_vImpactPoint);

    private Vector3 m_vDirection = Vector3.zero;
    private float m_fSpeed = 0.0f;
    private Vector3 m_vNextPosition;
}
