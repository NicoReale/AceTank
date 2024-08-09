using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossTrigger : MonoBehaviour
{
    public Action OnBossKilled;

    private void OnDisable()
    {
        OnBossKilled?.Invoke();
    }
}
