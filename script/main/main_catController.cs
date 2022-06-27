using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_catController : MonoBehaviour
{
    int cat_index;

    public GameObject cat_img;
    public RuntimeAnimatorController[] cat_controll = new RuntimeAnimatorController[16];

    private void Awake()
    {
        cat_index = userManager.um.currentSelectCat;
    }
    // Start is called before the first frame update
    void Start()
    {
        cat_img.GetComponent<Animator>().runtimeAnimatorController = cat_controll[cat_index];
    }
}
