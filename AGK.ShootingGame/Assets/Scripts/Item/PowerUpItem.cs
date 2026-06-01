using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;

    private void Update()
    {
        Collider2D player = Physics2D.OverlapBox(
            transform.position,
            new Vector2(transform.localScale.x, transform.localScale.y),
            0f,
            1 << Layers.Player
            );

        if (player == null)
        {
            return;
        }

        playerStatus.PowerUpLevel++;
        Destroy(gameObject);
    }
}