using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenditLever : MonoBehaviour, Activatable
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
        if(i < l)
        {
            w = wheel[i].GetComponent<BanditWheel>();
            w.setStopWheel();
            i++;
        }

        //if(i >= l)
        //{
        //    BanditData.Instance.solved = true;
        //}
    }
}
