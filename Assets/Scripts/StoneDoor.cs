using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor: MonoBehaviour {
    public float doorMovSpeed = 0.02f;
    public ParticleSystem DoorDust1;
    public ParticleSystem DoorDust2;
    public AudioClip sucessSound;
    public AudioClip wallSound;
    private AudioSource audioSource;
    private float initialDoorHeight;
    private float newDoorHeight;
    private DoorState state = DoorState.Closed;

    private enum DoorState
    {
        Closed, Moving, Open
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        initialDoorHeight = newDoorHeight = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
		if(state == DoorState.Moving)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            newDoorHeight = transform.position.y;
            if (initialDoorHeight - newDoorHeight > 4)
            {
                state = DoorState.Closed;
            }
        }
        else if(state == DoorState.Closed)
        {
                audioSource.Stop();
                DoorDust1.Stop();
                DoorDust2.Stop();
        }
	}

    public void Open()
    {
        //start sound of rockdoor
        audioSource.clip = wallSound;
        audioSource.loop = wallSound;
        audioSource.Play();

        //start displaying the dust from the rockdoor
        DoorDust1.Play();
        DoorDust2.Play();

        state = DoorState.Moving;
    }
}
