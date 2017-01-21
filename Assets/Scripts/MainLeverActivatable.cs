using UnityEngine;
using System.Collections;
using System;

public class MainLeverActivatable : MonoBehaviour, Activatable {
    public bool Active;
   // public GameObject leverObject;
    public Gamemaster gm;
    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Up", true);
        Active = true;
    }

    public void Activate()
    {
        Active = !Active;
        animator.SetBool("Up",Active);

        if (!Active)
        {
            gm.startDice();
        }
        else
        {
            gm.ResetDice();
        }

    }
}
