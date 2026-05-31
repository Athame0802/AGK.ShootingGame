using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator dieAnimator = default;
    [SerializeField] private PlayerStatus playerStatus = default;
    [SerializeField] private float fallSpeed = default;
    [SerializeField] private Camera mainCamera = default;

    private const float FALL_SPEED_COEFFICIENT = 0.001f;
    private float cameraDownEnd = default;

    public void Start()
    {
        cameraDownEnd = mainCamera.ScreenToWorldPoint(new Vector3(0, 0)).y;
    }

    public void TakeDamage(int damage)
    {
        playerStatus.Health -= damage;
        
        if (playerStatus.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dieAnimator.SetBool("expl", true);
        InputManager.Instance.enabled = false;
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        int index = 0;
        while (transform.position.y > cameraDownEnd)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - index * FALL_SPEED_COEFFICIENT * fallSpeed, transform.position.z);
            
            yield return null;
            index++;
        }

        GameManager.Instance.GameOver();
    }
}
