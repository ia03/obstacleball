#if UNITY_ANDROID || UNITY_IPHONE

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    string gameID;

    public GameObject coinpanel;
    Text cointext;
    
    
    public void Awake()
    {
#if UNITY_ANDROID
        gameID = "1142904";
#endif
#if UNITY_IPHONE
        gameID = "1142905";
#endif
        Advertisement.Initialize(gameID, true);
    }

    public void coinvideo()
    {
        ShowAd("rewardedVideo");
        coinpanel.SetActive(false);
        Time.timeScale = 1f;

        
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        GetComponent<PlayerController>().reloadcointext();
    }

    public void novideo()
    {
        coinpanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowAd(string zone = "")
    {


        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);
        return;
    }

    void AdCallbackhandler(ShowResult result)
    {
        cointext = GameObject.FindWithTag("cointext").GetComponent<Text>();
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 3);
                cointext.text = "Coins: " + PlayerPrefs.GetInt("coins");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
        
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}


#endif