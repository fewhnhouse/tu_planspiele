using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGameManager : MonoBehaviour {

    //Public
    public float TimeForEachRound = 1f;
    public int NumberOfRounds = 5;
    public GameObject PlatformPrefab;
    public Vector2 FieldDimensions;
    public int NumberOfRows;
    public int NumberOfColumns;

    //Private
    private float difficulty = 0;                       //current difficulty       
    private List<int> safeNumbers = new List<int>();    //current safe number for the player
    private float timeLeftInRound;                      
    private Tile[,] tiles;                              //2d array that saves each tile [0,0] is bottom left
    private State state = State.SetDifficulty;          //current game state
    private bool fieldSet = false;
    private bool rulesSet = false;
    private float timesPassed = 0;

    public bool RulesSet
    {
        get
        {
            return rulesSet;
        }

        set
        {
            rulesSet = value;
        }
    }

    private enum State
    {
        SetDifficulty, Started, Ended
    }

	// Use this for initialization
	void Start () {
        SetDifficulty(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(state)
        {
            case State.Started:
                timeLeftInRound -= Time.deltaTime;

                if (timeLeftInRound <= 0)
                {
                    EndOfRound();
                }

                timesPassed += Time.deltaTime;
                break;

            case State.Ended:
                break;

        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        /*Gizmos.DrawWireCube(
            transform.TransformPoint(new Vector3(FieldDimensions.x/2, 0, FieldDimensions.y/ 2)),
            new Vector3(FieldDimensions.x, 2, FieldDimensions.y));*/

        float widthEachTile = FieldDimensions.x / (NumberOfColumns - 1);
        float depthEachTile = FieldDimensions.y / (NumberOfRows - 1);
        Vector3 tileSize = PlatformPrefab.GetComponent<BoxCollider>().size;
        tileSize = new Vector3(tileSize.x, tileSize.z, tileSize.y);

        for (int x = 0; x < NumberOfColumns; x++)
        {
            for (int z = 0; z < NumberOfRows; z++)
            {
                //calculate position relative to manager
                Vector3 position = new Vector3(x * widthEachTile, 0, z * depthEachTile);
                //create new tile

                Gizmos.DrawWireCube(transform.TransformPoint(position), tileSize);
            }
        }

    }

    /// <summary>
    /// Sets the difficulty (1,2,3,4 ) of the tile game.
    /// The higher the difficulty the less the dominant number appears each round; making it harder for the player to find an easy combination to cross the field.
    /// </summary>
    /// <param name="difficulty">Difficulty (1,2,3,4) of the game depending on the prize money set.</param>
    public void SetDifficulty(int difficulty)
    {
        this.difficulty = Mathf.Clamp(difficulty, 1, 4);
        StartGame();
    }

    /// <summary>
    /// Sets the active safe numbers of the player (int 1, ... 9) in the tile game
    /// A safe number means the player may move onto platforms with this number without the platform collapsing under them.
    /// </summary>
    /// <param name="activeNumbers">List of safe numbers</param>
    public void SetSafeNumbers(List<int> safeNumbers)
    {
        if(safeNumbers == null)
        {
            Debug.Log("TileGameManger: safeNumbers List is null.");
            return;
        }

        //remove numbers outside of range
        for (int i = safeNumbers.Count - 1; i > -1; i--)
        {
            if (safeNumbers[i] < 1 || safeNumbers[i] > 9)
            { 
                Debug.Log("TileGameManger: Removed " + safeNumbers[i] + " from safeNumbers List - Out of Range (1-9).");
                safeNumbers.Remove(i);
            }
        }

        if(safeNumbers.Count == 0)
        {
            Debug.Log("TileGameManger: safeNumbers List is empty.");
            return;
        }

        //remove duplicates
        List<int> noDupes = new List<int>();
        foreach (int i in safeNumbers)
        {
            if (!noDupes.Contains(i))
            {
                noDupes.Add(i);
            }
        }
        //todo sort ascending?

        //show on console
        string s = "Set safe numbers to: ";
        foreach(int i in noDupes)
        {
            s += i + ", ";
        }
        Debug.Log(s);

        //safe number is definitely a non-empty list
        this.safeNumbers = noDupes;
    }

    public int[] GetSafeNumbers()
    {
        int[] result = new int[safeNumbers.Count];
        safeNumbers.CopyTo(result);

        return result;
    }

    public bool IsSafeNumber(int number)
    {
        return safeNumbers.Contains(number);
    }

    public float GetTimeLeftInRound()
    {
        return timeLeftInRound;
    }

    public float GetNumberOfRoundsLeft()
    {
        return NumberOfRounds;
    }

    public float GetTimePassedInSeconds()
    {
        return timesPassed;
    }

    private void StartGame()
    {
        SetField();
        RandomizeField();

        timeLeftInRound = TimeForEachRound;
        state = State.Started;
    }

    private Tile GetTile(int x, int z)
    {
        if(x < 0 || z < 0 || x > NumberOfColumns - 1 || z > NumberOfRows - 1)
        {
            return null;
        }

        return tiles[x,z];
    }

    private void SetField()
    {
        if (fieldSet)
            return;

        tiles = new Tile[NumberOfColumns, NumberOfRows];
        float widthEachTile = FieldDimensions.x / (NumberOfColumns - 1);
        float depthEachTile = FieldDimensions.y / (NumberOfRows - 1);

        for(int x = 0; x < NumberOfColumns; x++)
        {
            for(int z = 0; z < NumberOfRows; z++)
            {
                //calculate position relative to manager
                Vector3 position = new Vector3(x * widthEachTile, 0, z * depthEachTile);
                //create new tile
                GameObject newTile = Instantiate(PlatformPrefab, transform.TransformPoint(position), PlatformPrefab.transform.rotation, transform);

                //set game manager
                Tile t = newTile.GetComponent<Tile>();
                t.SetManager(this);

                //add tile to array
                tiles[x, z] = t;
            }
        }

        fieldSet = true;
    }

    //randomize tile values depending on difficulty
    //todo refine algorithm maybe?
    private void RandomizeField()
    {
        //choose dominant number
        int dominantNumber = Random.Range(1, 9);

        //Add tiles to list
        List<Tile> allTiles = new List<Tile>(tiles.Length);

        for (int x = 0; x < NumberOfColumns; x++)
        {
            for (int z = 0; z < NumberOfRows; z++)
            {
                allTiles.Add(GetTile(x, z));
            }
        }

        //shuffle tiles
        int n = allTiles.Count;
        while (n > 1)
        {
            int k = (Random.Range(0, n) % n);
            n--;
            Tile value = allTiles[k];
            allTiles[k] = allTiles[n];
            allTiles[n] = value;
        }

        //split list into dominant and non-dominant number tiles depending on difficulty
        float percentageDominantNumber = 1;
        if(difficulty == 4)
        {
            percentageDominantNumber = 0f;
        }
        else if(difficulty == 3)
        {
            percentageDominantNumber = 0.2f;
        }
        else if(difficulty == 2)
        {
            percentageDominantNumber = 0.3f;
        }
        else
        {
            percentageDominantNumber = 0.4f;
        }
        int cutoffIndex = Mathf.RoundToInt(allTiles.Count * percentageDominantNumber);

        List<Tile> dominantTiles = allTiles.GetRange(0, cutoffIndex);
        List<Tile> nonDominantTiles = allTiles.GetRange(cutoffIndex, allTiles.Count - cutoffIndex);
        List<int> nonDominantNumbers = new List<int>(8);
        for(int i = 1; i < 10; i++)
        {
            if(i != dominantNumber)
            {
                nonDominantNumbers.Add(i);
            }
        }

        //set number in dominant tiles
        foreach(Tile t in dominantTiles)
        {
            t.SetValue(dominantNumber);
        }

        //set numbers in non-dominant tiles
        foreach(Tile t in nonDominantTiles)
        {
            t.SetValue(nonDominantNumbers[Random.Range(0, 8)]);
        }

    }

    private void ResetTiles()
    {
        for (int x = 0; x < NumberOfColumns; x++)
        {
            for (int z = 0; z < NumberOfRows; z++)
            {
                GetTile(x, z).Reset();
            }
        }
    }

    private void EndOfRound()
    {

        if (NumberOfRounds > 1)
        {
            RandomizeField();

            ResetTiles();

            timeLeftInRound = TimeForEachRound;
            NumberOfRounds--;
        }
        else
        {
            EndOfGame();
        }
        
    }

    private void EndOfGame()
    {
        Debug.Log("TileManager: End of Game");
        state = State.Ended;
        //todo...
    }
}
