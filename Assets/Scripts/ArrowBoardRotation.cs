using UnityEngine;
using System.Collections;
using System;

public class ArrowBoardRotation : FigureMovementBehaviourScript , Activatable
{

    //public GameObject arrowBoards;
 
    public GameObject buttons;
    private int i = 0;
    
    private bool arrowSelectionPhase = true;
    float[] PositionArray = { 0.0f, 0.0f, 0.0f, 0.0f };
    public BoxCollider collider0, collider1, collider2, collider3;
    public BoxCollider[] colliders ;
    // Use this for initialization
    void Start()
    {
        colliders = new BoxCollider[] { collider0, collider1, collider2, collider3 };
    }

    public void setI(int iNew)
    {
        i = iNew;
    }

    public void setArrowSelectionPhase(bool b)
    {
        arrowSelectionPhase = b;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (i < 4 && buttons.transform.GetChild(i).GetComponent<ButtonScript>().getPressed() )
        {
            if (i < 3)
            {
                figure.GetComponent<FigureMovementBehaviourScript>().arrowBoardNumber++;
            }

            if (i<4) {
                i++;
                colliders[i-1].enabled = false;
                if (i<4) {
                    colliders[i].enabled = true;
                }
            }
            
            if (i == 4)
            {
                arrowSelectionPhase = false;
                figure.GetComponent<FigureMovementBehaviourScript>().interactionPhaseFinished = true;
                arrowPositions();
                colliders[i-1].enabled = false;
                colliders[0].enabled = true;

            }
        }

    }

    void arrowPositions()
    {
        
        for (int i = 0; i < 4; i++)
        {            
            figure.GetComponent<FigureMovementBehaviourScript>().arrowPositionArray[i]= PositionArray[i];
        }
    }

    public void Activate()
    {
        if (arrowSelectionPhase == true)
        {
            //arrowBoards.transform.GetChild(i).GetChild(0).rotation *= Quaternion.Euler(0, 90, 0);

            arrowBoards.transform.GetChild(i).GetComponent<Animator>().SetTrigger("Activate");

            PositionArray[i] = (PositionArray[i] + 1) % 4;
        }

        //if (arrowSelectionPhase == true)
        //{
        //    switch (arrowBoards.transform.GetChild(i).eulerAngles.ToString())
        //    {
        //        case "(0.0, 0.0, 0.0)":
        //            arrowBoards.transform.GetChild(i).eulerAngles = new Vector3(0f, 90f, 0f);

        //            interactionButtonPressed = false;
                    
        //            break;
        //        case "(0.0, 90.0, 0.0)":
        //            arrowBoards.transform.GetChild(i).eulerAngles = new Vector3(0f, 180f, 0f);
        //            interactionButtonPressed = false;
        //            break;
        //        case "(0.0, 180.0, 0.0)":
        //            arrowBoards.transform.GetChild(i).eulerAngles = new Vector3(0f, 270f, 0f);
        //            interactionButtonPressed = false;
        //            break;
        //        case "(0.0, 270.0, 0.0)":
        //            arrowBoards.transform.GetChild(i).eulerAngles = new Vector3(0f, 0f, 0f);
        //            interactionButtonPressed = false;
        //            break;
        //    }
        //}

    }
}

