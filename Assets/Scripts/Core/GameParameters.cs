using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    [SerializeField] private float timerSec;
    [SerializeField] private float timerDelay;
    
    [Space]
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private int playerStartHealth;
    
    [Space] 
    [SerializeField] private int rocketDamage;
    [SerializeField] private float rocketFireInterval;

    [Space] 
    [SerializeField] private int nTopResults = 5;
    [SerializeField] private string resultKeyTemplate = "Result";
    [SerializeField] private string resultRecTemplate = ". Boxes collected:";

    
    private void Awake()
    {
        // Singleton initialization
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public float TimerSec() => timerSec;
    public float TimerDelay() => timerDelay;
    
    public float PlayerMoveSpeed() => playerMoveSpeed;
    public int PlayerStartHealth() => playerStartHealth;

    public int RocketDamage() => rocketDamage;
    public float RocketFireInterval() => rocketFireInterval;

    public int NTopResults() => nTopResults;
    public string ResultKeyTemplate() => resultKeyTemplate;
    public string ResultRecTemplate() => resultRecTemplate;
    
}

