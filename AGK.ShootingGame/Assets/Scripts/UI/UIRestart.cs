using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRestart : MonoBehaviour
{
    void Start()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager == null)
        {
            D.Log("못 찾음");
            return;
        }
    }

    public void OnRestartButtonClick()
    {
        GameManager.Instance.OnRestartButtonClicked();
    }
}
