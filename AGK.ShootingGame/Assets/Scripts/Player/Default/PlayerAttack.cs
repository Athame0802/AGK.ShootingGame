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
        D.LogWarning("플레이어 공격 리스타트");
        if (currentAttackCorutine != null)
        { 
            StopCoroutine(currentAttackCorutine);
            D.LogWarning("플레이어 현 공격을 성공적으로 찾음");
        }

        currentAttackCorutine = StartCoroutine(AttackByCoolDown());
    }

    private IEnumerator AttackByCoolDown()
    {
        D.LogWarning("플레이어 공격 시작됨");
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
