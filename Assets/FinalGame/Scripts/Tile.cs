using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo when tile is resetting, play may move freely on platform
[RequireComponent(typeof(Renderer))]
public class Tile : MonoBehaviour {
    //Public
    // set these in the prefab
    public float FallSpeed;
    public float FallDistance;
    [Header("Animation")]
    public float ResetAnimationTime;
    public AnimationCurve ResetCurve;
    public float EmissionAnimationTime;
    public AnimationCurve EmissionCurve;

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

        startPosition = transform.position;
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
                StartCoroutine(resetAnimation());
                break;

            case State.Stay:
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

        //new value
        if(newValue != value)
        {
            //load new material
            Material newMaterial = Resources.Load("FinalPlatformMaterials/platform" + newValue, typeof(Material)) as Material;

            //set new material
            tileRenderer.material = newMaterial;

            //play animation, when number changes
            StartCoroutine(emissionAnimation());
        }

        value = newValue;
    }

    void OnTriggerStay(Collider c)
    {
        //if the player is on the tile
        if (c.gameObject.CompareTag("Player"))
        {
            //if this tile is not safe
            if (gameManager != null && !gameManager.IsSafeNumber(value))
            {
                state = State.Falling;
            }
        }
    }

    public void Reset()
    {
        state = State.Reset;
    }

    //hacky
    //changes emission on all tiles with same material, but since all tiles change number at the same time, it doesnt matter
    private IEnumerator emissionAnimation()
    {
        float duration = EmissionAnimationTime;
        float elapsedTime = 0f;
        
        //make sure to set it back to the original
        tileRenderer.material.SetColor("_EmissionColor", Color.black);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float brightness = EmissionCurve.Evaluate(elapsedTime / duration);

            tileRenderer.material.SetColor("_EmissionColor", new Color(brightness, brightness, brightness));

            yield return null;
        }
        
        //make sure to set it back to the original
        tileRenderer.material.SetColor("_EmissionColor", Color.black);

    }

    private IEnumerator resetAnimation()
    {
        Vector3 bottomPosition = transform.position;

        float duration = ResetAnimationTime;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(bottomPosition, startPosition, ResetCurve.Evaluate(elapsedTime / duration));

            yield return null;
        }

        transform.position = startPosition;
        state = State.Stay;
    }
}
