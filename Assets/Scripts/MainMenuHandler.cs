using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject rulesPanel;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
    }
    public void HideRules()
    {
        rulesPanel.SetActive(false);
    }
}