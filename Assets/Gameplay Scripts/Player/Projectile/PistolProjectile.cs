using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : Projectile
{
    public float Damage = 0f;

    protected override void OnObjectHit(GameObject in_gObjectHit, Vector3 in_vImpactPoint)
    {
        IDamageable iDamageableComponent = in_gObjectHit.GetComponent<IDamageable>();

        if (iDamageableComponent != null)
            iDamageableComponent.OnTakeDamage(Damage, eDamageType.Point);
    }
}