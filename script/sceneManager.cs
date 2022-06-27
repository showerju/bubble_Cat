using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager I;
    bool buff;
    bool ads;

    private void Awake()
    {
        I = this;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
