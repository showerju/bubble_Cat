using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_buttonManager : MonoBehaviour
{
    public GameObject leaf;

    public GameObject[] pannel = new GameObject[2];
    public Image[] toggleBtn = new Image[3];
    //(on,on,on / off,off,off)
    public Sprite[] img_toggle = new Sprite[6];

    private void Awake()
    {
        Time.timeScale = 1.0f;

        leaf.SetActive(false);

        if (!userManager.um.keepBGM)
        {
            soundManager.I.play_BGM("BGM", 0.15f, 0.9f);
            userManager.um.keepBGM = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (userManager.um.init_vive == 1)
        {
            toggleBtn[0].sprite = img_toggle[0];
        }
        if (userManager.um.init_vive == 0)
        {
            toggleBtn[0].sprite = img_toggle[3];
        }

        if (userManager.um.init_sfx == 1)
        {
            toggleBtn[1].sprite = img_toggle[1];
            soundManager.I.on_sfx();
        }
        if (userManager.um.init_sfx == 0)
        {
            toggleBtn[1].sprite = img_toggle[4];
            soundManager.I.off_sfx();
        }

        if (userManager.um.init_BGM == 1)
        {
            toggleBtn[2].sprite = img_toggle[2];
            soundManager.I.on_BGM();
        }
        if (userManager.um.init_BGM == 0)
        {
            toggleBtn[2].sprite = img_toggle[5];
            soundManager.I.off_BGM();
        }
    }

    public void scene_play()
    {
        sceneManager.I.LoadScene("play");
        soundManager.I.clear();
        soundManager.I.play_sfx("btnClick", 1,1.7f);        
    }

    public void scene_closet()
    {
        sceneManager.I.LoadScene("closet");
        soundManager.I.play_sfx("btnClick", 1, 1.7f);
    }

    public void btn_setting()
    {
        Time.timeScale = 0.0f;
        pannel[0].SetActive(true);
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    public void btn_closeSetting()
    {
        Time.timeScale = 1.0f;
        pannel[0].SetActive(false);
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);
    }


    //±§∞Ì ===========================================================
    public void btn_adsleaf()
    {
        Time.timeScale = 0.0f;
        pannel[1].SetActive(true);
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    public void btn_adscancel()
    {
        Time.timeScale = 1.0f;
        pannel[1].SetActive(false);
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    public void btn_showAds()
    {
        Time.timeScale = 1.0f;
        pannel[1].SetActive(false);
        leaf.SetActive(false);

        main_adsManager.I.ShowRewardAd();        
    }


    //≈‰±€ ===========================================================
    public void toggle_vive()
    {
        PlayerPrefs.SetInt("states_vive", userManager.um.init_vive == 0 ? 1 : 0);
        userManager.um.init_vive = userManager.um.init_vive == 0 ? 1 : 0;
        soundManager.I.play_sfx("btnClick", 1);

        if (userManager.um.init_vive == 1)
        {
            toggleBtn[0].sprite = img_toggle[0];
            vibeManager.I.vibe_1(10);
        }
        if (userManager.um.init_vive == 0)
        {
            toggleBtn[0].sprite = img_toggle[3];
            Vibration.Cancel();
        }
    }

    public void toggle_SFX()
    {
        PlayerPrefs.SetInt("states_sfx", userManager.um.init_sfx == 0 ? 1 : 0);
        userManager.um.init_sfx = userManager.um.init_sfx == 0 ? 1 : 0;
        soundManager.I.play_sfx("btnClick",1);

        if(userManager.um.init_sfx == 1)
        {
            toggleBtn[1].sprite = img_toggle[1];
            soundManager.I.on_sfx();
        }
        if (userManager.um.init_sfx == 0)
        {
            toggleBtn[1].sprite = img_toggle[4];
            soundManager.I.off_sfx();
        }
    }

    public void toggle_BGM()
    {
        PlayerPrefs.SetInt("states_BGM", userManager.um.init_BGM == 0 ? 1 : 0);
        userManager.um.init_BGM = userManager.um.init_BGM == 0 ? 1 : 0;
        soundManager.I.play_sfx("btnClick", 1);

        if (userManager.um.init_BGM == 1)
        {
            toggleBtn[2].sprite = img_toggle[2];
            soundManager.I.on_BGM();
        }
        if (userManager.um.init_BGM == 0)
        {
            toggleBtn[2].sprite = img_toggle[5];
            soundManager.I.off_BGM();
        }
    }
}
