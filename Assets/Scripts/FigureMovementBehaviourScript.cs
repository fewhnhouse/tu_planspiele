using UnityEngine;
using System.Collections;

public class FigureMovementBehaviourScript : MonoBehaviour
{

    public GameObject koordinateSystem;
    public GameObject figure;
    public GameObject reward;
    public GameObject arrowBoards;
    public GameObject colorBoard;
    public GameObject bridg;




    public float[] arrowPositionArray = new float[4];
    public int[,] colorCardArray = new int[4,16];
    public float speed;
    public int arrowBoardNumber;
    public bool interactionPhaseFinished = false;

    private bool startMovement = false;
    private int colorCardi = 0;
    private int[] positionQueue = new int[8];
    private bool positionsStored = false;
    private int startPosition = 0;
    private int step = 0;
    private Vector3 current;
    private Vector3 target;
    private int randomRewardField;
    private int randomFigureField;
    private bool movementFinished = false;
    public bool won;
    private Transform endMarker;



    // Use this for initialization
    void Start()
    {
        setFigure();
        current = figure.transform.position;
        //setFigure muss immer vor setReward ausgeführt werden damit reward und figure nicht auf dem gleichen Feld stehen.
        setReward();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (interactionPhaseFinished == true && positionsStored == false)
        {
            positionsStored = true;
            movementQueue(firstMove(startPosition));
            startMovement = true;
            step = 0;
        }
                
        if (startMovement == true)
        {
            if (step < positionQueue.Length)
            {
                moveOneStep(positionQueue[step]);
            }
            else
            {
                startMovement = false;
                movementFinished = true;
            }
        }

        if (movementFinished == true)
        {
            if (figure.transform.position.x == reward.transform.position.x && figure.transform.position.z == reward.transform.position.z)
            {
                onWin();
            }
            else
            {
                onLose();
            }
            movementFinished = false;
        }

        if (won) {

            
            current = Vector3.MoveTowards(bridg.transform.position, new Vector3(bridg.transform.position.x, 35.0f, bridg.transform.position.z), Time.deltaTime * speed);
            bridg.transform.position = current;

            if (bridg.transform.position == new Vector3(bridg.transform.position.x, 35.0f, bridg.transform.position.z))
            {
                won = false;
            }
        }
    }

    int firstMove(int position)
    {
        //Gibt uns basierend auf der Farbe die Startposition der Figur bzw. die Position nach dem sie zum ersten mal das Feld betritt.
        position = movement4Colors(position);
        storePosition(position);
        colorCardi++;
        return position;
    }


