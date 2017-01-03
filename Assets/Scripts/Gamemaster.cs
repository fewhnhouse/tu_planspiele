using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnHandler))]
public class Gamemaster : MonoBehaviour {
    public List<RequirementHandler.Mode> scenarios = new List<RequirementHandler.Mode>();
    public List<string> scenarioText = new List<string>();
    public Text currentScenarioText;

    public Activatable bridge;
    public LeverActivatable[] levers;

    private SpawnHandler SpHandler;
    private RequirementHandler RHandler;
    private int currentModeIndex;

	// Use this for initialization
	void Start ()
    {
        SpHandler = GetComponent<SpawnHandler>();
        RHandler = GetComponent<RequirementHandler>();
        SetScenario(0);
	}
    
    public void startDice()
    {
        int[] dicenumber = new int[4];
        bool anyActive = false;
        for (int i = 0; i < dicenumber.Length; i++)
        {
            int value = levers[i].Active ? 1 : 0;
            dicenumber[i] = value;

            //check if any lever is active
            if (!anyActive && value == 1)
                anyActive = true;
        }
        
        if(anyActive)
            SpHandler.Spawn(dicenumber);
    }

    public void ResetDice()
    {
        SpHandler.ResetDice();
    }

    public void NextScenario()
    {
        currentModeIndex++;

        if (currentModeIndex < scenarios.Count)
            SetScenario(currentModeIndex);
        else
            Finished();
    }

    private void SetScenario(int index)
    {
        currentModeIndex = index;
        currentScenarioText.text = scenarioText[currentModeIndex];
        RHandler.SetRequirement(scenarios[currentModeIndex]);
    }

    private void Finished()
    {
        Debug.Log("Finished Würfel Puzzle");
    }
}
