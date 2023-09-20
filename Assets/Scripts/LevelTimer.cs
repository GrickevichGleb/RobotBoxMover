using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float _startTime;
    private float _startDelay;
    private float _timeRemaining;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = GameParameters.Instance.TimerSec();
        _startDelay = GameParameters.Instance.TimerDelay();
        _timeRemaining = _startTime;

        StartCoroutine(TimerCoroutine(_startDelay));
    }
    

    private IEnumerator TimerCoroutine(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        while (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
            timerText.text = _timeRemaining.ToString("0.#");
            yield return null;
        }
        // Out of time
        GameController.Instance.GameOver();
    }
}
