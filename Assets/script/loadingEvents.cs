using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_bgm()
    {
        soundManager.I.play_BGM("BGM",0.15f,0.9f);
    }

    public void play_vibe()
    {

        Vibration.Vibrate(30);

    }

    public void play_bbop()
    {
        soundManager.I.play_sfx("clothBbop", 0.4f,2.0f);
    }

    public void play_pop()
    {
        soundManager.I.play_sfx("textDraw", 0.7f);
    }

    public void play_bbap()
    {
        soundManager.I.play_sfx("textBbap", 0.7f);
    }
    public void play_catBasic()
    {
        soundManager.I.play_sfx("catBasic", 0.1f);
    }
    public void play_bubblebubble()
    {
        soundManager.I.play_sfx("nodeBubble", 0.6f,0.5f);
    }
}
