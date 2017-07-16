using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDamageType
{
    Point,
    Splash,
}
public interface IDamageable
{
    void OnTakeDamage(float aDamage, eDamageType in_eDamageType = eDamageType.Point);
}
