using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textManager : MonoBehaviour
{
    public static textManager I;

    //chur, score
    public Text[] texts = new Text[2];
    public GameObject[] textsBG = new GameObject[2];

    float chur;
    float high_score;

    void Awake()
    {
        I = this;
        high_score = userManager.um.init_score;
    }

    // Start is called before the first frame update
    void Start()
    {
        chur_updator();
        score_updator();
    }

    public void chur_updator()
    {
        chur = userManager.um.init_chur;
        texts[0].text = chur.ToString("N0");
    }

    public void score_updator()
    {
        high_score = userManager.um.init_score;
        texts[1].text = high_score.ToString("N0");
    }
}
