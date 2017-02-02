using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSounds : MonoBehaviour {

    [Range (0,1)]
    public float SFXVolume = 0.35f;
    [Range(0,1)]
    public float SFXSpatialBlend = .5f;
    public List<AudioClip> DieHitSounds = new List<AudioClip>();

    private List<AudioSource> audioSources = new List<AudioSource>();
    private Die DieScript;
    private bool hasPlayedStopSound = false;

    // Use this for initialization

    void Start()
    {
        //Get die script
        DieScript = GetComponent<Die>();

        //add at least 2 audiosource
        AddNewAudioSource();
        AddNewAudioSource();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        if(DieScript != null && !DieScript.rolling && !hasPlayedStopSound)
        {
            OnDieSound();
            hasPlayedStopSound = true;
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bowl"))
        {
            OnDieSound();
        }
        
    }

    private void OnDieSound()
    {
        AudioSource source = GetNonPlayingAudioSource();
        if (source == null)
        {
            Debug.Log("No Audiosources on " + gameObject.name);
        }

        AudioClip clip = GetRandomClip();
        if (clip == null)
        {
            Debug.Log("No Audioclips on " + gameObject.name);
        }

        source.clip = clip;
        source.Play();
    }

    private AudioSource AddNewAudioSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = SFXSpatialBlend;
        source.volume = SFXVolume;
        audioSources.Add(source);
        return source;
    }

    private AudioSource GetNonPlayingAudioSource()
    {
        AudioSource nonPlayingSource = null;

        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                nonPlayingSource = source;
                break;
            }
        }

        if (null == nonPlayingSource)
        {
            return AddNewAudioSource();
        }
        else
        {
            return nonPlayingSource;
        }
    }

    private AudioClip GetRandomClip()
    {
        return DieHitSounds[Random.Range(0, DieHitSounds.Count)];
    }
}
