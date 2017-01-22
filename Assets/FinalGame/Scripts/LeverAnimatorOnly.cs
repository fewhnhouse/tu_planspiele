using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAnimatorOnly : MonoBehaviour {
    private bool up = true;
    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        animator.SetBool("Up", up);
    }

    public void Up(bool playSound)
    {
        up = true;
        animator.SetBool("Up", up);

        if(playSound)
            audioSource.Play();
    }

    public void Down(bool playSound)
    {
        up = false;
        animator.SetBool("Up", up);

        if (playSound)
            audioSource.Play();
    }

    public bool IsUp()
    {
        return up;
    }
}
