using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObject/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public int Max_health = 5;

    public int PowerUpLevel 
    { 
        get { return _powerUpLevel; } 
        set 
        {
            if (value > 5)
            {
                _powerUpLevel = 5;
                OnPowerUpLevelChanged?.Invoke();
                Health++;
                return;
            }

            if (value < 0)
            {
                _powerUpLevel = 0;
                OnPowerUpLevelChanged?.Invoke();
                return;
            }

            _powerUpLevel = value; 
            D.LogWarning("플레이어 파워업 레벨이 성공적으로 변경됨");
            OnPowerUpLevelChanged?.Invoke();  
        } 
    }

    public float AttackCooldown 
    { 
        get { return _attackCooldown; } 
        set 
        {
            if (value < 0)
            {
                _attackCooldown = 0.1f; // 사실 이것도 상수로 해야하는데
                Health++;
                OnAttackCooldownChanged?.Invoke();
                return;
            }

            _attackCooldown = value;
            D.LogWarning("플레이어 공격 쿨타임 레벨이 성공적으로 변경됨");
            OnAttackCooldownChanged?.Invoke(); 
        } 
    
    }
    
    public int Health
    {
        get { return _health; }
        set
        {
            if (value > Max_health)
            {
                _health = Max_health;
                OnHealthChanged?.Invoke();
                return;
            }

            _health = value;
            D.LogWarning("플레이어 체력이 성공적으로 변경됨");
            OnHealthChanged?.Invoke();
        }
    }


    [SerializeField] private float _attackCooldown = 0.75f;
    [SerializeField] private int _powerUpLevel = 1;
    [SerializeField] private int _health = 5;
    // public int PetLevel = default;

    public int BestScene = 0;

    public event Action OnPowerUpLevelChanged;
    public event Action OnAttackCooldownChanged;
    public event Action OnHealthChanged;
}