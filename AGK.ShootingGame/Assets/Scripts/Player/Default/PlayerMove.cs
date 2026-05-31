using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float playerSpeed;

    private float cameraLeftEnd;
    private float cameraRightEnd;
    private float cameraUpEnd;
    private float cameraDownEnd;

    private void Start()
    {
        GetCameraEnds();
    }

    private void FixedUpdate()
    {
        Vector3 posBeforeClamp = GetMove();
        transform.position = LimitPosInCamera(posBeforeClamp);
    }

    private void GetCameraEnds()
    {
        float mainCameraZ = mainCamera.transform.position.z;
        Vector3 cameraLeftUpCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCameraZ));
        Vector3 cameraRightDownCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCameraZ));

        cameraLeftEnd = cameraLeftUpCorner.x;
        cameraRightEnd = cameraRightDownCorner.x;

        cameraUpEnd = cameraLeftUpCorner.y;
        cameraDownEnd = cameraRightDownCorner.y;
    }

    private Vector3 GetMove()
    {
        float horizontalMove = InputManager.Instance.HorizontalInput * playerSpeed * Time.deltaTime;
        float VerticalMove = InputManager.Instance.VerticalInput * playerSpeed * Time.deltaTime;

        return new Vector3(
            transform.position.x + horizontalMove, 
            transform.position.y + VerticalMove, 
            transform.position.z
            );
    }

    private Vector3 LimitPosInCamera(Vector3 posBeforeClamp)
    {
        return new Vector3(
            Mathf.Clamp(posBeforeClamp.x, cameraLeftEnd, cameraRightEnd),
            Mathf.Clamp(posBeforeClamp.y, cameraDownEnd, cameraUpEnd),
            posBeforeClamp.z
            );
    }
}
