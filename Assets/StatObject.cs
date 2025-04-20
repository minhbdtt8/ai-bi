using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatObject : MonoBehaviour
{
    public enum StatType { Happiness, Diligence, Selfishness }

    public StatType statType;
    public int minValue = 5;
    public int maxValue = 20;
    public float chanceToGain = 0.6f;
    public float chanceToLose = 0.3f;
    public float chanceOfDeath = 0.1f;

    public int deathHappinessLoss = 100; // nếu chết do tai nạn
    public TextMeshProUGUI narrativeText; // Kiểm tra xem object này đã được kéo vào đúng trong Inspector chưa

    private string[] accidentNarratives = new string[]
    {
        "Bạn bị tai nạn khi đang làm việc quá sức...",
        "Một sự cố bất ngờ đã xảy ra...",
        "Bạn bị trượt ngã khi đang chạy vội...",
        "Cuộc đời kết thúc đột ngột vì một tai nạn không ngờ..."
    };

    private string[] gainNarratives = new string[]
    {
        "Bạn cảm thấy có tiến triển tích cực hôm nay!",
        "Một sự kiện làm bạn thấy tự tin hơn.",
        "Bạn đang sống tốt và có động lực.",
        "Một hành động tốt khiến bạn hài lòng với bản thân."
    };

    private string[] loseNarratives = new string[]
    {
        "Bạn cảm thấy thiếu động lực hôm nay.",
        "Bạn có hành vi khiến bản thân cảm thấy hối tiếc.",
        "Một ngày không như mong đợi...",
        "Bạn mất kiểm soát và cảm thấy thất vọng."
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float roll = Random.value;

            // Kiểm tra tỉ lệ chết do tai nạn
            if (roll <= chanceOfDeath)
            {
                // Tai nạn chết người
                PlayerStats.IsDeadByAccident = true;
                PlayerStats.Happiness = Mathf.Max(0, PlayerStats.Happiness - deathHappinessLoss);

                ShowNarrative(accidentNarratives[Random.Range(0, accidentNarratives.Length)]);
                Debug.Log("Player died due to accident!");

                //  Chuyển đến scene tổng kết
                SceneManager.LoadScene("End"); // Đảm bảo tên đúng và đã được thêm vào Build Settings
            }
            else if (roll <= chanceOfDeath + chanceToGain)
            {
                // Thắng lợi, tăng stat
                int gain = Random.Range(minValue, maxValue + 1);
                ModifyStat(gain);
                ShowNarrative(gainNarratives[Random.Range(0, gainNarratives.Length)]);
            }
            else
            {
                // Thất bại, giảm stat
                int loss = Random.Range(minValue, maxValue + 1);
                ModifyStat(-loss);
                ShowNarrative(loseNarratives[Random.Range(0, loseNarratives.Length)]);
            }

            // Cập nhật UI
            StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
            if (statsDisplay != null)
                statsDisplay.UpdateStatsUI();

            Destroy(gameObject);
        }
    }

    void ModifyStat(int amount)
    {
        // Dựa vào loại stat để thay đổi
        switch (statType)
        {
            case StatType.Happiness:
                PlayerStats.Happiness = Mathf.Clamp(PlayerStats.Happiness + amount, 0, 100);
                break;
            case StatType.Diligence:
                PlayerStats.Diligence = Mathf.Clamp(PlayerStats.Diligence + amount, 0, 100);
                break;
            case StatType.Selfishness:
                PlayerStats.Selfishness = Mathf.Clamp(PlayerStats.Selfishness + amount, 0, 100);
                break;
        }

        Debug.Log($"Modified {statType} by {amount}");
    }

    void ShowNarrative(string message)
    {
        if (narrativeText != null)
        {
            narrativeText.text = message;
            CancelInvoke(nameof(ClearNarrative));
            Invoke("ClearNarrative", 5f);
        }
    }

    void ClearNarrative()
    {
        if (narrativeText != null)
        {
            narrativeText.text = "";
        }
    }
}
