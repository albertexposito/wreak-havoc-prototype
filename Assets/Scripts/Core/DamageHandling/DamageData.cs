using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageData
{
    public int damageAmount;

    public Vector3 hitPosition;
    public float knockbackForce;
    public Vector3 hitdirection;

    public DamageType damageType;

    public BasePlayerCharacter instigator;
    public DamageableElement receiver;

    public DamageData(DamageType damageType, int damage, float knockbackForce, Vector3 hitPosition, Vector3 hitDirection, BasePlayerCharacter instigator, DamageableElement receiver)
    {
        this.damageType = damageType;

        this.damageAmount = damage;
        this.knockbackForce = knockbackForce;
        
        this.hitPosition = hitPosition;
        this.hitdirection = hitDirection;

        this.instigator = instigator;
        this.receiver = receiver;
    }

    public override string ToString()
    {
        return $"damageAmount: {damageAmount}, hitDirection: {hitdirection}, knockbackForce: {knockbackForce}";

    }
}

public enum DamageType
{
    MELEE = 0,
    RANGED = 1,
}
