using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;

    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerStatus originalPlayerStatus;
    [SerializeField] private PlayerStatus bestRecordPlayerStatus;
    
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

        ResetPlayerStatus(playerStatus, originalPlayerStatus);
        ResetPlayerStatus(bestRecordPlayerStatus, originalPlayerStatus);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.Stage1);

        currentScene = (int)Scenes.Stage1;
        playerStatus.BestScene = (int)Scenes.Stage1;
        ResetPlayerStatus(bestRecordPlayerStatus, playerStatus);
    }

    public void MoveToNextScene()
    {
        if (currentScene == (int)Scenes.LAST_STAGE)
        {
            SceneManager.LoadScene((int)Scenes.End);
            return;
        }
        
        SceneManager.LoadScene(++currentScene);
        playerStatus.BestScene = currentScene;
        ResetPlayerStatus(bestRecordPlayerStatus, playerStatus);
    }

    public void GameOver()
    {
        SceneManager.LoadScene((int)Scenes.GameOver);
        InputManager.Instance.enabled = true;
    }   

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void ResetPlayerStatus(PlayerStatus a, PlayerStatus b)
    {
        a.Health = b.Health;
        a.PowerUpLevel = b.PowerUpLevel;
        a.AttackCooldown = b.AttackCooldown;
    }

    public void MoveToBestScene()
    {
        SceneManager.LoadScene(playerStatus.BestScene);
    }

    public void OnRestartButtonClicked()
    {
        MoveToBestScene();
        ResetPlayerStatus(playerStatus, bestRecordPlayerStatus);
    }

    public void OnStartButtonClicked()
    {
        StartGame();
    }

    public void OnExitButtonClicked()
    {
        ExitGame();
    }
}