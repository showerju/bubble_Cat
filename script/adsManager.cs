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
            gameId = "4430078"; // �� iOS ���Ӿ��̵�
        }
        else
        {
            adType = "Rewarded_Android";
            gameId = "4430079"; // �� �ȵ���̵� ���Ӿ��̵�
        }

        Advertisement.Initialize(gameId);
    }

    //������
    public void ShowRewardAd()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show(adType, options);
        }
    }

    //���� ����� �ô°�? �ôٸ� ����
    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.LogError("���� ���⿡ �����߽��ϴ�.");
                break;
            case ShowResult.Skipped:
                Debug.Log("���� ��ŵ�߽��ϴ�.");
                break;
            case ShowResult.Finished:
                // ���� ���� ���� ��� 
                
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