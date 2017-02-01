using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnHandler))]
public class Gamemaster : MonoBehaviour {
    public bool cheat;
    public List<RequirementHandler.Mode> scenarios = new List<RequirementHandler.Mode>();
    public List<string> scenarioText = new List<string>();
    public Text currentScenarioText;
    
    public LeverActivatable[] levers;
    public StoneDoor stoneDoor;

    private SpawnHandler SpHandler;
    private RequirementHandler RHandler;
    private int currentModeIndex;
    private bool hasCheated = false;

	// Use this for initialization
	void Awake ()
    {
        SpHandler = GetComponent<SpawnHandler>();
        RHandler = GetComponent<RequirementHandler>();
        SetScenario(0);
	}

    void Update()
    {
        if(!hasCheated && cheat)
        {
            hasCheated = true;
            Finished();
        }
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

    public void Finished()
    {
        Loadscripte.load.finishedLevels[0] = true;
        stoneDoor.Open();
        currentScenarioText.text = "Hinweis: 3";
        SpHandler.Finished();
    }
}
