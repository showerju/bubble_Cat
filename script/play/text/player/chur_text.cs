using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chur_text : MonoBehaviour
{
    public Text churtxt;
    private float churscript;
    public float chur;
    float temp;


    // Start is called before the first frame update
    void Start()
    {
        temp = 0.0f;
        //churscript = gameManager.I.chur;
        chur = 0.0f;
        add_chur();
    }

    // Update is called once per frame
    void Update()
    {
        temp += Time.deltaTime;
        if (temp >= 0.35f)
        {
            churUpdate();
        }
    }

    void churUpdate()
    {
        if (chur < churscript)
        {
            Time.timeScale = 45.0f;

            chur += Time.deltaTime;
            churtxt.text = chur.ToString("N0");
        }

    }

    void add_chur()
    {
        PlayerPrefs.SetFloat("chur", chur);
    }
}
