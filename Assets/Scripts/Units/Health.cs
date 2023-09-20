using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int startHealth;

    private Player _player;
    
    private int _currentHealth;


    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _currentHealth = GameParameters.Instance.PlayerStartHealth();
    }

    public int CurrentHealth() => _currentHealth;


    public void GetDamage(int damage)
    {
        Debug.Log("Player got damage");
        _currentHealth -= damage;
        //_player.GetAnimator().SetTrigger("gotHit");
        _player.GetAnimator().Play("PlayerGotHit");
        _player.GetBoxMover().TryPutBox();

        if (_currentHealth <= 0)
        {
            // On GameOver
            GameController.Instance.GameOver();
        }
    }
}
