using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;

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

    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.Stage1);
    }

    // TODO : 스테이지 순서를 넣은 SO를 만들어 다음으로 이동 가능하게 하기
    public void MoveToNextScene()
    {

    }

    // TODO : Gameover 씬 추가해서 넣기
    public void GameOver()
    {

    }   
}