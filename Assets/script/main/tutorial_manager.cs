using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_manager : MonoBehaviour
{
    public GameObject tutorial;

    //튜토리얼 최초 실행 여부
    bool tuto_bool;

    //튜토리얼 이미지
    public Image tuto_box;
    public Sprite[] tuto_img = new Sprite[7];
    //튜토리얼 텍스트
    public GameObject tuto_txt_ob;
    public Text tuto_txt;
    private RectTransform txt_rect;

    public GameObject tutoPlay_bg;

    //index
    int index;
    List<int> index_img = new List<int>() { 0, 1, 1, 2, 2, 2, 3, 4, 4, 5, 1, 6, 1, 0,0 };
    List<string> index_txt = new List<string>()
    {
        "반갑다냥~!", "고양이 꾹꾹 마을에 온 것을 환영한다냥","우리 마을에 대해서 간단하게 설명해주겠다냥!",
        "빨래를 눌러 빨래를 할 수 있다냥!", "빨래를 하면 츄르를 얻을 수 있다냥","츄르로 옷장에서 다양한 악세사리를 구매할 수 있다냥~!",
        "집을 누르면 옷장을 볼 수 있다냥", "이건 최고점수를 보여주는 숫자다냥","높은 점수를 획득하여 다양한 고양이를 만나보라냥!",
        "가지고 있는 츄르는 여기에서 확인할 수 있다냥!","여기는 게임화면이다냥","발바닥을 눌러 화살표를 깨면 빨래가 된다냥",
        "시간안에 최대한 많은 빨래를 완료해서 츄르를 획득하라냥!","그럼 행복한 꾹꾹이 시간되라냥!","그럼 행복한 꾹꾹이 시간되라냥!"
    };
    

    void Awake()
    {        
        tutorial.SetActive(false);
        tuto_bool = userManager.um.init_tuto;
    }

    // Start is called before the first frame update
    void Start()
    {
        txt_rect = tuto_txt_ob.GetComponent<RectTransform>();

        tuto_box.sprite = tuto_img[index_img[index]];
        tuto_txt.text = index_txt[index];

        index = 0;
        if (!tuto_bool)
        {
            tutoPlay_bg.SetActive(false);
            Time.timeScale = 0.0f;
            tutorial.SetActive(true);

            //기본고양이 소유
            PlayerPrefs.SetInt(string.Format("{0}_cathave", 0), 1);
            PlayerPrefs.SetInt(string.Format("{0}_catready", 0), 1);
            userManager.um.readyCat[0] = true;
            userManager.um.haveCat[0] = true;
        }
    }

    public void btn_tuto()
    {
        index++;
        int index_num = index_img[index];        

        if (index < 14)
        {
            tuto_box.sprite = tuto_img[index_img[index]];
            tuto_txt.text = index_txt[index];
            soundManager.I.play_sfx("tutoPop", 1);
            vibeManager.I.vibe_1(10);
        }        

        if (index_num == 0 || index_num == 1)
        {
            txt_rect.anchoredPosition = new Vector2(0, 0);
        }
        if (index_num == 2)
        {
            txt_rect.anchoredPosition = new Vector2(0, 515);
        }
        if (index_num == 3)
        {
            txt_rect.anchoredPosition = new Vector2(0, -423);
        }
        if (index_num == 4 || index_num == 5)
        {
            txt_rect.anchoredPosition = new Vector2(0, 599);
        }
        if (index_num == 6)
        {
            txt_rect.anchoredPosition = new Vector2(0, 55);
        }
        if (index > 9)
        {
            tutoPlay_bg.SetActive(true);
        }

        if(index == 14)
        {
            tutorial.SetActive(false);
            PlayerPrefs.SetInt("tuto_bool", 0);
            userManager.um.init_tuto = true;

            Time.timeScale = 1.0f;
        }
    }


}
