using System.Collections;
using System.Collections.Generic;
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
    private Tile[,] tiles;
    private bool gameEnded = false;

	// Use this for initialization
	void Start () {
        SetField();
        RandomizeField();

        timeLeftInRound = TimeForEachRound;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameEnded)
        {
            timeLeftInRound -= Time.deltaTime;

            if (timeLeftInRound <= 0)
            {
                EndOfRound();
            }
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            transform.TransformPoint(new Vector3(FieldDimensions.x/2, 0, FieldDimensions.y/ 2)),
            new Vector3(FieldDimensions.x, 2, FieldDimensions.y));
    }

    /// <summary>
    /// Sets the difficulty (float 0...1 ) of the tile game.
    /// The higher the difficulty the less the dominant number appears each round; making it harder for the player to find an easy combination to cross the field.
    /// </summary>
    /// <param name="difficulty">Difficulty (0...1) of the game depending on the prize money set.</param>
    public void SetDifficulty(float difficulty)
    {
        this.difficulty = Mathf.Clamp01(difficulty);
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

        //safe number is definitely a non-empty list
        this.safeNumbers = safeNumbers;
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
                GameObject newTile = Instantiate(PlatformPrefab, position, PlatformPrefab.transform.rotation, transform);

                //set game manager
                Tile t = newTile.GetComponent<Tile>();
                t.SetManager(this);

                //add tile to array
                tiles[x, z] = t;
            }
        }
    }

    private void RandomizeField()
    {
        for (int x = 0; x < NumberOfColumns; x++)
        {
            for (int z = 0; z < NumberOfRows; z++)
            {
                GetTile(x, z).SetValue(Random.Range(1, 9));
            }
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
        Debug.Log("TileManager: End of Round");

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
        gameEnded = true;
    }
}
