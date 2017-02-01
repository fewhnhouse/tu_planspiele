using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaditsMove : MonoBehaviour {

    private float cooldown = 3.0f;
    public GameObject gameObject;
    public GameObject target;
    private float destiany;
    public int speed = 3;
    private float distanz;
    // Use this for initialization
    private void Start()
    {
        destiany = target.transform.position.y;
        distanz = Mathf.Abs(target.transform.position.y - gameObject.transform.position.y);
    }

    public void Update()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (gameObject.transform.position.y <= destiany)
            {
                Vector3 v = gameObject.transform.position;
                gameObject.transform.position = new Vector3(v.x,v.y +1.0f/50.0f *distanz* Time.deltaTime*speed, v.z);
            }
        }
    }
}
