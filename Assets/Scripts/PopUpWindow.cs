using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PopUpWindow : MonoBehaviour
{
    public event Action OnMethodCall;

    public void ConfirmButtonPressed()
    {
        OnMethodCall();
        Destroy(gameObject);
    }

    public void CancelButtonPressed()
    {
        Destroy(gameObject);
    }

}
