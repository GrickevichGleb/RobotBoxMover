using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Button pickPuttButton;
    
    private Player _player;
    private Mover _playerMover;
    private BoxMover _playerBoxMover;
    
    private Controls _controls;

    private Vector2 _leftStickValue;
    private Vector2 _rightStickValue;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
        
        _player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerMover = _player.GetMover();
        _playerBoxMover = _player.GetBoxMover();

        pickPuttButton.onClick.AddListener(OnPickPuttButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        ReadStickInputs();
    }

    private void FixedUpdate()
    {
        ProcessLeftStickInput();
    }
    

    
    // Interface for buttons, sticks etc.
    
    private void OnPickPuttButtonClicked()
    {
        _playerBoxMover.TryPickOrPuttBox();
    }

    private void ProcessLeftStickInput()
    {
        _playerMover.Look(_leftStickValue);
        _playerMover.Move(_leftStickValue);
    }

    private void ReadStickInputs()
    {
        _leftStickValue = _controls.Player.Move.ReadValue<Vector2>();
        _rightStickValue = _controls.Player.Look.ReadValue<Vector2>();
    }
    
}
