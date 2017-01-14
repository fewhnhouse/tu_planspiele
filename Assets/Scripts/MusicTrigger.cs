using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

    public int themeID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera" && other.gameObject.GetComponent<AudioSource>().clip != other.gameObject.GetComponent<AudioHandler>().myClips[themeID])
        {
            Debug.Log("Started Theme : " + themeID);
            StartCoroutine(other.gameObject.GetComponent<AudioHandler>().fadeOut(3.0f, themeID, other.gameObject.GetComponent<AudioSource>().volume));
        }
    }
}
