using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour, Activatable
{

    public CharacterController playerCC;
    public GameObject playerPosition;
    public GameObject figureFromDirectionGame;
    public bool playerOnBoat;

    private Animator myAnimator;

	// Use this for initialization
	void Start () {

        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
	}
	

    public void Activate()
    {

        if (figureFromDirectionGame.GetComponent<FigureMovementBehaviourScript>().won)
        {
            playerCC.enabled = false;
            playerOnBoat = true;
            playerPosition.transform.position = getBoatPosition();
        }
        else
        {
            Debug.Log("Win first");
        }
    }

    private Vector3 getBoatPosition()
    {
        return new Vector3(transform.position.x + 1.0f, transform.position.y + 2.5f, transform.position.z);
    }
}
