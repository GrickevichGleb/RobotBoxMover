using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int _collectedBoxes = 0;

    private int _nTopResults = 5;
    private string _resultKeyTemplate = "Result";
    
    
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

    private void Start()
    {
        _nTopResults = GameParameters.Instance.NTopResults();
        _resultKeyTemplate = GameParameters.Instance.ResultKeyTemplate();
    }

    // Public accessors
    public int CollectedBoxes() => _collectedBoxes;
    

    // Public methods
    public void AddCollectedBox()
    {
        _collectedBoxes += 1;
    }


    public void GameOver()
    {
        SaveResult();
        SceneTransition.SwitchToScene("MenuScene");
    }


    // Private methods
    private void SaveResult()
    {
        List<int> resultsList = new List<int>();
        for (int i = 0; i < _nTopResults; i++)
        {
            int result = PlayerPrefs.GetInt(_resultKeyTemplate + i, 0);
            resultsList.Add(result);
        }
        // Adding new result and sorting list
        resultsList.Add(_collectedBoxes);
        resultsList.Sort();
        resultsList.Reverse();
        
        // Writing new _nTopResults
        for (int i = 0; i < _nTopResults; i++)
        {
            PlayerPrefs.SetInt(_resultKeyTemplate + i, resultsList[i]);
        }

        _collectedBoxes = 0;
    }
}
