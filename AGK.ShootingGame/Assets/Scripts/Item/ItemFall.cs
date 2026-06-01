using UnityEngine;

public class ItemFall : MonoBehaviour, IDespawnable
{
    [SerializeField] private float fallSpeed = default;

    private void Update()
    {
        transform.position = 
            new Vector3(
                transform.position.x,
                transform.position.y - fallSpeed * Time.deltaTime,
                0f
                );
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}