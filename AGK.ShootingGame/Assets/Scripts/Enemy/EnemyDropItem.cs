using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [SerializeField] private GameObject powerUpItem = default;
    [SerializeField] private GameObject attackSpeedItem = default;
    [SerializeField] private float dropChance = default;
    
    public void Drop(Transform transform)
    {
        int random = Random.Range(0, 100);
        D.Log($"랜덤 숫자 : {random}");

        if (random > dropChance)
        {
            return;
        }

        if (Random.Range(0, 100) > 50)
        {
            Instantiate(powerUpItem, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(attackSpeedItem, transform.position, transform.rotation);
        }
    }
}