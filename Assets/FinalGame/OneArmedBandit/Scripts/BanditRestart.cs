using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditRestart : MonoBehaviour, Activatable {

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
        if(BanditData.Instance.restartBandit)
        {
            BanditData.Instance.restartBandit = false;
            BanditData.Instance.solved = false;
            for (int i = 0; i < l; i++)
            {
                w = wheel[i].GetComponent<BanditWheel>();
                w.setStartWheel();
            }
            i = 0;
        }
    }
}
