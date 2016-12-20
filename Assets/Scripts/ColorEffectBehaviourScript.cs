using UnityEngine;
using System.Collections;

public class ColorEffectBehaviourScript : FigureMovementBehaviourScript
{

    public Texture red;
    public Texture green;
    public Texture blue;
    public Texture purple;

    bool shrinking = true;


    //public GameObject colorBoard;

    private Texture[,] colorEffectCards = new Texture[4, 16];
    private Texture[] colorEffectCard0;
    private Texture[] colorEffectCard1;
    private Texture[] colorEffectCard2;
    private Texture[] colorEffectCard3;

    private int random;
    private bool changeRandomTextureArray = true;
    private Texture[] tempRandomTexture = new Texture[16];
    private int tempRandom = 100; //muss nur irgendeinen Wert >3 haben, da es sonst beim ersten Aufruf von randomTexture() zu Problemen kommen kann.

    // Use this for initialization
    void Start()
    {
        Texture[] tempColorEffectsCard0 = { green, red, purple, red, purple, red, blue, purple, blue, purple, red, green, purple, green, red, blue };
        Texture[] tempColorEffectsCard1 = { purple, red, blue, green, blue, green, purple, blue, red, purple, red, purple, red, blue, green, red };
        Texture[] tempColorEffectsCard2 = { blue, red, green, purple, green, red, purple, blue, purple, blue, red, purple, red, purple, red, green };
        Texture[] tempColorEffectsCard3 = { red, green, blue, red, purple, red, purple, red, blue, purple, green, blue, green, blue, red, purple };
        /*
        * Texture[] colorEffectsCard3 = 
        * Texture[] colorEffectsCard4 = 
        * Texture[] colorEffectsCard5 = 
        * Texture[] colorEffectsCard.....
        */

        colorEffectCard0 = tempColorEffectsCard0;
        colorEffectCard1 = tempColorEffectsCard1;
        colorEffectCard2 = tempColorEffectsCard2;
        colorEffectCard3 = tempColorEffectsCard3;

        generateColorEffectCardsOrder();
        changeColors(0);
        computeNewColorCardArray(0);

    }

    // Update is called once per frame
    void Update()
    {
        arrowBoardNumber = figure.GetComponent<FigureMovementBehaviourScript>().arrowBoardNumber;

        if (arrowBoardNumber == 1)
        {
            changeColors(arrowBoardNumber);
            computeNewColorCardArray(arrowBoardNumber);
        }
        if (arrowBoardNumber == 2)
        {
            changeColors(arrowBoardNumber);
            computeNewColorCardArray(arrowBoardNumber);
        }
        if (arrowBoardNumber == 3)
        {
            changeColors(arrowBoardNumber);
            computeNewColorCardArray(arrowBoardNumber);
        }


        for (int i = 0; i < 16; i++)
        {
            colorBoard.transform.GetChild(i).Rotate(Vector3.back * (20* Time.deltaTime));
            if (colorBoard.transform.GetChild(i).localScale.x >= 10 && shrinking) {
                colorBoard.transform.GetChild(i).localScale -= new Vector3(1, 1, 0) * Time.deltaTime*3;
            }else
            {
                colorBoard.transform.GetChild(i).localScale += new Vector3(1, 1, 0) * Time.deltaTime*3;
            }
            
        }
        if (colorBoard.transform.GetChild(0).localScale.x<=10 )
        {
            shrinking = false;
        }else if (colorBoard.transform.GetChild(0).localScale.x >= 20) { shrinking = true; }

    }

    Texture[] randomTexture()
    {
        Texture[] tempTextureArray = new Texture[16];
        random = Random.Range(0, 4);
        if (random == tempRandom)
        {
            randomTexture();
        }
        if (random == 0)
        {
            tempTextureArray = colorEffectCard0;
        }
        if (random == 1)
        {
            tempTextureArray = colorEffectCard1;
        }
        if (random == 2)
        {
            tempTextureArray = colorEffectCard2;
        }
        if (random == 3)
        {
            tempTextureArray = colorEffectCard3;
        }
        tempRandom = random;
        return tempTextureArray;
    }

    public void generateColorEffectCardsOrder()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < colorEffectCard1.Length; j++)
            {
                if (changeRandomTextureArray == true)
                {
                    tempRandomTexture = randomTexture();
                    changeRandomTextureArray = false;
                }
                if (i == 0)
                {
                    colorEffectCards[i, j] = tempRandomTexture[j];
                }
                if (i == 1)
                {
                    colorEffectCards[i, j] = tempRandomTexture[j];
                }
                if (i == 2)
                {
                    colorEffectCards[i, j] = tempRandomTexture[j];
                }
                if (i == 3)
                {
                    colorEffectCards[i, j] = tempRandomTexture[j];
                }
            }
            changeRandomTextureArray = true;
        }
    }

    public void computeNewColorCardArray(int arrowBoardNumber)
    {
        for (int i = 0; i < 16; i++)
        {
            if (colorEffectCards[arrowBoardNumber, i] == green)
            {
                figure.GetComponent<FigureMovementBehaviourScript>().colorCardArray[arrowBoardNumber, i] = 4;
            }
            if (colorEffectCards[arrowBoardNumber, i] == red)
            {
                figure.GetComponent<FigureMovementBehaviourScript>().colorCardArray[arrowBoardNumber, i] = 1;
            }
            if (colorEffectCards[arrowBoardNumber, i] == blue)
            {
                figure.GetComponent<FigureMovementBehaviourScript>().colorCardArray[arrowBoardNumber, i] = 3;
            }
            if (colorEffectCards[arrowBoardNumber, i] == purple)
            {
                figure.GetComponent<FigureMovementBehaviourScript>().colorCardArray[arrowBoardNumber, i] = 2;
            }
        }
    }

    public void changeColors(int arrowBoardNumber)
    {
        for (int i = 0; i < 16; i++)
        {
            colorBoard.transform.GetChild(i).GetComponent<Renderer>().material.mainTexture = colorEffectCards[arrowBoardNumber, i];
        }
    }

}
