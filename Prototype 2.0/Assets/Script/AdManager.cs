using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour {
	public static AdManager Instance{set;get;}

	public string bannerId;
	public string videoId;
	
	private void Start()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);
	
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Admob.Instance().initAdmob(bannerId,videoId);
		//Admob.Instance().setTesting(true);
		Admob.Instance().loadInterstitial();
		#endif
	}
	
	public void ShowBanner()
	{
		#if UNITY_EDITOR
		Debug.Log("Unable to play ads from EDITOR");
		#elif UNITY_ANDROID	
			Admob.Instance().showBannerRelative(AdSize.Banner,AdPosition.BOTTOM_CENTER, 8);
		#endif
	}
	
	public void DestroyBanner()
	{
		Admob.Instance().removeBanner();
	}
	
	public void ShowVideo()
	{
		#if UNITY_EDITOR
		Debug.Log("Unable to play ads from EDITOR");
		#elif UNITY_ANDROID
		if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
			Admob.Instance().loadInterstitial();

        }
        else
        {
            Admob.Instance().loadInterstitial();
            Admob.Instance().showInterstitial();
        }
		#endif
	}

}
