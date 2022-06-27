using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userManager : MonoBehaviour
{
    public static userManager um;

    public List<bool> readyCat = new List<bool>();
    public List<bool> haveCat = new List<bool>();
    public List<bool> viewCat = new List<bool>();

    public int currentSelectCat;
    public float init_score;
    public float init_chur;

    public bool init_tuto;
    public bool init_leaf;
    public bool init_buff;
    public string active_mainbuff;
    public int main_adsKind;
    public int buff_timer;
    //타이머 중복 실행 방지
    public bool start_timer;
    public int second;

    public int init_vive;
    public int init_sfx;
    public int init_BGM;

    public bool keepBGM;

    //======================
    //타이머 관련
    public int _time;
    public int st_time;

    void Awake()
    {
        um = this;

        init_ads();
        init_userInfo();
        check_unlockCat();

        Debug.Log(init_tuto);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("지웠다");
        }
    }

    public void init_ads()
    {
        PlayerPrefs.DeleteKey("active_leaf");
        init_leaf = false;
        PlayerPrefs.DeleteKey("main_adskind");
        main_adsKind = 0;
        
    }

    private void init_userInfo()
    {
        keepBGM = true;
        //내가 지금 선택한 고양이
        currentSelectCat = PlayerPrefs.GetInt("CatKind",0);
        //츄르
        init_chur = PlayerPrefs.GetFloat("have_chur", 0);
        //최고점수
        init_score = PlayerPrefs.GetFloat("high_score", 0);
        //튜토리얼
        init_tuto = PlayerPrefs.HasKey("tuto_bool");

        //메인화면 광고================================================

        //main_버프 여부
        init_buff = PlayerPrefs.HasKey("buffMain");
            //PlayerPrefs.HasKey("buffMain");
        //main_나뭇잎 출현 여부
        init_leaf = PlayerPrefs.HasKey("active_leaf");
        //main_나뭇잎에 적힌 버프종류
        main_adsKind = PlayerPrefs.GetInt("main_adskind", 0);
        //buff_timer
        buff_timer = PlayerPrefs.GetInt("buffTimer", 0);
        //타이머 중복실행 방지
        start_timer = false;
        second = 1;

        //버프수령
        active_mainbuff = PlayerPrefs.GetString("active_mainbuff"); ;
            //PlayerPrefs.GetString("active_mainbuff");

        //=============================================================

        //설정값(0 = off, 1 = on)
        init_vive = PlayerPrefs.GetInt("states_vive", 1);
        init_sfx = PlayerPrefs.GetInt("states_sfx", 1);
        init_BGM = PlayerPrefs.GetInt("states_BGM", 1);

        for (int i = 0; i < Const.CAT_MAX_COUNT; i++)
        {
            readyCat.Add(PlayerPrefs.HasKey(string.Format("{0}_catready", i)));
            haveCat.Add(PlayerPrefs.HasKey(string.Format("{0}_cathave", i)));
        }
    }

    public void timer()
    {
        //시간값 buffTimer가 존재한다면(재부팅시)
        if (!PlayerPrefs.HasKey("buffTimer"))
        {
            //없다면 걍 광고에서 받은 값으로
            _time = st_time;
        }
        else
        {
            //존재한다면 남은 시간으로 계산
            _time = buff_timer;
        }
       
        //그 값을 여기에 저장
        PlayerPrefs.SetInt("buffTimer", _time);
        //갱신
        buff_timer = _time;
        start_timer = true;

        StartCoroutine(buffTimer());
    }

    public IEnumerator buffTimer()
    {
        Debug.Log("에엥 실행중");

        while (_time > 0)
        {
            _time--;

            PlayerPrefs.SetInt("buffTimer",buff_timer);
            buff_timer = _time;

            Debug.Log(_time);            

            yield return new WaitForSeconds(second);

            if (_time == 0)
            {
                Debug.Log("끝났다네!");
                PlayerPrefs.DeleteKey("buffMain");
                init_buff = false;
                PlayerPrefs.DeleteKey("buffTimer");
                PlayerPrefs.DeleteKey("active_mainbuff");
                active_mainbuff = "null";
                start_timer = false;
            }
        }       
    }

    void check_unlockCat()
    {
        float score = init_score;
        for (int i = 1; i <= score / Const.UNLOCK_SCORE; ++i)
        {
            if (!PlayerPrefs.HasKey(string.Format("{0}_catready", i)))
            {
                PlayerPrefs.SetInt(string.Format("{0}_catready", i), i + 1);
                userManager.um.readyCat[i] = true;
                //Debug.Log(i+1 + "번째 고양이 준.비");
            }
        }
    }
}
