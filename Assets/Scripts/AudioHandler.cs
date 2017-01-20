using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

    AudioSource m_audioSource;
    public AudioClip[] myClips;

	// Use this for initialization
	void Start () {
        m_audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKey("f"))
        {
            StartCoroutine(fadeOut(4f));
        }
        if (Input.GetKey("g"))
        {
            StartCoroutine(fadeIn(1.0f, 4f));
        }*/
	}
    //fade in in 0.1f steps
    public IEnumerator fadeIn(float toVolume, float duration)
    {
        float diffVolume = toVolume - m_audioSource.volume;
        float step = (diffVolume/(duration / 0.1f));
        while (m_audioSource.volume < toVolume)
        {
            m_audioSource.volume += step;
            yield return new WaitForSeconds(0.1f);
        }
    }
    //fade out in 0.1f steps
    public IEnumerator fadeOut(float duration)
    {
        
        float step = (m_audioSource.volume/(duration / 0.1f));
        while (m_audioSource.volume > 0)
        {
            m_audioSource.volume -= step;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public IEnumerator fadeOut(float duration, int id, float vol)
    {
        float step = (m_audioSource.volume / (duration / 0.1f));
        while (m_audioSource.volume > 0)
        {
            m_audioSource.volume -= step;
            yield return new WaitForSeconds(0.1f);
        }
        setClip(id);
        StartCoroutine(fadeIn(vol, duration));
        yield return null;
    }

    public bool setClip(int i)
    {
        if (i < myClips.Length)
        {
            m_audioSource.clip = myClips[i];
            m_audioSource.Play();
            return true;
        }
        return false;
    }
}
