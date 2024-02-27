using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public float maxOxygen = 100f;
    public float oxygenDepletionRate = 10f; // Oxygen depletion rate per second

    private float currentOxygen;
    private bool isInOxygenReplenishArea;

    public Text oxygenText; // Reference to the oxygen UI text

    private void Start()
    {
        currentOxygen = maxOxygen;
        UpdateOxygenUI();
    }

    private void Update()
    {
        if (!isInOxygenReplenishArea)
        {
            if (currentOxygen > 0f)
            {
                // Deplete oxygen if outside oxygen replenish area
                currentOxygen -= Time.deltaTime * oxygenDepletionRate;
                UpdateOxygenUI();
            }
        }
    }

    private void UpdateOxygenUI()
    {
        // Update the oxygen UI text with the current oxygen percentage
        int oxygenPercentage = Mathf.RoundToInt((currentOxygen / maxOxygen) * 100);
        oxygenText.text = "Oxygen: " + oxygenPercentage + "%";

        // Handle game over if oxygen runs out
        if (currentOxygen <= 0f)
        {
            // Implement game over logic
            Debug.Log("Out of oxygen! Game over.");
            // You can trigger a game over screen, restart the level, or take any other appropriate action here.
        }
    }

    void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger entered: " + other.gameObject.name);
    if (other.CompareTag("OxygenReplenishArea"))
    {
        Debug.Log("Player entered oxygen replenish area.");
        currentOxygen = maxOxygen;
        UpdateOxygenUI();
        isInOxygenReplenishArea = true;
    }
}

void OnTriggerExit(Collider other)
{
    Debug.Log("Trigger exited: " + other.gameObject.name);
    if (other.CompareTag("OxygenReplenishArea"))
    {
        Debug.Log("Player exited oxygen replenish area.");
        isInOxygenReplenishArea = false;
    }
}

}
