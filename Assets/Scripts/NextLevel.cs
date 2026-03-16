using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string nextLevelSceneName;
     public void LoadNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelSceneName);
        Time.timeScale = 1;
    }
}
