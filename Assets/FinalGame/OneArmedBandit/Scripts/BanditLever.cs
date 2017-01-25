using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditLever : MonoBehaviour, Activatable
{
    public GameObject[] wheel;
    private BanditWheel w;
    private int l = 0;
    private int i = 0;

    void Start()
    {
        l = wheel.Length;
    }

    public void Activate()
    {
        //Debug.Log("length: " + l + "; i: " + i);

        if (i < l)
        {
            w = wheel[i].GetComponent<BanditWheel>();
            w.setStopWheel();
        }

        i++;
        if (i == l)
        {
            BanditData.Instance.solved = true;
        }
        else if (i > l)
        {
            for (int i = 0; i < 4; i++)
            {
                w = wheel[i].GetComponent<BanditWheel>();
                w.setStartWheel();
            }
            i = 0;
        }
    }
}
