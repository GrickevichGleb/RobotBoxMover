using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class RocketLauncherPlatform : MonoBehaviour
{
    [SerializeField] private Missile missilePref;
    [SerializeField] private Transform launchPosition;
    [SerializeField] private AudioSource launch_SFX;
    
    [SerializeField] private Transform launcherObj;

    private float fireInterval = 2f;
    
    private bool _isFiring;
    private float _fireIntervalTimer;

    private ObjectPool<Missile> _missilesPool;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initializing objects pool
        _missilesPool = new ObjectPool<Missile>(
            () => Instantiate(missilePref),
            missile => { if(missile != null) missile.gameObject.SetActive(true); },
            missile => { missile.gameObject.SetActive(false); },
            missile => { missile.DestroyMissile(); },
            false,
            10,
            20
        );
        fireInterval = GameParameters.Instance.RocketFireInterval();
        int initialDelay = Random.Range(2, (int) fireInterval);
        InvokeRepeating(nameof(FireAtRandDirection), initialDelay, fireInterval);
    }
    
    
    
    private async void FireAtRandDirection()
    {
        if(_isFiring) return;
        
        _isFiring = true;
        
        float yDegree = Random.Range(-45, 46); //getting degrees as ints

        Vector3 rotation = new Vector3(0f, yDegree, 0f);
        await launcherObj.DOLocalRotate(rotation, 1f, RotateMode.Fast).AsyncWaitForCompletion();
        
        await Task.Delay(1000);
        LaunchMissile();
    }

    private void LaunchMissile()
    {
        Missile missile = _missilesPool.Get();
        
        if(launch_SFX != null)launch_SFX.Play();
        
        missile.transform.position = launchPosition.transform.position;
        missile.transform.rotation = launchPosition.transform.rotation;
        float distance = 60f;
        missile.Init(distance, DestroyMissileAction);

        _isFiring = false;
    }


    private void DestroyMissileAction(Missile missile)
    {
        missile.transform.DOKill();
        _missilesPool.Release(missile);
    }


    private void OnDestroy()
    {
        transform.DOKill();
    }
}
