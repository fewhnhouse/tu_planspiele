using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

    public int themeID;
    private GameObject audioHandler;
	// Use this for initialization
	void Start () {
        audioHandler = GameObject.FindGameObjectWithTag("AudioHandler");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeInHierarchy && other.gameObject.tag == "MainCamera" && audioHandler.GetComponent<AudioSource>().clip != audioHandler.GetComponent<AudioHandler>().myClips[themeID])
        {
            Debug.Log("Started Theme : " + themeID);
            StartCoroutine(audioHandler.GetComponent<AudioHandler>().fadeOut(3.0f, themeID, audioHandler.GetComponent<AudioSource>().volume));
        }
    }
}
