using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject exhaustTrailObj;

    private Action<Missile> _destroyAction;
    
    private float _speed;
    private int _damage;
    // ??
    // private Vector3 _startPoint;
    private Vector3 _targetPoint;

    private void Start()
    {
        _damage = GameParameters.Instance.RocketDamage();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health health))
            health.GetDamage(_damage);
        
        _destroyAction(this);
    }

    public void Init(float distance, Action<Missile> destroyAction)
    {
        _destroyAction = destroyAction;
        
        //_startPoint = startPoint;
        _targetPoint = transform.position + transform.forward.normalized * distance;


        transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
        transform.DOMove(_targetPoint, 2f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() => _destroyAction(this));
        transform.DORotate(new Vector3(0f, 0f, -360f), 4, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        
        exhaustTrailObj.SetActive(true);
    }


    public void DestroyMissile()
    {
        transform.DOKill();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
