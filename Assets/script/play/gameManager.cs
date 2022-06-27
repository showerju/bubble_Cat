using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private const string HAND_R_RES_PATH = "Images/cat/{0}/normal/{0}_hand_R";
    private const string HAND_L_RES_PATH = "Images/cat/{0}/normal/{0}_hand_L";

    private const string HEAD_N_RES_PATH = "Images/cat/{0}/normal/{0}_head_N";
    private const string HEAD_F_RES_PATH = "Images/cat/{0}/normal/{0}_head_F";

    //캐릭터 선택index
    int cat_index;
    //싱글톤-------------------------
    public static gameManager I;
    void Awake()
    {
        if (feverBool)
        {
            fv_Node = Random.Range(1, 5);
        }

        soundManager.I.play_BGM("BGM", 0.3f, 0.9f);

        //캐릭터 선택index
        cat_index = userManager.um.currentSelectCat;

        I = this;
        Time.timeScale = 0.0f;

        Sprite HandR = Resources.Load<Sprite>(string.Format(HAND_R_RES_PATH, string.Format("{0:00}", cat_index+1)));
        Sprite HandL = Resources.Load<Sprite>(string.Format(HAND_L_RES_PATH, string.Format("{0:00}", cat_index+1)));

        Sprite HeadN = Resources.Load<Sprite>(string.Format(HEAD_N_RES_PATH, string.Format("{0:00}", cat_index+1)));
        Sprite HeadF = Resources.Load<Sprite>(string.Format(HEAD_F_RES_PATH, string.Format("{0:00}", cat_index+1)));

        mCatControler.setSpriteInfo_hand(HandR, HandL);
        mCatControler.setSpriteInfo_head(HeadN, HeadF);

        ads_bool = PlayerPrefs.HasKey("adsBool");
        watch_bool = PlayerPrefs.HasKey("watchBool");

        if (feverBool)
        {
            fv_Node = Random.Range(1, 5);
        }
    }
    //-------------------------------

    public nodeManager mNodeManager;
    //pannel 관리(start,pause,setting,over)
    public GameObject[] pannel = new GameObject[6];

    //노드
    public List<int> x_list = new List<int>();
    public int x;
    int count;
    int node_count;
    int score_length;

    //노드배경변경
    public Image img_nodeBG;
    public Sprite[] nodeBG = new Sprite[4];

    //버튼방지
    public GameObject protector;

    //===================================================
    //text 관리(time,chur,score)
    public Text[] texts = new Text[3];
    //add_textGroup 관리(time,chur,score)
    public Text[] add_texts = new Text[3];
    //add_Group 애니메이션(time,chur,score)
    public Animator[] add_anim = new Animator[3];

    //check_txt 관리(perfect,good,miss)
    public GameObject[] check_txt = new GameObject[3];
    //check_anim 관리(perfect,good,miss)
    public Animator[] check_anim = new Animator[3];
    //false를 위한 index
    int check_index;

    //게임오버
    public Animator water_anim;
    //text 관리(highScore,getScore,getChur)
    public Text[] txt_over = new Text[3];
    //게임오버 버튼(스코어 확인 후 able)
    public GameObject[] over_btn = new GameObject[2];

    float over_chur;

    //skip_time은 스코어 집계 후 high_score가 실행되도록 조정해줌
    float over_score;
    int skip_time;
    bool scoreSound;
    float over_highScore;

    float finalChur;

    float save_highScore;
    float save_haveChur;
    //===================================================
    //광고
    bool ads_bool;
    public bool watch_bool;

    float ads_saveScore;
    public string adsKinds;
    //===================================================
    //FEVER TIME
    float fever;
    public bool feverBool;
    public bool fv_miss;
    public Slider fever_gage;

    public Image fever_mask;
    public GameObject fv_effect;
    public GameObject fv_eyes;

    int fv_Node;
    float fv_time;
    float height;
    float fv_bar;


    //===================================================

    //time
    public float sttime = 80.0f;
    public float _time;
    int _min = 0;
    int _sec = 0;

    //chur
    float _chur;

    //score
    float score;

    //check
    public List<int> miss_check = new List<int>();

    //거품
    public GameObject c_bubble;

    int tempTime;
    //===================================================
    //고양이
    public void start_handAnim()
    {
        mCatControler.playButtonAnim();
    }
    //꼬리 anim
    public Animator anim_tail;

    //고양이 sprite 교체(normal) - 캐릭터 변경시
    public Image c_head_n;
    public Image c_hand_n;
    public Image c_body_n;
    public Image c_tail_n;

    //고양이 sprite 교체(miss) - 캐릭터 변경시
    public Image c_upper;
    public Image c_lower;

    //normal
    //머리
    public Sprite[] c_heads_n = new Sprite[16];
    //몸통
    public Sprite[] c_bodies_n = new Sprite[16];
    //꼬리
    public Sprite[] c_tails_n = new Sprite[16];

    //miss
    //머리
    public Sprite[] c_uppers = new Sprite[16];
    //몸통
    public Sprite[] c_lowers = new Sprite[16];
    //빨래 더러워짐
    public GameObject dirty;

    // 고양이 조종
    public CatController mCatControler;
    //=====================================================


    // Start is called before the first frame update
    void Start()
    {        
        cat_setting();        

        Time.timeScale = 0.0f;

        fever = 0;
        fever_gage.value = fever;
        feverBool = false;
        fv_time = 8.0f;
        height = 1000 / fv_time;

        count = 0;                
        node_count = 0;
        finalChur = 0.0f;
        skip_time = 0;
        scoreSound = true;

        if (userManager.um.init_buff)
        {
            if (userManager.um.active_mainbuff == "chur")
            {
                _time = sttime;
                fever = 0;
            }
            if (userManager.um.active_mainbuff == "score")
            {
                _time = sttime;
                fever = 0;
            }
            if (userManager.um.active_mainbuff == "time")
            {
                _time = sttime + 30;
                fever = 0;
            }
            if (userManager.um.active_mainbuff == "fever")
            {
                _time = sttime;
            }
        }
        else
        {
            _time = sttime;
        }
        
        if (ads_bool)
        {
            score = PlayerPrefs.GetFloat("tempScore", 0);
            _chur = PlayerPrefs.GetFloat("tempChur", 0);
            PlayerPrefs.DeleteKey("tempScore");
            PlayerPrefs.DeleteKey("tempChur");

            texts[1].text = _chur.ToString("N0");
            texts[2].text = score.ToString("N0");

            PlayerPrefs.DeleteKey("adsBool");
            PlayerPrefs.DeleteKey("watchBool");
        }
        else
        {
            score = 0;
            _chur = 0.0f;
        }

        save_highScore = userManager.um.init_score;
        save_haveChur = userManager.um.init_chur;
        over_score = 0.0f;
        over_chur = 0.0f;

        txt_over[0].text = save_highScore.ToString("N0");
        miss_check.Clear();
    }

    //빨래---------------------------
    public GameObject[] childcloth = new GameObject[10];

    int count_cloth = -1;

    // Update is called once per frame
    void Update()
    {
        time_update();

        if(_time < 0)
        {      
            game_over();
        }
    }


    public void start_game()
    {
        twinkle_controll();

        //시간제어
        Time.timeScale = 1.0f;

        pannel[0].SetActive(false);
        node_length();
        InvokeRepeating("makenodes_instan", 0.0f, 0.02f);        
    }

    public void play_game()
    {
        if (!feverBool)
        {
            Invoke("changNodeBG", 0.4f);
        }
        else
        {
            fv_Node = Random.Range(1, 5);
            fv_miss = false;
            Invoke("fv_changNodeBG", 0.4f);
        }

        

        make_protector();
        node_length();
        InvokeRepeating("makenodes_instan", 0.4f, 0.005f);
    }

    //노드갯수 제어
    public void node_length()
    {
        int value01 = Random.Range(1, 101);

        if (score <= 10)
        {
            node_count = Random.Range(2, 4);
        }

        if (score > 10 && score <= 30)
        {
            if (value01 <= 40)
            {
                node_count = Random.Range(2, 4);
            }
            if (value01 > 40)
            {
                node_count = Random.Range(4, 8);
            }
        }

        if (score > 30 && score <= 60)
        {
            if (value01 <= 10)
            {
                node_count = Random.Range(3, 7);
            }
            if (value01 <= 40 && value01 > 10) 
            {
                node_count = Random.Range(5, 8);
            }
            if (value01 > 40 && value01 <= 100)
            {
                node_count = Random.Range(6, 11);
            }
        }

        if (score > 60 && score <= 90)
        {
            if (value01 <= 10)
            {
                node_count = Random.Range(3, 6);
            }
            if (value01 <= 30 && value01 > 10)
            {
                node_count = Random.Range(5, 9);
            }
            if (value01 > 30 && value01 <= 100)
            {
                node_count = Random.Range(8, 13);
            }
        }

        if (score > 90 && score <= 120)
        {
            if (value01 <= 10)
            {
                node_count = Random.Range(3, 6);
            }
            if (value01 <= 20 && value01 > 10)
            {
                node_count = Random.Range(7, 9);
            }
            if (value01 <= 30 && value01 > 20)
            {
                node_count = Random.Range(9, 13);
            }
            if (value01 > 20 && value01 <= 100)
            {
                node_count = Random.Range(10, 20);
            }

        }

        if (score > 120)
        {
            if (value01 <= 30)
            {
                node_count = Random.Range(3, 6);
            }
            if (value01 <= 40 && value01 > 30)
            {
                node_count = Random.Range(7, 12);
            }
            if (value01 <= 50 && value01 > 40)
            {
                node_count = Random.Range(9, 16);
            }
            if (value01 > 50 && value01 <= 100)
            {
                node_count = Random.Range(12, 24);
            }
        }

        score_length = node_count;
    }

    //노드 출력 제어
    public void makenodes_instan()
    {
        if (!feverBool)
        {
            x = Random.Range(1, 5);
        }
        else
        {
            x = fv_Node;
        }

        mNodeManager.createNode();


        x_list.Add(x);

        count++;

        if (count >= node_count)
        {
            CancelInvoke("makenodes_instan");
            count = 0;
            miss_check.Clear();
        }
    }
    void changNodeBG()
    {
        img_nodeBG.sprite = nodeBG[0];
    }

    void fv_changNodeBG()
    {
        img_nodeBG.sprite = nodeBG[3];
    }

    //초기화_2miss에서 사용
    public void initializing()
    {
        x_list.Clear();
    }

    //=====================================================
    //게임오버
    void game_over()
    {
        //고양이&물 멈춰!
        cat_tail_anim();
        water_anim.SetBool("IsStop", true);

        //Debug.Log("게임오버");
        _time = 0;
        c_bubble.SetActive(false);

        check_unlockCat();

        //바로 실행되면 node에서 index 오류 발생
        Invoke("hideNodes", 0.5f);

        pannel[3].SetActive(true);
        pannel[4].SetActive(true);
        over_btn[0].GetComponent<Button>().interactable = false;
        over_btn[1].GetComponent<Button>().interactable = false;        

        //over_Txt

        over_chur_txt();

        over_score_txt();       

        if (skip_time == 1)
        {
            over_highScore_txt();            
        }

        if (skip_time == 3)
        {            
            Time.timeScale = 1.0f;
            StartCoroutine(wait_over());   

            soundManager.I.clear_sfx();
            scoreSound = true;
        }

        if (scoreSound)
        {
            if(skip_time == 0)
            {
                soundManager.I.play_sfx("score", 0.4f,0.8f);
                scoreSound = false;
            }        
        }
    }
    private IEnumerator wait_over()
    {        
        if (watch_bool)
        {
            pannel[5].SetActive(false);
            pannel[4].SetActive(true);

            yield return new WaitForSeconds(1.3f);

            int ads = Random.Range(1, 11);
            {
                if (ads < 10)
                {
                    adsManager.I.ShowRewardAd();
                }
            }

            Time.timeScale = 0.0f;
            over_btn[0].GetComponent<Button>().interactable = true;
            over_btn[1].GetComponent<Button>().interactable = true;
        }
        else
        {
            pannel[4].SetActive(true);

            yield return new WaitForSeconds(1.3f);

            int ads = Random.Range(1, 11);
            {
                if (ads < 2)
                {
                    adsManager.I.ShowRewardAd();
                }
            }

            Time.timeScale = 0.0f;
            pannel[5].SetActive(true);
            pannel[4].SetActive(false);
            over_btn[0].GetComponent<Button>().interactable = true;
            over_btn[1].GetComponent<Button>().interactable = true;
        }
    }

    void check_unlockCat()
    {
        for (int i = 1; i <= score / Const.UNLOCK_SCORE; ++i)
        {
            if (!PlayerPrefs.HasKey(string.Format("{0}_catready", i)))
            {
                PlayerPrefs.SetInt(string.Format("{0}_catready", i), i+1);
                userManager.um.readyCat[i] = true;
                //Debug.Log(i+1 + "번째 고양이 준.비");
            }
        }
    }

    //버튼방지
    void make_protector()
    {
        protector.SetActive(true);
        Invoke("disable_protector", 0.45f);
    }
    void disable_protector()
    {
        protector.SetActive(false);
    }
    //게임오버 후 화면 지우기
    void hideNodes()
    {
        mNodeManager.hide_nodes();
        x_list.Clear();
    }
 
    //over화면 최종 집계txt
    void over_chur_txt()
    {
        finalChur = save_haveChur + _chur;
        PlayerPrefs.SetFloat("have_chur", finalChur);        
        userManager.um.init_chur = finalChur;

        if (over_chur < _chur)
        {
            over_chur += Time.deltaTime*50;

            txt_over[2].text = over_chur.ToString("N0");
        }
    }

    void over_score_txt()
    {
        if(over_score < score)
        {
            over_score += Time.deltaTime*120;
            txt_over[1].text = over_score.ToString("N0");
        }
        if(over_score >= score)
        {
            skip_time = 1;
        }
    }

    void over_highScore_txt()
    {
        if(score > save_highScore)
        {
            skip_time = 2;
            save_highScore += Time.deltaTime*120;
            txt_over[0].text = save_highScore.ToString("N0");

            PlayerPrefs.SetFloat("high_score", score);
            userManager.um.init_score = score;
        }
        if(score <= save_highScore)
        {
            skip_time = 3;
        }
    }
    //=====================================================

    //스코어,츄르,시간 관리

    void time_update()
    {
        _time -= Time.deltaTime;

        if (_time > 59)
        {
            _min = (int)_time / 60;
            _sec = (int)_time % 60;
        }
        if (_time < 60)
        {
            _min = 0;
            _sec = (int)_time;
        }
        texts[0].text = string.Format("{0:D2}:{1:D2}", _min, _sec);
    }
    void Text_update()
    {
        texts[1].text = _chur.ToString("N0");
        texts[2].text = score.ToString("N0");
    }
   
    //perfect 여부 확인(miss는 nodemanager)
    public void checker_good()
    {
  
        if (miss_check.Count == 0)
        {
            if (!feverBool)
            {
                fever += 20;
                fever_gage.value = fever;
            }            

            if(fever == 120)
            {
                img_nodeBG.sprite = nodeBG[3];

                feverBool = true;
                fv_miss = false;
                StartCoroutine(feverOff());
                Debug.Log("피버타이이임");

                fever = 0;
                fever_gage.value = fever;

                Debug.Log(feverBool);
            }
            Debug.Log(fever);

            if (userManager.um.init_vive == 1)
            {
                Vibration.Vibrate(30);
            }
            if (userManager.um.init_vive == 0)
            {
                Vibration.Cancel();
            }

            img_nodeBG.sprite = nodeBG[1];
            perfect_text();
            soundManager.I.play_sfx("nodeBubble", 0.7f);

            if (score_length < 8)
            {
                if (userManager.um.init_buff)
                {
                    if (userManager.um.active_mainbuff == "score")
                    {
                        score += 3*1.5f;                        
                        add_texts[2].text = string.Format("+ 4.5");

                        _chur += 0.3f;
                        add_texts[1].text = string.Format("+ 0.3");
                    }
                    if (userManager.um.active_mainbuff == "chur")
                    {
                        _chur += 0.3f*2;
                        add_texts[1].text = string.Format("+ 0.6");

                        score += 3;
                        add_texts[2].text = string.Format("+ 3");
                    }
                    if(userManager.um.active_mainbuff == "time")
                    {
                        score += 3;
                        _chur += 0.3f;

                        add_texts[1].text = string.Format("+ 0.3");
                        add_texts[2].text = string.Format("+ 3");
                    }
                    if (userManager.um.active_mainbuff == "fever")
                    {
                        score += 3;
                        _chur += 0.3f;

                        add_texts[1].text = string.Format("+ 0.3");
                        add_texts[2].text = string.Format("+ 3");
                    }
                }
                else
                {
                    score += 3;
                    _chur += 0.3f;

                    add_texts[1].text = string.Format("+ 0.3");
                    add_texts[2].text = string.Format("+ 3");
                }                

                Text_update();

                addscore_anim();
                addchur_anim();
            }
            if (score_length >= 8 && score_length < 11)
            {
                _time += 1.0f;

                if (userManager.um.init_buff)
                {
                    if (userManager.um.active_mainbuff == "score")
                    {
                        score += 4 * 1.5f;
                        add_texts[2].text = string.Format("+ 6");

                        _chur += 0.4f;
                        add_texts[1].text = string.Format("+ 0.4");
                    }
                    if (userManager.um.active_mainbuff == "chur")
                    {
                        _chur += 0.4f * 2;
                        add_texts[1].text = string.Format("+ 0.8");

                        score += 4;
                        add_texts[2].text = string.Format("+ 4");
                    }
                    if (userManager.um.active_mainbuff == "time")
                    {
                        score += 4;
                        _chur += 0.4f;

                        add_texts[1].text = string.Format("+ 0.4");
                        add_texts[2].text = string.Format("+ 4");
                    }
                    if (userManager.um.active_mainbuff == "fever")
                    {
                        score += 4;
                        _chur += 0.4f;

                        add_texts[1].text = string.Format("+ 0.4");
                        add_texts[2].text = string.Format("+ 4");
                    }
                }
                else
                {
                    score += 4;
                    _chur += 0.4f;

                    add_texts[1].text = string.Format("+ 0.4");
                    add_texts[2].text = string.Format("+ 4");
                }
                
                add_texts[0].text = string.Format("+ 1sec");

                Text_update();

                addtime_anim();
                addscore_anim();
                addchur_anim();
            }

            if (score_length >= 10 && score_length < 13)
            {
                _time += 1.5f;

                if (userManager.um.init_buff)
                {
                    if (userManager.um.active_mainbuff == "score")
                    {
                        score += 5 * 1.5f;
                        add_texts[2].text = string.Format("+ 7.5");

                        _chur += 0.5f;
                        add_texts[1].text = string.Format("+ 0.5");
                    }
                    if (userManager.um.active_mainbuff == "chur")
                    {
                        _chur += 0.5f * 2;
                        add_texts[1].text = string.Format("+ 1.0");

                        score += 5;
                        add_texts[2].text = string.Format("+ 5");
                    }
                    if (userManager.um.active_mainbuff == "time")
                    {
                        score += 5;
                        _chur += 0.5f;

                        add_texts[1].text = string.Format("+ 0.5");
                        add_texts[2].text = string.Format("+ 5");
                    }
                    if (userManager.um.active_mainbuff == "fever")
                    {
                        score += 5;
                        _chur += 0.5f;

                        add_texts[1].text = string.Format("+ 0.5");
                        add_texts[2].text = string.Format("+ 5");
                    }
                }
                else
                {
                    score += 5;
                    _chur += 0.5f;

                    add_texts[1].text = string.Format("+ 0.5");
                    add_texts[2].text = string.Format("+ 5");
                    
                }

                add_texts[0].text = string.Format("+ 1.5sec");

                Text_update();

                addtime_anim();
                addscore_anim();
                addchur_anim();
            }

            if (score_length >= 13)
            {
                _time += 2.0f;

                if (userManager.um.init_buff)
                {
                    if (userManager.um.active_mainbuff == "score")
                    {
                        score += 6 * 1.5f;
                        add_texts[2].text = string.Format("+ 9");

                        _chur += 0.6f;
                        add_texts[1].text = string.Format("+ 0.6");
                    }
                    if (userManager.um.active_mainbuff == "chur")
                    {
                        _chur += 0.6f * 2;
                        add_texts[1].text = string.Format("+ 1.2");

                        score += 6;
                        add_texts[2].text = string.Format("+ 6");
                    }
                    if (userManager.um.active_mainbuff == "time")
                    {
                        score += 6;
                        _chur += 0.6f;

                        add_texts[1].text = string.Format("+ 0.6");
                        add_texts[2].text = string.Format("+ 6");
                    }
                    if (userManager.um.active_mainbuff == "fever")
                    {
                        score += 6;
                        _chur += 0.6f;

                        add_texts[1].text = string.Format("+ 0.6");
                        add_texts[2].text = string.Format("+ 6");
                    }
                }
                else
                {
                    score += 6;
                    _chur += 0.6f;

                    add_texts[1].text = string.Format("+ 0.6");
                    add_texts[2].text = string.Format("+ 6");
                }
                
                add_texts[0].text = string.Format("+ 2sec");

                Text_update();

                addtime_anim();
                addscore_anim();
                addchur_anim();
            }
        }
        if(miss_check.Count == 1)
        {
            good_text();
            soundManager.I.play_sfx("nodeBubble", 0.7f);

            if (userManager.um.init_buff)
            {
                if (userManager.um.active_mainbuff == "score")
                {
                    score += 1 * 1.5f;
                    add_texts[2].text = string.Format("+ 1.5");

                    _chur += 0.1f;
                    add_texts[1].text = string.Format("+ 0.1");
                }
                if (userManager.um.active_mainbuff == "chur")
                {
                    _chur += 0.1f * 2;
                    add_texts[1].text = string.Format("+ 0.2");

                    score += 1;
                    add_texts[2].text = string.Format("+ 1");
                }
                if (userManager.um.active_mainbuff == "time")
                {
                    score += 1;
                    _chur += 0.1f;

                    add_texts[1].text = string.Format("+ 0.1");
                    add_texts[2].text = string.Format("+ 1");
                }
                if (userManager.um.active_mainbuff == "fever")
                {
                    score += 1;
                    _chur += 0.1f;

                    add_texts[1].text = string.Format("+ 0.1");
                    add_texts[2].text = string.Format("+ 1");
                }
            }
            else
            {
                score += 1;
                _chur += 0.1f;

                add_texts[1].text = string.Format("+ 0.1");
                add_texts[2].text = string.Format("+ 1");
            }            

            Text_update();

            addscore_anim();
            addchur_anim();
        }
    }

    public void checker_bad1()
    {
        _time -= 2;

        add_texts[0].text = string.Format("- 2sec");
        addtime_anim();
    }
    public void checker_bad2()
    {
        img_nodeBG.sprite = nodeBG[2];

        _time -= 3;

        add_texts[0].text = string.Format("- 3sec");
        addtime_anim();
    }

    //옷 제어
    public void del_cloth_active()
    {
        StartCoroutine(del_cloth_co());
    }

    private IEnumerator del_cloth_co()
    {
        if (count_cloth <= 10)
        {
            count_cloth++;
            childcloth[count_cloth].gameObject.SetActive(false);
        }
        if (count_cloth == 9)
        {
            for (int i = 0; i < childcloth.Length; i++)
            {
                childcloth[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.04f);
            }
            count_cloth = -1;
        }
    }

    //고양이 제어
    void cat_setting()
    {
        c_head_n.sprite = c_heads_n[cat_index];
        c_body_n.sprite = c_bodies_n[cat_index];
        c_tail_n.sprite = c_tails_n[cat_index];

        c_upper.sprite = c_uppers[cat_index];
        c_lower.sprite = c_lowers[cat_index];
    }
    //눈깜빡임
    void twinkle_controll()
    {
        StartCoroutine(stay());
    }

    IEnumerator stay()
    {
        while (true)
        {
            mCatControler.twinkleEyes();
            yield return new WaitForSeconds(2.0f);
        }
    }

    //꼬리
    void cat_tail_anim()
    {
        anim_tail.SetBool("IsStop",true);
    }

    public void cat_miss_anim()
    {
        mCatControler.playMissAnim();
    }

    //거품만들기
    public void start_bubble()
    {
        c_bubble.SetActive(true);
    }

    //ui제어
    //text 출력
    void perfect_text()
    {
        check_index = 0;
        check_txt[0].gameObject.SetActive(true);
        check_anim[0].Play("Base Layer.check_anim", 0, 0.0f);
        Invoke("set_false", 0.3f);
    }
    void good_text()
    {
        check_index = 1;
        check_txt[1].gameObject.SetActive(true);
        check_anim[1].Play("Base Layer.check_anim", 0, 0.0f);
        Invoke("set_false", 0.4f);
    }
    public void miss_text()
    {
        check_index = 2;
        check_txt[2].gameObject.SetActive(true);
        check_anim[2].Play("Base Layer.check_anim", 0, 0.0f);
        Invoke("set_false", 0.4f);
    }
    //text 비활성화
    void set_false()
    {
        check_txt[check_index].gameObject.SetActive(false);
    }


    //애니메이션
    void addtime_anim()
    {
        add_texts[0].gameObject.SetActive(true);
        add_anim[0].Play("Base Layer.add_time", 0, 0.0f);
    }

    //fever 구현
    void fever_start()
    {
        StartCoroutine(feverOff());
    }

    private IEnumerator feverOff()
    {
        fv_effect.SetActive(true);
        fv_eyes.SetActive(true);

        float height = 1000/fv_time;
        fv_bar = 1000 + height;

        while (fv_bar > 0)
        {
            fv_bar -= height;
            fever_mask.rectTransform.sizeDelta = new Vector2(880, fv_bar);
            yield return new WaitForSeconds(1.0f);

            if (fv_bar == 0)
            {
                feverBool = false;
                fv_effect.SetActive(false);
                fv_eyes.SetActive(false);
                break;
            }
        }        
    }

    void addchur_anim()
    {
        add_texts[1].gameObject.SetActive(true);
        add_anim[1].Play("Base Layer.add_chur", 0, 0.0f);
    }

    void addscore_anim()
    {
        add_texts[2].gameObject.SetActive(true);
        add_anim[2].Play("Base Layer.add_score", 0, 0.0f);
    }

    //버튼
    public void pause()
    {
        Time.timeScale = 0.0f;
        pannel[1].SetActive(true);
        pannel[2].SetActive(false);
    }

    public void pause_close()
    {
        Time.timeScale = 1.0f;
        pannel[1].SetActive(false);

    }
    public void setting()
    {
        pannel[1].SetActive(false);
        pannel[2].SetActive(true);
    }
    //광고
    public void adsScore()
    {
        PlayerPrefs.SetFloat("tempScore", score);
        PlayerPrefs.SetFloat("tempChur", _chur);

        PlayerPrefs.SetInt("adsBool",1);
        PlayerPrefs.SetInt("watchBool",1);

        sceneManager.I.LoadScene("play");
    }
    public void adsChur()
    {
        finalChur = save_haveChur + _chur * 2;
        PlayerPrefs.SetFloat("have_chur", finalChur);
        userManager.um.init_chur = finalChur;

        sceneManager.I.LoadScene("main");
    }
}
