using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    private int currentScene = default;
    private float time = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.Stage1);
    }

    public void MoveToNextScene()
    {
        if (currentScene == (int)Scenes.LAST_STAGE)
        {
            SceneManager.LoadScene((int)Scenes.End);
            return;
        }
        
        SceneManager.LoadScene(++currentScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene((int)Scenes.GameOver);
    }   
}