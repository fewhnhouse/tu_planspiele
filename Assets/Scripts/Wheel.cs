using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

    public int wheelNum = 0;

    private bool turnUp = false;
    private bool turnDown = false;
    private int currentNumber = 1;
    private float rotationAngle = 40.0f;
    private float smoothRotation = 5.0f;
    private Quaternion targetRotation;
    private float target;
    private bool alreadyChanged = false;


    // Use this for initialization
    void Start()
    {
        WheelData.Instance.setCurrentNumber(wheelNum, currentNumber);
        targetRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnUp)
        {
            WheelData.Instance.updateNumber = true;
            turnUp = false;

            rotateDown();

            targetRotation *= Quaternion.Euler(Vector3.left * 40);

        }
        else if (turnDown)
        {
            WheelData.Instance.updateNumber = true;
            turnDown = false;

            rotateUp();

            targetRotation *= Quaternion.Euler(Vector3.right * 40);
        }

        //Apply Rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothRotation);
    }

    public int getCurrentNumber()
    {
        return WheelData.Instance.getCurrentNumber(wheelNum);
    }

    public void rotateUp()
    {
        if (currentNumber == 1)
        {
            WheelData.Instance.setCurrentNumber(wheelNum, currentNumber = 9);
        }
        else
        {
            WheelData.Instance.setCurrentNumber(wheelNum, --currentNumber);
        }
    }

    public void rotateDown()
    {
        WheelData.Instance.setCurrentNumber(wheelNum, currentNumber = (currentNumber % 9) + 1);
    }

    public void setTurnUp()
    {
        turnUp = true;
    }

    public void setTurnDown()
    {
        turnDown = true;
    }
}
