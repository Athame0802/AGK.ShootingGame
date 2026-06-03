using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerBullet playerBulletPrefab = default;
    [SerializeField] private PlayerStatus playerStatus = default;
    [SerializeField] private List<Transform> AttackLocations = new(5);

    private Coroutine currentAttackCorutine = default;

    private void Start()
    {
        playerStatus.OnPowerUpLevelChanged += RefreshAttack;
        playerStatus.OnAttackCooldownChanged += RefreshAttack;
        
        currentAttackCorutine = StartCoroutine(AttackByCoolDown());
    }

    private void OnDestroy()
    {
        playerStatus.OnPowerUpLevelChanged -= RefreshAttack;
        playerStatus.OnAttackCooldownChanged -= RefreshAttack;
    }

    private void RefreshAttack()
    {
        if (currentAttackCorutine != null)
        { 
            StopCoroutine(currentAttackCorutine);
        }

        currentAttackCorutine = StartCoroutine(AttackByCoolDown());
    }

    private IEnumerator AttackByCoolDown()
    {
        WaitForSeconds attackCD = new(playerStatus.AttackCooldown);
        
        while(true)
        {
            for (int i = 0; i < playerStatus.PowerUpLevel; i++)
            {
                playerBulletPrefab.SpawnAtLocation(transform);
            }

            yield return attackCD;
        }
    }
}
