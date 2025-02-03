using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}