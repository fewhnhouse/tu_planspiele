using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAnimation: MonoBehaviour {
    public bool UpdateBoxCollider = true;
    private bool closed = true;
    private Animator animator;
    private Collider doorCollider;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (UpdateBoxCollider)
            doorCollider = GetComponent<Collider>();

        Close();
    }

    public void Open()
    {
        closed = false;
        UpdateCollider();
        animator.SetBool("closed", closed);
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
