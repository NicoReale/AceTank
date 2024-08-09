using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPlayer : MonoBehaviour
{
    public Transform target;
    public float height = 5;
    public float distance = 10;

    void Update()
    {
        Vector3 cameraPosition = target.position - target.forward * distance;
        cameraPosition.y = height;
        transform.position = cameraPosition;
        transform.LookAt(target);
    }
}
