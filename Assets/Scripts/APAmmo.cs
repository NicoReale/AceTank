using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APAmmo : Ammo
{

    float liveTime = 0;

    public override void Fire(Vector3 direction)
    {
        p_Directon = direction;
    }


    void Update()
    {
        transform.position += p_Directon * 15 * Time.deltaTime;
        liveTime += Time.deltaTime;
        if(liveTime > 3)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        var targetHit = collision.GetComponent<IDamagable>();
        if (collision != null)
        {
            if(targetHit != null)
            {
                targetHit.ReceiveDamage(_Damage);
            }
            Destroy(gameObject);
        }
    }
}
