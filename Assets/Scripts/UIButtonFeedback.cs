using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float waveSpeed = 1;
    public float waveAmplitude = 1;

    private RectTransform _rectTransform;
    private Vector3 _startPosition;

    /// <summary>
    /// Scale Lerp
    /// </summary>
    public float scaleFactor = 1.5f;
    public float lerpSpeed = 10.0f;
    private Vector3 _startScale;
    private bool isPressed = false;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.localPosition;
        _startScale = transform.localScale;
        waveSpeed *= Random.Range(1, 1.5f);
    }

    private void Update()
    {
        float wave = Mathf.Sin(Time.time * waveSpeed) * waveAmplitude;
        _rectTransform.localPosition = _startPosition + new Vector3(0, wave, 0);
        if (isPressed)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale * scaleFactor, Time.deltaTime * lerpSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale, Time.deltaTime * lerpSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
