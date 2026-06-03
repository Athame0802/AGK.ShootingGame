using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private List<Image> UIList = new(5);
    [SerializeField] private PlayerStatus playerStatus;

    private void Awake()
    {
        playerStatus.OnHealthChanged += CheckUI;
    }

    private void Start()
    {
        CheckUI();
    }

    private void OnDestroy()
    {
        playerStatus.OnHealthChanged -= CheckUI;
    }

    private void CheckUI()
    {
        int healthIndex = playerStatus.Health - 1;

        for (int i = 0; i < UIList.Count; i++)
        {
            if (i <= healthIndex)
                UIList[i].color = Color.white;
            else
                UIList[i].color = Color.gray;
        }
    }
}