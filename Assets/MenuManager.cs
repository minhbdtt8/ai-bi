using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Slider happinessSlider;
    public Slider diligenceSlider;
    public Slider selfishnessSlider;

    public void PlayGame()
    {
        // Lấy giá trị từ Slider
        int selectedHappiness = (int)happinessSlider.value;
        int selectedDiligence = (int)diligenceSlider.value;
        int selectedSelfishness = (int)selfishnessSlider.value;

        // Random chỉ số với sai số ngẫu nhiên
        PlayerStats.Happiness = RandomizeStat(selectedHappiness);
        PlayerStats.Diligence = RandomizeStat(selectedDiligence);
        PlayerStats.Selfishness = RandomizeStat(selectedSelfishness);

        // In chỉ số sau khi random ra Console
        Debug.Log($"Final Stats - Happiness: {PlayerStats.Happiness}, Diligence: {PlayerStats.Diligence}, Selfishness: {PlayerStats.Selfishness}");

        // Chuyển sang game scene
        SceneManager.LoadScene("SampleScene");
    }

    private int RandomizeStat(int originalValue)
    {
        // Random mức sai số từ 1% đến 50%
        float deviationPercent = Random.Range(1f, 50f);
        float deviationValue = (originalValue * deviationPercent) / 100f;

        // Random cộng hoặc trừ sai số
        int finalValue = Random.Range(0, 2) == 0 ? originalValue - (int)deviationValue : originalValue + (int)deviationValue;

        return finalValue; // Không giới hạn giá trị
    }
}
