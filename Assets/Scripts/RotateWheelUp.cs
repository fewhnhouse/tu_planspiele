using UnityEngine;
using System.Collections;

public class RotateWheelUp : MonoBehaviour, Activatable {

    public GameObject wheel;
    private Wheel w;

    void Start()
    {
        w = wheel.GetComponent<Wheel>();
    }

    public void Activate()
    {
        if (!WheelData.Instance.solved)
            w.setTurnUp();
    }
}
