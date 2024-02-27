using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    public static OxygenUI Instance;

    public Text oxygenText;
    public float maxOxygen = 100f;

    private float currentOxygen;

    private void Awake()
    {
        Instance = this;
        currentOxygen = maxOxygen;
        UpdateOxygenUI();
    }

    public float CurrentOxygen
    {
        get { return currentOxygen; }
    }

    public void UpdateOxygen(float depletionAmount)
    {
        currentOxygen -= depletionAmount;
        UpdateOxygenUI();
    }

    private void UpdateOxygenUI()
    {
        int oxygenPercentage = Mathf.RoundToInt((currentOxygen / maxOxygen) * 100);
        oxygenText.text = "Oxygen: " + oxygenPercentage + "%";
    }
}
