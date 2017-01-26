using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGameUI : MonoBehaviour {
    public List<NumberDoor> NumberDoors = new List<NumberDoor>();

	public void SetSafeNumbers(List<int> safeNumbers)
    {
        //open all doors with a valid number
        for (int i = 0; i < safeNumbers.Count; i++)
        {
            NumberDoors[i].SetValue(safeNumbers[i]);
        }

        //close all doors without a number
        for (int i = safeNumbers.Count; i < NumberDoors.Count; i++)
        {
            NumberDoors[i].Close();
        }
    }
}
