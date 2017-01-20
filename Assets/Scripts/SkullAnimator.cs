using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAnimator : MonoBehaviour {

    private bool closed;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

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
}
