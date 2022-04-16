using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneProvider
{
    private SceneProvider() { }
    private static SceneProvider _instance;
    public static SceneProvider GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SceneProvider();
        }
        return _instance;
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadMainScene()
    {
        LoadScene(1);
    }

    public void LoadLevelScene()
    {
        LoadScene(2);
    }
}
