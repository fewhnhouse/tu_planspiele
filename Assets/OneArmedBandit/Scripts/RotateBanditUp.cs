using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBanditUp : MonoBehaviour, Activatable
{
    public GameObject wheel;
    private BanditWheel w;

    void Start()
    {
        w = wheel.GetComponent<BanditWheel>();
    }

    public void Activate()
    {
        if (!BanditData.Instance.solved)
            w.setTurnUp();
    }
}
