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
	
    void Update()
    {
        if (playerOnBoat)
        {
            playerPosition.transform.position = getBoatPosition();
            if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                JumpFromBoat();
            }
        }
    }

    public void Activate()
    {

        if (!figureFromDirectionGame.GetComponent<FigureMovementBehaviourScript>().won)
        {
            Debug.Log("activated");
            playerCC.enabled = false;
            playerOnBoat = true;
            playerPosition.transform.position = getBoatPosition();
            myAnimator.enabled = true;
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

    private void JumpFromBoat()
    {
        playerOnBoat = false;
        playerPosition.transform.position = new Vector3(playerPosition.transform.position.x + 4.5f, playerPosition.transform.position.y, playerPosition.transform.position.z + 3.5f);
        myAnimator.enabled = false;
        playerCC.enabled = true;
    }


}
