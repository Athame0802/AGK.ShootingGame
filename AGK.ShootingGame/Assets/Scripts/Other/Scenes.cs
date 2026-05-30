using UnityEngine.SceneManagement;

public static class Scenes
{
    public const int LAST_STAGE = 1;
    public static int Stage1 = SceneManager.GetSceneByName("Stage1").buildIndex;
    public static int GameOver = SceneManager.GetSceneByName("GameOver").buildIndex;
    public static int End = SceneManager.GetSceneByName("End").buildIndex;
}