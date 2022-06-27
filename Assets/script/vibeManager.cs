using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibeManager : MonoBehaviour
{
    public static vibeManager I;

    private void Awake()
    {
        I = this;
    }
    public void vibe_1(int second)
    {
        Vibration.Vibrate((long)second);
    }
    public void vibe_2(int second1, int second2)
    {
        long[] pattern = new long[4];
        pattern[0] = 0;
        pattern[1] = second1;
        pattern[2] = 20;
        pattern[3] = second2;
    }
}
