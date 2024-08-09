using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    public float _Damage = 20;
    public Vector3 p_Directon;
    public abstract void Fire(Vector3 direction);

    public virtual void setDamage(float damage)
    {
        _Damage += damage;
    }
}
