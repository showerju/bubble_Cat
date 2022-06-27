using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class main_activeBuff : MonoBehaviour
{
    int _min;
    int _sec;

    int _time;

    public Text txt_time;
    public Text kind_buff;

    public Image icon_buff;
    public Sprite[] img_buff = new Sprite[4];

    string buff;

    private void Awake()
    {
        _time = userManager.um._time;
        buff = userManager.um.active_mainbuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(userManager.um.second);
        if (buff == "chur")
        {
            icon_buff.sprite = img_buff[0];
            kind_buff.text = "x 2";
        }
        if (buff == "score")
        {
            icon_buff.sprite = img_buff[1];
            kind_buff.text = "x 1.5";
        }
        if (buff == "time")
        {
            icon_buff.sprite = img_buff[2];
            kind_buff.text = "+30sec";
        }
        if (buff == "fever")
        {
            icon_buff.sprite = img_buff[3];
            kind_buff.text = "fever";
        }
    }

    // Update is called once per frame
    void Update()
    {
        _time = userManager.um.buff_timer;

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
        txt_time.text = string.Format("{0:D2}:{1:D2}", _min, _sec);

        if (!userManager.um.init_buff)
        {
            gameObject.SetActive(false);
        }
    }
}

