using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public static class SceneLoader
    {
        public static void LoadSceneByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}