using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loadscripte : MonoBehaviour {

    private Vector3 pos;
    private bool loaded;
    public ArrayList finishedLevels;
    public static Loadscripte load;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        if (load == null)
        {
            load = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
    void Start()
    {
        loaded = false;
        // grose der Arraylist inizalisiert
        finishedLevels = new ArrayList();
        finishedLevels.Add(false);
        finishedLevels.Add(false);
        finishedLevels.Add(false);
    }

    public Vector3 getPos()
    {
        return pos;
    }
    public void setPos(Vector3 p)
    {
        pos = p;
    }
    public bool getLoaded()
    {
        return loaded;
    }
    public void setLoaded(bool l)
    {
        loaded = l;
    }
}
