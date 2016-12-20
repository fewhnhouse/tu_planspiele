using UnityEngine;
using System.Collections;

public class WheelLogic : MonoBehaviour {

    public GameObject door;
    public AudioClip[] turnSounds;
    public AudioClip sucessSound;
    public AudioClip wallSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WheelData.Instance.updateNumber && WheelData.Instance.isTurning)
        { 
            UpdateSum();
            printSum();
            playSound();

            WheelData.Instance.updateNumber = false;
            WheelData.Instance.isTurning = false;
        }

        if (WheelData.Instance.sum == 21)
        {
            if (WheelData.Instance.getCurrentNumber(0) == 7 &&
                WheelData.Instance.getCurrentNumber(1) == 9 &&
                WheelData.Instance.getCurrentNumber(2) == 2 &&
                WheelData.Instance.getCurrentNumber(3) == 3)
            {
                if (!WheelData.Instance.solved)
                {
                    audioSource.clip = wallSound;
                    audioSource.loop = wallSound;
                    audioSource.Play();
                    WheelData.Instance.doorMoving = true;
                }
                WheelData.Instance.solved = true;
                Debug.Log("Fertig");

                if (WheelData.Instance.doorMoving)
                {
                    door.transform.Translate(Vector3.down * 0.01f, Space.World);
                }
                else
                {
                    audioSource.Stop();
                }
            }
        }
    }

    private void UpdateSum ()
    {
        WheelData.Instance.sum = 0;
        for (int i = 0; i < 4; i++)
        {
            WheelData.Instance.sum += WheelData.Instance.getCurrentNumber(i);
        }
    }

    private void printSum()
    {
        Debug.Log("Wheel 0: " + WheelData.Instance.getCurrentNumber(0) +
            ",            Wheel 1: " + WheelData.Instance.getCurrentNumber(1) +
            ",            Wheel 2: " + WheelData.Instance.getCurrentNumber(2) +
            ",            Wheel 3: " + WheelData.Instance.getCurrentNumber(3));
        Debug.Log(WheelData.Instance.sum);
    }

    private void playSound()
    {
        int n = Random.Range(1, turnSounds.Length);
        audioSource.clip = turnSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        //turnSounds[n] = turnSounds[0];
        //turnSounds[0] = audioSource.clip;
    }
}
