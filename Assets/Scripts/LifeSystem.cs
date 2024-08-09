using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour, IDamagable
{
    public float HP;
    public float MaxHP;

    public Action UIUpdate;

    public virtual void ReceiveDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            StartCoroutine(onDeath());
            return;
        }
        DamageFeedback();
    }

    public virtual void Heal(float healing)
    {
        float newLife = Mathf.Min(HP + healing, MaxHP);
        HP = newLife;
        UIUpdate?.Invoke();
    }

    public virtual void DamageFeedback()
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerator ColorChange()
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerator onDeath()
    {
        throw new NotImplementedException();
    }
}
