using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] public UnityEvent staminaEffect;

    [SerializeField] Image _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = "5038397";

    static AdsManager instance = null;
    public static AdsManager Instance => instance;

    private void Awake()
    {
#if UNITY_IOS
                _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        if (instance == null)
        {
            instance = FindObjectOfType<AdsManager>();
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        _showAdButton.enabled = false;
        Advertisement.Show(_adUnitId, this);
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);

        if (placementId.Equals(_adUnitId))
        {
            _showAdButton.enabled = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            //Debug.Log("Unity Ads Rewarded Ad Completed");
            //_stamina.FillStamina();
            staminaEffect?.Invoke();
        }
    }
}
