using UnityEngine;
using System.Collections;

public class HighscoreLever : MonoBehaviour, Activatable
{
    [Range(1, 4)]
    public int m_difficulty;
    public bool Active = false;
    public TileGameManager myManager;
    public LeverAnimatorOnly leverAnimation;
    private Vector3 startEulerAngles;

    public void Start()
    {
        if (!Active)
        {
            leverAnimation.Up(false);
        }
        else
        {
            leverAnimation.Down(false);
        }
    }

    public void Activate()
    {
        Active = !Active;

        if (!myManager.RulesSet)
        {
            myManager.SetDifficulty(m_difficulty);
            myManager.RulesSet = true;
            //somewhere here the gold needs to be dropped
        }

        RotateLever();
    }

    private void RotateLever()
    {
        if (!Active)
        {
            leverAnimation.Up(true);
        }
        else 
        {
            leverAnimation.Down(true);  
        }
    }
}
