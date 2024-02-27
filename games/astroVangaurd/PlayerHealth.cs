using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver(); // Call the GameOver function when the player's health reaches zero
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        // Implement game over logic here, such as showing a game over screen or ending the game.
        // You can use SceneManager.LoadScene() to load a game over scene or handle the game over logic accordingly.
        SceneManager.LoadScene("gameover"); // Replace "GameOverScene" with the name of your game over scene
    }
}
