using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour, IDetector
{
    public bool isDetected = false;
    GameObject detectedBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerBehaviour>() != null)
        {
            isDetected = true;
            detectedBehaviour = other.GetComponent<PlayerBehaviour>().gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerBehaviour>() != null)
        {
            isDetected = false;
            detectedBehaviour = null;
        }
    }

    public bool IsDetected()
    {
        return isDetected;
    }

    public GameObject Object()
    {
        return detectedBehaviour;
    }
}
