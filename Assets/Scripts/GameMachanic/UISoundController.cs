using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISoundController : MonoBehaviour
{
    public GameObject soundMenu;

    private void Start()
    {
        soundMenu.SetActive(false);
    }
    public void OpenSoundPanel()
    {
        soundMenu.SetActive(true);
    }

    public void CloseSoundPanel()
    {
        soundMenu.SetActive(false);
    }
}
