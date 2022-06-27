using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class closet_buttonManager : MonoBehaviour
{
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void home()
    {
        SceneManager.LoadScene("main");
        soundManager.I.play_sfx("btnClick", 0.6f, 1.0f);
    }
}
