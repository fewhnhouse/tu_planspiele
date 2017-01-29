using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreasureChestAnimator : MonoBehaviour {
    public UnityEvent OnOpen;

    private bool open = false;
    private Animator animator;
    private bool firedEvent = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //if we are in open, the animation is over, and not in transition
        //then check to only fire event once
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (!firedEvent)
            {
                firedEvent = true;

                OnOpen.Invoke();
            }
        }

    }

    public void Open()
    {
        open = true;
        animator.SetBool("open", open);
    }
}
