using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAnimation: MonoBehaviour {
    private bool closed = true;
    private Animator animator;
    private Collider doorCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();
        Close();
    }

    public void Open()
    {
        closed = false;

        animator.SetBool("closed", closed);
    }

    public void Close()
    {
        closed = true;

        animator.SetBool("closed", closed);
    }

    public bool isClosed()
    {
        return closed;
    }

    private void UpdateCollider()
    {
        doorCollider.enabled = closed;
    }
}
