using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class main_adsManager : MonoBehaviour
{
    public static main_adsManager I;

    public string main_adsKind;

    public GameObject _timer;
    public GameObject leaf;

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

        //나뭇잎 출현 여부 결정
        if (!userManager.um.init_buff)
        {
            //버프타이머
            _timer.SetActive(false);

            if (!userManager.um.init_leaf)
            {
                //나뭇잎 값이 없다면 x 돌리고
                ads_btnActive();
            }
            else
            {
                //있다면 그냥 나뭇잎 출력하셈
                leaf.SetActive(true);
            }
        }
        else
        {
            _timer.SetActive(true);

            if (!userManager.um.start_timer)
            {
                userManager.um.timer();
            }
            else
            {
                return;
            }
        }
    }

    public void ShowRewardAd()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show(adType, options);
        }
    }

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

                PlayerPrefs.SetInt("buffMain", 1);
                userManager.um.init_buff = true;                

                if (main_adsKind == "chur")
                {
                    PlayerPrefs.SetString("active_mainbuff", "chur");
                    userManager.um.active_mainbuff = "chur";
                    userManager.um.st_time = 300;
                }
                if (main_adsKind == "score")
                {
                    PlayerPrefs.SetString("active_mainbuff", "score");
                    userManager.um.active_mainbuff = "score";
                    userManager.um.st_time = 300;
                }
                if (main_adsKind == "time")
                {
                    PlayerPrefs.SetString("active_mainbuff", "time");
                    userManager.um.active_mainbuff = "time";
                    userManager.um.st_time = 300;
                }
                if (main_adsKind == "fever")
                {
                    PlayerPrefs.SetString("active_mainbuff", "fever");
                    userManager.um.active_mainbuff = "fever";
                    userManager.um.st_time = 600;
                }

                userManager.um.init_ads();

                userManager.um.timer();
                _timer.SetActive(true);

                break;
        }
    }

    void ads_btnActive()
    {
        int x = Random.Range(1, 11);
        Debug.Log(x);

        if (x <= 6)
        {
            PlayerPrefs.SetInt("active_leaf",1);
            userManager.um.init_leaf = true;
            leaf.SetActive(true);

            Debug.Log("나뭇잎 등장");
        }
        if (x > 6)
        {
            Debug.Log("광고없음");
            return;
        }
    }
}