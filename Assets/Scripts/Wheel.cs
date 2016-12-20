using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

    public int wheelNum = 0;
    public GameObject wheel_up;
    public GameObject wheel_down;

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
        var ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Input.GetButtonDown("Fire1") && !WheelData.Instance.isTurning && !WheelData.Instance.solved)
        {
            if (Physics.Raycast(ray1, out hit, 3))
            {
                //somehow the collider down isn't recognized. Needs to be fixed!
                //for now I switched the functions rotateUp and rotateDown and the rotation Vectors left and right
                if ((hit.collider.gameObject == wheel_up) && wheel_up != null)
                {
                    WheelData.Instance.updateNumber = true;
                    WheelData.Instance.isTurning = true;

                    rotateDown();

                    targetRotation *= Quaternion.Euler(Vector3.left * 40);

                }
                else if ((hit.collider.gameObject == wheel_down) && wheel_down != null)
                {
                    WheelData.Instance.updateNumber = true;
                    WheelData.Instance.isTurning = true;

                    rotateUp();

                    targetRotation *= Quaternion.Euler(Vector3.right * 40);
                }
            }
        }

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

    //IEnumerator spinWheel()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //}
}
