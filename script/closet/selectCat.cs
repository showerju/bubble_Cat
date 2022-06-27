using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectCat : MonoBehaviour
{
    public Image cat;
    public Image c_name;
    public GameObject useCat;

    public Text txt_cat_number;
    public Text txt_unlockScore;
    public Text txt_unlockChur;

    public GameObject pannel_Unlock;
    public Image ui_unlock;

    public Text txtUi;


    //(true,false)
    public Sprite[] img_uiUnlock = new Sprite[2];

    public GameObject btn_cat;

    public GameObject btn_unlock;

    public Button color_btn;
    private ColorBlock btn_color;

    //보유
    private Color btn_setColor_H;
    private Color btn_setColor_H1;
    private Color btn_setColor_H2;
    private Color btn_setColor_H3;

    //미보유    
    private Color btn_setColor_N;
    private Color btn_setColor_N1;
    private Color btn_setColor_N2;
    private Color btn_setColor_N3;

    int needChur;

    bool btn_havecat;

    public Sprite[] img_cat = new Sprite[16];
    public Sprite[] img_locked = new Sprite[16];
    public Sprite[] img_name = new Sprite[16];

    int x;
    //고양이 시작값(선택)
    int set_x;

    float score;
    

    // Start is called before the first frame update
    private void Awake()
    {
        score = PlayerPrefs.GetFloat("high_score", 0);
        x = userManager.um.currentSelectCat;
        set_x = PlayerPrefs.GetInt("CatKind", 0);

        pannel_Unlock.SetActive(false);
    }

    void Start()
    {
        btn_color = color_btn.colors;

        //보유
        btn_setColor_H = Color.white;
        btn_setColor_H1 = Color.white;
        btn_setColor_H2 = Color.white;
        btn_setColor_H3 = Color.white;
        //미보유
        btn_setColor_N = Color.gray;
        btn_setColor_N1 = Color.gray;
        btn_setColor_N2 = Color.gray;
        btn_setColor_N3 = Color.gray;

        cat.sprite = img_cat[x];
        c_name.sprite = img_name[x];
        txt_cat_number.text = string.Format("{0}/16", x+1);

        btn_havecat = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_left()
    {
        useCat.SetActive(false);        
        
        if (x >= 0)
        {
            x -= 1;

            if (x == set_x)
            {
                useCat.SetActive(true);
            }

            Invoke("change_img", 0.01f);            
        }
        if (x < 0)
        {
            x = 15;

            if (x == set_x)
            {
                useCat.SetActive(true);
            }

            Invoke("change_img", 0.01f);
        }
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    public void btn_right()
    {
        useCat.SetActive(false);        

        if (x <= 15)
        {
            x += 1;

            if (x == set_x)
            {
                useCat.SetActive(true);
            }

            Invoke("change_img", 0.01f);
        }
        if (x > 15)
        {
            x = 0;

            if (x == set_x)
            {
                useCat.SetActive(true);
            }

            Invoke("change_img", 0.01f);
        }
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }

    
    void change_img()
    {
        if (userManager.um.readyCat[x])
        {
            cat.sprite = img_cat[x];

            btn_color.normalColor = btn_setColor_H;
            btn_color.highlightedColor = btn_setColor_H1;
            btn_color.selectedColor = btn_setColor_H2;
            btn_color.disabledColor = btn_setColor_H3;

            color_btn.colors = btn_color;

            if (userManager.um.haveCat[x])            
            {
                
                btn_havecat = true;                
            }
            else
            {
                btn_color.normalColor = btn_setColor_N;
                btn_color.highlightedColor = btn_setColor_N1;
                btn_color.selectedColor = btn_setColor_N2;
                btn_color.disabledColor = btn_setColor_N3;

                color_btn.colors = btn_color;
                btn_havecat = false;
                
            }
        }
        else
        {
            cat.sprite = img_cat[x];
            btn_havecat = false;
            cat.sprite = img_locked[x];
            btn_color.normalColor = btn_setColor_H;
            btn_color.highlightedColor = btn_setColor_H1;
            btn_color.selectedColor = btn_setColor_H2;
            btn_color.disabledColor = btn_setColor_H3;

            color_btn.colors = btn_color;
        }

        c_name.sprite = img_name[x];
        txt_cat_number.text = string.Format("{0}/16", x+1);
    }

    public void selectCatButton()
    {
        int score = x * 60;
        needChur = 50 + (100 * (x - 1));

        if (btn_havecat)
        {
            //고양이 선택가능(선택함)
            useCat.SetActive(true);           

            PlayerPrefs.SetInt("CatKind", x);
            userManager.um.currentSelectCat = x;

            set_x = PlayerPrefs.GetInt("CatKind", 0);

            Debug.Log(userManager.um.currentSelectCat);

            soundManager.I.play_sfx("catBasic", 0.1f,0.8f);
        }
        else
        {
            pannel_Unlock.SetActive(true);
            txt_unlockScore.text = string.Format("{0:N0} 달성 시", score);
            txt_unlockChur.text = string.Format("{0:N0} 필요", needChur);
            btn_unlock.GetComponent<Button>().interactable = false;
            soundManager.I.play_sfx("btnFalse", 0.4f, 0.9f);
            
            if (userManager.um.readyCat[x])
            {
                //고양이 해금조건 판넬
                txt_unlockScore.text = string.Format("{0:N0} 달성 시",score);
                txt_unlockChur.text = string.Format("{0:N0} 필요", needChur);
                btn_unlock.GetComponent<Button>().interactable = true;                
            }
        }
        
    }

    public void unlockCat()
    {
        float have_chur = PlayerPrefs.GetFloat("have_chur", 0);

        if(needChur > have_chur)
        {
            ui_unlock.sprite = img_uiUnlock[1];
            txtUi.text = "구입실패!"+"\n"+"츄르가 부족하다냥!";
            txtUi.gameObject.SetActive(true);

            txt_unlockScore.gameObject.SetActive(false);
            txt_unlockChur.gameObject.SetActive(false);
            btn_unlock.SetActive(false);

            soundManager.I.play_sfx("buyFailed", 1);
            if (userManager.um.init_vive == 1)
            {
                vibeManager.I.vibe_2(100, 200);
            }
            if (userManager.um.init_vive == 0)
            {
                Vibration.Cancel();
            }            
        }

        if(needChur <= have_chur)
        {
            ui_unlock.sprite = img_uiUnlock[1];
            txtUi.text = "구입성공!" + "\n" + "새로운 고양이와 친하게 지내라냥!";
            txtUi.gameObject.SetActive(true);

            txt_unlockScore.gameObject.SetActive(false);
            txt_unlockChur.gameObject.SetActive(false);
            btn_unlock.SetActive(false);

            //츄르 계산
            PlayerPrefs.SetFloat("have_chur", have_chur - needChur);
            userManager.um.init_chur = have_chur - needChur;        
            
            //소유한 고양이 추가
            PlayerPrefs.SetInt(string.Format("{0}_cathave", x), 1);
            userManager.um.haveCat[x] = true;

            //글자 업데이트
            textManager.I.chur_updator();
            change_img();

            soundManager.I.play_sfx("buySuccess", 1);
        }
    }
    
    public void cl_pannel()
    {
        soundManager.I.play_sfx("btnClick", 0.3f, 0.7f);

        pannel_Unlock.SetActive(false);

        ui_unlock.sprite = img_uiUnlock[0];
        btn_unlock.SetActive(true);
        txt_unlockScore.gameObject.SetActive(true);
        txt_unlockChur.gameObject.SetActive(true);

        txtUi.gameObject.SetActive(false);
    }
}   
