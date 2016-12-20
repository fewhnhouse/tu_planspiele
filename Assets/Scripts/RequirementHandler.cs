using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Handles what happens when dice are finished rolling

public class RequirementHandler : MonoBehaviour {
    public Mode Requirement;
    public enum Mode
    {
        SUM7, ATLEASTTWOFIVES, MAXONEEVENNUMBER
    }


    private Gamemaster gm;
	// Use this for initialization
	void Start () {
        gm = GetComponent<Gamemaster>();
	}
	
	public void SetRequirement(Mode scenario)
    {
        Requirement = scenario;
    }

    public void CheckRequirements(List<int> diceValues)
    {
        bool requirementMet = false;
        switch (Requirement)
        {
            case Mode.SUM7:
                //Debug.Log(sum7(diceValues));
                requirementMet = sum7(diceValues);
                break;

            case Mode.ATLEASTTWOFIVES:
                //Debug.Log(leastTwoFives(diceValues));
                requirementMet = leastTwoFives(diceValues);
                break;

            case Mode.MAXONEEVENNUMBER:
                requirementMet = maxOneEvenNumber(diceValues);
                break;
        }

        if (requirementMet)
        {
            gm.NextScenario();
        }
    }

    private bool sum7(List<int> diceValues)
    {
        int sum = 0;
        foreach(int i in diceValues)
        {
            sum += i;
        }

        return sum == 7;
    }

    private bool leastTwoFives(List<int> diceValues)
    {
        int fives = 0;
        foreach(int i in diceValues)
        {
            if (i == 5)
                fives++;
        }

        return fives >= 2;
    }

    private bool maxOneEvenNumber(List<int> diceValues)
    {
        int evenNumbers = 0;
        foreach(int i in diceValues)
        {
            if(i % 2 == 0)
            {
                evenNumbers++;
            }
        }

        return evenNumbers <= 1;
    }
}
