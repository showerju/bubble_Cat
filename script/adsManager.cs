using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class adsManager : MonoBehaviour
{
    public static adsManager I;

    string adType;
    string gameId;
    void Awake()
    {
        I = this;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adType = "Rewarded_iOS";
            gameId = "4430078"; // 내 iOS 게임아이디
        }
        else
        {
            adType = "Rewarded_Android";
            gameId = "4430079"; // 내 안드로이드 게임아이디
        }

        Advertisement.Initialize(gameId);
    }

    //광고보기
    public void ShowRewardAd()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show(adType, options);
        }
    }

    //광고를 제대로 봤는가? 봤다면 보상
    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.LogError("광고 보기에 실패했습니다.");
                break;
            case ShowResult.Skipped:
                Debug.Log("광고를 스킵했습니다.");
                break;
            case ShowResult.Finished:
                // 광고 보기 보상 기능 
                
                if(gameManager.I.adsKinds == "score")
                {
                    gameManager.I.adsScore();
                }
                if(gameManager.I.adsKinds == "chur")
                {
                    gameManager.I.adsChur();
                }
                
                break;
        }
    }
}