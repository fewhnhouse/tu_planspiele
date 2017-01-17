using UnityEngine;
using System.Collections;

public class HighscoreLever : MonoBehaviour, Activatable
{
    [Range(1, 4)]
    public int m_difficulty;
    public bool Active = false;
    public TileGameManager myManager;
    public GameObject leverObject;
    private Vector3 startEulerAngles;

    public void Start()
    {
        startEulerAngles = leverObject.transform.eulerAngles;
        //light.enabled = Active;
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
        }
        else
        {
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);
        }
    }

    public void Activate()
    {
        Active = !Active;
        RotateLever();
    }

    private void RotateLever()
    {
        if (!Active)
        {
            leverObject.transform.eulerAngles = new Vector3(-70, startEulerAngles.y, startEulerAngles.z);
            GetComponent<AudioSource>().Play();
        }
        else 
        {
            if (!myManager.RulesSet)
            {
                myManager.SetDifficulty(m_difficulty);
                myManager.RulesSet = true;
                //somewhere here the gold needs to be dropped
            }
            leverObject.transform.eulerAngles = new Vector3(-100, startEulerAngles.y, startEulerAngles.z);       
            GetComponent<AudioSource>().Play();
        }

    }
}
