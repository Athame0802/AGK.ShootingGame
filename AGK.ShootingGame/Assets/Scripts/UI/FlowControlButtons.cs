using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FlowControlButtons : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        GameManager.Instance.OnRestartButtonClicked();
    }

    public void OnExitButtonClicked()
    {
        GameManager.Instance.OnExitButtonClicked();
    }

    public void OnStartButtonClicked()
    {
        GameManager.Instance.OnStartButtonClicked();
    }
}
