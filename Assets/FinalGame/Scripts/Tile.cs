using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class Tile : MonoBehaviour {
    //Public
    // set these in the prefab
    public float FallSpeed;
    public float FallDistance;
    public float ResetSpeed;

    //private
    private int value;
    private Renderer tileRenderer;
    private TileGameManager gameManager;
    private State state = State.Stay;
    private Vector3 startPosition;

    private enum State
    {
        Falling, Reset, Stay
    }

	// Use this for initialization
	void Awake () {
        tileRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Falling:
                //so it doesnt fall infinitely
                if (transform.position.y > -FallDistance)
                {
                    transform.Translate(new Vector3(0, -FallSpeed, 0) * Time.deltaTime, Space.World);
                }
                break;
                
            case State.Reset:
                //reset to start position
                transform.position = Vector3.Lerp(transform.position, startPosition, 0.5f);
                if(Vector3.Distance(transform.position, startPosition) < 0.2f)
                {
                    transform.position = startPosition;
                    state = State.Stay;
                }
                break;

            case State.Stay:
                startPosition = transform.position;
                break;
        }
    }

    //sets gamemanager where it will get the information of which are the safe numbers
    public void SetManager(TileGameManager manager)
    {
        gameManager = manager;
    }

    public void SetValue(int newValue)
    {
        if(newValue < 1 || newValue > 9)
        {
            Debug.LogError("Tile: New Value" + newValue + " is not within range");
            return;
        }

        if(newValue != value)
        {
            //load new material
            Material newMaterial = Resources.Load("FinalPlatformMaterials/platform" + newValue, typeof(Material)) as Material;

            //set new material
            tileRenderer.material = newMaterial;
        }

        value = newValue;
    }

    void OnTriggerStay(Collider c)
    {
        //if the player is on the tile
        if (c.gameObject.CompareTag("Player"))
        {
            //if this tile is not safe
            if (!gameManager.IsSafeNumber(value))
            {
                state = State.Falling;
            }
        }
    }

    public void Reset()
    {
        state = State.Reset;
    }
}
