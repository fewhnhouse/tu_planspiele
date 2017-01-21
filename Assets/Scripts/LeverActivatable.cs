using UnityEngine;
using System.Collections;

public class LeverActivatable : MonoBehaviour, Activatable {
    public bool Active;
    //public GameObject leverObject;
    public SkullAnimator Skull;
    private Animator animator;

    private Vector3 startEulerAngles;
    public void Start()
    {
        Active = false;
        animator = GetComponent<Animator>();
        animator.SetBool("Up", !Active);
        Skull.Close();
        /*
        startEulerAngles = leverObject.transform.eulerAngles;
        
        if (Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
            Skull.Close();
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
            Skull.Open();
        }
        */
    }

	public void Activate()
    {
        Active = !Active;
        RotateLever();
    }

    private void RotateLever()
    {
        Skull.GetComponent<Animator>().SetBool("closed", !Active);
        animator.SetBool("Up", !Active);
        GetComponent<AudioSource>().Play();
        /*
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
            
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
            GetComponent<AudioSource>().Play();
        }
        */

    }
}
