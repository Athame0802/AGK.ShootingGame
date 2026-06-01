using UnityEngine;

public class EndLine : MonoBehaviour
{
    [SerializeField] private float boxSizeX = default;
    [SerializeField] private float boxSizeY = default;

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(
            transform.position,
            new Vector2(boxSizeX, boxSizeY),
            0f,
            1 << Layers.Player
            );

        if (playerCollider == null)
        {
            return;
        }

        if (!playerCollider.TryGetComponent<PlayerMove>(out PlayerMove playerMove))
        {
            return;
        }

        playerMove.EndStage();
    }
}