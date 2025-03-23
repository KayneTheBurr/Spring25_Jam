using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public static SceneChangeHandler instance;
    public static event Action onSceneChange;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChangeSceneByName(string sceneName)
    {
        onSceneChange?.Invoke();
        SceneManager.LoadScene(sceneName);
    }

    public static void ChangeSceneByIndex(int sceneIndex)
    {
        onSceneChange?.Invoke();
        SceneManager.LoadScene(sceneIndex);
    }
}
