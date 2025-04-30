using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Random giá trị từ 1 đến 100
        int selectedHappiness = Random.Range(1, 101);
        int selectedDiligence = Random.Range(1, 101);
        int selectedSelfishness = Random.Range(1, 101);

        // Random chỉ số với sai số ngẫu nhiên
        PlayerStats.Happiness = RandomizeStat(selectedHappiness);
        PlayerStats.Diligence = RandomizeStat(selectedDiligence);
        PlayerStats.Selfishness = RandomizeStat(selectedSelfishness);

        
        // Chuyển sang game scene
        SceneManager.LoadScene("SampleScene");
    }

    private int RandomizeStat(int originalValue)
    {
        // Random mức sai số từ 1% đến 50%
        float deviationPercent = Random.Range(1f, 50f);
        float deviationValue = (originalValue * deviationPercent) / 100f;

        // Random cộng hoặc trừ sai số
        int finalValue = Random.Range(0, 2) == 0
            ? originalValue - (int)deviationValue
            : originalValue + (int)deviationValue;

        // Giới hạn giá trị trong khoảng 1 đến 100
        return Mathf.Clamp(finalValue, 1, 100);
    }
}
