﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditLogic : MonoBehaviour {

    public bool cheat = false;
    public TileGameManager tGM;
    public AudioClip[] turnSounds;
    public AudioClip sucessSound;
    public AudioClip wallSound;
    private AudioSource audioSource;
    private List<int> safeNums = new List<int>();


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BanditData.Instance.updateNumber)
        {
            UpdateSum();
            //printSum();
            //playSound();
            safeNums.Add(BanditData.Instance.getCurrentNumber(0));

            BanditData.Instance.updateNumber = false;
        }

        if (BanditData.Instance.solved)
        {
            for (int i = 0; i < 4; i++)
            {
                safeNums.Add(BanditData.Instance.getCurrentNumber(i));
            }
            tGM.SetSafeNumbers(safeNums);
        }
    }

    private void UpdateSum()
    {
        BanditData.Instance.sum = 0;
        for (int i = 0; i < 4; i++)
        {
            BanditData.Instance.sum += BanditData.Instance.getCurrentNumber(i);
        }
    }

    private void printSum()
    {
        Debug.Log("Wheel 0: " + BanditData.Instance.getCurrentNumber(0) +
            ",            Wheel 1: " + BanditData.Instance.getCurrentNumber(1) +
            ",            Wheel 2: " + BanditData.Instance.getCurrentNumber(2) +
            ",            Wheel 3: " + BanditData.Instance.getCurrentNumber(3));
        Debug.Log(BanditData.Instance.sum);
    }

    private void playSound()
    {
        int n = Random.Range(1, turnSounds.Length);
        audioSource.clip = turnSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
