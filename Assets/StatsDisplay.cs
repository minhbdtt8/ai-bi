using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public TMP_Text happinessText;
    public TMP_Text diligenceText;
    public TMP_Text selfishnessText;

    void Start()
    {
        UpdateStatsUI();
    }

    void Update()
    {
        UpdateStatsUI(); // Luôn cập nhật UI
    }

    public void UpdateStatsUI()
    {
        happinessText.text = "Happiness: " + PlayerStats.Happiness;
        diligenceText.text = "Diligence: " + PlayerStats.Diligence;
        selfishnessText.text = "Selfishness: " + PlayerStats.Selfishness;
    }
}
