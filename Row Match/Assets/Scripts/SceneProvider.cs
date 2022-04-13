using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProvider : MonoBehaviour
{
    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadMainScene()
    {
        LoadScene(0);
    }

    public void LoadLevelScene()
    {
        LoadScene(1);
    }
}
