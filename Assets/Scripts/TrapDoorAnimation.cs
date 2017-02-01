using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAnimation: MonoBehaviour {
    public bool UpdateBoxCollider = true;
    public bool PlaySound = true;
    private bool closed = true;
    private Animator animator;
    private Collider doorCollider;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (PlaySound)
            audioSource = GetComponent<AudioSource>();

        if (UpdateBoxCollider)
            doorCollider = GetComponent<Collider>();

        Close();
    }

    public void Open()
    {
        closed = false;
        UpdateCollider();
        animator.SetBool("closed", closed);

        if (PlaySound)
            audioSource.Play();
    }

    public void Close()
    {
        closed = true;
        UpdateCollider();
        animator.SetBool("closed", closed);
    }

    public bool isClosed()
    {
        return closed;
    }

    private void UpdateCollider()
    {
        if(UpdateBoxCollider)
            doorCollider.enabled = closed;
    }
}
