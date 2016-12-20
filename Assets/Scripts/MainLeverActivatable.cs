using UnityEngine;
using System.Collections;
using System;

public class MainLeverActivatable : MonoBehaviour, Activatable {
    public bool Active;
    public GameObject leverObject;
    public Gamemaster gm;


    private Vector3 startEulerAngles;
    public void Start()
    {

        startEulerAngles = leverObject.transform.eulerAngles;
        //light.enabled = Active;
        RotateLever();
    }

    public void Activate()
    {
        Active = !Active;

        if (Active)
        {
            gm.startDice();
        }
        else
        {
            gm.ResetDice();
        }

        RotateLever();
    }

    private void RotateLever()
    {
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
        }

    }
}
