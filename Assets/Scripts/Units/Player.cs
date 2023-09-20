using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Intended to be used as an Interface for Player obj components
    // and process events (callbacks)

    [SerializeField] private Mover playerMover;
    [SerializeField] private BoxMover playerBoxMover;
    [SerializeField] private Animator playerAnimator;
    [Space]
    
    [SerializeField] private Transform boxHoldingTransform;
    [SerializeField] private Transform boxPuttingTransform;


    public bool canMove = true;
 

    // Components getters
    public Mover GetMover() => playerMover;
    public BoxMover GetBoxMover() => playerBoxMover;
    public Animator GetAnimator() => playerAnimator;
   
    // Positions getters
    public Transform GetBoxHoldingTransform() => boxHoldingTransform;
    public Transform GetBoxPuttingTransform() => boxPuttingTransform;

}
