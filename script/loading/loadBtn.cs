using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadBtn : MonoBehaviour
{
    public GameObject img_pts;
    public GameObject btn_pts;
    float a;


    // Start is called before the first frame update
    void Start()
    {
        a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime;

        if (a > 6)
        {
            btn_pts.SetActive(true);
        }

        if (a > 8.5)
        {
            img_pts.SetActive(true);
        }
    }

    public void scene_home()
    {
        sceneManager.I.LoadScene("main");
        soundManager.I.play_sfx("bubbleStart",0.3f);
        vibeManager.I.vibe_1(20);
    }
}
