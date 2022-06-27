using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    // �� ������ �޼�
    private Sprite hand_R;
    private Sprite hand_L;    

    private GameObject mObjNormal;
    private GameObject mObjMiss;

    private Image mImgHand;

    private Sprite head_N;
    private Sprite head_F;

    private Image mImgHead;


    private void Awake()
    {
        mObjNormal = transform.Find("normal").gameObject;
        mObjMiss = transform.Find("miss").gameObject;

        if (mImgHand == null)
        {
            mImgHand = mObjNormal.transform.Find("hand").GetComponent<Image>();
            mImgHead = mObjNormal.transform.Find("head").GetComponent<Image>();
        }
    }
    public void setSpriteInfo_head(Sprite head_n, Sprite head_f)
    {
        head_N = head_n;
        head_F = head_f;

        if (mImgHead == null)
        {
            mObjNormal = transform.Find("normal").gameObject;
            mImgHead = mObjNormal.transform.Find("head").GetComponent<Image>();
        }

        mImgHead.sprite = head_N;
    }

    public void setSpriteInfo_hand(Sprite hand_r, Sprite hand_l)
    {
        hand_R = hand_r;
        hand_L = hand_l;
        
        if(mImgHand == null)
        {
            mObjNormal = transform.Find("normal").gameObject;
            mImgHand = mObjNormal.transform.Find("hand").GetComponent<Image>();
        }

        mImgHand.sprite = hand_R;
    }
    public void twinkleEyes()
    {
        StartCoroutine(twinkle_1());
    }
    IEnumerator twinkle_1()
    {
        mImgHead.sprite = head_F;
        yield return new WaitForSeconds(0.5f);
        mImgHead.sprite = head_N;
    }

    /// <summary>
    /// �̽��� ������ �����
    /// </summary>
    public void playMissAnim()
    {
        mObjMiss.SetActive(true);
        mObjNormal.SetActive(false);
    }

    /// <summary>
    /// ��ư�� �������� ����� �ִϸ��̼�
    /// </summary>
    public void playButtonAnim()
    {
        mObjNormal.SetActive(true);
        mObjMiss.SetActive(false);
        // 3�� ������    / �� ���� ���̸�         / �긦 ���� / �����̸� �긦 ����
        mImgHand.sprite = mImgHand.sprite == hand_R ? hand_L : hand_R;
    }
}