    int movement4Colors(int position)
    {
        //int tempPosition = position;
        if (position == 0)
        {
            //position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], 0, colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], 0, colorCardArray[colorCardi, position + 4], colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 1 || position == 2)
        {
            //position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], 0, colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], 0, colorCardArray[colorCardi, position + 4], colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 3)
        {
            //position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, 0, colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, 0, colorCardArray[colorCardi, position + 4], colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 4 || position == 8)
        {
            //position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4],colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 5 || position == 6 || position == 9 || position == 10)
        {
           //position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4], colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 7 || position == 11)
        {
            //position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4], position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, colorCardArray[colorCardi, position - 4], colorCardArray[colorCardi, position + 4], colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 12)
        {
            //position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], 0, position);
            position = compareProbabilities(0, colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], 0, colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 13 || position == 14)
        {
            //position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], 0, position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], colorCardArray[colorCardi, position + 1], colorCardArray[colorCardi, position - 4], 0, colorCardArray[colorCardi, position], position);
            return position;
        }
        if (position == 15)
        {
            //position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, colorCardArray[colorCardi, position - 4], 0, position);
            position = compareProbabilities(colorCardArray[colorCardi, position - 1], 0, colorCardArray[colorCardi, position - 4], 0, colorCardArray[colorCardi, position], position);
            return position;
        }

        return position;
    }

    /*
     * Ich sortiere die Wahrscheinlichkeiten der Farben mit interger Zahlen (die kleinste Zahl hat die höchste Wahrscheinlichkeit)
     * --> green = 4, blue = 3, purple = 2, red = 1;
     * --> left = 0; up = 1; right = 2; down = 3; stay = 4;
     */

    int bubbleSort(int left, int right, int up, int down, int stay)
    {
        int[,] array = { { left, 0 }, { right, 1 }, { up, 2 }, { down, 3 }, {stay, 4 } };
        int temp;
        int tempDirection;
        int row = 0;
        for (int i = 1; i < 5; i++)
        {
            for (int j = 0; j < 5 - i; j++)
            {
                if (array[j, row] > array[j + 1, row])
                {
                    temp = array[j, row];
                    array[j, row] = array[j + 1, row];
                    array[j + 1, row] = temp;

                    tempDirection = array[j, row + 1];
                    array[j, row + 1] = array[j + 1, row + 1];
                    array[j + 1, row + 1] = tempDirection;
                }
            }
        }
        return array[4, 1];
    }

    int compareProbabilities(int left, int right, int up, int down, int stay, int position)
    {
        int tempDirection = bubbleSort(left, right, up, down, stay);
        if (tempDirection == 0)
        {
            position--;
        }
        if (tempDirection == 1)
        {
            position++;
        }
        if (tempDirection == 2)
        {
            position -= 4;
        }
        if (tempDirection == 3)
        {
            position += 4;
        }
        /*
         * if (tempDirection == 4)   if the figure stays already on the highest probabilityfield
            {
                return position;
            }
        */

        return position;//Gibt die Position zurück die die höchste Wahrscheinlichkeit hat, zu welcher sich der Spieler bewegt.
    }

    void moveOneStep(int position)
    {
        target = new Vector3(koordinateSystem.transform.GetChild(position).position.x, figure.transform.position.y, koordinateSystem.transform.GetChild(position).position.z);
        if (current != target)
        {
            current = Vector3.MoveTowards( current, target, Time.deltaTime * speed);
            figure.transform.position = current;
        }
        else
        {
            step++;
            current = target;
        }
    }

    void storePosition(int position)
    {
        positionQueue[step] = position;
        step++;
    }

    void movementQueue(int position)
    {
        for (int i = 0; i < arrowPositionArray.Length; i++)
        {
            if (arrowPositionArray[i] == 0 && position % 4 != 3)
            {
                position++;
                    //figure.transform.position = new Vector3(koordinateSystem.transform.GetChild(position).position.x, 19f, koordinateSystem.transform.GetChild(position).position.z);                
            }
            if (arrowPositionArray[i] == 2 && position < 12)
            {
                position += 4;
                    //figure.transform.position = new Vector3(koordinateSystem.transform.GetChild(position).position.x, 19f, koordinateSystem.transform.GetChild(position).position.z);    
            }
            if (arrowPositionArray[i] == 3 && position % 4 != 0)
            {
                position--;
                    //figure.transform.position = new Vector3(koordinateSystem.transform.GetChild(position).position.x, 19f, koordinateSystem.transform.GetChild(position).position.z);   
            }
            if (arrowPositionArray[i] == 1 && position > 3)
            {
                position -=4;
                    //figure.transform.position = new Vector3(koordinateSystem.transform.GetChild(position).position.x, 19f, koordinateSystem.transform.GetChild(position).position.z);               
            }
            storePosition(position);
            if (i < 3)
            {
                position = movement4Colors(position);
                storePosition(position);
            }
            colorCardi++;
        }
    }

    void setReward()
    {
        randomRewardField = Random.Range(0, 16);
        if (randomRewardField == randomFigureField)
        {
            setReward();
        }
        reward.transform.position = new Vector3(koordinateSystem.transform.GetChild(randomRewardField).position.x, reward.transform.position.y, koordinateSystem.transform.GetChild(randomRewardField).position.z);
    }

    void onWin()
    {
        Debug.Log("Gewonnen");
        won = true;
        
    }

    void onLose()
    {
        Debug.Log("Verloren! Versuch's nochmal");
        resetGame();
    }

    void setFigure()
    {
        randomFigureField = Random.Range(0, 16);
        figure.transform.position = new Vector3(koordinateSystem.transform.GetChild(randomFigureField).position.x, figure.transform.position.y, koordinateSystem.transform.GetChild(randomFigureField).position.z);
        startPosition = randomFigureField;
    }

    void resetGame()
    {
        current = figure.transform.position;
        arrowBoardNumber = 0;
        arrowBoards.GetComponent<ArrowBoardRotation>().setI(0);
        interactionPhaseFinished = false;
        arrowBoards.GetComponent<ArrowBoardRotation>().setArrowSelectionPhase(true);
        positionsStored = false;
        colorCardi = 0;
        step = 0;
        startPosition = positionQueue[7];
        colorBoard.GetComponent<ColorEffectBehaviourScript>().generateColorEffectCardsOrder();
        colorBoard.GetComponent<ColorEffectBehaviourScript>().changeColors(0);
        colorBoard.GetComponent<ColorEffectBehaviourScript>().computeNewColorCardArray(0);
        arrowBoards.GetComponent<ArrowBoardRotation>().buttons.GetComponentInChildren<ButtonScript>().restButtons();
    }

}
