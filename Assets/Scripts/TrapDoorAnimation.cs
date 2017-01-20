using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAnimation: MonoBehaviour {
    private bool closed;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
}
