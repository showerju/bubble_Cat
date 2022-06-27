using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeManager : MonoBehaviour
{
    public bool isNodeActive
    {
        get
        {
            for(int i = 0; i < childnode.Length; ++i)
            {
                if (childnode[i].isActiveAndEnabled)
                {
                    return true;
                }
            }

            return false;
        }
    }

    int count = 0;
    int btn = 0 ;
    private float _Time;

    //x값 리스트
    private List<int> x_list;
    //miss값 리스트
    private List<int> miss_list;
    
    //--------------------------------------------------------------------
    //오브젝트 풀
    //자식 node 23개를 미리 받음
    public node[] childnode = new node[25];

    //화살표 4개를 미리 받음(up,left,right,down)
    public Sprite[] sprArrow = new Sprite[4];
    //--------------------------------------------------------------------
    //btn_bubble 오브젝트 풀(up,left,right,down)
    public int btn_index;
    public GameObject[] btn_bubble = new GameObject[4];
    public Animator[] btn_bubble_anim = new Animator[4];
    //--------------------------------------------------------------------
    //miss_distotion animation
    public Animator anim_miss_distotion;

    //_eff controll
    public Animator _eff;
    public Animator _effFv;


    private void Awake()
    {
        hide_nodes();
    }

    public void hide_nodes()
    {
        for (int i = 0; i < childnode.Length; ++i)
        {
            childnode[i].gameObject.SetActive(false);
        }
    }

    //매니저에서 랜덤으로 받아온 x 값을 
    public void createNode()
    {
        int nodeIndex = getFastestNodeIndex();
        childnode[nodeIndex].setNodeInfo(sprArrow[gameManager.I.x - 1]);
    }

    private int getFastestNodeIndex()
    {        
        for (int i = 0; i < childnode.Length; ++i)
        {
            if (!childnode[i].isActive)
            {
                return i;
            }
        }

        Debug.LogError("notFoundnode Index");
        return -1;
    }

    void Start()
    {
        x_list = gameManager.I.x_list;
        miss_list = gameManager.I.miss_check;
    }

    //노드관리
    void check_node()
    {     
        soundManager.I.play_sfx("bubblePop",1);
        gameManager.I.dirty.SetActive(false);

        if (userManager.um.init_vive == 1)
        {
            Vibration.Vibrate(10);
        }
        if (userManager.um.init_vive == 0)
        {
            Vibration.Cancel();
        }

        gameManager.I.start_handAnim();
        gameManager.I.start_bubble();

        false_node();

        if (gameManager.I.feverBool)
        {
            gameManager.I.fv_eyes.SetActive(true);
        }

        btn = 0;
    }

    void false_node()
    {
        childnode[count].gameObject.SetActive(false);

        if (x_list.Count >= 1)
        {
            count++;
        }
        if (x_list.Count == 1)
        {
            count = 0;
            gameManager.I.del_cloth_active();
        }

        x_list.RemoveAt(0);
    }

    void miss_node()
    {
        btn = 0;
        miss_list.Add(1);
        gameManager.I.cat_miss_anim();
        gameManager.I.fv_eyes.SetActive(false);
        

        if (miss_list.Count == 1)
        {
            gameManager.I.checker_bad1();
            node_distotion(0.0f);

            soundManager.I.play_sfx("bubbleMiss", 0.3f);
            if (userManager.um.init_vive == 1)
            {
                Vibration.Vibrate(50);
            }
            if (userManager.um.init_vive == 0)
            {
                Vibration.Cancel();
            }
        }
        //두번틀림
        if(miss_list.Count == 2)
        {
            gameManager.I.checker_bad2();
            gameManager.I.initializing();
            gameManager.I.del_cloth_active();
            gameManager.I.miss_text();

            gameManager.I.dirty.SetActive(true);

            soundManager.I.play_sfx("bubbleMiss", 0.3f);
            soundManager.I.play_sfx("bubbleMeow", 0.1f);

            if (gameManager.I.feverBool)
            {
                gameManager.I.feverBool = false;
                gameManager.I.fv_effect.SetActive(false);
            }            

            if (userManager.um.init_vive == 1)
            {
                vibeManager.I.vibe_1(10);
            }
            if (userManager.um.init_vive == 0)
            {
                Vibration.Cancel();
            }

            count = 0;

            for (int i = 0; i < childnode.Length; ++i)
            {
                childnode[i].gameObject.SetActive(false);
            }            
        }
    }


    //버튼
    public void btn_up()
    {       
        if (!isNodeActive)
        {
            return;
        }
        if (btn < 0 && btn != 1)
        {
            return;
        }

        btn = 1;
        btn_index = 0;
        btn_bubble_on();

        if (x_list[0] == btn)
        {
            check_node();
            if (!gameManager.I.feverBool)
            {
                _eff.SetTrigger("up");
            }
            else
            {
                _effFv.SetTrigger("fever");
            }
        }
        if (btn > 0)
        {
            if (x_list[0] != btn)
            {
                miss_node();
            }
        }
    }
    public void btn_left()
    {
        if (!isNodeActive)
        {
            return;
        }
        if (btn < 0 && btn != 2)
        {
            return;
        }

        btn = 2;
        btn_index = 1;
        btn_bubble_on();

        if (x_list[0] == btn)
        {
            check_node();
            if (!gameManager.I.feverBool)
            {
                _eff.SetTrigger("left");
            }
            else
            {
                _effFv.SetTrigger("fever");
            }
        }

        if(btn > 0)
        {
            if (x_list[0] != btn)
            {
                miss_node();
            }
        }
    }
    public void btn_right()
    {
        if (!isNodeActive)
        {
            return;
        }
        if (btn < 0 && btn != 3)
        {
            return;
        }

        btn = 3;
        btn_index = 2;
        btn_bubble_on();

        if (x_list[0] == btn)
        {
            check_node();
            if (!gameManager.I.feverBool)
            {
                _eff.SetTrigger("right");
            }
            else
            {
                _effFv.SetTrigger("fever");
            }
        }
        if (btn > 0)
        {
            if (x_list[0] != btn)
            {
                miss_node();
            }
        }
    }
    public void btn_down()
    {
        if (!isNodeActive)
        {
            return;
        }
        if (btn < 0 && btn != 4)
        {
            return;
        }

        btn = 4;
        btn_index = 3;
        btn_bubble_on();

        if (x_list[0] == btn)
        {
            check_node();
            if (!gameManager.I.feverBool)
            {
                _eff.SetTrigger("down");
            }
            else
            {
                _effFv.SetTrigger("fever");
            }            
        }
        if (btn > 0)
        {
            
            if (x_list[0] != btn)
            {
                miss_node();
            }
        }
    }
    void node_distotion(float time)
    {
        anim_miss_distotion.Play("Base Layer.miss_distotion", 0, time);
    }

    void btn_bubble_on()
    {
        btn_bubble[btn_index].gameObject.SetActive(true);
        btn_bubble_anim[btn_index].Play("Base Layer.btn_bubble", 0, 0.0f);
    }
}
