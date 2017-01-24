using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGameUI : MonoBehaviour {
    public Text TimeText;
    public Text NumberText;
    public TileGameManager GameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Set timer text
        TimeText.text = "Zeit übrig: " + string.Format("{0:00}", GameManager.GetTimeLeftInRound());

        //set safe numbers
        string zahlenText = "Zahlen:";
        foreach(int i in GameManager.GetSafeNumbers())
        {
            zahlenText += " " + i + ",";
        }

        NumberText.text = zahlenText;
	}
}
