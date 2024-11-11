using UnityEngine.SceneManagement;

public class SceneLoader : IUtility
{
    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
