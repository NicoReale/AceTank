using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBehaviour : MonoBehaviour
{
    LineRenderer lineRenderer;
    public LayerMask _layerMask;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, 1000f, _layerMask, QueryTriggerInteraction.Collide))
        {
            lineRenderer.SetPosition(1, hit.point);
            if(hit.collider.GetComponent<IDamagable>() != null)
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
        }
    }
}
