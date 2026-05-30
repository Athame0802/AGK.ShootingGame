using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = default;
    public float HorizontalInput { get; private set; } = default;
    public float VerticalInput { get; private set; } = default;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(HORIZONTAL);
        VerticalInput = Input.GetAxisRaw(VERTICAL);
    }
}
