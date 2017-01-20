using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour, Activatable
{

    public CharacterController playerCC;
    public GameObject playerPosition;
    public GameObject figureFromDirectionGame;

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
            Debug.Log("activate");
        }
    }
}
