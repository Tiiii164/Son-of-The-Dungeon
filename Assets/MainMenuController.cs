using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
