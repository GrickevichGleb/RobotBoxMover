using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scoreTexts;
    
    private string _resultKeyTemplate;
    private string _resultTextTemplate;
    
    // Start is called before the first frame update
    void Start()
    {
        _resultTextTemplate = GameParameters.Instance.ResultRecTemplate();
        _resultKeyTemplate = GameParameters.Instance.ResultKeyTemplate();
        
        DisplayResults();
    }

    private void DisplayResults()
    {
        // Fetching and displaying top 5 results
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int result = PlayerPrefs.GetInt(_resultKeyTemplate + i, 0);
            string resText = i + _resultTextTemplate + result;
            scoreTexts[i].text = resText;
        }
    }
}
