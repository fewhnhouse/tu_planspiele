using UnityEngine;
using System.Collections;

public class WheelData {

    private static WheelData instance;

    private static int[] wheels = new int[4];
    private static bool numberChanged;
    private static bool turning = false;
    private static bool stopRotating;
    private static int wheelSum;
    private static int currentlyTurning;
    private static bool done = false;
    private static bool moveDoor = false;

    private WheelData()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        instance.Paused = false;
    }

    public static WheelData Instance
    {
        get
        {
            if (instance == null)
            {
                new WheelData();
            }

            return instance;
        }
    }

    public bool Paused
    {
        get;
        set;
    }

    public int getCurrentNumber (int wheelNum)
    {
        return wheels[wheelNum];
    }

    public void setCurrentNumber(int wheelNum, int number)
    {
        wheels[wheelNum] = number;
    }

    public bool updateNumber
    {
        get
        {
            return numberChanged;
        }
        set
        {
            numberChanged = value;
        }
    }

    public bool isTurning
    {
        get
        {
            return turning;
        }
        set
        {
            turning = value;
        }
    }

    public int sum
    {
        get
        {
            return wheelSum;
        }
        set
        {
            wheelSum = value;
        }
    }

    public int wheelNumTurning
    {
        get
        {
            return currentlyTurning;
        }
        set
        {
            currentlyTurning = value;
        }
    }

    public bool stopRotation
    {
        get
        {
            return stopRotating;
        }
        set
        {
            stopRotating = value;
        }
    }

    public bool solved
    {
        get
        {
            return done;
        }
        set
        {
            done = value;
        }
    }

    public bool doorMoving
    {
        get
        {
            return moveDoor;
        }
        set
        {
            moveDoor = value;
        }
    }
}
