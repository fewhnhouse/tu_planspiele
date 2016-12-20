using UnityEngine;
using System.Collections;
using System;

public class ButtonScript : MonoBehaviour, Activatable
{
    public GameObject buttons;
    bool pressed = false;
    public int num, pre;

    public void Activate()
    {
        if (num -1 >=0)
        {
            pre = num - 1;
        }else
        {
            if (!pressed)
            {
                this.GetComponent<Renderer>().material.color = Color.blue;
                pressed = true;
            }
        }
        if (buttons.transform.GetChild(pre).GetComponent<ButtonScript>().getPressed() && !pressed)
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
            pressed = true;
        }
    }

    public bool getPressed()
    {
        return pressed;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons.transform.GetChild(i).GetComponent<ButtonScript>().pressed = false;
            buttons.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
