using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObject/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public int PowerUpLevel 
    { 
        get { return _powerUpLevel; } 
        set 
        { 
            _powerUpLevel = value; 
            OnPowerUpLevelChanged?.Invoke();  
        } 
    }

    public float AttackCooldown 
    { 
        get { return _attackCooldown; } 
        set 
        {
            _attackCooldown = value; 
            OnAttackCooldownChanged?.Invoke(); 
        } 
    
    }

    [SerializeField] private float _attackCooldown = 0.75f;
    [SerializeField] private int _powerUpLevel = 1;

    // public int PetLevel = default;
    public int Health = default;
    public event Action OnPowerUpLevelChanged;
    public event Action OnAttackCooldownChanged;
}