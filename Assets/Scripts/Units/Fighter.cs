using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField] private Transform missileLaunchPosition;
    [SerializeField] private Missile missilePref;
    [SerializeField] private float missileReloadTime;
    [SerializeField] private float missileFireRate;
    
    private float _lastMissileLaunchTime;
    private float _missileReloadTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _missileReloadTime = 1f / missileFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TryLaunchMissile()
    {
        if (Time.time < _lastMissileLaunchTime + _missileReloadTime) 
            return;

        LaunchMissile();

        _lastMissileLaunchTime = Time.time;
    }


    private void LaunchMissile()
    {
        //Missile missile = Instantiate(missilePref, missileLaunchPosition.position, Quaternion.identity);
        //missile.Init(missileLaunchPosition.position, Vector3.zero); // !!! debug !!!
    }


    
}
