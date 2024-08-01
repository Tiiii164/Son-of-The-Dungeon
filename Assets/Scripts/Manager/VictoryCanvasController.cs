using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCanvasController : MonoBehaviour
{
    [SerializeField] GameObject victoryCanvas;
    [SerializeField] float delayTime = 2.0f; 

    private void Awake()
    {
        victoryCanvas.SetActive(false);
    }

    private void Update()
    {
        CheckForVictory();
    }

    private void CheckForVictory()
    {
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        
        if (enemies.Length == 0)
        {
            StartCoroutine(ShowVictoryPanelAfterDelay());
        }
    }

    private IEnumerator ShowVictoryPanelAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        OpenVictoryPanel();
    }

    public void OpenVictoryPanel()
    {
        victoryCanvas.SetActive(true);
        Debug.Log("Victory panel activated!");
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
