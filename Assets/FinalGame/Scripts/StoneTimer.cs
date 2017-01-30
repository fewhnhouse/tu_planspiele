using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTimer : MonoBehaviour {
    public Transform StoneSlab;
    public Transform StartTarget;
    public Transform EndTarget;
    public float ResetTime;
    public AnimationCurve ResetAnimationCurve;

    private State state = State.Stop;
    private float percentage;
    private AudioSource gongSound;

    private enum State
    {
        MovingDown, MovingUp, Stop
    }

    // Use this for initialization
    void Start () {
        gongSound = GetComponent<AudioSource>();

        StoneSlab.transform.position = StartTarget.position;
        SetPercentage(1);
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Stop:
                break;

            case State.MovingDown:
                StoneSlab.transform.position = Vector3.Lerp(StartTarget.position, EndTarget.position, GetPercentage());
                break;

            case State.MovingUp:
                break;
                
        }
	}

    public void SetPercentage(float percentage)
    {
        this.percentage = Mathf.Clamp01(percentage);
    }

    //0 == start, 1 == end
    public float GetPercentage()
    {
        return percentage;
    }

    public void StartTimer()
    {
        state = State.MovingDown;
    }

    public void ResetTimer()
    {
        state = State.MovingUp;

        gongSound.Play();

        StartCoroutine(resetCoroutine());
    }

    private IEnumerator resetCoroutine()
    {
        float elapsedTime = 0;
        while(elapsedTime < ResetTime)
        {
            elapsedTime += Time.deltaTime;

            StoneSlab.transform.position = Vector3.Lerp(EndTarget.position, StartTarget.position, ResetAnimationCurve.Evaluate(elapsedTime/ResetTime));
            yield return null;
        }

        state = State.MovingDown;
    }
}
