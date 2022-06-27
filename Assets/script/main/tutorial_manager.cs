using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_manager : MonoBehaviour
{
    public GameObject tutorial;

    //Ʃ�丮�� ���� ���� ����
    bool tuto_bool;

    //Ʃ�丮�� �̹���
    public Image tuto_box;
    public Sprite[] tuto_img = new Sprite[7];
    //Ʃ�丮�� �ؽ�Ʈ
    public GameObject tuto_txt_ob;
    public Text tuto_txt;
    private RectTransform txt_rect;

    public GameObject tutoPlay_bg;

    //index
    int index;
    List<int> index_img = new List<int>() { 0, 1, 1, 2, 2, 2, 3, 4, 4, 5, 1, 6, 1, 0,0 };
    List<string> index_txt = new List<string>()
    {
        "�ݰ��ٳ�~!", "����� �ڲ� ������ �� ���� ȯ���Ѵٳ�","�츮 ������ ���ؼ� �����ϰ� �������ְڴٳ�!",
        "������ ���� ������ �� �� �ִٳ�!", "������ �ϸ� �򸣸� ���� �� �ִٳ�","�򸣷� ���忡�� �پ��� �Ǽ��縮�� ������ �� �ִٳ�~!",
        "���� ������ ������ �� �� �ִٳ�", "�̰� �ְ������� �����ִ� ���ڴٳ�","���� ������ ȹ���Ͽ� �پ��� ����̸� ���������!",
        "������ �ִ� �򸣴� ���⿡�� Ȯ���� �� �ִٳ�!","����� ����ȭ���̴ٳ�","�߹ٴ��� ���� ȭ��ǥ�� ���� ������ �ȴٳ�",
        "�ð��ȿ� �ִ��� ���� ������ �Ϸ��ؼ� �򸣸� ȹ���϶��!","�׷� �ູ�� �ڲ��� �ð��Ƕ��!","�׷� �ູ�� �ڲ��� �ð��Ƕ��!"
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

            //�⺻����� ����
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
