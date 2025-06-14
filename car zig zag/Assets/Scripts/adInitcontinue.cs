using UnityEngine;
using UnityEngine.Advertisements;

public class adInitcontinue : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId = "YOUR_ANDROID_GAME_ID";
    [SerializeField] private string _iOSGameId = "YOUR_IOS_GAME_ID";
    [SerializeField] private bool _testMode = true;

    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; // Use Android ID for Editor testing
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Debug.Log("Initializing Unity Ads...");
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else
        {
            Debug.Log("Unity Ads already initialized.");
            GetComponent<continueAD>()?.LoadAd();
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        GetComponent<continueAD>()?.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error} - {message}");
    }
}
