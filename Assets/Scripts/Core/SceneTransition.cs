using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition _instance;
    private static bool _isFadingIn = false;
    
    private Animator _animator;
    private AsyncOperation _loadingSceneOperation;

    private void Start()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
        
        if(_isFadingIn)
            _animator.SetTrigger("fadeIn");
    }
    

    // Update is called once per frame
    void Update()
    {
        // Some animations
    }

    public static void SwitchToScene(string sceneName)
    {
        DOTween.KillAll(); // Killing all DOTweens before changing scene
        
        _instance._animator.SetTrigger("fadeOut");

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _instance._loadingSceneOperation.allowSceneActivation = false;

    }


    // Animation event
    public void OnAnimationCompleted()
    {
        _isFadingIn = true;
        _loadingSceneOperation.allowSceneActivation = true;
    
    }
}
