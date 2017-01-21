using UnityEngine;
using System.Collections;

public class LeverActivatable : MonoBehaviour, Activatable {
    public bool Active = false;
    public GameObject leverObject;
    public SkullAnimator Skull;

    private Vector3 startEulerAngles;
    public void Start()
    {
        startEulerAngles = leverObject.transform.eulerAngles;
        
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
            Skull.Close();
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
            Skull.Open();
        }
    }

	public void Activate()
    {
        Active = !Active;
        RotateLever();
    }

    private void RotateLever()
    {
        Skull.GetComponent<Animator>().SetBool("closed", !Active);
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
            GetComponent<AudioSource>().Play();
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
            GetComponent<AudioSource>().Play();
        }
   
    }
}
