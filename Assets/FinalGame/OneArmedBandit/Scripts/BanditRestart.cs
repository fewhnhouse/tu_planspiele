using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditRestart : MonoBehaviour, Activatable {

    public GameObject[] wheel;
    public float LeverAnimationDelay;
    private BanditWheel w;
    private int l = 0;
    private int i = 0;
    private Animator animator;

    void Start()
    {
        l = wheel.Length;
        animator = GetComponent<Animator>();
        animator.SetBool("Up", true);
    }

    public void Activate()
    {
        if(BanditData.Instance.restartBandit)
        {
            BanditData.Instance.restartBandit = false;
            BanditData.Instance.solved = false;
            for (int i = 0; i < l; i++)
            {
                w = wheel[i].GetComponent<BanditWheel>();
                w.setStartWheel();
            }
            i = 0;

            StartCoroutine(trapAnimation());
        }


    }

    private IEnumerator trapAnimation()
    {
        animator.SetBool("Up", false);
        yield return new WaitForSeconds(LeverAnimationDelay);

        animator.SetBool("Up", true);
    }
}
