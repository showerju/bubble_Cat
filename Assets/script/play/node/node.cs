using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class node : MonoBehaviour
{
    // 화살표 이미지
    public Image sprArrow;
    int x;

    private List<int> x_list;

    public bool isActive
    {
        get
        {
            return gameObject.activeInHierarchy;
        }
    }

    public void setNodeInfo(Sprite arrow)
    {
        x = gameManager.I.x;

        sprArrow.sprite = arrow;
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        x_list = gameManager.I.x_list;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //모든 노드가 사라졌을 때 작동함
    private void OnDisable()
    {
        if (x_list.Count <= 1)
        {
            gameManager.I.checker_good();
            gameManager.I.play_game();
        }
    }
}
