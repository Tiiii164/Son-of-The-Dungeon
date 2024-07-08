using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpecificSettings : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            InputManager.instance.DisableDashAction();
        }
        else
        {
            InputManager.instance.EnableDashAction();
        }
    }
}
