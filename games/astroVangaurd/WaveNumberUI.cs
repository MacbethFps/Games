using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNumberUI : MonoBehaviour
{
    public Text waveNumberText;

    void Start()
    {
        HideWaveNumber();
    }

    public void ShowWaveNumber(int waveNumber)
    {
        if (waveNumberText != null)
        {
            waveNumberText.text = "Wave " + waveNumber;
            waveNumberText.gameObject.SetActive(true);
        }
    }

    public void HideWaveNumber()
    {
        if (waveNumberText != null)
        {
            waveNumberText.gameObject.SetActive(false);
        }
    }
}
