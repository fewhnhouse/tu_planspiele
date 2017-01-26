using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDoor : MonoBehaviour {
    public Tile tile;
    public TrapDoorAnimation trapDoor;
    public float OpenDelay;

    private bool open = false;

	// Use this for initialization
	void Start () {
        Close();
	}

    public void Close()
    {
        trapDoor.Close();
        open = false;
    }

    public void SetValue(int value)
    {
        if (open)
        {
            StartCoroutine(NewValue(value));
        }
        else
        {
            tile.SetValue(value);
            trapDoor.Open();
            open = true;
        }
    }

    private IEnumerator NewValue(int value)
    {
        trapDoor.Close();
        yield return new WaitForSeconds(OpenDelay);

        tile.SetValue(value);
        trapDoor.Open();

        open = true;
    }
}
