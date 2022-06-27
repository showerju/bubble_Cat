using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_adsController : MonoBehaviour
{
    public Text[] ads_text = new Text[2];
    bool temp_mainAds;

    public Image buff_icon;
    public Sprite[] buff_img = new Sprite[4];

    int x;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("main_adskind"))
        {
            x = Random.Range(1, 5);

            PlayerPrefs.SetInt("main_adskind",x);
            userManager.um.main_adsKind = x;
        }
        else
        {
            x = userManager.um.main_adsKind;
        }
        //1 = chur, 2 = score, 3 = time, 4 = fever

        Debug.Log(x);

        if(x == 1)
        {
            main_adsManager.I.main_adsKind = "chur";
            buff_icon.sprite = buff_img[0];

            ads_text[0].text = "x 2 , 5min";
            ads_text[1].text = "5분 동안 츄르 획득량 2배";
        }
        if (x == 2)
        {
            main_adsManager.I.main_adsKind = "score";
            buff_icon.sprite = buff_img[1];

            ads_text[0].text = "x 1.5 , 5min";
            ads_text[1].text = "5분 동안 스코어 획득량 2배";
        }
        if (x == 3)
        {
            main_adsManager.I.main_adsKind = "time";
            buff_icon.sprite = buff_img[2];

            ads_text[0].text = "+30sec, 5min";
            ads_text[1].text = "5분 동안 30초 추가";
        }
        if (x == 4)
        {
            main_adsManager.I.main_adsKind = "fever";
            buff_icon.sprite = buff_img[3];

            ads_text[0].text = "Fever, 10min";
            ads_text[1].text = "10분 동안 시작 시 피버타임";
        }
    }
}
