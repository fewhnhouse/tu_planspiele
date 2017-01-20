using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWheel : MonoBehaviour {

    public int wheelNum = 0;
    public float smoothRotation = 5.0f;

    private bool turnUp = false;
    private bool turnDown = false;
    private int currentNumber = 1;
    private float rotationAngle = 40.0f;
    private Quaternion targetRotation;
    private Quaternion sourceRotation;
    private float target;
    private bool stopWheel = false;


    // Use this for initialization
    void Start()
    {
        BanditData.Instance.setCurrentNumber(wheelNum, currentNumber);
        targetRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnUp)
        {
            BanditData.Instance.updateNumber = true;
            turnUp = false;

            rotateDown();

            targetRotation *= Quaternion.Euler(Vector3.left * rotationAngle);
            sourceRotation = transform.rotation;

            //Debug.Log("sourceRotation: " + sourceRotation + ";  targetRotation: " + targetRotation);
            //Debug.Log(Quaternion.Angle(transform.rotation, targetRotation));
        }
        else if (turnDown)
        {
            BanditData.Instance.updateNumber = true;
            turnDown = false;

            rotateUp();

            targetRotation *= Quaternion.Euler(Vector3.right * rotationAngle);
        }

        //Apply Rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothRotation);

        if (Quaternion.Angle(transform.rotation, targetRotation) <= 99.0f && !stopWheel)
        {
            turnUp = true;
        }
    }

    public int getCurrentNumber()
    {
        return BanditData.Instance.getCurrentNumber(wheelNum);
    }

    public void rotateUp()
    {
        if (currentNumber == 1)
        {
            BanditData.Instance.setCurrentNumber(wheelNum, currentNumber = 9);
        }
        else
        {
            BanditData.Instance.setCurrentNumber(wheelNum, --currentNumber);
        }
    }

    public void rotateDown()
    {
        BanditData.Instance.setCurrentNumber(wheelNum, currentNumber = (currentNumber % 9) + 1);
    }

    public void setTurnUp()
    {
        turnUp = true;
    }

    public void setTurnDown()
    {
        turnDown = true;
    }

    public void setStopWheel()
    {
        stopWheel = true;
    }
}
