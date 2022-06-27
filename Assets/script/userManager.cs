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
    //Ÿ�̸� �ߺ� ���� ����
    public bool start_timer;
    public int second;

    public int init_vive;
    public int init_sfx;
    public int init_BGM;

    public bool keepBGM;

    //======================
    //Ÿ�̸� ����
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
            Debug.Log("������");
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
        //���� ���� ������ �����
        currentSelectCat = PlayerPrefs.GetInt("CatKind",0);
        //��
        init_chur = PlayerPrefs.GetFloat("have_chur", 0);
        //�ְ�����
        init_score = PlayerPrefs.GetFloat("high_score", 0);
        //Ʃ�丮��
        init_tuto = PlayerPrefs.HasKey("tuto_bool");

        //����ȭ�� ����================================================

        //main_���� ����
        init_buff = PlayerPrefs.HasKey("buffMain");
            //PlayerPrefs.HasKey("buffMain");
        //main_������ ���� ����
        init_leaf = PlayerPrefs.HasKey("active_leaf");
        //main_�����ٿ� ���� ��������
        main_adsKind = PlayerPrefs.GetInt("main_adskind", 0);
        //buff_timer
        buff_timer = PlayerPrefs.GetInt("buffTimer", 0);
        //Ÿ�̸� �ߺ����� ����
        start_timer = false;
        second = 1;

        //��������
        active_mainbuff = PlayerPrefs.GetString("active_mainbuff"); ;
            //PlayerPrefs.GetString("active_mainbuff");

        //=============================================================

        //������(0 = off, 1 = on)
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
        //�ð��� buffTimer�� �����Ѵٸ�(����ý�)
        if (!PlayerPrefs.HasKey("buffTimer"))
        {
            //���ٸ� �� ������ ���� ������
            _time = st_time;
        }
        else
        {
            //�����Ѵٸ� ���� �ð����� ���
            _time = buff_timer;
        }
       
        //�� ���� ���⿡ ����
        PlayerPrefs.SetInt("buffTimer", _time);
        //����
        buff_timer = _time;
        start_timer = true;

        StartCoroutine(buffTimer());
    }

    public IEnumerator buffTimer()
    {
        Debug.Log("���� ������");

        while (_time > 0)
        {
            _time--;

            PlayerPrefs.SetInt("buffTimer",buff_timer);
            buff_timer = _time;

            Debug.Log(_time);            

            yield return new WaitForSeconds(second);

            if (_time == 0)
            {
                Debug.Log("�����ٳ�!");
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
                //Debug.Log(i+1 + "��° ����� ��.��");
            }
        }
    }
}
