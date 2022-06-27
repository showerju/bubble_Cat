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
            gameId = "4430078"; // �� iOS ���Ӿ��̵�
        }
        else
        {
            adType = "Rewarded_Android";
            gameId = "4430079"; // �� �ȵ���̵� ���Ӿ��̵�
        }

        Advertisement.Initialize(gameId);

        //������ ���� ���� ����
        if (!userManager.um.init_buff)
        {
            //����Ÿ�̸�
            _timer.SetActive(false);

            if (!userManager.um.init_leaf)
            {
                //������ ���� ���ٸ� x ������
                ads_btnActive();
            }
            else
            {
                //�ִٸ� �׳� ������ ����ϼ�
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
                Debug.LogError("���� ���⿡ �����߽��ϴ�.");
                break;
            case ShowResult.Skipped:
                Debug.Log("���� ��ŵ�߽��ϴ�.");
                break;
            case ShowResult.Finished:
                // ���� ���� ���� ��� 

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

            Debug.Log("������ ����");
        }
        if (x > 6)
        {
            Debug.Log("�������");
            return;
        }
    }
}