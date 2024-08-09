using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBehaviour : MonoBehaviour
{
    Vector3 objective;

    // Update is called once per frame
    void Update()
    {
        if(objective != null)
          transform.forward = objective - transform.position;
    }

    public void SetPointerObjective(Vector3 pos)
    {
        objective = pos;
    }
}
