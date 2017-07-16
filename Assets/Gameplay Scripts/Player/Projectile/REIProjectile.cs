using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not inheriting from Projectile because no raycasting actually takes place
public class REIProjectile : BaseBehaviour
{
    public void Init(Transform in_parent, Vector3 in_position)
    {
        transform.parent = in_parent;
        vWorldPosition = in_position;
    }

    public void Explode()
    {
        // TODO: Damage objects
        transform.parent = null;
        vWorldPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
