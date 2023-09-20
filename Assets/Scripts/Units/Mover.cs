using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Parameters
    private float _moveSpeed;
    private float _turnSpeed = 360f;
    
    // Technical variables
    private Vector3 _inputMoveDir;
    private Vector3 _inputLookDir;

    // Components
    private Player _player;
    private Rigidbody _rb;
    private void Start()
    {
        _moveSpeed = GameParameters.Instance.PlayerMoveSpeed();

        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }


    public void Look(Vector2 input)
    {
        if (!_player.canMove) return;

        if (input == Vector2.zero) return;
        
        _inputLookDir.Set(input.x, 0f, input.y);
        _inputLookDir = _inputLookDir.ToIso();

        var relative = (transform.position + _inputLookDir) - transform.position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }
    
    
    public void Move(Vector2 input)
    {
        if (!_player.canMove) return;
        
        _inputMoveDir.Set(input.x, 0f, input.y);
        _inputMoveDir = _inputMoveDir.ToIso();
        
        _rb.MovePosition(transform.position + _inputMoveDir * (_inputMoveDir.magnitude * _moveSpeed * Time.deltaTime));
    }
    
    
}
