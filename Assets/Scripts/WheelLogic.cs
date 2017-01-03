using UnityEngine;
using System.Collections;

public class WheelLogic : MonoBehaviour {

    public bool cheat = false;
    public GameObject door;
    public float doorMovSpeed = 0.02f;
    public GameObject doorDust1;
    public GameObject doorDust2;
    private ParticleSystem ddPS1;
    private ParticleSystem ddPS2;
    public AudioClip[] turnSounds;
    public AudioClip sucessSound;
    public AudioClip wallSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ddPS1 = doorDust1.GetComponent<ParticleSystem>();
        ddPS2 = doorDust2.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WheelData.Instance.updateNumber)
        { 
            UpdateSum();
            //printSum();
            playSound();

            WheelData.Instance.updateNumber = false;
        }

        if (WheelData.Instance.sum == 21 || cheat)
        {
            if ((WheelData.Instance.getCurrentNumber(0) == 7 &&
                WheelData.Instance.getCurrentNumber(1) == 9 &&
                WheelData.Instance.getCurrentNumber(2) == 2 &&
                WheelData.Instance.getCurrentNumber(3) == 3) || cheat)
            {
                if (!WheelData.Instance.solved)
                {
                    //start sound of rockdoor
                    audioSource.clip = wallSound;
                    audioSource.loop = wallSound;
                    audioSource.Play();

                    //start displaying the dust from the rockdoor
                    ddPS1.Play();
                    ddPS2.Play();

                    WheelData.Instance.doorMoving = true;
                }
                WheelData.Instance.solved = true;

                if (WheelData.Instance.doorMoving)
                {
                    door.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
                }
                else
                {
                    audioSource.Stop();
                    ddPS1.Stop();
                    ddPS2.Stop();
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
    }
}
