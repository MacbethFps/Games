using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the main game scene (replace "MainGameScene" with the actual name of your game scene)
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        // Exit the application when the "Exit Game" button is clicked
        Application.Quit();
    }
}
