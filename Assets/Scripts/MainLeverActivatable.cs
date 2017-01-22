using UnityEngine;
using System.Collections;
using System;

public class MainLeverActivatable : MonoBehaviour, Activatable {
    public bool Active;
   // public GameObject leverObject;
    public Gamemaster gm;
    private Animator animator;
    private AudioSource audioSource;
    public void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("Up", true);
        Active = true;
    }

    public void Activate()
    {
        Active = !Active;
        animator.SetBool("Up",Active);
        audioSource.Play();

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
