using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float playerSpeed;

    [SerializeField] private float playerEndXAnimationSpeed;
    [SerializeField] private float playerEndYAnimationSpeed;

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

    public void EndStage()
    {
        InputManager.Instance.enabled = false;
        StartCoroutine(EndStageMove());
    }

    private IEnumerator EndStageMove()
    {
        WaitForSeconds waitSeconds = new WaitForSeconds(0.025f);
        this.enabled = false;

        while (true)
        {
            if (Mathf.Abs(transform.position.x) >= 0.1f)
            {
                float CalculatedX = Mathf.MoveTowards(transform.position.x, 0f, playerEndXAnimationSpeed);
                transform.position = new Vector3(CalculatedX, transform.position.y, transform.position.z);
            }
            else 
            {
                float CalculatedY = Mathf.Lerp(transform.position.y, cameraUpEnd + 5f, playerEndYAnimationSpeed);

                transform.position = new Vector3(transform.position.x, CalculatedY, transform.position.z);


                if (Mathf.Abs(transform.position.y - (cameraUpEnd + 5f)) <= 0.1f)
                {
                    InputManager.Instance.enabled = true;
                    GameManager.Instance.MoveToNextScene();
                    yield break;
                }
            }

            yield return waitSeconds;
        }
    }
}
