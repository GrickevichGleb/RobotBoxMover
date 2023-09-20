using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Text soundCheckmarkText;
    
    private string _soundPrefKey = "sound";
    private Color _activeColor = Color.green;
    private Color _inactiveColor = Color.red;

    private int _sound; // -1 - off / 1 - on
    
    // Start is called before the first frame update
    void Start()
    {
        _sound = PlayerPrefs.GetInt("sound", -1);
        SoundSwitchUpdateState();
    }


    public void SwitchSound()
    {
        _sound = _sound * -1;
        PlayerPrefs.SetInt(_soundPrefKey, _sound);
        
        SoundSwitchUpdateState();
    }

    private void SoundSwitchUpdateState()
    {
        if (_sound == -1)
        {
            soundCheckmarkText.text = "X";
            soundCheckmarkText.color = _inactiveColor;

            audioMixer.SetFloat("Master_Volume", -80f);
        }
        else
        {
            soundCheckmarkText.text = "V";
            soundCheckmarkText.color = _activeColor;
            
            audioMixer.SetFloat("Master_Volume", 0f);
        }
    }

}
