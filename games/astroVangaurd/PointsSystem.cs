using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public int pointsPerSmallEnemy = 10;
    public int pointsPerToughEnemy = 20;

    private int totalPoints = 0;
    private int multiplier = 1;

    public Text pointsText;
    public Text multiplierText;

    void Update()
    {
        UpdateMultiplierText();
    }

    public void AddPointsOnDestroy(bool isToughEnemy)
    {
        int pointsToAdd = isToughEnemy ? pointsPerToughEnemy : pointsPerSmallEnemy;
        pointsToAdd *= multiplier;
        totalPoints += pointsToAdd;
        UpdatePointsText();
    }

    public void IncreaseMultiplier()
    {
        multiplier++;
        UpdateMultiplierText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + totalPoints.ToString();
        }
    }

    private void UpdateMultiplierText()
    {
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: x" + multiplier.ToString();
        }
    }
}
