using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Load the "GameScene" by its name
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        // Load the "MainMenuScene" by its name (replace "MainMenuScene" with the actual name of your main menu scene)
        SceneManager.LoadScene("MainMenu");
    }
}
