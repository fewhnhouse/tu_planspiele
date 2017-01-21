using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//handles spawning of dice and gives them to dicemanager

[RequireComponent(typeof(DiceManager))]
public class SpawnHandler: MonoBehaviour {
    public float DiceDespawnDelay;
    public float TrapDoorCloseDelay;
    public TrapDoorAnimation TrapDoor;
    [Range(0f, 90f)]
    public float MaxAngle;
    [Range(1f, 10f)]
    public float MaxLinearForce;
    [Range(1f, 10f)]
    public float MaxAngularForce;
    public List<Transform> Spawnpoints = new List<Transform>();
    public List<GameObject> d6Prefabs = new List<GameObject>();
    public List<GameObject> d10Prefabs = new List<GameObject>();

    private DiceManager DManager;
    private List<GameObject> spawnedDice = new List<GameObject>(4);
    private int maxDiceTypes = 0;
	// Use this for initialization
	void Start () {
        maxDiceTypes = d6Prefabs.Count + d10Prefabs.Count;
        DManager = GetComponent<DiceManager>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Transform t in Spawnpoints)
        {
            if (t)
            {
                Gizmos.DrawWireSphere(t.position, .2f);
                Gizmos.DrawLine(t.position, t.position + t.right);
            }
        }
    }

    public void Spawn(int[] diceTypes)
    {
        //spawn new dice
        /*for (int i = 0; i < diceTypes.Length && i < maxDiceTypes;)
        {
            //if still dice to spawn
            if(diceTypes[i] > 0)
            {
                //is still a valid dice type, otherwise terminate
                if (!SpawnAtPoint(i, Spawnpoints[spawnedDice.Count]))
                {
                    return;
                }
                diceTypes[i]--;
            }
            
            //if already used all spawnpoints
            if(spawnedDice.Count > Spawnpoints.Count)
            {
                break;
            }
            //if all dice of this type are finished spawning move onto next dicetype
            else if(diceTypes[i] <= 0)
            {
                i++;
            }
        }*/

        for(int i = 0; i < diceTypes.Length; i++)
        {
            if(diceTypes[i] == 1)
                SpawnAtPoint(i, Spawnpoints[i]);
        }

        //tell dicemanager to track newly spawned dice
        DManager.SetDice(spawnedDice);
    }

    public void ResetDice()
    {
        DeleteSpawnedDice();
    }

    private void DeleteSpawnedDice()
    {
        TrapDoor.Open();
        foreach (GameObject g in spawnedDice)
        {
            Destroy(g, DiceDespawnDelay);
        }
        spawnedDice.Clear();
        DManager.Reset();

        StartCoroutine(CloseTrapDoor());
    }

    private bool SpawnAtPoint(int diceType, Transform spawnPoint)
    {
        GameObject g = null;
        //Spawn d6
        if(diceType < d6Prefabs.Count)
        {
            //Position and random rotation
            g = (GameObject) Instantiate(d6Prefabs[diceType], spawnPoint.position, Random.rotation);
        }
        //Spawn d10
        else if (diceType - d6Prefabs.Count < d10Prefabs.Count)
        {
            diceType -= d6Prefabs.Count;

            g = (GameObject)Instantiate(d10Prefabs[diceType], spawnPoint.position, Random.rotation);
        }
        //invalid dicetype
        else
        {
            //Debug.Log("Invalid Dicetype: " + diceType);
            return false;
            
        }
        //add to spawned list
        spawnedDice.Add(g);

        //slighty angled random force and random rotational force
        Rigidbody r = g.GetComponent<Rigidbody>();
        r.angularVelocity = Random.onUnitSphere * Random.Range(1, MaxAngularForce);

        //use red axis as standard spawn direction
        Vector3 standardDir = Vector3.right;
        Vector3 leftRightDeviation = Vector3.Slerp(standardDir, Vector3.forward,(MaxAngle * Random.value) / 90);
        Vector3 upDownDeviation = Vector3.Slerp(standardDir, Vector3.up, (MaxAngle * Random.value) / 90);

        Vector3 spawnDirection = (leftRightDeviation + upDownDeviation) / 2f;
        spawnDirection = spawnPoint.TransformDirection(spawnDirection);
        r.velocity = spawnDirection * MaxLinearForce;

        return true;

    }

    private IEnumerator CloseTrapDoor()
    {
        yield return new WaitForSeconds(TrapDoorCloseDelay);

        TrapDoor.Close();
    }
}
