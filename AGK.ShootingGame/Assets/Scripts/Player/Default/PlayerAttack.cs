using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private IPoolGetable pool = default;
    [SerializeField] private PlayerStatus playerStatus = default;
    [SerializeField] private List<Transform> AttackLocations = new(5);

    private Coroutine currentAttackCorutine = default;

    private void Start()
    {
        if (!TryGetComponent<IPoolGetable>(out pool))
        {
            D.LogError("플레이어가 IPoolGetable을 찾지 못함!");
            return;
        }

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
        StopCoroutine(currentAttackCorutine);

        currentAttackCorutine = StartCoroutine(AttackByCoolDown());
    }

    private IEnumerator AttackByCoolDown()
    {
        WaitForSeconds attackCD = new(playerStatus.AttackCooldown);
        
        while(true)
        {
            for (int i = 0; i < playerStatus.PowerUpLevel; i++)
            {
                pool.GetAtLocation(AttackLocations[i]);
            }

            yield return attackCD;
        }
    }
}
