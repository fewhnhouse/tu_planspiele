using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, Activatable {
    public AudioSource TreasureSound;

    private bool hasBeenActivated = false;

    public void Activate()
    {
        if (!hasBeenActivated)
        {
            hasBeenActivated = true;

            TreasureSound.Play();
            GetComponent<TreasureChestAnimator>().Open();
        }
    }
}
