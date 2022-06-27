using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class play_buttonManger : MonoBehaviour
{
    public Image[] toggleBtn = new Image[3];
    //(on,on,on / off,off,off)
    public Sprite[] img_toggle = new Sprite[6];

    private void Start()
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

    //play=====================================
    //btn
    public void btn_start_game()
    {       
        gameManager.I.start_game();
        soundManager.I.play_sfx("bubbleStart", 0.15f, 1.5f);
        userManager.um.keepBGM = false;
    }

    public void btn_pause()
    {
        gameManager.I.pause();
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    public void btn_keepPlay()
    {
        gameManager.I.pause_close();
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);
    }

    public void btn_setting()
    {
        gameManager.I.setting();
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);
    }
    
    //±§∞Ì==============================================================
    public void btn_adsScore()
    {
        gameManager.I.adsKinds = "score";
        adsManager.I.ShowRewardAd();

        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }
    public void btn_adsChur()
    {
        gameManager.I.adsKinds = "chur";
        adsManager.I.ShowRewardAd();

        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }
    //==================================================================

    //scene

    public void scene_play()
    {
        sceneManager.I.LoadScene("play");
        soundManager.I.clear();
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);
    }

    public void scene_home()
    {
        sceneManager.I.LoadScene("main");
        soundManager.I.clear();
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);

        if (gameManager.I.watch_bool)
        {
            PlayerPrefs.DeleteKey("watchBool");
        }
    }

    public void scene_home2()
    {
        //if (userManager.um.init_buff)
        //{
        //    userManager.um.timer();
        //}

        sceneManager.I.LoadScene("main");
        soundManager.I.clear();
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);

        if (gameManager.I.watch_bool)
        {
            PlayerPrefs.DeleteKey("watchBool");
        }
    }

    //≈‰±€========================================================================
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
        soundManager.I.play_sfx("btnClick", 1);

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
